using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;
using Common;


namespace BussinessObject
{
    [Serializable]
    public class Student_Course : Base
    {
        #region Variables

        private int _studentID;
        private Student _linkStudent = null;

        private int _stdAcademicCalenderID;
        private Std_AcademicCalender _parent = null;

        private int _dscntSetUpID;
        private int _retakeNo;

        private int _node_CourseID;
        private NodeCourse _nodeCourse = null;

        private List<Std_CourseStatus> _student_CourseStatuses;

        private SortedList<int?, Std_CourseStatus> _student_SortedCourseStatuses;
        #endregion

        #region Constants
        #region columns

        private const string STUDENTIDCOURSEID = "Student_CourseID";

        private const string STUDENTID = "StudentID";
        private const string STUDENTID_PA = "@StudentID";

        private const string STDACID = "StdAcademicCalenderID";
        private const string STDACID_PA = "@StdAcademicCalenderID";

        private const string DSCNTSETUPID = "DscntSetUpID";
        private const string DSCNTSETUPID_PA = "@DscntSetUpID";

        private const string RETAKENO = "RetakeNo";
        private const string RETAKENO_PA = "@RetakeNo";

        private const string NODECOURSEID = "Node_CourseID";
        private const string NODECOURSEID_PA = "@Node_CourseID";
        #endregion

        #region Allcolumns
        private const string ALLCOLUMNS = STUDENTIDCOURSEID + ", "
                                + STUDENTID + ", "
                                + STDACID + ", "
                                + DSCNTSETUPID + ", "
                                + RETAKENO + ", "
                                + NODECOURSEID + ", ";
        #endregion

        #region NoPKcolumns
        private const string NOPKCOLUMNS = STUDENTID + ", "
                                + STDACID + ", "
                                + DSCNTSETUPID + ", "
                                + RETAKENO + ", "
                                + NODECOURSEID + ", ";
        #endregion

        private const string TABLENAME = " [StudentCourse] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        #region Insert
        private const string INSERT = "INSERT INTO" + TABLENAME
                     + "("
                     + ALLCOLUMNS
                     + BASECOLUMNS
                     + ")"
                     + "VALUES ( "
                     + ID_PA + ", "
                     + STUDENTID_PA + ", "
                     + STDACID_PA + ", "
                     + DSCNTSETUPID_PA + ", "
                     + RETAKENO_PA + ", "
                     + NODECOURSEID_PA + ", "
                     + CREATORID_PA + ", "
                     + CREATEDDATE_PA + ", "
                     + MODIFIERID_PA + ", "
                     + MOIDFIEDDATE_PA + ")";
        #endregion

        #region Update
        private const string UPDATE = "UPDATE" + TABLENAME
        + "SET [" + STUDENTID + "] = " + STUDENTID_PA + ", "
        + "[" + STDACID + "] = " + STDACID_PA + ", "
        + "[" + DSCNTSETUPID + "] = " + DSCNTSETUPID_PA + ", "
        + "[" + RETAKENO + "] = " + RETAKENO_PA + ", "
        + "[" + NODECOURSEID + "] = " + NODECOURSEID_PA + ", "
        + "[" + CREATORID + "] = " + CREATORID_PA + ","
        + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ","
        + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ","
        + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Constructor
        public Student_Course()
            : base()
        {
            _studentID = 0;
            _linkStudent = null;

            _stdAcademicCalenderID = 0;
            _parent = null;

            _dscntSetUpID = 0;
            _retakeNo = 0;

            _node_CourseID = 0;
            _nodeCourse = null;

            _student_CourseStatuses = null;
            _student_SortedCourseStatuses = null;
        }
        #endregion

        #region Properties

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
                sqlParam.ParameterName = STDACID_PA;

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

