using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;
using Common;

namespace BussinessObject
{
    [Serializable]
    public class Std_CourseStatus:Base
    {
        #region DBColumns
        /*
        Std_CourseStatusID	        int	            Unchecked
        CourseStatusID	            int	            Unchecked
        Student_CourseID	        int	            Unchecked
        GPA	                        money	        Checked
        CreatedBy	                int	            Unchecked
        CreatedDate	                datetime	    Unchecked
        ModifiedBy	                int	            Checked
        ModifiedDate	            datetime	    Checked
         */
        #endregion

        #region Variables
        private int _courseStatusID;
        private CourseStatus _linkCrsStat;

        private int _student_CourseID;
        private Student_Course _linkStdCrs;

        private Nullable<decimal> _gPA;
        private string _grade;
        #endregion

        #region Constants
        #region columns

        private const string STD_COURSESTATUSID = "Std_CourseStatusID";

        private const string COURSESTATUSID = "CourseStatusID";
        private const string COURSESTATUSID_PA = "@CourseStatusID";

        private const string STUDENT_COURSEID = "Student_CourseID";
        private const string STUDENT_COURSEID_PA = "@Student_CourseID";

        private const string GPAC = "GPA";
        private const string GPAC_PA = "@GPA";

        private const string GRADE = "Grade";
        private const string GRADE_PA = "@Grade";
        
        #endregion

        #region Allcolumns
        private const string ALLCOLUMNS = STD_COURSESTATUSID + ", "
                                + COURSESTATUSID + ", "
                                + STUDENT_COURSEID + ", "
                                + GPAC + ", "
                                + GRADE + ", ";
        #endregion

        #region NoPKcolumns
        private const string NOPKCOLUMNS = COURSESTATUSID + ", "
                                + STUDENT_COURSEID + ", "
                                + GPAC + ", "
                                + GRADE + ", ";
        #endregion

        private const string TABLENAME = " [StdCourseStatus] ";

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
                     + COURSESTATUSID_PA + ", "
                     + STUDENT_COURSEID_PA + ", "
                     + GPAC_PA + ", "
                     + GRADE_PA + ", "
                     + CREATORID_PA + ", "
                     + CREATEDDATE_PA + ", "
                     + MODIFIERID_PA + ", "
                     + MOIDFIEDDATE_PA + ")";
        #endregion

        #region Update
        private const string UPDATE = "UPDATE" + TABLENAME
        + "SET " + COURSESTATUSID + " = " + COURSESTATUSID_PA + ", "
        + STUDENT_COURSEID + " = " + STUDENT_COURSEID_PA + ", "
        + GPAC + " = " + GPAC_PA + ", "
        + GRADE + " = " + GRADE_PA + ", "
        + CREATORID + " = " + CREATORID_PA + ","
        + CREATEDDATE + " = " + CREATEDDATE_PA + ","
        + MODIFIERID + " = " + MODIFIERID_PA + ","
        + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region COnstructor
        public Std_CourseStatus()
            : base()
        {
            _courseStatusID = 0;
            _linkCrsStat = null;

            _student_CourseID = 0;
            _linkStdCrs = null;

            _gPA = null;
            _grade = string.Empty;
        } 
        #endregion

        #region Properties
        public int CourseStatusID
        {
            get { return _courseStatusID; }
            set { _courseStatusID = value; }
        }
        public CourseStatus LinkCrsStat
        {
            get
            {
                if (_linkCrsStat == null)
                {
                    if ((_courseStatusID > 0))
                    {
                        _linkCrsStat = CourseStatus.Get(_courseStatusID);
                    }
                }
                return _linkCrsStat;
            }
        }
        private SqlParameter CourseStatusIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = COURSESTATUSID_PA;

                sqlParam.Value = CourseStatusID;

