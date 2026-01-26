using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class EquivalentCourse : Base
    {
        #region DBCloumns
        /* [EquivalentID] [int] NOT NULL,
        [ParentCourseID] [int] NOT NULL,
	    [ParentVersionID] [int] NOT NULL,
	    [EquiCourseID] [int] IDENTITY(1,1) NOT NULL,
	    [EquiVersionID] [int] NOT NULL
         */
        #endregion

        #region Variables
        private int _parentCourseID;
        private int _parentVersionID;
        private int _equiCourseID;
        private int _equiVersionID;
        private int _lastEquiCourseID;
        private int _lastEquiVersionID;
        private Course _equiCourse = null;
        private Course _parentEquiCourse = null;

        private Course _parCourse = null;


        #endregion

        #region Constants
        private const string PARENTCOURSEID_PA = "@ParentCourseID";
        private const string PARENTVERSIONID_PA = "@ParentVersionID";
        private const string EQUICOURSEID_PA = "@EquiCourseID";
        private const string EQUIVERSIONID_PA = "@EquiVersionID";
        
        private const string ALLCOLUMNS = "[ParentCourseID], "
                                        + "[ParentVersionID], "
                                        + "[EquiCourseID], "
                                        + "[EquiVersionID], ";

        private const string TABLENAME = " [EquiCourse] ";

        private const string SELECT = "select tbl.ParentCourseID, "
                                       + "tbl.ParentVersionID, tbl.Title, "
                                       + "tbl.Credits parentcredits, tbl.EquiCourseID, "
                                       + "tbl.EquiVersionID, c.Title EquiCourseTitle, "
                                       + "c.Credits EquiCourseCredits, tbl.CreatedBy, "
                                       + "tbl.CreatedDate, tbl.ModifiedBy, tbl.ModifiedDate from "
                                            + "(select e.ParentCourseID, e.ParentVersionID, "
                                            + "c.Title, c.Credits, e.EquiCourseID, e.EquiVersionID, "
                                            + "e.CreatedBy, e.CreatedDate, e.ModifiedBy, "
                                            + "e.ModifiedDate from EquiCourse e, Course c "
                                            + "where e.ParentCourseID = c.CourseID and e.ParentVersionID = c.VersionID"
                                            +") tbl, Course c "
                                        + "where tbl.EquiCourseID = c.CourseID and tbl.EquiVersionID = c.VersionID";
        private const string SELECT_1 = "Select " + ALLCOLUMNS + BASECOLUMNS + " from " + TABLENAME;

        private const string INSERT = "INSERT INTO" + TABLENAME
                             + "("
                             + ALLCOLUMNS
                             + BASECOLUMNS
                             + ")"
                             + "VALUES ( "
                             + PARENTCOURSEID_PA + ", "
                             + PARENTVERSIONID_PA + ", "
                             + EQUICOURSEID_PA + ", "
                             + EQUIVERSIONID_PA + ", "
                             + CREATORID_PA + ", "
                             + CREATEDDATE_PA + ", "
                             + MODIFIERID_PA + ", "
                             + MOIDFIEDDATE_PA + ")";

        //private const string UPDATE = "UPDATE" + TABLENAME
        //                + "SET [ParentCourseID] = " + PARENTCOURSEID_PA + ", "
        //                + "[ParentVersionID] = " + PARENTVERSIONID_PA + ", "
        //                + "[EquiCourseID] = " + EQUICOURSEID_PA + ", "
        //                + "[EquiVersionID] = " + EQUIVERSIONID_PA + ", "
        //                + "[CreatedBy] = " + CREATORID_PA + ","
        //                + "[CreatedDate] = " + CREATEDDATE_PA + ","
        //                + "[ModifiedBy] = " + MODIFIERID_PA + ","
        //                + "[ModifiedDate] = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE" + TABLENAME;
        #endregion

        #region Propeties
        public int ParentCourseID
        {
            get { return _parentCourseID; }
            set { _parentCourseID = value; }
        }
        private SqlParameter ParentCourseIDParam
        {
            get
            {
                SqlParameter courseIDParam = new SqlParameter();
                courseIDParam.ParameterName = PARENTCOURSEID_PA;
                courseIDParam.Value = _parentCourseID;
                return courseIDParam;
            }
        } 

        public int ParentVersionID
        {
            get { return _parentVersionID; }
            set { _parentVersionID = value; }
        }
        private SqlParameter ParentVersionIDParam
        {
            get
            {
                SqlParameter versionIDParam = new SqlParameter();
                versionIDParam.ParameterName = PARENTVERSIONID_PA;
                versionIDParam.Value = _parentVersionID;
                return versionIDParam;
            }
        } 

        /// <summary>
        /// Equi course
        /// </summary>
        public Course ParCourse
        {
            get
            {
                if (_parCourse == null)
                {
                    if ((ParentCourseID > 0) && (ParentVersionID > 0))
                    {
                        _parCourse = Course.GetCourse(ParentCourseID, ParentVersionID);
                    }
                }
                return _parCourse;
            }
        } 
        

        public int EquiCourseID
        {
            get { return _equiCourseID; }
            set { _equiCourseID = value; }
        }

        private SqlParameter EquiCourseIDParam
        {
            get
            {
                SqlParameter equiCourseIDParam = new SqlParameter();
                equiCourseIDParam.ParameterName = EQUICOURSEID_PA;
                equiCourseIDParam.Value = _equiCourseID;
                return equiCourseIDParam;
            }
        } 

        public int EquiVersionID
        {
            get { return _equiVersionID; }
            set { _equiVersionID = value; }
        }

        private SqlParameter EquiVersioIDParam
        {
            get
            {
                SqlParameter equiVersioIDParam = new SqlParameter();
                equiVersioIDParam.ParameterName = EQUIVERSIONID_PA;
                equiVersioIDParam.Value = _equiVersionID;
                return equiVersioIDParam;
            }
        } 
        /// <summary>
        /// Equi course
        /// </summary>
        public Course EquiCourse
        {
            get
            {
                if (_equiCourse == null)
                {
                    if ((EquiCourseID > 0) && (EquiVersionID > 0))
                    {
                        _equiCourse = Course.GetCourse(EquiCourseID, EquiVersionID);
                    }
                }
                return _equiCourse;
            }
        }
        public Course ParentEquiCourse
        {
            get
            {
                if (_equiCourse == null)
                {
                    if ((ParentCourseID > 0) && (ParentVersionID > 0))
                    {
                        _parentEquiCourse = Course.GetCourse(ParentCourseID, ParentVersionID);
                    }
                }
                return _parentEquiCourse;
            }
        } 
        public int LastEquiCourseID
        {
            get { return _lastEquiCourseID; }
            set { _lastEquiCourseID = value; }
        }

        public int LastEquiVersionID
        {
            get { return _lastEquiVersionID; }
            set { _lastEquiVersionID = value; }
        }
        
        #endregion        

        #region Methods
#if (false)
        /// <summary>
        /// Map a null handled data reader to a course object
        /// </summary>
        /// <param name="nullHandler">It is a null handled data reader</param>
        /// <returns>Course</returns>
#endif
        private static EquivalentCourse EquiCourseMapper(SQLNullHandler nullHandler)
        {
            EquivalentCourse equiCourse = new EquivalentCourse();
            equiCourse.ParentCourseID = nullHandler.GetInt32("ParentCourseID");
            equiCourse.ParentVersionID = nullHandler.GetInt32("ParentVersionID");
            equiCourse.EquiCourseID = nullHandler.GetInt32("EquiCourseID");
            equiCourse.EquiVersionID = nullHandler.GetInt32("EquiVersionID");
            equiCourse.LastEquiCourseID = nullHandler.GetInt32("EquiCourseID");
            equiCourse.LastEquiVersionID = nullHandler.GetInt32("EquiVersionID");                
            equiCourse.CreatorID = nullHandler.GetInt32("CreatedBy");
            equiCourse.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            equiCourse.ModifierID = nullHandler.GetInt32("ModifiedBy");
            equiCourse.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");
            return equiCourse;
        }
#if (false)
        /// <summary>
        /// Link a datareader containing a collection of courses to a nullhandler and then call the mapper
        /// </summary>
        /// <param name="theReader">Raw datareader</param>
        /// <returns>Collection of course</returns>
#endif
        private static List<EquivalentCourse> MapEquiCourses(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<EquivalentCourse> equiCourses = null;

            while (theReader.Read())
            {
                if (equiCourses == null)
                {
                    equiCourses = new List<EquivalentCourse>();
                }
                EquivalentCourse equiCourse = EquiCourseMapper(nullHandler);
                equiCourses.Add(equiCourse);
            }
            return equiCourses;
        }

#if (false)
        /// <summary>
        /// Link a datareader containing a collection of courses to a nullhandler and then call the mapper
        /// </summary>
        /// <param name="theReader">Raw datareader</param>
        /// <returns>Collection of course</returns>
#endif
        private static EquivalentCourse MapEquiCourse(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            EquivalentCourse equiCourses = null;
            if (theReader.Read())
            {
                equiCourses = new EquivalentCourse();
                equiCourses = EquiCourseMapper(nullHandler);
            }
            return equiCourses;
        }

        public static List<EquivalentCourse> GetEquiCourses()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<EquivalentCourse> ecs = MapEquiCourses(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return ecs;
        }

        /// <summary>
        /// Get all or particular information by course title
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static List<EquivalentCourse> GetEquiCourses(string parameter)
        {
            string command = "Select * from ( " + SELECT + " ) tab where tab.Title like '%" + parameter + "%' or tab.EquiCourseTitle like '%" + parameter + "%'";
                            
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<EquivalentCourse> rts = MapEquiCourses(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return rts;
        }
        /// <summary>
        /// Get equivalent course information by parent course and version id
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static List<Course> GetEquiCourse(int intParentCourseID, int intParentVersionID)
        {
            string command = SELECT_1 + " where ParentCourseID = " + intParentCourseID + " and ParentVersionID = " + intParentVersionID;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<EquivalentCourse> rt = MapEquiCourses(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return GetCoursesInfo(rt);
        }
        /// <summary>
        /// Get parent course information by equivalent course and version id
        /// </summary>
        /// <param name="intEquiCourseID"></param>
        /// <param name="intEquiVersionID"></param>
        /// <returns></returns>
        public static List<Course> GetParentByEquiCourses(int intEquiCourseID, int intEquiVersionID)
        {
            string command = SELECT_1 + " where EquiCourseID = " + intEquiCourseID + " and EquiVersionID = " + intEquiVersionID;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<EquivalentCourse> rt = MapEquiCourses(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return GetCoursesInfoReverse(rt);
        }
        /// <summary>
        /// get parent courses for a equivalent course
        /// </summary>
        /// <param name="intEquiCourseID"></param>
        /// <param name="intEquiVersionID"></param>
        /// <returns></returns>
        public static List<EquivalentCourse> GetParentByEquiCourse(int intEquiCourseID, int intEquiVersionID)
        {
            string command = SELECT_1 + " where EquiCourseID = " + intEquiCourseID + " and EquiVersionID = " + intEquiVersionID;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<EquivalentCourse> rt = MapEquiCourses(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return rt;
        }
        public static List<EquivalentCourse> GetEquiCourses(int intParentCourseID, int intParentVersionID)
        {
            string command = SELECT_1 + " where ParentCourseID = " + intParentCourseID + " and ParentVersionID = " + intParentVersionID;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<EquivalentCourse> rt = MapEquiCourses(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return rt;
        }
        public static List<Course> GetCoursesInfo(List<EquivalentCourse> equiCourses)
        {
            List<Course> courses = null;
            if (equiCourses != null)
            {
                foreach (EquivalentCourse obj in equiCourses)
                {
                    if (courses == null)
                    {
                        courses = new List<Course>();
                    }
                    courses.Add(obj.EquiCourse);
                }
            }
            return courses;            
        }
        public static List<Course> GetCoursesInfoReverse(List<EquivalentCourse> equiCourses)
        {
            List<Course> courses = null;
            if (equiCourses != null)
            {
                foreach (EquivalentCourse obj in equiCourses)
                {
                    if (courses == null)
                    {
                        courses = new List<Course>();
                    }
                    courses.Add(obj.ParentEquiCourse);
                }
            }
            return courses;
        }
        /// <summary>
        /// Get parent course information by equivalent course and version id
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        //public static List<EquivalentCourse> GetParentCourse(int intEquiCourseID, int intEquiVersionID)
        //{
        //    string command = "Select tab.ParentCourseID, tab.ParentVersionID, tab.Title, "
        //                    + "tab.Credits parentcredits, tab.CreatedBy, tab.CreatedDate, "
        //                    + "tab.ModifiedBy, tab.ModifiedDate from ( " + SELECT + " ) tab where tab.EquiCourseID = "
        //                    + intEquiCourseID + " and tab.EquiVersionID = " + intEquiVersionID;

        //    SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
        //    SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

        //    List<EquivalentCourse> rt = MapEquiCourses(theReader);
        //    theReader.Close();

        //    MSSqlConnectionHandler.CloseDbConnection();

        //    return rt;
        //}

        /// <summary>
        /// Get equivalent course information by parent course and version id
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        //public static EquivalentCourse GetEquiCourseInfo(int intParentCourseID, int intParentVersionID)
        //{
        //    string command = "Select select tab.EquiCourseID, tab.EquiVersionID, tab.Title EquiCourseTitle, tab.Credits EquiCourseCredits, tab.CreatedBy, tab.CreatedDate, tab.ModifiedBy, tab.ModifiedDate "
        //                        + " from ( " + SELECT + " ) tab where tab.ParentCourseID = " + intParentCourseID + " and tab.ParentVersionID = " + intParentVersionID;

        //    SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
        //    SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

        //    EquivalentCourse rt = MapEquiCourse(theReader);
        //    theReader.Close();

        //    MSSqlConnectionHandler.CloseDbConnection();

        //    return rt;
        //}
        /// <summary>
        /// Get parent course information by equivalent course and version id
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        //public static EquivalentCourse GetParentCourseInfo(int intEquiCourseID, int intEquiVersionID)
        //{
        //    string command = "Select tab.ParentCourseID, tab.ParentVersionID, tab.Title, "
        //                    + "tab.Credits parentcredits, tab.CreatedBy, tab.CreatedDate, "
        //                    + "tab.ModifiedBy, tab.ModifiedDate from ( " + SELECT + " ) tab where tab.EquiCourseID = "
        //                    + intEquiCourseID + " and tab.EquiVersionID = " + intEquiVersionID;

        //    SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
        //    SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

        //    EquivalentCourse rt = MapEquiCourse(theReader);
        //    theReader.Close();

        //    MSSqlConnectionHandler.CloseDbConnection();

        //    return rt;
        //}

        public static bool IsExist(EquivalentCourse ec)
        {
            string command = "Select * from ( " + SELECT + " ) tab where tab.EquiCourseID = "
                            + ec.EquiCourseID + " and tab.EquiVersionID = " + ec.EquiVersionID +
                            "and tab.ParentCourseID = " + ec.ParentCourseID + " and tab.ParentVersionID = " + ec.ParentVersionID;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            object ob = MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }
        internal static bool IsExist(EquivalentCourse ec, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            string command = SELECT_1 + "where EquiCourseID = " + ec.EquiCourseID + " and EquiVersionID = " + ec.EquiVersionID;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);
            return (Convert.ToInt32(ob) > 0);
        }
        internal static bool IsExistReverse(EquivalentCourse ec, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            string command = "Select * from ( " + SELECT + " ) tab where tab.EquiCourseID = "
                            + ec.ParentCourseID + " and tab.EquiVersionID = " + ec.ParentVersionID +
                            "and tab.ParentCourseID = " + ec.EquiCourseID + " and tab.ParentVersionID = " + ec.EquiVersionID;

            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);
            return (Convert.ToInt32(ob) > 0);
        }
        public static bool HasDuplicate(EquivalentCourse obj, int intEuiCourseID, int intEquiVersionID)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                if (obj.Id == 0)
                {
                    if (EquivalentCourse.IsExist(obj))
                    {
                        return true;
                    }
                    
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (obj.LastEquiCourseID != intEuiCourseID && obj.LastEquiVersionID != intEquiVersionID)
                    {
                        return EquivalentCourse.IsExist(obj);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// duplication checking for equivalent course
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="sqlConn"></param>
        /// <param name="sqlTran"></param>
        /// <returns></returns>
        internal static bool HasDuplicate(EquivalentCourse obj, SqlConnection sqlConn, SqlTransaction sqlTran)
        {            
            if (EquivalentCourse.IsExist(obj, sqlConn, sqlTran))
            {
                return true;
            }
            else if (EquivalentCourse.IsExistReverse(obj, sqlConn, sqlTran))
            {
                return true;
            }
            else
            {
                return false;
            }                           
        }

        internal static int Save(EquivalentCourse equiCourse, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                string command = INSERT;
                SqlParameter[] sqlParams = new SqlParameter[] { equiCourse.ParentCourseIDParam,
                                                                equiCourse.ParentVersionIDParam,
                                                                equiCourse.EquiCourseIDParam,
                                                                equiCourse.EquiVersioIDParam,                                                                
                                                                equiCourse.CreatorIDParam,
                                                                equiCourse.CreatedDateParam,
                                                                equiCourse.ModifierIDParam,
                                                                equiCourse.ModifiedDateParam};

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);                
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        public static int Save(List<EquivalentCourse> listEquiCourses, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                foreach (EquivalentCourse ec in listEquiCourses)
                {
                    Save(ec, sqlConn, sqlTran);
                }
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        public static int Delete(int intParentCourseID, int intParentVersionID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE + " where ParentCourseID = " + intParentCourseID + " and ParentVersionID = " + intParentVersionID;

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        internal static int Delete(int intParentCourseID, int intParentVersionID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                string command = DELETE + " where ParentCourseID = " + intParentCourseID + " and ParentVersionID = " + intParentVersionID;
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);                
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        public static int Delete()
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE;

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        #endregion
    }
}
