using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class Teacher :Base
    {
        #region DBColumns
        //TeacherID	        int	            Unchecked   1m1
        //Code	            varbinary(50)	Unchecked   2m2
        //Prefix	        int	            Checked     3o3
        //FirstName	        varchar(100)	Unchecked   4m4
        //MiddleName	    varchar(100)	Checked     5o5 
        //LastName	        varchar(100)	Unchecked   6m6
        //NickOrOtherName	varchar(100)	Checked     7o7 
        //DOB	            datetime	    Unchecked   8m8
        //Gender	        int	            Unchecked   9m9
        //BloodGroup	    int	            Checked     10
        //ReligionID	    int	            Checked     11
        //NationalityID	    int	            Checked     12
        //Photo	            varchar(1000)	Checked     13
        //AddressID	        int	            Checked     14
        //SpouseName	    varchar(100)	Checked     15
        //MaritalStatus	    int	            Checked     16
        //DeptID	        int	            Checked     17m10

        //User_ID           int

        //CreatedBy	        int	            Unchecked   18m11
        //CreatedDate	    datetime	    Unchecked   19m12
        //ModifiedBy	    int	            Checked     20o13
        //ModifiedDate	    datetime	    Checked     21o14
        #endregion

        #region Variable Declaration
        //private string _code;
        //private Prefix _prefix;
        //private string _firstName;
        //private string _middleName;
        //private string _lastName;
        //private string _nickOrOtherName;
        //private DateTime _dOB;
        //private Gender _gender;
        //private BloodGroup _bloodGroup;
        //private string _spouseName;
        //private MaritalStatus _maritalStatus;
        //private int _deptID;
        //private Department _dept;
        //private string _lastCode;
        //private int _userId;

        //Sajib
        private string _code;
        private int _deptID;
        private int _createdBy;
        private DateTime _createdDate;
        private int _modifiedBy;
        private DateTime _modifiedDate;
        private int _schoolId;
        private string _remarks;
        private string _history;
        private int _personId;
        private string _attribute1;
        private string _attribute2;
        //Sajib
        #endregion

        #region Constructor
        public Teacher()
            : base()
        {
            #region
            //_code = string.Empty;
            //_prefix = Prefix.MR;
            //_firstName = string.Empty;
            //_middleName = string.Empty;
            //_lastName = string.Empty;
            //_nickOrOtherName = string.Empty;
            //_dOB = DateTime.MinValue;
            //_gender = Gender.Male;
            //_bloodGroup = BloodGroup.NA;
            //_spouseName = string.Empty;
            //_maritalStatus = MaritalStatus.NA;
            //_deptID = 0;
            //_dept = null;
            //_lastCode = string.Empty;
            //_userId = 0;
            #endregion
            //Sajib
            _code = string.Empty;
            _deptID = 0;
            _createdBy = 0;
            _createdDate = DateTime.MinValue;
            _modifiedBy = 0;
            _modifiedDate = DateTime.MinValue;
            _schoolId = 0;
            _remarks = string.Empty;
            _history = string.Empty;
            _personId = 0;
            _attribute1 = string.Empty;
            _attribute2 = string.Empty;

            //Sajib
        } 
        #endregion

        #region Constants
        #region Column Constants
        //private const string TEACHERID = "TeacherID";

        //private const string CODE = "Code";
        //private const string CODE_PA = "@Code";

        //private const string PREFIX = "Prefix";
        //private const string PREFIX_PA = "@Prefix";

        //private const string FIRSTNAME = "FirstName";
        //private const string FIRSTNAME_PA = "@FirstName";

        //private const string MIDDLENAME = "MiddleName";
        //private const string MIDDLENAME_PA = "@MiddleName";

        //private const string LASTNAME = "LastName";
        //private const string LASTNAME_PA = "@LastName";

        //private const string NICKOROTHERNAME = "NickOrOtherName";
        //private const string NICKOROTHERNAME_PA = "@NickOrOtherName";

        //private const string DOB = "DOB";
        //private const string DOB_PA = "@DOB";

        //private const string GENDER = "Gender";
        //private const string GENDER_PA = "@Gender";

        //private const string BLOODGROUP = "BloodGroup";
        //private const string BLOODGROUP_PA = "@BloodGroup";

        //private const string RELIGIONID = "ReligionID";
        //private const string RELIGIONID_PA = "@ReligionID";

        //private const string NATIONALITYID = "NationalityID";
        //private const string NATIONALITYID_PA = "@NationalityID";

        //private const string PHOTO = "Photo";
        //private const string PHOTO_PA = "@Photo";

        //private const string ADDRESSID = "AddressID";
        //private const string ADDRESSID_PA = "@AddressID";

        //private const string SPOUSENAME = "SpouseName";
        //private const string SPOUSENAME_PA = "@SpouseName";

        //private const string MARITALSTATUS = "MaritalStatus";
        //private const string MARITALSTATUS_PA = "@MaritalStatus";

        //private const string DEPTID = "DeptID";
        //private const string DEPTID_PA = "@DeptID";

        //private const string USERID = "user_Id";
        //private const string USERID_PA = "@user_Id";

        //Sajib
        private const string TEACHERID = "EmployeeID";

        private const string CODE = "Code";
        private const string CODE_PA = "@Code";

        private const string DEPTID = "DeptID";
        private const string DEPTID_PA = "@DeptID";

        private const string CREATEDBY = "CreatedBy";
        private const string CREATEDBY_PA = "@CreatedBy";

        private const string CREATEDDATE = "CreatedDate";
        private const string CREATEDDATE_PA = "@CreatedDate";

        private const string MODIFIEDBY = "ModifiedBy";
        private const string MODIFIEDBY_PA = "@ModifiedBy";

        private const string MODIFIEDDATE = "ModifiedDate";
        private const string MODIFIEDDATE_PA = "@ModifiedDate";

        private const string SCHOOLID = "SchoolId";
        private const string SCHOOLID_PA = "@SchoolId";

        private const string REMARKS = "Remarks";
        private const string REMARKS_PA = "@Remarks";

        private const string HISTORY = "History";
        private const string HISTORY_PA = "@History";

        private const string PERSONID = "PersonId";
        private const string PERSONID_PA = "@PersonId";

        private const string ATTRIBUTE1 = "Attribute1";
        private const string ATTRIBUTE1_PA = "@Attribute1";

        private const string ATTRIBUTE2 = "Attribute2";
        private const string ATTRIBUTE2_PA = "@Attribute2";
        //Sajib
        #endregion

        #region PKCOlumns
        //private const string ALLCOLUMNS = "[" + TEACHERID + "], "
        //                        + "[" + CODE + "], "
        //                        + "[" + PREFIX + "], "
        //                        + "[" + FIRSTNAME + "], "
        //                        + "[" + MIDDLENAME + "], "
        //                        + "[" + LASTNAME + "], "
        //                        + "[" + NICKOROTHERNAME + "], "
        //                        + "[" + DOB + "], "
        //                        + "[" + GENDER + "], "
        //                        //+ "[" + BLOODGROUP + "], "
        //                        //+ "[" + RELIGIONID + "], "
        //                        //+ "[" + NATIONALITYID + "], "
        //                        //+ "[" + PHOTO + "], "
        //                        //+ "[" + ADDRESSID + "], "
        //                        //+ "[" + SPOUSENAME + "], "
        //                        //+ "[" + MARITALSTATUS + "], "
        //                        + "[" + DEPTID + "], "
        //                        + "[" + USERID + "], "; 
        private const string ALLCOLUMNS = "[" + TEACHERID + "], "
                                        + "[" + CODE + "], "
                                        + "[" + DEPTID + "], "
                                        + "[" + CREATEDBY + "], "
                                        + "[" + CREATEDDATE + "], "
                                        + "[" + MODIFIEDBY + "], "
                                        + "[" + MODIFIEDDATE + "], "
                                        + "[" + SCHOOLID + "], "
                                        + "[" + REMARKS + "], "
                                        + "[" + HISTORY + "], "
                                        + "[" + PERSONID + "], "
                                        + "[" + ATTRIBUTE1 + "], "
                                        + "[" + ATTRIBUTE2 + "], "; 
        #endregion

        #region NOPKCOLUMNS
        //private const string NOPKCOLUMNS = "[" + CODE + "], "//1
        //                        + "[" + PREFIX + "], "//2
        //                        + "[" + FIRSTNAME + "], "//3
        //                        + "[" + MIDDLENAME + "], "//4
        //                        + "[" + LASTNAME + "], "//5
        //                        + "[" + NICKOROTHERNAME + "], "//6
        //                        + "[" + DOB + "], "//7
        //                        + "[" + GENDER + "], "//8
        //                        //+ "[" + BLOODGROUP + "], "//9
        //                        //+ "[" + RELIGIONID + "], "//10
        //                        //+ "[" + NATIONALITYID + "], "//11
        //                        //+ "[" + PHOTO + "], "//12
        //                        //+ "[" + ADDRESSID + "], "//13
        //                        //+ "[" + SPOUSENAME + "], "//14
        //                        //+ "[" + MARITALSTATUS + "], "//15
        //                        + "[" + DEPTID + "], "
        //                        + "[" + USERID + "], ";
        private const string NOPKCOLUMNS =  "[" + CODE + "], "//1
                                        + "[" + DEPTID + "], "
                                        + "[" + CREATEDBY + "], "
                                        + "[" + CREATEDDATE + "], "
                                        + "[" + MODIFIEDBY + "], "
                                        + "[" + MODIFIEDDATE + "], "
                                        + "[" + SCHOOLID + "], "
                                        + "[" + REMARKS + "], "
                                        + "[" + HISTORY + "], "
                                        + "[" + PERSONID + "], "
                                        + "[" + ATTRIBUTE1 + "], "
                                        + "[" + ATTRIBUTE2 + "], ";
        #endregion

        private const string TABLENAME = " [Employee] ";

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
                     + CODE_PA + ", "//1
                     + DEPTID_PA + ", "//2
                     + CREATEDBY_PA + ", "//3
                     + CREATEDDATE_PA + ", "//4
                     + MODIFIEDBY_PA + ", "//5
                     + MODIFIEDDATE_PA + ", "//6
                     + SCHOOLID_PA + ", "//7
                     + REMARKS_PA + ", "//8
                     + HISTORY_PA + ", "//9
                     + PERSONID_PA + ", "//10
                     + ATTRIBUTE1_PA + ", "//11
                     + ATTRIBUTE2_PA + ")";//20 
        #endregion

        #region UPDATE
        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET [" + CODE + "] = " + CODE_PA + ", "//1
                    + "[" + DEPTID + "] = " + DEPTID_PA + ", "//2
                    + "[" + CREATEDBY + "] = " + CREATEDBY_PA + ", "//3
                    + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ", "//4
                    + "[" + MODIFIEDBY + "] = " + MODIFIEDBY_PA + ", "//5
                    + "[" + MODIFIEDDATE + "] = " + MODIFIEDDATE_PA + ", "//6
                    + "[" + SCHOOLID + "] = " + SCHOOLID_PA + ", "//7
                    + "[" + REMARKS + "] = " + REMARKS_PA + ", "//8
                    + "[" + HISTORY + "] = " + HISTORY_PA + ", "//9
                    + "[" + PERSONID + "] = " + PERSONID_PA + ", "//10
                    + "[" + ATTRIBUTE1 + "] = " + ATTRIBUTE1_PA + ", "//11
                    + "[" + ATTRIBUTE2 + "] = " + ATTRIBUTE2_PA;//20 
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Properties
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        private SqlParameter CodeParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = CODE_PA;

                sqlParam.Value = Code;

                return sqlParam;
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
                SqlParameter param = new SqlParameter();
                param.ParameterName = DEPTID_PA;

                param.Value = DeptID;

                return param;
            }
        }

        public int CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }
        private SqlParameter CreatedByParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = CREATEDBY_PA;

                param.Value = CreatedBy;

                return param;
            }
        }

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }
        private SqlParameter CreatedDateParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = CREATEDDATE_PA;

                param.Value = CreatedDate;

                return param;
            }
        }

        public int ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }
        private SqlParameter ModifiedByParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MODIFIEDBY_PA;

                sqlParam.Value = ModifiedBy;

                return sqlParam;
            }
        }

        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }
        private SqlParameter ModifiedDateParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = MODIFIEDDATE_PA;

                param.Value = ModifiedDate;

                return param;
            }
        }

        public int SchoolId
        {
            get { return _schoolId; }
            set { _schoolId = value; }
        }
        private SqlParameter SchoolIdParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = SCHOOLID_PA;

                sqlParam.Value = SchoolId;

                return sqlParam;
            }
        }

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        private SqlParameter RemarksParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = REMARKS_PA;

                param.Value = Remarks;

                return param;
            }
        }

        public string History
        {
            get { return _history; }
            set { _history = value; }
        }
        private SqlParameter HistoryParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = HISTORY_PA;

                param.Value = History;

                return param;
            }
        }

        public int PersonId
        {
            get { return _personId; }
            set { _personId = value; }
        }
        private SqlParameter PersonIdParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = PERSONID_PA;

                sqlParam.Value = PersonId;

                return sqlParam;
            }
        }

        public string Attribute1
        {
            get { return _attribute1; }
            set { _attribute1 = value; }
        }
        private SqlParameter Attribute1Param
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = ATTRIBUTE1_PA;

                param.Value = Attribute1;

                return param;
            }
        }

        public string Attribute2
        {
            get { return _attribute2; }
            set { _attribute2 = value; }
        }
        private SqlParameter Attribute2Param
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = ATTRIBUTE2_PA;

                param.Value = Attribute2;

                return param;
            }
        }

        #endregion

        #region Methods
        private static Teacher Mapper(SQLNullHandler nullHandler)
        {
            Teacher teacher = new Teacher();

            teacher.Id = nullHandler.GetInt32(TEACHERID);//1
            teacher.Code = nullHandler.GetString(CODE);//2
            teacher.DeptID = nullHandler.GetInt32(DEPTID);
            teacher.CreatedBy = nullHandler.GetInt32(CREATEDBY);//3
            teacher.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);//4
            teacher.ModifiedBy = nullHandler.GetInt32(MODIFIEDBY);//5
            teacher.ModifiedDate = nullHandler.GetDateTime(MODIFIEDDATE);//6
            teacher.SchoolId = nullHandler.GetInt32(SCHOOLID);//7
            teacher.Remarks = nullHandler.GetString(REMARKS);//8
            teacher.History = nullHandler.GetString(HISTORY);//9
            teacher.PersonId = nullHandler.GetInt32(PERSONID);//9
            teacher.Attribute1 = nullHandler.GetString(ATTRIBUTE1);//10
            teacher.Attribute2 = nullHandler.GetString(ATTRIBUTE2);//11
            return teacher;
        }
        private static Teacher MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Teacher teacher = null;
            if (theReader.Read())
            {
                teacher = new Teacher();
                teacher = Mapper(nullHandler);
            }

            return teacher;
        }
        private static List<Teacher> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Teacher> teachers = null;

            while (theReader.Read())
            {
                if (teachers == null)
                {
                    teachers = new List<Teacher>();
                }
                Teacher teacher = Mapper(nullHandler);
                teachers.Add(teacher);
            }

            return teachers;
        }

        public static List<Teacher> Gets()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Teacher> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        public static List<Teacher> Gets(string parameter)
        {
            string command = SELECT
                            + "WHERE [" + CODE + "] LIKE '%" + parameter + "%'";
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Teacher> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        public static Teacher Get(int iD)
        {
            string command = SELECT
                            + "WHERE [" + TEACHERID + "] = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(iD, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            Teacher obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
        public static Teacher Get(string code)
        {
            string command = SELECT
                            + "WHERE [" + CODE + "] = " + CODE_PA;

            SqlParameter codeParam = MSSqlConnectionHandler.MSSqlParamGenerator(code, CODE_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { codeParam });

            Teacher obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }

        public static bool IsExist(string code)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(" + TEACHERID + ") FROM" + TABLENAME
                            + "WHERE [" + CODE + "] = " + CODE_PA;
            SqlParameter codeParam = MSSqlConnectionHandler.MSSqlParamGenerator(code, CODE_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { codeParam });

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }
        internal static bool IsExist(string code, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            string command = "SELECT COUNT(" + TEACHERID + ") FROM" + TABLENAME
                            + "WHERE [" + CODE + "] = " + CODE_PA;
            SqlParameter codeParam = MSSqlConnectionHandler.MSSqlParamGenerator(code, CODE_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran, new SqlParameter[] { codeParam });

            return (Convert.ToInt32(ob) > 0);
        }
        public static bool HasDuplicateCode(Teacher obj)
        {
            if (obj == null)
            {
                return Teacher.IsExist(obj.Code);
            }
            else
            {
                if (obj.Id == 0)
                {
                    if (Teacher.IsExist(obj.Code))
                    {
                        return Teacher.IsExist(obj.Code);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (obj.Code != obj.Code)
                    {
                        return Teacher.IsExist(obj.Code);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public static bool HasDuplicateCode(Teacher obj, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            if (obj == null)
            {
                return Teacher.IsExist(obj.Code, sqlConn, sqlTran);
            }
            else
            {
                if (obj.Id == 0)
                {
                    if (Teacher.IsExist(obj.Code, sqlConn, sqlTran))
                    {
                        return Teacher.IsExist(obj.Code, sqlConn, sqlTran);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (obj.Code != obj.Code)
                    {
                        return Teacher.IsExist(obj.Code, sqlConn, sqlTran);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static int Save(Teacher obj)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (HasDuplicateCode(obj, sqlConn, sqlTran))
                {
                    throw new Exception("Duplicate Code Not Allowed.");
                }

                if (obj.Id == 0)
                {
                    #region Insert
                    command = INSERT;
                    sqlParams = new SqlParameter[] { obj.CodeParam,  
                                                     obj.DeptIDParam,
                                                     obj.CreatedByParam,
                                                     obj.CreatedDateParam,
                                                     obj.ModifiedByParam,
                                                     obj.ModifiedDateParam,
                                                     obj.SchoolIdParam,
                                                     obj.RemarksParam,
                                                     obj.HistoryParam,
                                                     obj.PersonIdParam,
                                                     obj.Attribute1Param, 
                                                     obj.Attribute2Param };
                    #endregion
                }
                else
                {

                    #region Update
                    command = UPDATE
                    + " WHERE [" + TEACHERID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { obj.CodeParam,  
                                                     obj.DeptIDParam,
                                                     obj.CreatedByParam,
                                                     obj.CreatedDateParam,
                                                     obj.ModifiedByParam,
                                                     obj.ModifiedDateParam,
                                                     obj.SchoolIdParam,
                                                     obj.RemarksParam,
                                                     obj.HistoryParam,
                                                     obj.PersonIdParam,
                                                     obj.Attribute1Param,  
                                                     obj.Attribute2Param };
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
        public static int Delete(int iD)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE
                                + "WHERE [" + TEACHERID + "] = " + ID_PA;

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
        #endregion

        public static Teacher GetByPersonId(int personId)
        {
            string command = SELECT
                           + "WHERE [" + PERSONID + "] = " + PERSONID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(personId, PERSONID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            Teacher obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
    }
}