                return sqlParam;
            }
        }

        public int Student_CourseID
        {
            get { return _student_CourseID; }
            set { _student_CourseID = value; }
        }
        public Student_Course LinkStdCrs
        {
            get
            {
                if (_linkStdCrs == null)
                {
                    if ((_student_CourseID > 0))
                    {
                        _linkStdCrs = Student_Course.Get(_student_CourseID);
                    }
                }
                return _linkStdCrs;
            }
        }
        private SqlParameter Student_CourseIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = STUDENT_COURSEID_PA;

                sqlParam.Value = Student_CourseID;

                return sqlParam;
            }
        }

        public Nullable<decimal> GPA
        {
            get { return _gPA; }
            set { _gPA = value; }
        }
        private SqlParameter GPAParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = GPAC_PA;
                if (!GPA.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = GPA.Value;
                }
                return sqlParam;
            }
        }


        public string Grade
        {
            get { return _grade; }
            set { _grade = value; }
        }
        public int GradeOrderKey
        {
            get
            {
                return BOConstants.Grades.IndexOfValue(Grade);
            }
        }
        private SqlParameter GradeParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = GRADE_PA;
                if (Grade == string.Empty || Grade == BOConstants.Grades[0].ToString())
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = Grade;
                }
                return sqlParam;
            }
        }

        #endregion

        #region Methods
        private static Std_CourseStatus Mapper(SQLNullHandler nullHandler)
        {
            Std_CourseStatus obj = new Std_CourseStatus();

            obj.Id = nullHandler.GetInt32(STD_COURSESTATUSID);//0
            obj.CourseStatusID = nullHandler.GetInt32(COURSESTATUSID);//4
            obj.Student_CourseID = nullHandler.GetInt32(STUDENT_COURSEID);//2
            obj.GPA = nullHandler.GetDecimal(GPAC);//3
            obj.Grade = nullHandler.GetString(GRADE);
            obj.CreatorID = nullHandler.GetInt32(CREATORID);//5
            obj.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);//6
            obj.ModifierID = nullHandler.GetInt32(MODIFIERID);//7
            obj.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);//8

            return obj;
        }
        private static Std_CourseStatus MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Std_CourseStatus obj = null;
            if (theReader.Read())
            {
                obj = new Std_CourseStatus();
                obj = Mapper(nullHandler);
            }

            return obj;
        }
        private static List<Std_CourseStatus> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Std_CourseStatus> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<Std_CourseStatus>();
                }
                Std_CourseStatus obj = Mapper(nullHandler);
                collection.Add(obj);
            }

            return collection;
        }
        private static SortedList<int?, Std_CourseStatus> MapSortedCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            SortedList<int?, Std_CourseStatus> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new SortedList<int?, Std_CourseStatus>();
                }
                Std_CourseStatus obj = Mapper(nullHandler);
                collection.Add(obj.GradeOrderKey, obj);
            }

            return collection;
        }

        public static List<Std_CourseStatus> Gets()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Std_CourseStatus> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        public static Std_CourseStatus Get(int iD)
        {
            string command = SELECT
                            + "WHERE [" + STD_COURSESTATUSID + "] = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(iD, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            Std_CourseStatus obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }

        public static List<Std_CourseStatus> GetByStudent_CourseID(int std_CourseID)
        {
            string command = SELECT
                            + "WHERE [" + STUDENT_COURSEID + "] = " + STUDENT_COURSEID_PA;

            SqlParameter academicCalenderIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(std_CourseID, STUDENT_COURSEID_PA);


            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { academicCalenderIDParam });


            List<Std_CourseStatus> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        public static SortedList<int?, Std_CourseStatus> GetSortedByStudent_CourseID(int std_CourseID)
        {
            string command = SELECT
                            + "WHERE [" + STUDENT_COURSEID + "] = " + STUDENT_COURSEID_PA;

            SqlParameter academicCalenderIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(std_CourseID, STUDENT_COURSEID_PA);


            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { academicCalenderIDParam });


            SortedList<int?, Std_CourseStatus> collection = MapSortedCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }

        public static int Save(Std_CourseStatus obj)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                //if (HasDuplicateCode(obj, academicCalenderID, courseID, versionID, sectionName, sqlConn, sqlTran))
                //{
                //    throw new Exception("Duplicate Section Name Not Allowed.");
                //}

                if (obj.Id == 0)
                {
                    #region Insert
                    command = INSERT;
                    sqlParams = new SqlParameter[] { obj.CourseStatusIDParam, 
                                                     obj.Student_CourseIDParam,   
                                                     obj.GPAParam,  
                                                     obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                     obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                     obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                     obj.ModifiedDateParam };//+ MOIDFIEDDATE_PA + ")";//15 
                    #endregion
                }
                else
                {

                    #region Update
                    command = UPDATE
                    + " WHERE [" + STD_COURSESTATUSID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { obj.CourseStatusIDParam, 
                                                     obj.Student_CourseIDParam,   
                                                     obj.GPAParam, 
                                                     obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                     obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                     obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                     obj.ModifiedDateParam, //+ MOIDFIEDDATE_PA + ")";//15 
                                                     obj.IDParam };
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

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
        internal static int Save(Std_CourseStatus obj, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = string.Empty;
            SqlParameter[] sqlParams = null;

            if (obj.Id == 0)
            {
                #region Insert
                command = INSERT;
                sqlParams = new SqlParameter[] { obj.CourseStatusIDParam, 
                                                     obj.Student_CourseIDParam,   
                                                     obj.GPAParam,  
                                                     obj.GradeParam,
                                                     obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                     obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                     obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                     obj.ModifiedDateParam };//+ MOIDFIEDDATE_PA + ")";//15 
                #endregion
            }
            else
            {

                #region Update
                command = UPDATE
                + " WHERE [" + STD_COURSESTATUSID + "] = " + ID_PA;
                sqlParams = new SqlParameter[] { obj.CourseStatusIDParam, 
                                                     obj.Student_CourseIDParam,   
                                                     obj.GPAParam, 
                                                     obj.GradeParam,
                                                     obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                     obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                     obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                     obj.ModifiedDateParam, //+ MOIDFIEDDATE_PA + ")";//15 
                                                     obj.IDParam };
                #endregion
            }
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);


            return counter;
        }

        public static int Delete(int iD)
        {
            try
            {
                int counter = 0;
                string command = string.Empty;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                command = DELETE
                                + "WHERE [" + STD_COURSESTATUSID + "] = " + ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(iD, ID_PA);

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

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
        internal static int Delete(int iD, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                string command = string.Empty;
                //SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                //SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                command = DELETE
                                + "WHERE [" + STD_COURSESTATUSID + "] = " + ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(iD, ID_PA);

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

                //MSSqlConnectionHandler.CommitTransaction();
                //MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static int DeleteByParent(int std_CourseID)
        {
            try
            {
                int counter = 0;
                string command = string.Empty;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                command = DELETE
                                + "WHERE [" + STUDENT_COURSEID + "] = " + STUDENT_COURSEID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(std_CourseID, STUDENT_COURSEID_PA);

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

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
        internal static int DeleteByParent(int std_CourseID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                string command = string.Empty;
                //SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                //SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                command = DELETE
                                + "WHERE [" + STUDENT_COURSEID + "] = " + STUDENT_COURSEID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(std_CourseID, STUDENT_COURSEID_PA);

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

                //MSSqlConnectionHandler.CommitTransaction();
                //MSSqlConnectionHandler.CloseDbConnection();
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
