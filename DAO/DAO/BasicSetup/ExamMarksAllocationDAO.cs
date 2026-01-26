using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ExamMarksAllocationDAO :BaseDAO
    {
        #region Column
        private const string EXAMMARKSALLOCATIONID = "ExamMarksAllocationID";
        private const string EXAMMARKSALLOCATIONID_PA = "@ExamMarksAllocationID";

        private const string EXAMTYPENAMEID = "ExamTypeNameID";
        private const string EXAMTYPENAMEID_PA = "@ExamTypeNameID";

        private const string ALLOTTEDMARKS = "AllottedMarks";
        private const string ALLOTTEDMARKS_PA = "@AllottedMarks";

        private const string EXAMNAME = "ExamName";
        private const string EXAMNAME_PA = "@ExamName";
        #endregion

        #region ALLCOLUMNS
        private const string ALLCOLUMNS = //EXAMMARKSALLOCATIONID + ", "
                                            EXAMTYPENAMEID + ", "
                                          + ALLOTTEDMARKS + ", "
                                          + EXAMNAME + " ";
        #endregion

        #region TABLE NAME
        private const string TABLENAME = " [ExamMarksAllocation] ";
        #endregion

        #region DELETE
        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Insert
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                             + ALLCOLUMNS + " , "
                             + BASECREATORCOLUMNS + ") "
                             + "VALUES ( "

                             + EXAMTYPENAMEID_PA + " , "
                             + ALLOTTEDMARKS_PA + " , "
                             + EXAMNAME_PA + " , "
                             + CREATORID_PA + " , "
                             + CREATEDDATE_PA + " )";
        #endregion


        internal static int Save(List<ExamMarksAllocationEntity> _exmMarkAllocations, int examTypeNameID, System.Data.SqlClient.SqlConnection sqlConn, System.Data.SqlClient.SqlTransaction sqlTran)
        {
            try
            {
                DAOParameters dParam = null;
                int i = 0;

                string command = INSERT;

                foreach (ExamMarksAllocationEntity item in _exmMarkAllocations)
                {
                    dParam = new DAOParameters();

                    //dParam.AddParameter(EXAMMARKSALLOCATIONID_PA, item.Id);
                    dParam.AddParameter(EXAMTYPENAMEID_PA, examTypeNameID);
                    dParam.AddParameter(ALLOTTEDMARKS_PA, item.AllottedMarks);
                    dParam.AddParameter(EXAMNAME_PA, item.ExamName);

                    List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

                    i = QueryHandler.ExecuteSaveBatchAction(command, sqlParams, sqlConn, sqlTran);
                }

                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ExamMarksAllocationEntity> GetExamMarksAllocation(int xmTypNmID)
        {
            try
            {
                List<ExamMarksAllocationEntity> xmMrkAllos = null;
                DAOParameters dParam = new DAOParameters();

                string command = "Select * from " + TABLENAME + " WHERE " + EXAMTYPENAMEID + " = " + EXAMTYPENAMEID_PA;

                dParam.AddParameter(EXAMTYPENAMEID_PA, xmTypNmID);
                List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlDataReader theReader = QueryHandler.ExecuteSelect(command, sqlParams, sqlConn);

                if (theReader != null)
                {
                    xmMrkAllos = MapMrkAllos(theReader);
                }

                theReader.Close();
                MSSqlConnectionHandler.CloseDbConnection(); //Close DB Connection

                return xmMrkAllos; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<ExamMarksAllocationEntity> MapMrkAllos(SqlDataReader theReader)
        {
            try
            {
                SQLNullHandler nullHandler = new SQLNullHandler(theReader);

                List<ExamMarksAllocationEntity> xmMrkAllos = null;

                while (theReader.Read())
                {
                    if (xmMrkAllos == null)
                    {
                        xmMrkAllos = new List<ExamMarksAllocationEntity>();
                    }
                    ExamMarksAllocationEntity xmMrkAllo = xmMrkAlloMapper(nullHandler);
                    xmMrkAllos.Add(xmMrkAllo);
                }
                return xmMrkAllos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static ExamMarksAllocationEntity xmMrkAlloMapper(SQLNullHandler nullHandler)
        {
            ExamMarksAllocationEntity xmMrkAllo = new ExamMarksAllocationEntity();

            xmMrkAllo.Id = nullHandler.GetInt32(EXAMMARKSALLOCATIONID);
            xmMrkAllo.ExamTypeNameID = nullHandler.GetInt32(EXAMTYPENAMEID);
            xmMrkAllo.AllottedMarks = nullHandler.GetInt32(ALLOTTEDMARKS);
            xmMrkAllo.ExamName = nullHandler.GetString(EXAMNAME);

            xmMrkAllo.CreatorID = nullHandler.GetInt32("CreatedBy");
            xmMrkAllo.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            xmMrkAllo.ModifierID = nullHandler.GetInt32("ModifiedBy");
            xmMrkAllo.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return xmMrkAllo;
        }

        internal static int Delete(int id, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                DAOParameters dParam = new DAOParameters();

                string command = DELETE + "Where " + EXAMTYPENAMEID + " = " + EXAMTYPENAMEID_PA;

                dParam.AddParameter(EXAMTYPENAMEID_PA, id);
                List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

                int i = QueryHandler.ExecuteSaveBatchAction(command, sqlParams, sqlConn, sqlTran);

                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        internal static int Save(List<ExamMarksAllocationEntity> _exmMarkAllocations, ExamTypeNameEntity _examTypeName, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
             try
            {
                DAOParameters dParam = null;
                int i = 0;

                string command = INSERT;

                foreach (ExamMarksAllocationEntity item in _exmMarkAllocations)
                {
                    dParam = new DAOParameters();

                    //dParam.AddParameter(EXAMMARKSALLOCATIONID_PA, item.Id);
                    dParam.AddParameter(EXAMTYPENAMEID_PA, _examTypeName.Id);
                    dParam.AddParameter(ALLOTTEDMARKS_PA, item.AllottedMarks);
                    dParam.AddParameter(EXAMNAME_PA, item.ExamName);

                    dParam.AddParameter(CREATORID_PA, _examTypeName.CreatorID);
                    dParam.AddParameter(CREATEDDATE_PA, _examTypeName.CreatedDate);

                    List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

                    i = QueryHandler.ExecuteSaveBatchAction(command, sqlParams, sqlConn, sqlTran);
                }

                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
