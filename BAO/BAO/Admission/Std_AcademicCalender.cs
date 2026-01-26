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
    public class Std_AcademicCalender:Base
    {
        #region DBColumns
        //StdAcademicCalenderID	int	            Unchecked
        //StudentID	            int	            Unchecked
        //AcademicCalenderID	int	            Unchecked
        //Description	        varchar(200)	Checked
        //CreatedBy	            int	            Checked
        //CreatedDate	        datetime	    Checked
        //ModifiedBy	        int	            Unchecked
        //ModifiedDate	        datetime	    Unchecked
        #endregion

        #region Variables
        private string _description;

        private int _academicCalenderID;
        private AcademicCalender _linkAcademicCalender;

        private int _studentID;
        private Student _linkStudent;

        private List<Student_ACUDetail> _student_ACUDetails;
        #endregion

        #region Constructor
        public Std_AcademicCalender()
            : base()
        {
            _studentID = 0;
            _linkStudent = null;

            _academicCalenderID = 0;
            _linkAcademicCalender = null;

            _description = string.Empty;

            _student_ACUDetails = null;
        } 
        #endregion
         
        #region Constants
        #region Column Constants
        private const string STDACADEMICCALENDERID = "StdAcademicCalenderID";//0

        private const string ACADEMIC_CALENDER_ID = "AcademicCalenderID";
        private const string ACADEMIC_CALENDER_ID_PA = "@AcademicCalenderID";

        private const string DESCRIPTION = "Description";
        private const string DESCRIPTION_PA = "@Description";

        private const string STUDENTID = "StudentID";
        private const string STUDENTID_PA = "@StudentID";
        #endregion

        #region PKCOlumns
        private const string ALLCOLUMNS = "[" + STDACADEMICCALENDERID + "], "//0
                                        + "[" + ACADEMIC_CALENDER_ID + "], "
                                        + "[" + DESCRIPTION + "], "
                                        + "[" + STUDENTID + "], ";//11
        #endregion

        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + ACADEMIC_CALENDER_ID + "], "
                                         + "[" + DESCRIPTION + "], "
                                         + "[" + STUDENTID + "], ";//11
        #endregion

        private const string TABLENAME = " [StdAcademicCalender] ";

        #region SELECT
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;
        #endregion

        #region INSERT
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                     + ALLCOLUMNS
                     + BASECOLUMNS + ")"
                     + "VALUES ( "
                     + ID_PA + ", "//0
                     + ACADEMIC_CALENDER_ID_PA + ", "
                     + DESCRIPTION_PA + ", "
                     + STUDENTID_PA + ", "
                     + CREATORID_PA + ", "//12
                     + CREATEDDATE_PA + ", "//13
                     + MODIFIERID_PA + ", "//14
                     + MOIDFIEDDATE_PA + ")";//15 
        #endregion

        #region UPDATE
        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET [" + ACADEMIC_CALENDER_ID + "] = " + ACADEMIC_CALENDER_ID_PA + ", "//1
                    + "[" + DESCRIPTION + "] = " + DESCRIPTION_PA + ", "//2
                    + "[" + STUDENTID + "] = " + STUDENTID_PA + ", "//3
                    + "[" + CREATORID + "] = " + CREATORID_PA + ", "//12
                    + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ", "//13
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "//14
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;//15
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Properties
        public int AcademicCalenderID
        {
            get
            {
                return this._academicCalenderID;
            }
            set
            {
                this._academicCalenderID = value;
            }
        }
        private SqlParameter AcademicCalenderIDParam
        {
            get
            {
                SqlParameter academicCalenderIDParam = new SqlParameter();
                academicCalenderIDParam.ParameterName = ACADEMIC_CALENDER_ID_PA;

                academicCalenderIDParam.Value = AcademicCalenderID;

                return academicCalenderIDParam;
            }
        }
        public AcademicCalender LinkAcademicCalender
        {
            get
            {
                if (_linkAcademicCalender == null)
                {
                    if ((_academicCalenderID > 0))
                    {
                        _linkAcademicCalender = AcademicCalender.Get(_academicCalenderID);
                    }
                }
                return _linkAcademicCalender;
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
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

        public List<Student_ACUDetail> Student_ACUDetails
        {
            get
            {
                if (_student_ACUDetails == null)
                {
                    if (this.Id > 0)
                    {
                        _student_ACUDetails = Student_ACUDetail.GetByAcademicCalendar(this.Id);
                    }
                    else
                    {
                        _student_ACUDetails = new List<Student_ACUDetail>();
                    }
                }
                return _student_ACUDetails;
            }
        }
        public bool HasStudent_ACUDetails
        {
            get
            {
                if (_student_ACUDetails == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion

        #region Methods
        private static Std_AcademicCalender Mapper(SQLNullHandler nullHandler)
        {
            Std_AcademicCalender obj = new Std_AcademicCalender();

            obj.Id = nullHandler.GetInt32(STDACADEMICCALENDERID);//0
            obj.AcademicCalenderID = nullHandler.GetInt32(ACADEMIC_CALENDER_ID);//2
            obj.Description = nullHandler.GetString(DESCRIPTION);//3
            obj.StudentID = nullHandler.GetInt32(STUDENTID);//4
            obj.CreatorID = nullHandler.GetInt32(CREATORID);//5
            obj.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);//6
            obj.ModifierID = nullHandler.GetInt32(MODIFIERID);//7
            obj.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);//8

            return obj;
        }
        private static Std_AcademicCalender MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Std_AcademicCalender obj = null;
            if (theReader.Read())
            {
                obj = new Std_AcademicCalender();
                obj = Mapper(nullHandler);
            }

            return obj;
        }
        private static List<Std_AcademicCalender> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Std_AcademicCalender> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<Std_AcademicCalender>();
                }
                Std_AcademicCalender obj = Mapper(nullHandler);
                collection.Add(obj);
            }

            return collection;
        }

        public static List<Std_AcademicCalender> Gets()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Std_AcademicCalender> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        public static Std_AcademicCalender Get(int iD)
        {
            string command = SELECT
                            + "WHERE [" + STDACADEMICCALENDERID + "] = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(iD, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            Std_AcademicCalender obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
        public static Std_AcademicCalender Get(int academicCalenderID, int studentID)
        {
            string command = SELECT
                            + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + ACADEMIC_CALENDER_ID_PA
                            + " AND [" + STUDENTID + "] = " + STUDENTID_PA;

            SqlParameter sqlParam1 = MSSqlConnectionHandler.MSSqlParamGenerator(academicCalenderID, ACADEMIC_CALENDER_ID_PA);
            SqlParameter sqlParam2 = MSSqlConnectionHandler.MSSqlParamGenerator(studentID, STUDENTID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam1, sqlParam2});

            Std_AcademicCalender obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }

        public static List<Std_AcademicCalender> GetByACACAlenderID(int academicCalenderID)
        {
            string command = SELECT
                            + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + ACADEMIC_CALENDER_ID_PA;

            SqlParameter academicCalenderIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(academicCalenderID, ACADEMIC_CALENDER_ID_PA);


            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { academicCalenderIDParam });


            List<Std_AcademicCalender> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        public static List<Std_AcademicCalender> GetByStudentID(int studentID)
        {
            string command = SELECT
                            + "WHERE [" + STUDENTID + "] = " + STUDENTID_PA;

            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(studentID, STUDENTID_PA);


            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });


            List<Std_AcademicCalender> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }

        
        internal static int GetMaxID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newID = 0;

            string command = "SELECT MAX(" + STDACADEMICCALENDERID + ") FROM " + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newID = 1;
            }
            else if (ob is Int32)
            {
                newID = Convert.ToInt32(ob) + 1;
            }

            return newID;
        }

        #region May needed in future
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

        public static int Save(Std_AcademicCalender obj)
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
                    obj.Id = Std_AcademicCalender.GetMaxID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { obj.IDParam,//"[" + ACACAL_SECTIONID + "], "//0
                                                     obj.AcademicCalenderIDParam,  
                                                     obj.DescriptionParam,
                                                     obj.StudentIDParam, 
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
                    + " WHERE [" + STDACADEMICCALENDERID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { obj.AcademicCalenderIDParam,
                                                     obj.DescriptionParam,
                                                     obj.StudentIDParam, 
                                                     obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                     obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                     obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                     obj.ModifiedDateParam, //+ MOIDFIEDDATE_PA + ")";//15 
                                                     obj.IDParam };
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                Student_ACUDetail.DeleteByParent(obj.Id, sqlConn, sqlTran);

                if (obj.HasStudent_ACUDetails)
                {
                    foreach (Student_ACUDetail item in obj.Student_ACUDetails)
                    {
                        item.StdAcademicCalenderID = obj.Id;
                        Student_ACUDetail.Save(item, sqlConn, sqlTran);
                    }
                }


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
        public static int SaveImportData(Std_AcademicCalender obj, int index, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                
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
                    obj.Id = (index == 0) ? Std_AcademicCalender.GetMaxID(sqlConn, sqlTran) : index;
                    
                    sqlParams = new SqlParameter[] { obj.IDParam,//"[" + ACACAL_SECTIONID + "], "//0
                                                     obj.AcademicCalenderIDParam,  
                                                     obj.DescriptionParam,
                                                     obj.StudentIDParam, 
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
                    + " WHERE [" + STDACADEMICCALENDERID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { obj.AcademicCalenderIDParam,
                                                     obj.DescriptionParam,
                                                     obj.StudentIDParam, 
                                                     obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                     obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                     obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                     obj.ModifiedDateParam, //+ MOIDFIEDDATE_PA + ")";//15 
                                                     obj.IDParam };
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                Student_ACUDetail.DeleteByParent(obj.Id, sqlConn, sqlTran);

                if (obj.HasStudent_ACUDetails)
                {
                    foreach (Student_ACUDetail item in obj.Student_ACUDetails)
                    {
                        item.StdAcademicCalenderID = obj.Id;
                        item.StudentID = obj.StudentID;
                        Student_ACUDetail.Save(item, sqlConn, sqlTran);
                    }
                }
                return obj.Id;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        
        internal static int Save(Std_AcademicCalender obj, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = string.Empty;
            SqlParameter[] sqlParams = null;

            if (obj.Id == 0)
            {
                #region Insert
                obj.Id = Std_AcademicCalender.GetMaxID(sqlConn, sqlTran);
                command = INSERT;
                sqlParams = new SqlParameter[] {        obj.IDParam,//"[" + ACACAL_SECTIONID + "], "//0
                                                     obj.AcademicCalenderIDParam,  
                                                     obj.DescriptionParam,
                                                     obj.StudentIDParam, 
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
                + " WHERE [" + STDACADEMICCALENDERID + "] = " + ID_PA;
                sqlParams = new SqlParameter[] { obj.AcademicCalenderIDParam,
                                                     obj.DescriptionParam,
                                                     obj.StudentIDParam, 
                                                     obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                     obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                     obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                     obj.ModifiedDateParam, //+ MOIDFIEDDATE_PA + ")";//15 
                                                     obj.IDParam };
                #endregion
            }
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

            Student_ACUDetail.DeleteByParent(obj.Id, sqlConn, sqlTran);

            if (obj.HasStudent_ACUDetails)
            {
                foreach (Student_ACUDetail item in obj.Student_ACUDetails)
                {
                    item.StdAcademicCalenderID = obj.Id;
                    Student_ACUDetail.Save(item, sqlConn, sqlTran);
                }
            }

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

                Student_ACUDetail.DeleteByParent(iD, sqlConn, sqlTran);

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

                Student_ACUDetail.DeleteByParent(iD, sqlConn, sqlTran);

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

        public static int DeleteByACACAlenderID(int academicCalenderID)
        {
            try
            {
                int counter = 0;
                string command = string.Empty;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                command = DELETE
                                + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + ACADEMIC_CALENDER_ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(academicCalenderID, ACADEMIC_CALENDER_ID_PA);

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
        internal static int DeleteByACACAlenderID(int academicCalenderID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                string command = string.Empty;
                //SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                //SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                command = DELETE
                                + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + ACADEMIC_CALENDER_ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(academicCalenderID, ACADEMIC_CALENDER_ID_PA);

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

        public static int DeleteByStudentID(int studentID)
        {
            try
            {
                int counter = 0;
                string command = string.Empty;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                Student_ACUDetail.DeleteByStudentID(studentID, sqlConn, sqlTran);

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
                Student_ACUDetail.DeleteByStudentID(studentID, sqlConn, sqlTran);
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
        #endregion
    }
}
