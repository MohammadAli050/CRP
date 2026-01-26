using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class Program:Base
    {
        #region DBColumns
        //ProgramID
        //Code
        //ShortName
        //DetailName
        //TotalCredit
        //DeptID
        //ProgramTypeID
        #endregion

        #region Variables
        private string _code;
        private string _lastCode;
        private string _shortName;
        private string _detailName;
        private int _deptID;
        private Department _dept;
        private decimal _totalCredit;
        private int _programTypeID;
        private ProgramType _type;
        #endregion

        #region Constants
        private const string CODE_PA = "@Code";
        private const string SHORTNAME_PA = "@ShortName";
        private const string DETAILNAME_PA = "@DetailName";
        private const string TOTALCREDIT_PA = "@TotalCredit";
        private const string DEPTID_PA = "@DeptID";
        private const string PROGRAMTYPEID_PA = "@ProgramTypeID";

        private const string ALLCOLUMNS = "[ProgramID], "
                                        + "[Code], "
                                        + "[ShortName], "
                                        + "[DetailName], "
                                        + "[TotalCredit], "
                                        + "[DeptID], "
                                        + "[ProgramTypeID], ";

        private const string NOPKCOLUMNS = "[Code], "
                                        + "[ShortName], "
                                        + "[DetailName], "
                                        + "[TotalCredit], "
                                        + "[DeptID], "
                                        + "[ProgramTypeID], ";

        private const string TABLENAME = " [Program] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                             + NOPKCOLUMNS
                             + BASECOLUMNS + ")"
                             + "VALUES ( "
                             + CODE_PA + ", "
                             + SHORTNAME_PA + ", "
                             + DETAILNAME_PA + ", "
                             + TOTALCREDIT_PA + ", "
                             + DEPTID_PA + ", "
                             + PROGRAMTYPEID_PA + ", "
                             + CREATORID_PA + ", "
                             + CREATEDDATE_PA + ", "
                             + MODIFIERID_PA + ", "
                             + MOIDFIEDDATE_PA + ")";

        private const string UPDATE = "UPDATE" + TABLENAME
                            + "SET [Code] = " + CODE_PA + ", "//1
                            + "[ShortName] = " + SHORTNAME_PA + ","//2
                            + "[DetailName] = " + DETAILNAME_PA + ","//3
                            + "[TotalCredit] = " + TOTALCREDIT_PA + ","//4
                            + "[DeptID] = " + DEPTID_PA + ","//5
                            + "[ProgramTypeID] = " + PROGRAMTYPEID_PA + ","//6
                            + "[CreatedBy] = " + CREATORID_PA + ","//7
                            + "[CreatedDate] = " + CREATEDDATE_PA + ","//8
                            + "[ModifiedBy] = " + MODIFIERID_PA + ","//9
                            + "[ModifiedDate] = " + MOIDFIEDDATE_PA;//10

        private const string DELETE = "DELETE FROM" + TABLENAME;
        //private const string SELECT = "SELECT "
        //                    + "[ProgramID], "
        //                    + "[Code], "
        //                    + "[ShortName] "//+ "[Name], "
        //                    //+ "[TotalCredit], "
        //                    //+ "[TotalTrimester], "
        //                    //+ "[DeptID], "
        //                    //+ "[DetailName], "
        //                    //+ "[ProgramTypeID] "
        //                    + "FROM [Program] ";

        //private const string INSERT = "INSERT INTO [Program] "
        //                     + "([Code], "
        //                     + "[ShortName] "//+ "[Name], "
        //                     //+ "[TotalCredit], "
        //                     //+ "[Priority], "
        //                     //+ "[Priority], "
        //                     //+ "[Priority], "
        //                     //+ "[PassingGPA])"
        //                     + "VALUES ";

        ////private const string UPDATE = string.Empty;

        //private const string DELETE = "DELETE FROM [Program] ";
        #endregion

        #region Constructor
        public Program():base()
        {
            _shortName = string.Empty;
            _code = string.Empty;
            _detailName = string.Empty;
            _deptID = 0;
            _dept = null;
            _totalCredit = 0M;
            _programTypeID = 0;
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

        public string ShortName
        {
            get { return _shortName; }
            set { _shortName = value; }
        }
        private SqlParameter ShortNameParam
        {
            get
            {
                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = SHORTNAME_PA;

                nameParam.Value = ShortName;

                return nameParam;
            }
        }

        public string DetailName
        {
            get { return _detailName; }
            set { _detailName = value; }
        }
        private SqlParameter DetailNameParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = DETAILNAME_PA;

                if (DetailName == string.Empty)
                {
                    param.Value = DBNull.Value;
                }
                else
                {
                    param.Value = DetailName;
                }

                return param;
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
        public Department ParentDepartment
        {
            get
            {
                if (_dept == null)
                {
                    if (_deptID > 0)
                    {
                        _dept = Department.GetDept(_deptID);
                    }
                }
                return _dept;
            }
        }

        public decimal TotalCredit
        {
            get { return _totalCredit; }
            set { _totalCredit = value; }
        }
        private SqlParameter TotalCreditParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = TOTALCREDIT_PA;

                if (TotalCredit == 0)
                {
                    param.Value = DBNull.Value;
                }
                else
                {
                    param.Value = TotalCredit;
                }

                return param;
            }
        }

        public int ProgramTypeID
        {
            get { return _programTypeID; }
            set { _programTypeID = value; }
        }
        private SqlParameter ProgramTypeIDParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = PROGRAMTYPEID_PA;

                if (ProgramTypeID == 0)
                {
                    param.Value = DBNull.Value;
                }
                else
                {
                    param.Value = ProgramTypeID;
                }

                return param;
            }
        }
        public ProgramType Type
        {
            get
            {
                if (_type == null)
                {
                    if (_programTypeID > 0)
                    {
                        _type = ProgramType.GetProgramType(_programTypeID);
                    }
                }
                return _type;
            }
        }

        public string LastCode
        {
            get { return _lastCode; }
            set { _lastCode = value; }
        }
        #endregion

        #region Methods
        private static Program ProgramMapper(SQLNullHandler nullHandler)
        {
            Program program = new Program();

            program.Id = nullHandler.GetInt32("ProgramID");
            program.Code = nullHandler.GetString("Code");
            program.LastCode = nullHandler.GetString("Code");
            program.ShortName = nullHandler.GetString("ShortName");
            program.DetailName = nullHandler.GetString("DetailName");
            program.DeptID = nullHandler.GetInt32("DeptID");
            program.TotalCredit = nullHandler.GetDecimal("TotalCredit");
            program.ProgramTypeID = nullHandler.GetInt32("ProgramTypeID");
            program.CreatorID = nullHandler.GetInt32("CreatedBy");
            program.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            program.ModifierID = nullHandler.GetInt32("ModifiedBy");
            program.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return program;
        }
        private static List<Program> MapPrograms(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Program> programs = null;

            while (theReader.Read())
            {
                if (programs == null)
                {
                    programs = new List<Program>();
                }
                Program program = ProgramMapper(nullHandler);
                programs.Add(program);
            }

            return programs;
        }
        private static Program MapProgram(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Program program = null;
            if (theReader.Read())
            {
                program = new Program();
                program = ProgramMapper(nullHandler);
            }

            return program;
        }

        public static List<Program> GetPrograms()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Program> programs = MapPrograms(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return programs;
        }

        public static List<Program> GetProgramsByTeacherAndAcaCal(int teacher, int acaCal)
        {
            string command = @"select p.* from Program as p inner join		
                            (select s1.ProgramID, s1.TeacherOneID,s1.AcademicCalenderID from AcademicCalenderSection as s1 
                            where s1.TeacherOneID = " + teacher + " and s1.AcademicCalenderID = " + acaCal + " " +
                            " union " + 
                            "select s2.ProgramID,s2.TeacherTwoID,s2.AcademicCalenderID from AcademicCalenderSection  as s2 " +
                            "where s2.TeacherTwoID = " + teacher + " and s2.AcademicCalenderID = " + acaCal + ") as sec " +
                            "on p.ProgramID = sec.ProgramID";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Program> programs = MapPrograms(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return programs;
        }

        public static List<Program> GetPrograms(string parameter)
        {
            string command = SELECT
                            + "WHERE [ShortName] Like '%" + parameter + "%' OR [Code] LIKE '%" + parameter + "%'";
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Program> schools = MapPrograms(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return schools;
        }
        public static List<Program> GetProgramsByDeptID(string parameter)
        {
            string command = SELECT;
            if (parameter != string.Empty)
            {
                command = SELECT
                                + "WHERE [DeptID] = " + parameter;
            }
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Program> schools = MapPrograms(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return schools;
        }

        public static Program GetProgram(int programID)
        {
            string command = SELECT
                            + "WHERE ProgramID = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(programID, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            Program program = MapProgram(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return program;
        }
        public static Program GetProgram(string code)
        {
            string command = SELECT
                            + "WHERE Code = " + CODE_PA;

            SqlParameter codeParam = MSSqlConnectionHandler.MSSqlParamGenerator(code, CODE_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { codeParam });

            Program program = MapProgram(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return program;
        }
        public static bool HasDuplicateCode(Program dept)
        {
            if (dept == null)
            {
                return Program.IsExist(dept.Code);
            }
            else
            {
                if (dept.Id == 0)
                {
                    if (Program.IsExist(dept.Code))
                    {
                        return Program.IsExist(dept.Code);
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
                        return Program.IsExist(dept.Code);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        internal static bool HasDuplicateCode(Program dept, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            if (dept == null)
            {
                return Program.IsExist(dept.Code, sqlConn, sqlTran);
            }
            else
            {
                if (dept.Id == 0)
                {
                    if (Program.IsExist(dept.Code, sqlConn, sqlTran))
                    {
                        return Program.IsExist(dept.Code, sqlConn, sqlTran);
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
                        return Program.IsExist(dept.Code, sqlConn, sqlTran);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
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

        public static int Save(Program program)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (HasDuplicateCode(program, sqlConn, sqlTran))
                {
                    throw new Exception("Duplicate Program Code Not Allowed.");
                }

                if (program.Id == 0)
                {
                    command = INSERT;
                    sqlParams = new SqlParameter[] { program.CodeParam,  
                                                     program.ShortNameParam,
                                                     program.DetailNameParam,
                                                     program.TotalCreditParam,
                                                     program.DeptIDParam,
                                                     program.ProgramTypeIDParam,
                                                     program.CreatorIDParam, 
                                                     program.CreatedDateParam, 
                                                     program.ModifierIDParam, 
                                                     program.ModifiedDateParam };
                }
                else
                {


                    command = UPDATE
                            + " WHERE ProgramID = " + ID_PA;
                    sqlParams = new SqlParameter[] { program.CodeParam, //1 
                                                     program.ShortNameParam, //2
                                                     program.DetailNameParam,//3
                                                     program.TotalCreditParam,//4
                                                     program.DeptIDParam,//5
                                                     program.ProgramTypeIDParam,//6
                                                     program.CreatorIDParam, //7
                                                     program.CreatedDateParam, //8
                                                     program.ModifierIDParam, //9
                                                     program.ModifiedDateParam, //10
                                                     program.IDParam };
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
                                + "WHERE ProgramID = " + ID_PA;

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

        internal static string GetProgramCode(int programId, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            string command = "SELECT Code FROM" + TABLENAME
                             + "WHERE [ProgramID] = " + ID_PA;
            SqlParameter idParam = MSSqlConnectionHandler.MSSqlParamGenerator(programId, ID_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran, new SqlParameter[] { idParam });

            return Convert.ToString(ob) ;
        }
                
    }
}
