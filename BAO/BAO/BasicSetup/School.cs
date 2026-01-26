using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class School : Base
    {
        #region Variables
        private string _name;
        private string _code;
        private string _lastCode;
        #endregion

        #region Constants

        private const string NAME_PA = "@Name";

        private const string CODE_PA = "@Code";

        private const string ALLCOLUMNS = "[SchoolID], "
                                        + "[Name], "
                                        + "[Code], ";

        private const string NOPKCOLUMNS = "[Name], "
                                         + "[Code], ";

        private const string TABLENAME = " [School] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string INSERT = "INSERT INTO"+ TABLENAME +  "("
                             + NOPKCOLUMNS
                             + BASECOLUMNS +")"
                             + "VALUES ( "
                             + NAME_PA + ", "
                             + CODE_PA + ", "
                             + CREATORID_PA + ", "
                             + CREATEDDATE_PA + ", "
                             + MODIFIERID_PA + ", "
                             + MOIDFIEDDATE_PA + ")";

        private const string UPDATE = "UPDATE" + TABLENAME
                            + "SET [Name] = " + NAME_PA + ", "
                            + "[Code] = " + CODE_PA + ","
                            + "[CreatedBy] = " + CREATORID_PA + ","
                            + "[CreatedDate] = " + CREATEDDATE_PA + ","
                            + "[ModifiedBy] = " + MODIFIERID_PA + ","
                            + "[ModifiedDate] = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Constructor
        public School():base()
        {
            _name = string.Empty;
            _code = string.Empty;
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

        public string LastCode
        {
            get { return _lastCode; }
            set { _lastCode = value; }
        }
        #endregion

        #region Methods
        private static School SchoolMapper(SQLNullHandler nullHandler)
        {
            School school = new School();

            school.Id = nullHandler.GetInt32("SchoolID");
            school.Name = nullHandler.GetString("Name");
            school.Code = nullHandler.GetString("Code");
            school.LastCode = nullHandler.GetString("Code");
            school.CreatorID = nullHandler.GetInt32("CreatedBy");
            school.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            school.ModifierID = nullHandler.GetInt32("ModifiedBy");
            school.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return school;
        }
        private static List<School> MapSchools(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<School> schools = null;

            while (theReader.Read())
            {
                if (schools == null)
                {
                    schools = new List<School>();
                }
                School school = SchoolMapper(nullHandler);
                schools.Add(school);
            }

            return schools;
        }
        private static School MapSchool(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            School school = null;
            if (theReader.Read())
            {
                school = new School();
                school = SchoolMapper(nullHandler);
            }

            return school;
        }

        public static List<School> GetSchools()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<School> schools = MapSchools(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return schools;
        }
        public static List<School> GetSchools(string parameter)
        {
            string command = SELECT
                            + "WHERE [Name] Like '%" + parameter + "%' OR [Code] LIKE '%" + parameter + "%'";
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<School> schools = MapSchools(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return schools;
        }
        public static School GetSchool(int schoolID)
        {
            string command = SELECT
                            + "WHERE SchoolID = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(schoolID, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            School school = MapSchool(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return school;
        }
        public static School GetSchool(string code)
        {
            string command = SELECT
                            + "WHERE Code = " + CODE_PA;

            SqlParameter codeParam = MSSqlConnectionHandler.MSSqlParamGenerator(code, CODE_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { codeParam });

            School school = MapSchool(theReader);
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
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran, new SqlParameter[] { codeParam });

            return (Convert.ToInt32(ob) > 0);
        }
        public static bool HasDuplicateCode(School school)
        {
            if (school == null)
            {
                return School.IsExist(school.Code);
            }
            else
            {
                if (school.Id == 0)
                {
                    if (School.IsExist(school.Code))
                    {
                        return School.IsExist(school.Code);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (school.Code != school.LastCode)
                    {
                        return School.IsExist(school.Code);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public static bool HasDuplicateCode(School school, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            if (school == null)
            {
                return School.IsExist(school.Code, sqlConn, sqlTran);
            }
            else
            {
                if (school.Id == 0)
                {
                    if (School.IsExist(school.Code, sqlConn, sqlTran))
                    {
                        return School.IsExist(school.Code, sqlConn, sqlTran);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (school.Code != school.LastCode)
                    {
                        return School.IsExist(school.Code, sqlConn, sqlTran);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static int Save(School school)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (HasDuplicateCode(school, sqlConn, sqlTran))
                {
                    throw new Exception("Duplicate School Code Not Allowed.");
                }

                if (school.Id == 0)
                {
                    command = INSERT;
                    sqlParams = new SqlParameter[] { school.NameParam,  
                                                     school.CodeParam,
                                                     school.CreatorIDParam, 
                                                     school.CreatedDateParam, 
                                                     school.ModifierIDParam, 
                                                     school.ModifiedDateParam };
                }
                else
                {
                    command = UPDATE
                            + " WHERE SchoolID = " + ID_PA;
                    sqlParams = new SqlParameter[] { school.NameParam,  
                                                     school.CodeParam,
                                                     school.CreatorIDParam, 
                                                     school.CreatedDateParam, 
                                                     school.ModifierIDParam, 
                                                     school.ModifiedDateParam, 
                                                     school.IDParam };
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
        public static int Delete(int schoolID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE
                                + "WHERE SchoolID = " + ID_PA;

                SqlParameter userIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(schoolID, ID_PA);
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { userIDParam });

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
