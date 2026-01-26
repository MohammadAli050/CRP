using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class Department : Base
    {
        #region DBColumns
        //DeptID			int		Unchecked
        //Code			nvarchar(50)	Unchecked
        //Name			varchar(100)	Unchecked
        //OpeningDate		datetime	Checked
        //SchoolID		int		Unchecked
        //DetailedName		varchar(100)	Unchecked
        //ClosingDate		datetime	Checked
        //CreatedBy		int		Unchecked
        //CreatedDate		datetime	Unchecked
        //ModifiedBy		int		Checked
        //ModifiedDate		datetime	Checked 
        #endregion

        #region Variables
        private string _code;
        private string _lastCode;
        private string _name;
        private string _detailedName;
        private DateTime _openingDate;
        private DateTime _closingDate;
        private int _schoolID;
        private School _parentSchool = null;
        #endregion

        #region Constants

        private const string CODE = "Code";
        private const string CODE_PA = "@Code";

        private const string NAME = "Name";
        private const string NAME_PA = "@Name";

        private const string DETAILEDNAME = "DetailedName";
        private const string DETAILEDNAME_PA = "@DetailedName";

        private const string OPENINGDATE = "OpeningDate";
        private const string OPENINGDATE_PA = "@OpeningDate";

        private const string CLOSINGDATE = "ClosingDate";
        private const string CLOSINGDATE_PA = "@ClosingDate";

        private const string SCHOOLID = "SchoolID";
        private const string SCHOOLID_PA = "@SchoolID";

        private const string ALLCOLUMNS = "[DeptID], "
                                        + "[Code], "
                                        + "[Name], "
                                        + "[DetailedName], "
                                        + "[OpeningDate], "
                                        + "[ClosingDate], ";

        private const string NOPKCOLUMNS = "[Code], "
                                        + "[Name], "
                                        + "[DetailedName], "
                                        + "[OpeningDate], "
                                        + "[ClosingDate], ";

        private const string TABLENAME = " [Department] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                             + NOPKCOLUMNS
                             + BASECOLUMNS + ")"
                             + "VALUES ( "
                             + CODE_PA + ", "
                             + NAME_PA + ", "
                             + DETAILEDNAME_PA + ", "
                             + OPENINGDATE_PA + ", "
                             + CLOSINGDATE_PA + ", "
                             //+ SCHOOLID_PA + ", "
                             + CREATORID_PA + ", "
                             + CREATEDDATE_PA + ", "
                             + MODIFIERID_PA + ", "
                             + MOIDFIEDDATE_PA + ")";

        private const string UPDATE = "UPDATE" + TABLENAME
                            + "SET [Code] = " + CODE_PA + ", "
                            + "[Name] = " + NAME_PA + ","
                            + "[DetailedName] = " + DETAILEDNAME_PA + ","
                            + "[OpeningDate] = " + OPENINGDATE_PA + ","
                            + "[ClosingDate] = " + CLOSINGDATE_PA + ","
                            //+ "[SchoolID] = " + SCHOOLID_PA + ","
                            + "[CreatedBy] = " + CREATORID_PA + ","
                            + "[CreatedDate] = " + CREATEDDATE_PA + ","
                            + "[ModifiedBy] = " + MODIFIERID_PA + ","
                            + "[ModifiedDate] = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Constructor
        public Department():base()
        {
            _name = string.Empty;
            _code = string.Empty;
            _detailedName = string.Empty;
            _openingDate = DateTime.MinValue;
            _closingDate = DateTime.MinValue;
            _schoolID = 0;
            _parentSchool = null;
        }
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
                SqlParameter codeParam = new SqlParameter();
                codeParam.ParameterName = CODE_PA;

                codeParam.Value = Code;

                return codeParam;
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private SqlParameter NameParam
        {
            get
            {
                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = NAME_PA;

                nameParam.Value = Name;

                return nameParam;
            }
        }

        public string DetailedName
        {
            get { return _detailedName; }
            set { _detailedName = value; }
        }
        private SqlParameter DetailedNameParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = DETAILEDNAME_PA;

                param.Value = DetailedName;

                return param;
            }
        }

        public DateTime OpeningDate
        {
            get { return _openingDate; }
            set { _openingDate = value; }
        }
        private SqlParameter OpeningDateParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = OPENINGDATE_PA;

                if (_openingDate == DateTime.MinValue)
                {
                    param.Value = DBNull.Value;
                }
                else
                {
                    param.Value = _openingDate;
                }

                return param;
            }
        }

        public DateTime ClosingDate
        {
            get { return _closingDate; }
            set { _closingDate = value; }
        }
        private SqlParameter ClosingDateParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = CLOSINGDATE_PA;

                if (_closingDate == DateTime.MinValue)
                {
                    param.Value = DBNull.Value;
                }
                else
                {
                    param.Value = _closingDate;
                }

                return param;
            }
        }

        public int SchoolID
        {
            get { return _schoolID; }
            set { _schoolID = value; }
        }
        private SqlParameter SchoolIDParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = SCHOOLID_PA;

                param.Value = _schoolID;

                return param;
            }
        }
        public School ParentSchool
        {
            get
            {
                if (_parentSchool == null)
                {
                    if (_schoolID > 0)
                    {
                        _parentSchool = School.GetSchool(_schoolID);
                    }
                }
                return _parentSchool;
            }
        }


        public string LastCode
        {
            get { return _lastCode; }
            set { _lastCode = value; }
        }
        #endregion

        #region Methods
        private static Department DeptMapper(SQLNullHandler nullHandler)
        {
            Department dept = new Department();

            dept.Id = nullHandler.GetInt32("DeptID");
            dept.Name = nullHandler.GetString("Name");
            dept.Code = nullHandler.GetString("Code");
            dept.LastCode = nullHandler.GetString("Code");
            dept.DetailedName = nullHandler.GetString("DetailedName");
            dept.OpeningDate = nullHandler.GetDateTime("OpeningDate");
            dept.ClosingDate = nullHandler.GetDateTime("ClosingDate");
            dept.CreatorID = nullHandler.GetInt32("CreatedBy");
            dept.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            dept.ModifierID = nullHandler.GetInt32("ModifiedBy");
            dept.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return dept;
        }
        private static List<Department> MapDepts(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Department> depts = null;

            while (theReader.Read())
            {
                if (depts == null)
                {
                    depts = new List<Department>();
                }
                Department program = DeptMapper(nullHandler);
                depts.Add(program);
            }

            return depts;
        }
        private static Department MapDept(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Department dept = null;
            if (theReader.Read())
            {
                dept = new Department();
                dept = DeptMapper(nullHandler);
            }

            return dept;
        }

        public static List<Department> GetDepts()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Department> schools = MapDepts(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return schools;
        }
        public static List<Department> GetDepts(string parameter)
        {
            string command = SELECT
                            + "WHERE [Name] Like '%" + parameter + "%' OR [Code] LIKE '%" + parameter + "%'";
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Department> schools = MapDepts(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return schools;
        }
        public static Department GetDept(int schoolID)
        {
            string command = SELECT
                            + "WHERE DeptID = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(schoolID, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            Department school = MapDept(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return school;
        }
        public static Department GetDept(string code)
        {
            string command = SELECT
                            + "WHERE Code = " + CODE_PA;

            SqlParameter codeParam = MSSqlConnectionHandler.MSSqlParamGenerator(code, CODE_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { codeParam });

            Department school = MapDept(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return school;
        }

        public static bool IsExist(string code)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE [Code] = " + CODE_PA;
            SqlParameter codeParam = MSSqlConnectionHandler.MSSqlParamGenerator(code, CODE_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { codeParam });

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }
        internal static bool IsExist(string code, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE [Code] = " + CODE_PA;
            SqlParameter codeParam = MSSqlConnectionHandler.MSSqlParamGenerator(code, CODE_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran,new SqlParameter[] { codeParam });

            return (Convert.ToInt32(ob) > 0);
        }
        public static bool HasDuplicateCode(Department dept)
        {
            if (dept == null)
            {
                return Department.IsExist(dept.Code);
            }
            else
            {
                if (dept.Id == 0)
                {
                    if (Department.IsExist(dept.Code))
                    {
                        return Department.IsExist(dept.Code);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (dept.Code != dept.LastCode)
                    {
                        return Department.IsExist(dept.Code);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public static bool HasDuplicateCode(Department dept, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            if (dept == null)
            {
                return Department.IsExist(dept.Code, sqlConn, sqlTran);
            }
            else
            {
                if (dept.Id == 0)
                {
                    if (Department.IsExist(dept.Code, sqlConn, sqlTran))
                    {
                        return Department.IsExist(dept.Code, sqlConn, sqlTran);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (dept.Code != dept.LastCode)
                    {
                        return Department.IsExist(dept.Code, sqlConn, sqlTran);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static int Save(Department dept)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (HasDuplicateCode(dept, sqlConn,sqlTran))
                {
                    throw new Exception("Duplicate Department Code Not Allowed.");
                }

                if (dept.Id == 0)
                {
                    command = INSERT;
                    sqlParams = new SqlParameter[] { dept.NameParam,  
                                                     dept.CodeParam,
                                                     dept.DetailedNameParam,
                                                     dept.OpeningDateParam,
                                                     dept.ClosingDateParam,
                                                     //dept.SchoolIDParam,
                                                     dept.CreatorIDParam, 
                                                     dept.CreatedDateParam, 
                                                     dept.ModifierIDParam, 
                                                     dept.ModifiedDateParam };
                }
                else
                {

                    command = UPDATE
                            + " WHERE DeptID = " + ID_PA;
                    sqlParams = new SqlParameter[] { dept.NameParam,  
                                                     dept.CodeParam,
                                                     dept.DetailedNameParam,
                                                     dept.OpeningDateParam,
                                                     dept.ClosingDateParam,
                                                     //dept.SchoolIDParam,
                                                     dept.CreatorIDParam, 
                                                     dept.CreatedDateParam, 
                                                     dept.ModifierIDParam, 
                                                     dept.ModifiedDateParam, 
                                                     dept.IDParam };
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
        public static int Delete(int deptID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE
                                + "WHERE DeptID = " + ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(deptID, ID_PA);
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
    }
}
