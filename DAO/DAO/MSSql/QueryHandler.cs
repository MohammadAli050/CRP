using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;
using Common;

class QueryHandler
{
    //public static object ExecuteSelect(string cmd, string[,] pList)
    //{
    //    object ob = null;

    //    try
    //    {
    //        SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
    //        SqlParameter[] sqlParams = new SqlParameter[pList.Length];

    //        for (int i = 0; i < pList.Length; i++)
    //        {
    //            sqlParams[i] = MSSqlConnectionHandler.MSSqlParamGenerator(pList[i, 0], pList[i, 1]); // first value then name
    //        }

    //        ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(cmd, sqlConn, sqlParams);

    //    }
    //    catch (Exception ex)
    //    {
    //        ;//FixMe use logger
    //    }
    //    finally
    //    {
    //        MSSqlConnectionHandler.CloseDbConnection();
    //    }

    //    return ob;
    //}

    public static object ExecuteSelect(string command, List<SqlParam> pLists)
    {
        object ob = null;

        try
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlParameter[] sqlParams = new SqlParameter[pLists.Count];

            for (int i = 0; i < pLists.Count; i++)
            {
                sqlParams[i] = MSSqlConnectionHandler.MSSqlParamGenerator
                    (pLists[i].SqlParamValue, pLists[i].SqlParamName); // first value then name
            }

            ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, sqlParams);
        }

        catch (Exception)
        {
            //FixMe use logger            
        }
        finally
        {
            MSSqlConnectionHandler.CloseDbConnection();
        }
        return ob;
    }

    public static int ExecuteDelete(string command, List<SqlParam> pLists)
    {
        int count = 0;

        try
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlParameter[] sqlParams = new SqlParameter[pLists.Count];

            for (int i = 0; i < pLists.Count; i++)
            {
                sqlParams[i] = MSSqlConnectionHandler.MSSqlParamGenerator
                    (pLists[i].SqlParamValue, pLists[i].SqlParamName); // first value then name
            }

            count = DataAccess.MSSqlConnectionHandler.MSSqlExecuteAction(command, sqlConn, sqlParams);
        }

        catch (Exception)
        {
            //FixMe use logger            
        }
        finally
        {
            MSSqlConnectionHandler.CloseDbConnection();
        }
        return count;
    }

    internal static SqlDataReader ExecuteSelect(string command, List<SqlParameter> sqlParams, SqlConnection sqlConn)
    {
        SqlDataReader theReader = null;

        try
        {
            SqlParameter[] sqlParameters = new SqlParameter[sqlParams.Count];

            for (int i = 0; i < sqlParams.Count; i++)
            {
                sqlParameters[i] = MSSqlConnectionHandler.MSSqlParamGenerator
                    (sqlParams[i].Value, sqlParams[i].ParameterName.ToString());
            }

            theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, sqlParameters);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return theReader;
    }

    internal static int ExecuteSaveBatchAction(string command, List<SqlParameter> sqlParams, SqlConnection sqlConn, SqlTransaction sqlTran)
    {
        object noOfRowsAffected;

        try
        {
            SqlParameter[] sqlParameters = new SqlParameter[sqlParams.Count];

            for (int i = 0; i < sqlParams.Count; i++)
            {
                sqlParameters[i] = MSSqlConnectionHandler.MSSqlParamGenerator
                    (sqlParams[i].Value, sqlParams[i].ParameterName.ToString());
            }

            noOfRowsAffected = MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParameters);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return Convert.ToInt32(noOfRowsAffected);
    }

    internal static int GetMaxID(string command, SqlConnection sqlConn, SqlTransaction sqlTran)
    {
        int id = 0;

        object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

        if (ob == null || ob == DBNull.Value)
        {
            id = 1;
        }
        else if (ob is Int32)
        {
            id = Convert.ToInt32(ob) + 1;
        }
        return id;
    }

    internal static SqlDataReader ExecuteSelect(string command, SqlConnection sqlConn)
    {
        SqlDataReader theReader = null;

        try
        {
            theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return theReader;
    }

    internal static int ExecuteSaveAction(string command, List<SqlParameter> sqlParams, SqlConnection sqlConn)
    {
        object noOfRowsAffected;

        try
        {
            SqlParameter[] sqlParameters = new SqlParameter[sqlParams.Count];

            for (int i = 0; i < sqlParams.Count; i++)
            {
                sqlParameters[i] = MSSqlConnectionHandler.MSSqlParamGenerator
                    (sqlParams[i].Value, sqlParams[i].ParameterName.ToString());
            }

            noOfRowsAffected = MSSqlConnectionHandler.MSSqlExecuteAction(command, sqlConn, sqlParameters);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return Convert.ToInt32(noOfRowsAffected);
    }
    /// <summary>
    /// created by saima
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="ps"></param>
    /// <returns></returns>
    internal static SqlDataReader ExecuteSelectQuery(string cmd, List<SqlParameter> ps)
    {
        SqlDataReader rd;

        try
        {
            rd = MSSqlConnectionHandler.MSSqlExecuteQuerry(cmd, ps);
        }

        catch (Exception ex)
        {
            throw ex;
            //FixMe use logger            
        }
        return rd;
    }
    /// <summary>
    /// created by saima
    /// </summary>
    /// <param name="command"></param>
    /// <param name="ps"></param>
    /// <returns></returns>
    internal static SqlDataReader ExecuteSelectBatchQuery(string command, List<SqlParameter> ps)
    {
        SqlDataReader ob = null;

        try
        {
            ob = MSSqlConnectionHandler.MSSqlExecuteBatchQuerry(command, ps);
        }

        catch (Exception ex)
        {
            throw ex;
            //FixMe use logger            
        }
        return ob;
    }

    //internal static SqlDataReader ExecuteSelcet(string command, SqlConnection connection)
    //{
    //    SqlDataReader reader = null;
    //    try
    //    {
    //        reader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, connection);
    //    }
    //    catch (Exception exception)
    //    {
            
    //        throw exception;
    //    }
    //    return reader;
    //}

    internal static int ExecuteSelectBatchAction(string cmd, List<SqlParameter> ps)
    {
        int counter = 0;

        try
        {
            counter += MSSqlConnectionHandler.MSSqlExecuteBatchAction(cmd, ps);
        }

        catch (Exception ex)
        {
            throw ex;
            //FixMe use logger            
        }
        return counter;
    }

    internal static int ExecuteDeleteBatchAction(string cmd, List<SqlParameter> ps)
    {
        int counter = 0;

        try
        {
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(cmd, ps);
        }

        catch (Exception ex)
        {
            throw ex;
            //FixMe use logger            
        }
        return counter;
    }

    internal static int ExecuteSaveBatchsScalar(string command, SqlConnection sqlConn, SqlTransaction sqlTran)
    {
        int i = 0;

        object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

        return Convert.ToInt32(ob);
    }

    internal static int ExecuteSaveBatchScalar(string command, List<SqlParameter> sqlParams, SqlConnection sqlConn, SqlTransaction sqlTran)
    {
        object noOfRowsAffected;

        try
        {
            SqlParameter[] sqlParameters = new SqlParameter[sqlParams.Count];

            for (int i = 0; i < sqlParams.Count; i++)
            {
                sqlParameters[i] = MSSqlConnectionHandler.MSSqlParamGenerator
                    (sqlParams[i].Value, sqlParams[i].ParameterName.ToString());
            }

            noOfRowsAffected = MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran, sqlParameters);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return Convert.ToInt32(noOfRowsAffected);
    }

    internal static int MSSqlExecuteActionDelete(string cmd, SqlConnection sqlConn)
    {
        int counter = 0;

        try
        {
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteAction(cmd, sqlConn);
        }

        catch (Exception ex)
        {
            throw ex;
            //FixMe use logger            
        }
        return counter;
    }

    internal static int MSSqlExecuteScalar(string cmd, SqlConnection sqlConn)
    {

        object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(cmd, sqlConn);


        return Convert.ToInt32(ob);
    }
}

