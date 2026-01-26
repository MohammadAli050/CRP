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
    public class Student_ACUDetail : Base
    {
        #region DBColumns
        //StdACUDetailID	    int	            Unchecked
        //StdAcademicCalenderID	int	            Unchecked
        //StudentID	            int	            Unchecked
        //StatusTypeID	        int	            Unchecked
        //SchSetUpID	        int	            Checked
        //CGPA	                money	        Checked
        //GPA	                money	        Checked
        //Description	        varchar(200)	Checked
        //CreatedBy	            int	            Checked
        //CreatedDate	        datetime	    Checked
        //ModifiedBy	        int	            Unchecked
        //ModifiedDate	        datetime	    Unchecked 
        #endregion

        #region Variables
        private int _stdAcademicCalenderID;
        private Std_AcademicCalender _parent;
        private int _studentID;
        private Student _linkStudent;
        private int _statusTypeID;
        private StatusType _linkStatusType;
        private int _schSetUpID;
        private Nullable< decimal> _cGPA;
        private Nullable<decimal> _gPA;
        private string _description;
        #endregion

        #region Constructor
        public Student_ACUDetail()
        {_stdAcademicCalenderID = 0;
            _studentID = 0;
            _statusTypeID = 0;
            _schSetUpID = 0;
            _cGPA = 0;
            _gPA = 0;
            _description = string.Empty;
        } 
        #endregion

        #region Constants
        #region Column Constants
        private const string STDACUDETAILID = "StdACUDetailID";//0

        private const string STDACADEMICCALENDERID = "StdAcademicCalenderID";//1
        private const string STDACADEMICCALENDERID_PA = "@StdAcademicCalenderID";

        private const string STUDENTID = "StudentID";//2
        private const string STUDENTID_PA = "@StudentID";

        private const string STATUSTYPEID = "StatusTypeID";//3
        private const string STATUSTYPEID_PA = "@StatusTypeID";

        private const string SCHSETUPID = "SchSetUpID";//4
        private const string SCHSETUPID_PA = "@SchSetUpID";

        private const string CGPAC = "CGPA";//5
        private const string CGPAC_PA = "@CGPA";

        private const string GPAC = "GPA";//6
        private const string GPAC_PA = "@GPA";

        private const string DESCRIPTION = "Description";//7
        private const string DESCRIPTION_PA = "@Description";
        #endregion

        #region PKCOlumns
        private const string ALLCOLUMNS = "[" + STDACUDETAILID + "], "//0
                                        + "[" + STDACADEMICCALENDERID + "], "
                                        + "[" + STUDENTID + "], "
                                        + "[" + STATUSTYPEID + "], "//0
                                        + "[" + SCHSETUPID + "], "
                                        + "[" + CGPAC + "], "
                                        + "[" + GPAC + "], "
                                        + "[" + DESCRIPTION + "], ";//11
        #endregion

        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + STDACADEMICCALENDERID + "], "
                                        + "[" + STUDENTID + "], "
                                        + "[" + STATUSTYPEID + "], "//0
                                        + "[" + SCHSETUPID + "], "
                                        + "[" + CGPAC + "], "
                                        + "[" + GPAC + "], "
                                        + "[" + DESCRIPTION + "], ";//11
        #endregion

        private const string TABLENAME = " [StudentACUDetail] ";

        #region SELECT
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;
        #endregion

        #region INSERT
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                     + NOPKCOLUMNS
                     + BASECOLUMNS + ")"
                     + "VALUES ( "
                     //+ ID_PA + ", "//0
                     + STDACADEMICCALENDERID_PA + ", "
                     + STUDENTID_PA + ", "
                     + STATUSTYPEID_PA + ", "//0
                     + SCHSETUPID_PA + ", "
                     + CGPAC_PA + ", "
                     + GPAC_PA + ", "
                     + DESCRIPTION_PA + ", "
                     + CREATORID_PA + ", "//12
                     + CREATEDDATE_PA + ", "//13
                     + MODIFIERID_PA + ", "//14
                     + MOIDFIEDDATE_PA + ")";//15 
        #endregion

        #region UPDATE
        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET [" + STDACADEMICCALENDERID + "] = " + STDACADEMICCALENDERID_PA + ", "//1
                    + "[" + STUDENTID + "] = " + STUDENTID_PA + ", "//3
                    + "[" + STATUSTYPEID + "] = " + STATUSTYPEID_PA + ", "//3
                    + "[" + SCHSETUPID + "] = " + SCHSETUPID_PA + ", "//3
                    + "[" + CGPAC + "] = " + CGPAC_PA + ", "//3
                    + "[" + GPAC + "] = " + GPAC_PA + ", "//3
                    + "[" + DESCRIPTION + "] = " + DESCRIPTION_PA + ", "//2
                    + "[" + CREATORID + "] = " + CREATORID_PA + ", "//12
                    + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ", "//13
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "//14
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;//15
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion
        
        #region Properties
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

        public Nullable<decimal> CGPA
        {
            get { return _cGPA; }
            set { _cGPA = value; }
        }
        private SqlParameter CGPAParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = CGPAC_PA;
                if (!CGPA.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = CGPA.Value;
                }
                return sqlParam;
            }
        }

        public int SchSetUpID
        {
            get { return _schSetUpID; }
            set { _schSetUpID = value; }
        }
        private SqlParameter SchSetUpIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = SCHSETUPID_PA;
                if (SchSetUpID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = SchSetUpID;
                }
                return sqlParam;
            }
        }

        public int StatusTypeID
        {
            get { return _statusTypeID; }
            set { _statusTypeID = value; }
        }
        private SqlParameter StatusTypeIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = STATUSTYPEID_PA;

                if (StatusTypeID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = StatusTypeID;
                }

                return sqlParam;
            }
        }
        public StatusType LinkStatusType
        {
            get
            {
                if (_linkStatusType == null)
                {
                    if ((_statusTypeID > 0))
                    {
                        _linkStatusType = StatusType.Get(_statusTypeID);
                    }
                }
                return _linkStatusType;
            }
        }

        public int StudentID
        {
            get { return _studentID; }
            set { _studentID = value; }
        }
        private SqlParameter StudentIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = STUDENTID_PA;

                sqlParam.Value = StudentID;

                return sqlParam;
            }
        }
        public Student LinkStudent
        {
            get
            {
                if (_linkStudent == null)
                {
                    if ((_studentID > 0))
                    {
                        _linkStudent = Student.GetStudent(_studentID);
                    }
                }
                return _linkStudent;
            }
        }

        public int StdAcademicCalenderID
        {
            get { return _stdAcademicCalenderID; }
            set { _stdAcademicCalenderID = value; }
        }
        private SqlParameter StdAcademicCalenderIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = STDACADEMICCALENDERID_PA;

                sqlParam.Value = StdAcademicCalenderID;

                return sqlParam;
            }
        }
        public Std_AcademicCalender Parent
        {
            get
            {
                if (_parent == null)
                {
                    if ((_stdAcademicCalenderID > 0))
                    {
                        _parent = Std_AcademicCalender.Get(_stdAcademicCalenderID);
                    }
                }
                return _parent;
            }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private SqlParameter DescriptionParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = DESCRIPTION_PA;
                if (Description == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = Description;
                }
                return sqlParam;
            }
        } 
        #endregion

        #region Methods
        private static Student_ACUDetail Mapper(SQLNullHandler nullHandler)
        {
            Student_ACUDetail obj = new Student_ACUDetail();

            obj.Id = nullHandler.GetInt32(STDACUDETAILID);//0
            obj.CGPA = nullHandler.GetDecimal(CGPAC);//2
            obj.GPA = nullHandler.GetDecimal(GPAC);//2
            obj.SchSetUpID = nullHandler.GetInt32(SCHSETUPID);//2
            obj.StatusTypeID = nullHandler.GetInt32(STATUSTYPEID);//2
            obj.StdAcademicCalenderID = nullHandler.GetInt32(STDACADEMICCALENDERID);//2
            obj.Description = nullHandler.GetString(DESCRIPTION);//3
            obj.StudentID = nullHandler.GetInt32(STUDENTID);//4
            obj.CreatorID = nullHandler.GetInt32(CREATORID);//5
            obj.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);//6
            obj.ModifierID = nullHandler.GetInt32(MODIFIERID);//7
            obj.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);//8

            return obj;
        }
        private static Student_ACUDetail MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Student_ACUDetail obj = null;
            if (theReader.Read())
            {
                obj = new Student_ACUDetail();
                obj = Mapper(nullHandler);
            }

            return obj;
        }
        private static List<Student_ACUDetail> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Student_ACUDetail> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<Student_ACUDetail>();
                }
                Student_ACUDetail obj = Mapper(nullHandler);
                collection.Add(obj);
            }

            return collection;
        }
        private static SortedDictionary<decimal?,Student_ACUDetail> MapSortedCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            SortedDictionary<decimal?, Student_ACUDetail> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new SortedDictionary<decimal?,Student_ACUDetail>();
                }
                Student_ACUDetail obj = Mapper(nullHandler);
                collection.Add(obj.GPA,obj);
            }

            return collection;
        }

        public static List<Student_ACUDetail> Gets()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Student_ACUDetail> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        public static Student_ACUDetail Get(int iD)
        {
            string command = SELECT
                            + "WHERE [" + STDACADEMICCALENDERID + "] = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(iD, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            Student_ACUDetail obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
        public static List<Student_ACUDetail> GetByStudentID(int studentID)
        {
            string command = SELECT
                            + "WHERE [" + STUDENTID + "] = " + STUDENTID_PA;

            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(studentID, STUDENTID_PA);


            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });


            List<Student_ACUDetail> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }

        public static Student_ACUDetail GetMaxByStudentID(int studentID)
        {
            string command = SELECT
               + "WHERE ["+STDACUDETAILID+"] IN "
               + "(SELECT MAX([" + STDACUDETAILID + "]) FROM " + TABLENAME + " WHERE [" + STUDENTID + "] = " + studentID + ")";


            //SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(studentID, STUDENTID_PA);


            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);


            Student_ACUDetail obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
        public static List<Student_ACUDetail> GetByAcademicCalendar(int std_academicCalenderID)
        {
            string command = SELECT
                            + "WHERE [" + STDACADEMICCALENDERID + "] = " + STDACADEMICCALENDERID_PA;

            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(std_academicCalenderID, STDACADEMICCALENDERID_PA);


            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });


            List<Student_ACUDetail> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        public static SortedDictionary<decimal?, Student_ACUDetail> GetSortedByParent(int std_academicCalenderID)
        {
            string command = SELECT
                            + "WHERE [" + STDACADEMICCALENDERID + "] = " + STDACADEMICCALENDERID_PA;

            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(std_academicCalenderID, STDACADEMICCALENDERID_PA);


            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });


            SortedDictionary<decimal?,Student_ACUDetail> collection = MapSortedCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }

        #region May needed in future
        //internal static int GetMaxID(SqlConnection sqlConn, SqlTransaction sqlTran)
        //{
        //    int newID = 0;

        //    string command = "SELECT MAX(" + ACACAL_SECTIONID + ") FROM [" + TABLENAME + "]";
        //    object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

        //    if (ob == null || ob == DBNull.Value)
        //    {
        //        newID = 1;
        //    }
        //    else if (ob is Int32)
        //    {
        //        newID = Convert.ToInt32(ob) + 1;
        //    }

        //    return newID;
        //}

        //public static bool IsExist(int academicCalenderID, int intDeptID, int intProgID, int courseID, int versionID, string sectionName)
        //{
        //    string command = SELECT
        //                                + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + academicCalenderID
        //                                + " AND [" + COURSE_ID + "] = " + courseID
        //                                + " AND [" + VERSION_ID + "] = " + versionID
        //                                + " AND [" + SECTIONNAME + "] = " + "'" + sectionName + "'"
        //                                + " AND [" + DEPTID + "] = " + intDeptID
        //                                + " AND [" + PROGRAMID + "] = " + intProgID; ;

        //    //SqlParameter academicCalenderIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(academicCalenderID, ACADEMIC_CALENDER_ID_PA);
        //    //SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, COURSE_ID_PA);
        //    //SqlParameter versionIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(versionID, VERSION_ID_PA);
        //    //SqlParameter secNameParam = MSSqlConnectionHandler.MSSqlParamGenerator(sectionName, SECTIONNAME_PA);
        //    //SqlParameter deptIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(intDeptID, DEPTID_PA);
        //    //SqlParameter progIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(intProgID, PROGRAMID_PA);

        //    SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
        //    //object ob = MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { academicCalenderIDParam, courseIDParam, versionIDParam, secNameParam, deptIDParam, progIDParam });
        //    object ob = MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

        //    MSSqlConnectionHandler.CloseDbConnection();

        //    return (Convert.ToInt32(ob) > 0);
        //}
        //internal static bool IsExist(int academicCalenderID, int courseID, int versionID, string sectionName, SqlConnection sqlConn, SqlTransaction sqlTran)
        //{
        //    string command = SELECT
        //                                + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + ACADEMIC_CALENDER_ID_PA
        //                                + " AND [" + COURSE_ID + "] = " + COURSE_ID_PA
        //                                + " AND [" + VERSION_ID + "] = " + VERSION_ID_PA
        //                                + " AND [" + SECTIONNAME + "] = " + SECTIONNAME_PA;

        //    SqlParameter academicCalenderIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(academicCalenderID, ACADEMIC_CALENDER_ID_PA);
        //    SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, COURSE_ID_PA);
        //    SqlParameter versionIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(versionID, VERSION_ID_PA);
        //    SqlParameter secNameParam = MSSqlConnectionHandler.MSSqlParamGenerator(sectionName, SECTIONNAME_PA);

        //    object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran, new SqlParameter[] { academicCalenderIDParam, courseIDParam, versionIDParam, secNameParam });

        //    return (Convert.ToInt32(ob) > 0);
        //}
        //public static bool HasDuplicateCode(Std_AcademicCalender obj, int academicCalenderID, int intDeptID, int intProgramID, int courseID, int versionID, string sectionName)
        //{
        //    if (obj == null)
        //    {
        //        return AcademicCalenderSection.IsExist(obj.AcademicCalenderID, obj.DeptID, obj.ProgramID, obj.ChildCourseID, obj.ChildVersionID, obj.SectionName);
        //    }
        //    else
        //    {
        //        if (obj.Id == 0)
        //        {
        //            if (AcademicCalenderSection.IsExist(obj.AcademicCalenderID, obj.DeptID, obj.ProgramID, obj.ChildCourseID, obj.ChildVersionID, obj.SectionName))
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            if (obj.LastacademicCalenderID != academicCalenderID || obj.LastchildCourseID != courseID || obj.LastchildVersionID != versionID || obj.LastSectionName != sectionName || obj.LastdeptID != intDeptID || obj.LastProgramID != intProgramID)
        //            {
        //                return AcademicCalenderSection.IsExist(obj.AcademicCalenderID, obj.DeptID, obj.ProgramID, obj.ChildCourseID, obj.ChildVersionID, obj.SectionName);
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //}

        //internal static bool HasDuplicateCode(Std_AcademicCalender obj, int academicCalenderID, int courseID, int versionID, string sectionName, SqlConnection sqlConn, SqlTransaction sqlTran)
        //{
        //    if (obj == null)
        //    {
        //        return AcademicCalenderSection.IsExist(obj.AcademicCalenderID, obj.ChildCourseID, obj.ChildVersionID, obj.SectionName, sqlConn, sqlTran);
        //    }
        //    else
        //    {
        //        if (obj.Id == 0)
        //        {
        //            if (AcademicCalenderSection.IsExist(obj.AcademicCalenderID, obj.ChildCourseID, obj.ChildVersionID, obj.SectionName, sqlConn, sqlTran))
        //            {
        //                return AcademicCalenderSection.IsExist(obj.AcademicCalenderID, obj.ChildCourseID, obj.ChildVersionID, obj.SectionName, sqlConn, sqlTran);
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            if (obj.AcademicCalenderID != academicCalenderID || obj.ChildCourseID != courseID || obj.ChildVersionID != versionID || obj.SectionName != sectionName)
        //            {
        //                return AcademicCalenderSection.IsExist(obj.AcademicCalenderID, obj.ChildCourseID, obj.ChildVersionID, obj.SectionName, sqlConn, sqlTran);
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //} 
        #endregion

        public static int Save(Student_ACUDetail obj)
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
                    sqlParams = new SqlParameter[] { obj.StdAcademicCalenderIDParam, 
                                                     obj.StudentIDParam, 
                                                     obj.StatusTypeIDParam, 
                                                     obj.SchSetUpIDParam, 
                                                     obj.CGPAParam,  
                                                     obj.GPAParam,  
                                                     obj.DescriptionParam,
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
                    + " WHERE [" + STDACUDETAILID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { obj.StdAcademicCalenderIDParam, 
                                                     obj.StudentIDParam, 
                                                     obj.StatusTypeIDParam, 
                                                     obj.SchSetUpIDParam, 
                                                     obj.CGPAParam,  
                                                     obj.GPAParam,  
                                                     obj.DescriptionParam,
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
        internal static int Save(Student_ACUDetail obj, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = string.Empty;
            SqlParameter[] sqlParams = null;

            if (obj.Id == 0)
            {
                #region Insert
                command = INSERT;
                sqlParams = new SqlParameter[] { obj.StdAcademicCalenderIDParam, 
                                                     obj.StudentIDParam, 
                                                     obj.StatusTypeIDParam, 
                                                     obj.SchSetUpIDParam, 
                                                     obj.CGPAParam,  
                                                     obj.GPAParam,  
                                                     obj.DescriptionParam,
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
                + " WHERE [" + STDACUDETAILID + "] = " + ID_PA;
                sqlParams = new SqlParameter[] { obj.StdAcademicCalenderIDParam, 
                                                     obj.StudentIDParam, 
                                                     obj.StatusTypeIDParam, 
                                                     obj.SchSetUpIDParam, 
                                                     obj.CGPAParam,  
                                                     obj.GPAParam,  
                                                     obj.DescriptionParam,
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
                                + "WHERE [" + STDACADEMICCALENDERID + "] = " + ID_PA;

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
                                + "WHERE [" + STDACADEMICCALENDERID + "] = " + ID_PA;

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

        #region May be needed
        //public static int DeleteByACACAlenderID(int academicCalenderID)
        //{
        //    try
        //    {
        //        int counter = 0;
        //        string command = string.Empty;
        //        SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
        //        SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

        //        command = DELETE
        //                        + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + ACADEMIC_CALENDER_ID_PA;

        //        SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(academicCalenderID, ACADEMIC_CALENDER_ID_PA);

        //        counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

        //        MSSqlConnectionHandler.CommitTransaction();
        //        MSSqlConnectionHandler.CloseDbConnection();
        //        return counter;
        //    }
        //    catch (Exception ex)
        //    {
        //        MSSqlConnectionHandler.RollBackAndClose();
        //        throw ex;
        //    }

        //}
        //internal static int DeleteByACACAlenderID(int academicCalenderID, SqlConnection sqlConn, SqlTransaction sqlTran)
        //{
        //    try
        //    {
        //        int counter = 0;
        //        string command = string.Empty;
        //        //SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
        //        //SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

        //        command = DELETE
        //                        + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + ACADEMIC_CALENDER_ID_PA;

        //        SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(academicCalenderID, ACADEMIC_CALENDER_ID_PA);

        //        counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

        //        //MSSqlConnectionHandler.CommitTransaction();
        //        //MSSqlConnectionHandler.CloseDbConnection();
        //        return counter;
        //    }
        //    catch (Exception ex)
        //    {
        //        MSSqlConnectionHandler.RollBackAndClose();
        //        throw ex;
        //    }

        //} 
        #endregion

        public static int DeleteByStudentID(int studentID)
        {
            try
            {
                int counter = 0;
                string command = string.Empty;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                command = DELETE
                                + "WHERE [" + STUDENTID + "] = " + STUDENTID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(studentID, STUDENTID_PA);

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
        internal static int DeleteByStudentID(int studentID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                string command = string.Empty;
                //SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                //SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                command = DELETE
                                + "WHERE [" + STUDENTID + "] = " + STUDENTID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(studentID, STUDENTID_PA);

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

        public static int DeleteByParent(int std_academicCalenderID)
        {
            try
            {
                int counter = 0;
                string command = string.Empty;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                command = DELETE
                                + "WHERE [" + STDACADEMICCALENDERID + "] = " + STDACADEMICCALENDERID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(std_academicCalenderID, STUDENTID_PA);

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
        internal static int DeleteByParent(int std_academicCalenderID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                string command = string.Empty;
                //SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                //SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                command = DELETE
                                + "WHERE [" + STDACADEMICCALENDERID + "] = " + STDACADEMICCALENDERID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(std_academicCalenderID, STDACADEMICCALENDERID_PA);

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
