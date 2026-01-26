using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class OfferedCourse : Base
    {
        #region DBColumn
        //OfferID	int	Unchecked
        //AcademicCalenderID	int	Unchecked
        //DeptID	int	Checked
        //ProgramID	int	Checked
        //TreeRootID	int	Checked
        //CourseID	int	Unchecked
        //VersionID	int	Unchecked
        //CreatedBy	int	Unchecked
        //CreatedDate	datetime	Unchecked
        //ModifiedBy	int	Checked
        //ModifiedDate	datetime	Checked
        #endregion

        #region Variables

        private int _acaCalID;
        private int _deptID;
        private int _progID;
        private int _treeRootID;
        private int _courseID;
        private int _versionID;
        private Course _course;

        #endregion

        #region Constants

        #region Columns

        private const string OFFERID = "OfferID";
        private const string OFFERID_PA = "@OfferID";

        private const string ACADEMICCALENDERID = "AcademicCalenderID";
        private const string ACADEMICCALENDERID_PA = "@AcademicCalenderID";

        private const string COURSEID = "CourseID";
        private const string COURSEID_PA = "@CourseID";

        private const string VERSIONID = "VersionID";
        private const string VERSIONID_PA = "@VersionID";

        private const string DEPTID = "DeptID";
        private const string DEPTID_PA = "@DeptID";

        private const string PROGRAMID = "ProgramID";
        private const string PROGRAMID_PA = "@ProgramID";

        private const string TREEROOTID = "TreeRootID";
        private const string TREEROOTID_PA = "@TreeRootID";

        #endregion

        #region Allcolumns
        private const string ALLCOLUMNS = OFFERID + ", "
                                + ACADEMICCALENDERID + ", "
                                + DEPTID + ", "
                                + PROGRAMID + ", "
                                + TREEROOTID + ", "
                                + COURSEID + ", "
                                + VERSIONID + ", ";
        #endregion

        #region NoPkColumns
        private const string NOPKCOLUMNS = ACADEMICCALENDERID + ", "
                                + DEPTID + ", "
                                + PROGRAMID + ", "
                                + TREEROOTID + ", "
                                + COURSEID + ", "
                                + VERSIONID + ", ";
        #endregion

        private const string TABLENAME = " [OfferedCourse] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        #region Insert
        private const string INSERT = "INSERT INTO" + TABLENAME
                     + "("
                     + NOPKCOLUMNS
                     + BASECOLUMNS
                     + ")"
                     + "VALUES ( "
                     + ACADEMICCALENDERID_PA + ", "
                     + DEPTID_PA + ", "
                     + PROGRAMID_PA + ", "
                     + TREEROOTID_PA+ ", "
                     + COURSEID_PA + ", "
                     + VERSIONID_PA + ", "
                     + CREATORID_PA + ", "
                     + CREATEDDATE_PA + ", "
                     + MODIFIERID_PA + ", "
                     + MOIDFIEDDATE_PA + ")";
        #endregion

        #region Update
        //private const string UPDATE = "UPDATE" + TABLENAME
        //+ "SET " + ACADEMICCALENDERID + " = " + ACADEMICCALENDERID_PA + ", "
        //+ COURSEID + " = " + COURSEID_PA + ", "
        //+ VERSIONID + " = " + VERSIONID_PA + ", "
        //+ CREATORID + " = " + CREATORID_PA + ","
        //+ CREATEDDATE + " = " + CREATEDDATE_PA + ","
        //+ MODIFIERID + " = " + MODIFIERID_PA + ","
        //+ MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;

        #endregion

        #region Properties

        public int AcaCalID
        {
            get { return _acaCalID; }
            set { _acaCalID = value; }
        }
        private SqlParameter AcaCalIDParam
        {
            get
            {
                SqlParameter AcaCalIDParam = new SqlParameter();

                AcaCalIDParam.ParameterName = ACADEMICCALENDERID_PA;
                AcaCalIDParam.Value = AcaCalID;

                return AcaCalIDParam;
            }
        }

        public int DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }
        private SqlParameter DeptIDParam
        {
            get
            {
                SqlParameter deptIDParam = new SqlParameter();

                deptIDParam.ParameterName = DEPTID_PA;
                deptIDParam.Value = DeptID;

                return deptIDParam;
            }
        } 

        public int ProgID
        {
            get { return _progID; }
            set { _progID = value; }
        }
        private SqlParameter ProgIDParam
        {
            get
            {
                SqlParameter progIDParam = new SqlParameter();

                progIDParam.ParameterName = PROGRAMID_PA;
                progIDParam.Value = ProgID;

                return progIDParam;
            }
        } 

        public int TreeRootID
        {
            get { return _treeRootID; }
            set { _treeRootID = value; }
        }
        private SqlParameter TreeRootIDParam
        {
            get
            {
                SqlParameter treeRootIDParam = new SqlParameter();

                treeRootIDParam.ParameterName = TREEROOTID_PA;
                treeRootIDParam.Value = TreeRootID;

                return treeRootIDParam;
            }
        } 

        public int CourseID
        {
            get { return _courseID; }
            set { _courseID = value; }
        }
        private SqlParameter CourseIDParam
        {
            get
            {
                SqlParameter courseIDParam = new SqlParameter();

                courseIDParam.ParameterName = COURSEID_PA;
                courseIDParam.Value = CourseID;

                return courseIDParam;
            }
        } 

        public int VersionID
        {
            get { return _versionID; }
            set { _versionID = value; }
        }
        private SqlParameter VersionIDParam
        {
            get
            {
                SqlParameter versionIDParam = new SqlParameter();

                versionIDParam.ParameterName = VERSIONID_PA;
                versionIDParam.Value = VersionID;
                
                return versionIDParam;
            }
        }

        public Course Course
        {
            get
            {
                if (_course == null)
                {
                    if ((CourseID > 0) && (VersionID > 0))
                    {
                        _course = Course.GetCourse(CourseID, VersionID);
                    }
                }
                return _course;
            }
        }

        #endregion

        #region Mapping area

        private static OfferedCourse Mapper(SQLNullHandler nullHandler)
        {
            OfferedCourse offerCourse = new OfferedCourse();

            offerCourse.Id = nullHandler.GetInt32(OFFERID);
            offerCourse.AcaCalID = nullHandler.GetInt32(ACADEMICCALENDERID);
            offerCourse.DeptID = nullHandler.GetInt32(DEPTID);
            offerCourse.ProgID = nullHandler.GetInt32(PROGRAMID);
            offerCourse.TreeRootID = nullHandler.GetInt32(TREEROOTID);
            offerCourse.CourseID = nullHandler.GetInt32(COURSEID);
            offerCourse.VersionID = nullHandler.GetInt32(VERSIONID);
            offerCourse.CreatorID = nullHandler.GetInt32(CREATORID);
            offerCourse.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            offerCourse.ModifierID = nullHandler.GetInt32(MODIFIERID);
            offerCourse.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);

            return offerCourse;
        }
        private static List<OfferedCourse> map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<OfferedCourse> offerCourses = null;

            while (theReader.Read())
            {
                if (offerCourses == null)
                {
                    offerCourses = new List<OfferedCourse>();
                }
                OfferedCourse offerCourse = Mapper(nullHandler);
                offerCourses.Add(offerCourse);
            }
            return offerCourses;
        }
        private static OfferedCourse mapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            OfferedCourse offerCourse = null;
            if (theReader.Read())
            {
                offerCourse = new OfferedCourse();
                offerCourse = Mapper(nullHandler);
            }
            return offerCourse;
        }

        #endregion

        #region Public Methods

        public static int Save(List<OfferedCourse> offeredCourses, int AcaCalenderID)
        {
            int counter = 0;
            try
            {
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                Delete(AcaCalenderID, sqlConn, sqlTran);

                foreach (OfferedCourse oc in offeredCourses)
                {
                    string command = string.Empty;
                    SqlParameter[] sqlParams = null;
                    
                    command = INSERT;
                    sqlParams = new SqlParameter[] { oc.AcaCalIDParam,
                                             oc.DeptIDParam,
                                             oc.ProgIDParam,
                                             oc.TreeRootIDParam,
                                             oc.CourseIDParam, 
                                             oc.VersionIDParam, 
                                             oc.CreatorIDParam,
                                             oc.CreatedDateParam,
                                             oc.ModifierIDParam,
                                             oc.ModifiedDateParam};
                   
                    counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);                                 
                }

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();                
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
            return counter;
        }

        public static List<OfferedCourse> GetOfferedCourse(int acaCalenderID, int deptID, int progID, int treeRootID)
        {
            List<OfferedCourse> offeredCourseCollection = new List<OfferedCourse>();
            string command = string.Empty;

            if (deptID == 0 && progID == 0 && treeRootID == 0)
            {
                command = SELECT
                            + "WHERE AcademicCalenderID = " + acaCalenderID + " ORDER BY CourseID, VersionID";
            }
            else if(deptID != 0 && progID == 0 && treeRootID == 0)
            {
                command = SELECT
                            + "WHERE AcademicCalenderID = " + acaCalenderID + " AND DeptID = " + deptID + " ORDER BY CourseID, VersionID";
            }
            else if (deptID != 0 && progID != 0 && treeRootID == 0)
            {
                command = SELECT
                            + "WHERE AcademicCalenderID = " + acaCalenderID + " AND DeptID = " + deptID + " AND ProgramID = " + progID + " ORDER BY CourseID, VersionID";
            }
            else
            {
                command = SELECT
                            + "WHERE AcademicCalenderID = " + acaCalenderID + " AND DeptID = " + deptID + " AND ProgramID = " + progID + " AND TreeRootID = " + treeRootID + " ORDER BY CourseID, VersionID";
            }
            
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            offeredCourseCollection = map(theReader);

            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return offeredCourseCollection;
        }


        #endregion

        #region Internal Methods

        internal static int Delete(int AcaCalenderID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;
            try
            {
                string command = DELETE
                                + "WHERE AcademicCalenderID  = " + AcaCalenderID;
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
            return counter;
        }

        #endregion
    }
}
