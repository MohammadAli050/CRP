using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ExamTypeNameDAO : BaseDAO
    {
        #region Column
        private const string EXAMTYPENAMEID = "[ExamTypeNameID]";
        private const string EXAMTYPENAMEID_PA = "@ExamTypeNameID";

        private const string TYPEDEFINITIONID = "[TypeDefinitionID]";
        private const string TYPEDEFINITIONID_PA = "@TypeDefinitionID";

        private const string NAME = "[Name]";
        private const string NAME_PA = "@Name";

        private const string TOTALALLOTTEDMARKS = "[TotalAllottedMarks]";
        private const string TOTALALLOTTEDMARKS_PA = "@TotalAllottedMarks";

        private const string DEFAULT = "[Default]";
        private const string DEFAULT_PA = "@Default";
        #endregion

        #region ALLCOLUMNS
        private const string ALLCOLUMNS = EXAMTYPENAMEID + ", "
                                          + TYPEDEFINITIONID + ", "
                                          + NAME + ", "
                                          + TOTALALLOTTEDMARKS + ", "
                                          + DEFAULT + " , ";
        #endregion

        #region TABLE NAME
        private const string TABLENAME = " [ExamTypeName] ";
        #endregion

        #region DELETE
        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion 

        #region Insert
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                             + ALLCOLUMNS + " "                             
                             + BASECREATORCOLUMNS + ") "

                             + "VALUES ( " 
                             + EXAMTYPENAMEID_PA + " , "
                             + TYPEDEFINITIONID_PA + " , "
                             + NAME_PA + " , "     
                             + TOTALALLOTTEDMARKS_PA + " , "
                             + DEFAULT_PA + " , "
                             + CREATORID_PA + " , "
                             + CREATEDDATE_PA + " )";
        #endregion

        #region UPDATE
        private const string UPDATE =
           " UPDATE " + TABLENAME + "  SET " +
           TYPEDEFINITIONID + " = " + TYPEDEFINITIONID_PA + " , " +
           NAME + " = " + NAME_PA + " , " +
           TOTALALLOTTEDMARKS + " = " + TOTALALLOTTEDMARKS_PA + " , " +
           DEFAULT + " = " + DEFAULT_PA + " , " +
           MODIFIERID + " = " + MODIFIERID_PA + " , " +
           MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA +
           " WHERE " + EXAMTYPENAMEID + " = " + @EXAMTYPENAMEID_PA;
        #endregion
         
        
        internal static int Save(ExamTypeNameEntity _examTypeName, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                DAOParameters dParam = new DAOParameters();

                if (_examTypeName.Default == true)
                {
                    UpdateDefault(_examTypeName.TypeDefinitionID, sqlConn, sqlTran);
                }

                string command = INSERT;

                dParam.AddParameter(EXAMTYPENAMEID_PA, _examTypeName.Id);
                dParam.AddParameter(TYPEDEFINITIONID_PA, _examTypeName.TypeDefinitionID);
                dParam.AddParameter(NAME_PA, _examTypeName.Name);
                dParam.AddParameter(TOTALALLOTTEDMARKS_PA, _examTypeName.TotalAllottedMarks);
                dParam.AddParameter(DEFAULT_PA, _examTypeName.Default);
                dParam.AddParameter(CREATORID_PA, _examTypeName.CreatorID);
                dParam.AddParameter(CREATEDDATE_PA, _examTypeName.CreatedDate);
                List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

                int i = QueryHandler.ExecuteSaveBatchAction(command, sqlParams, sqlConn, sqlTran);

                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        private static void UpdateDefault(int typeDefinitionID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
             try
            {
                int i = 0;

                DAOParameters dParam = new DAOParameters();

                string command = "UPDATE " + TABLENAME + " SET  [Default] = 0  WHERE " + TYPEDEFINITIONID + " = " + TYPEDEFINITIONID_PA;

                dParam.AddParameter(TYPEDEFINITIONID_PA, typeDefinitionID);
                List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

                i = QueryHandler.ExecuteSaveBatchAction(command, sqlParams, sqlConn, sqlTran);
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        internal static int GetMaxExamTypeNameID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int examTypeNameID = 0;

                string command = "SELECT MAX(" + EXAMTYPENAMEID + ") FROM" + TABLENAME;
                examTypeNameID = QueryHandler.GetMaxID(command, sqlConn, sqlTran);

                return examTypeNameID;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public static List<ExamTypeNameEntity> GetAllExamTypeName(string name)
        {
           try
            {
                List<ExamTypeNameEntity> examTypeNames = null;
                //DAOParameters dParam = new DAOParameters();

                string command = "Select * from " + TABLENAME;

                if (name != string.Empty)
                {
                    command += " where Name Like '%" + name + "%'";
                }

                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlDataReader theReader = QueryHandler.ExecuteSelect(command, sqlConn);

                if (theReader != null)
                {
                    examTypeNames = MapExamTypeNames(theReader);
                }

                theReader.Close();
                MSSqlConnectionHandler.CloseDbConnection();

                return examTypeNames; 
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }

        private static List<ExamTypeNameEntity> MapExamTypeNames(SqlDataReader theReader)
        {
           try
            {
                SQLNullHandler nullHandler = new SQLNullHandler(theReader);

                List<ExamTypeNameEntity> examTypeNames = null;

                while (theReader.Read())
                {
                    if (examTypeNames == null)
                    {
                        examTypeNames = new List<ExamTypeNameEntity>();
                    }
                    ExamTypeNameEntity examTypeName = examTypeNameMapper(nullHandler);
                    examTypeNames.Add(examTypeName);
                }
                return examTypeNames;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }

        private static ExamTypeNameEntity examTypeNameMapper(SQLNullHandler nullHandler)
        {
            ExamTypeNameEntity examTypeName = new ExamTypeNameEntity();

            examTypeName.Id = nullHandler.GetInt32("ExamTypeNameID");
            examTypeName.TypeDefinitionID = nullHandler.GetInt32("TypeDefinitionID");
            examTypeName.Name = nullHandler.GetString("Name");
            examTypeName.TotalAllottedMarks = nullHandler.GetInt32("TotalAllottedMarks");
            examTypeName.Default = nullHandler.GetBoolean("Default");

            examTypeName.CreatorID = nullHandler.GetInt32("CreatedBy");
            examTypeName.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            examTypeName.ModifierID = nullHandler.GetInt32("ModifiedBy");
            examTypeName.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return examTypeName;
        }

        public static ExamTypeNameEntity GetExamTypeName(int id)
        {
             try
            {
                ExamTypeNameEntity examTypeNames = null;
                DAOParameters dParam = new DAOParameters();

                string command = "Select * from " + TABLENAME + "where ExamTypeNameID = " + EXAMTYPENAMEID_PA;

                dParam.AddParameter(EXAMTYPENAMEID_PA, id);
                List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlDataReader theReader = QueryHandler.ExecuteSelect(command, sqlParams, sqlConn);

                if (theReader != null)
                {
                    examTypeNames = MapExamTypeName(theReader);
                }

                theReader.Close();
                MSSqlConnectionHandler.CloseDbConnection();

                return examTypeNames;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static ExamTypeNameEntity MapExamTypeName(SqlDataReader theReader)
        {
            try
            {
                SQLNullHandler nullHandler = new SQLNullHandler(theReader);
                ExamTypeNameEntity examTypeName = null;

                while (theReader.Read())
                {
                    examTypeName = examTypeNameMapper(nullHandler);

                }
                return examTypeName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Delete(int id)
        {
             try
            {
                int i = 0;

                DAOParameters dParam = new DAOParameters();

                string command = DELETE + "Where " + EXAMTYPENAMEID + " = " + EXAMTYPENAMEID_PA;

                dParam.AddParameter(EXAMTYPENAMEID_PA, id);
                List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                i = QueryHandler.ExecuteSaveAction(command, sqlParams, sqlConn);
                MSSqlConnectionHandler.CloseDbConnection();


                if (i < 1)
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static int Update(ExamTypeNameEntity _examTypeName, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
           try
            {
                DAOParameters dParam = new DAOParameters();

                if (_examTypeName.Default == true)
                {
                    UpdateDefault(_examTypeName.TypeDefinitionID, sqlConn, sqlTran);
                }

                string command = UPDATE;

                dParam.AddParameter(EXAMTYPENAMEID_PA, _examTypeName.Id);
                dParam.AddParameter(TYPEDEFINITIONID_PA, _examTypeName.TypeDefinitionID);
                dParam.AddParameter(NAME_PA, _examTypeName.Name);
                dParam.AddParameter(TOTALALLOTTEDMARKS_PA, _examTypeName.TotalAllottedMarks);
                dParam.AddParameter(DEFAULT_PA, _examTypeName.Default);

                dParam.AddParameter(MODIFIERID_PA, _examTypeName.ModifierID);
                dParam.AddParameter(MOIDFIEDDATE_PA, _examTypeName.ModifiedDate);

                List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

                int i = QueryHandler.ExecuteSaveBatchAction(command, sqlParams, sqlConn, sqlTran);

                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
    }
}