        public int DscntSetUpID
        {
            get { return _dscntSetUpID; }
            set { _dscntSetUpID = value; }
        }
        private SqlParameter DscntSetUpIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = DSCNTSETUPID_PA;
                if (DscntSetUpID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = DscntSetUpID;
                }
                return sqlParam;
            }
        }

        public int RetakeNo
        {
            get { return _retakeNo; }
            set { _retakeNo = value; }
        }
        private SqlParameter RetakeNoParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = RETAKENO_PA;
                if (RetakeNo == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = RetakeNo;
                }
                return sqlParam;
            }
        }

        public int NodeCourseID
        {
            get { return _node_CourseID; }
            set { _node_CourseID = value; }
        }
        public NodeCourse NodeCourse
        {
            get
            {
                if (_nodeCourse == null)
                {
                    _nodeCourse = NodeCourse.GetNodeCourse(NodeCourseID);
                }
                return _nodeCourse;
            }
        }
        private SqlParameter NodeCourseIDParam
        {
            get
            {
                SqlParameter sqlNodeCourseIDParam = new SqlParameter();

                sqlNodeCourseIDParam.ParameterName = NODECOURSEID_PA;
                if (NodeCourseID == 0)
                {
                    sqlNodeCourseIDParam.Value = DBNull.Value;
                }
                else
                {
                    sqlNodeCourseIDParam.Value = NodeCourseID;
                }

                return sqlNodeCourseIDParam;
            }
        }

        public List<Std_CourseStatus> Std_CourseStatuses
        {
            get
            {
                if (_student_CourseStatuses == null)
                {
                    if (this.Id > 0)
                    {
                        _student_CourseStatuses = Std_CourseStatus.GetByStudent_CourseID(this.Id);
                    }
                    else
                    {
                        _student_CourseStatuses = new List<Std_CourseStatus>();
                    }
                }
                return _student_CourseStatuses;
            }
        }
        public SortedList<int?, Std_CourseStatus> Std_SortedCourseStatuses
        {
            get
            {
                if (_student_SortedCourseStatuses == null)
                {
                    if (this.Id > 0)
                    {
                        _student_SortedCourseStatuses = Std_CourseStatus.GetSortedByStudent_CourseID(this.Id);

                        string test = Std_SortedCourseStatuses[Std_SortedCourseStatuses.Single().Key].Grade;
                    }
                    else
                    {
                        _student_SortedCourseStatuses = new SortedList<int?, Std_CourseStatus>();
                    }
                }
                return _student_SortedCourseStatuses;
            }
        }
        public bool HasStudent_CourseStatuses
        {
            get
            {
                if (_student_CourseStatuses == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public string HighestGrade
        {
            get
            {
                if (_student_SortedCourseStatuses != null && _student_SortedCourseStatuses.Count > 0)
                {
                    return Std_SortedCourseStatuses[Std_SortedCourseStatuses.Single().Key].Grade;
                }
                else
                {
                    return BOConstants.Grades[0].ToString();
                }
            }
        }
        public int HighestGradeOrderKey
        {
            get
            {
                return BOConstants.Grades.IndexOfValue(HighestGrade);
            }
        }
        #endregion

        #region Methods
        private static Student_Course Mapper(SQLNullHandler nullHandler)
        {
            Student_Course obj = new Student_Course();

            obj.Id = nullHandler.GetInt32(STUDENTIDCOURSEID);//0
            obj.StudentID = nullHandler.GetInt32(STUDENTID);//4
            obj.StdAcademicCalenderID = nullHandler.GetInt32(STDACID);//2
            obj.DscntSetUpID = nullHandler.GetInt32(DSCNTSETUPID);//3
            obj.RetakeNo = nullHandler.GetInt32(RETAKENO);//3
            obj.NodeCourseID = nullHandler.GetInt32(NODECOURSEID);//3
            obj.CreatorID = nullHandler.GetInt32(CREATORID);//5
            obj.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);//6
            obj.ModifierID = nullHandler.GetInt32(MODIFIERID);//7
            obj.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);//8

            return obj;
        }
        private static Student_Course MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Student_Course obj = null;
            if (theReader.Read())
            {
                obj = new Student_Course();
                obj = Mapper(nullHandler);
            }

            return obj;
        }
        private static List<Student_Course> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Student_Course> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<Student_Course>();
                }
                Student_Course obj = Mapper(nullHandler);
                collection.Add(obj);
            }

            return collection;
        }

        public static List<Student_Course> Gets()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Student_Course> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        public static Student_Course Get(int iD)
        {
            string command = SELECT
                            + "WHERE [" + STUDENTIDCOURSEID + "] = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(iD, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            Student_Course obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
        public static Student_Course Get(int std_academicCalenderID, int studentID)
        {
            string command = SELECT
                            + "WHERE [" + STDACID + "] = " + STDACID_PA
                            + " AND [" + STUDENTID + "] = " + STUDENTID_PA;

            SqlParameter sqlParam1 = MSSqlConnectionHandler.MSSqlParamGenerator(std_academicCalenderID, STDACID_PA);
            SqlParameter sqlParam2 = MSSqlConnectionHandler.MSSqlParamGenerator(studentID, STUDENTID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam1, sqlParam2 });

            Student_Course obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }

        public static List<Student_Course> Gets(int std_academicCalenderID, int studentID)
        {
            string command = SELECT
                            + "WHERE [" + STDACID + "] = " + STDACID_PA
                            + " AND [" + STUDENTID + "] = " + STUDENTID_PA;

            SqlParameter sqlParam1 = MSSqlConnectionHandler.MSSqlParamGenerator(std_academicCalenderID, STDACID_PA);
            SqlParameter sqlParam2 = MSSqlConnectionHandler.MSSqlParamGenerator(studentID, STUDENTID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam1, sqlParam2 });

            List<Student_Course> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }

        public static List<Student_Course> GetBySTD_ACACAlenderID(int std_academicCalenderID)
        {
            string command = SELECT
                            + "WHERE [" + STDACID + "] = " + STDACID_PA;

            SqlParameter academicCalenderIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(std_academicCalenderID, STDACID_PA);


            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { academicCalenderIDParam });


            List<Student_Course> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        public static List<Student_Course> GetByStudentID(int studentID)
        {
            string command = SELECT
                            + "WHERE [" + STUDENTID + "] = " + STUDENTID_PA;

            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(studentID, STUDENTID_PA);


            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });


            List<Student_Course> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        internal static int GetMaxID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newID = 0;

            string command = "SELECT MAX(" + STUDENTIDCOURSEID + ") FROM " + TABLENAME;
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
        public static int Save(Student_Course obj)
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
                    obj.Id = Student_Course.GetMaxID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { obj.IDParam,//"[" + ACACAL_SECTIONID + "], "//0
                                                     obj.StudentIDParam,  
                                                     obj.StdAcademicCalenderIDParam,
                                                     obj.DscntSetUpIDParam,
                                                     obj.RetakeNoParam,
                                                     obj.NodeCourseIDParam,
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
                    + " WHERE [" + STUDENTIDCOURSEID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { obj.StudentIDParam,  
                                                     obj.StdAcademicCalenderIDParam,
                                                     obj.DscntSetUpIDParam,
                                                     obj.RetakeNoParam,
                                                     obj.NodeCourseIDParam,
                                                     obj.StudentIDParam,  
                                                     obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                     obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                     obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                     obj.ModifiedDateParam, //+ MOIDFIEDDATE_PA + ")";//15 
                                                     obj.IDParam };
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                Std_CourseStatus.DeleteByParent(obj.Id, sqlConn, sqlTran);

                if (obj.HasStudent_CourseStatuses)
                {
                    foreach (Std_CourseStatus item in obj.Std_CourseStatuses)
                    {
                        item.Student_CourseID = obj.Id;
                        Std_CourseStatus.Save(item, sqlConn, sqlTran);
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
        public static int SaveImportData(Student_Course obj, int index, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                
                string command = string.Empty;
                SqlParameter[] sqlParams = null;
                if (obj.Id == 0)
                {
                    #region Insert
                    obj.Id = (index == 0) ? Student_Course.GetMaxID(sqlConn, sqlTran) : index;
                    command = INSERT;
                    sqlParams = new SqlParameter[] { obj.IDParam,//"[" + ACACAL_SECTIONID + "], "//0
                                                     obj.StudentIDParam,  
                                                     obj.StdAcademicCalenderIDParam,
                                                     obj.DscntSetUpIDParam,
                                                     obj.RetakeNoParam,
                                                     obj.NodeCourseIDParam,
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
                    + " WHERE [" + STUDENTIDCOURSEID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { obj.StudentIDParam,  
                                                     obj.StdAcademicCalenderIDParam,
                                                     obj.DscntSetUpIDParam,
                                                     obj.RetakeNoParam,
                                                     obj.NodeCourseIDParam,
                                                     obj.StudentIDParam,  
                                                     obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                     obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                     obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                     obj.ModifiedDateParam, //+ MOIDFIEDDATE_PA + ")";//15 
                                                     obj.IDParam };
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                Std_CourseStatus.DeleteByParent(obj.Id, sqlConn, sqlTran);

                if (obj.HasStudent_CourseStatuses)
                {
                    foreach (Std_CourseStatus item in obj.Std_CourseStatuses)
                    {
                        item.Student_CourseID = obj.Id;
                        Std_CourseStatus.Save(item, sqlConn, sqlTran);
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
        internal static int Save(Student_Course obj, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = string.Empty;
            SqlParameter[] sqlParams = null;

            if (obj.Id == 0)
            {
                #region Insert
                obj.Id = Student_Course.GetMaxID(sqlConn, sqlTran);
                command = INSERT;
                sqlParams = new SqlParameter[] { obj.IDParam,//"[" + ACACAL_SECTIONID + "], "//0
                                                     obj.StudentIDParam,  
                                                     obj.StdAcademicCalenderIDParam,
                                                     obj.DscntSetUpIDParam,
                                                     obj.RetakeNoParam,
                                                     obj.NodeCourseIDParam,
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
                + " WHERE [" + STUDENTIDCOURSEID + "] = " + ID_PA;
                sqlParams = new SqlParameter[] { obj.StudentIDParam,  
                                                     obj.StdAcademicCalenderIDParam,
                                                     obj.DscntSetUpIDParam,
                                                     obj.RetakeNoParam,
                                                     obj.NodeCourseIDParam,
                                                     obj.StudentIDParam,  
                                                     obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                     obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                     obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                     obj.ModifiedDateParam, //+ MOIDFIEDDATE_PA + ")";//15 
                                                     obj.IDParam };
                #endregion
            }
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

            Std_CourseStatus.DeleteByParent(obj.Id, sqlConn, sqlTran);

            if (obj.HasStudent_CourseStatuses)
            {
                foreach (Std_CourseStatus item in obj.Std_CourseStatuses)
                {
                    item.Student_CourseID = obj.Id;
                    Std_CourseStatus.Save(item, sqlConn, sqlTran);
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

                Std_CourseStatus.DeleteByParent(iD, sqlConn, sqlTran);

                command = DELETE
                                + "WHERE [" + STUDENTIDCOURSEID + "] = " + ID_PA;

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

                Std_CourseStatus.DeleteByParent(iD, sqlConn, sqlTran);

                command = DELETE
                                + "WHERE [" + STUDENTIDCOURSEID + "] = " + ID_PA;

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

        #region May be Needed in future
        /*
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
        */
        /*
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
        */
        #endregion
        #endregion
        
    }
}
