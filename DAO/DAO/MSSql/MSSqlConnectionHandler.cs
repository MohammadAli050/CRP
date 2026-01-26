using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Transactions;
using Common;

namespace DataAccess
{
    /// <summary>
    /// Use this class for all database related activity.
    /// </summary>
    public class MSSqlConnectionHandler
    {
        #region Variables
        private static ConnectionStringSettings _connStrSet = new ConnectionStringSettings();
        private static SqlConnection _theCon;
        private static SqlTransaction _theTran;
        #endregion

        #region Constructor
        private MSSqlConnectionHandler()
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Create a new connection with the database
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {

            //string str = ConfigurationManager.ConnectionStrings["Connection String"].ConnectionString;
            //string desStr = Cryptography.DecryptString(str);
            //_connStrSet = new ConnectionStringSettings("Connection String", desStr); // ConfigurationManager.ConnectionStrings["Connection String"];

            _connStrSet =  ConfigurationManager.ConnectionStrings["Connection String"];
            

            _theCon = new SqlConnection(_connStrSet.ConnectionString);

            return _theCon;
        }
        /// <summary>
        /// Start a new transaction with the database
        /// </summary>
        /// <returns></returns>
        public static SqlTransaction StartTransaction()
        {
            if (_theCon.State != ConnectionState.Open)
            {
                _theCon.Open();
            }

            _theTran = _theCon.BeginTransaction();

            return _theTran;
        }

        public static void CommitTransaction()
        {
            try
            {
                _theTran.Commit();
            }
            catch (Exception Ex)
            {
                try
                {
                    _theTran.Rollback();
                }
                catch (Exception InnerEx)
                {
                    throw InnerEx;
                }
                throw Ex;
            }
        }
        public static void CloseDbConnection()
        {
            if (_theCon != null)
            {
                if (_theCon.State != System.Data.ConnectionState.Closed)
                {
                    _theCon.Close();
                }
            }
        }

        public static bool IsATransactionActive()
        {
            if (_theTran != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void RollBackAndClose()
        {
            if (_theTran != null)
            {
                if (_theTran.Connection != null)
                {
                    _theTran.Rollback();
                }
                else
                {
                    _theTran = null;
                }
            }
            if (_theCon != null)
            {
                if (_theCon.State != System.Data.ConnectionState.Closed)
                {
                    _theCon.Close();
                }
            }
        }

        public static int MSSqlExecuteAction(string command, SqlConnection connection)
        {
            SqlCommand theCom = new SqlCommand(command, connection);
            int noOfRowsAffected = 0;

            try
            {
                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }
                theCom.Transaction = theCom.Connection.BeginTransaction();

                noOfRowsAffected = theCom.ExecuteNonQuery();

                theCom.Transaction.Commit();
            }
            catch (Exception Ex)
            {
                try
                {
                    if (theCom.Transaction != null)
                    {
                        theCom.Transaction.Rollback();
                    }
                }
                catch (Exception InnerEx)
                {
                    throw InnerEx;
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                throw Ex;
            }

            return noOfRowsAffected;
        }
        public static int MSSqlExecuteActionLTM(string command, SqlConnection connection)
        {
            SqlCommand theCom = new SqlCommand(command, connection);
            int noOfRowsAffected = 0;

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (theCom.Connection.State != ConnectionState.Open)
                    {
                        theCom.Connection.Open();
                    }
                    //theCom.Transaction = theCom.Connection.BeginTransaction();

                    noOfRowsAffected = theCom.ExecuteNonQuery();

                    //theCom.Transaction.Commit();
                    tx.Complete();
                }
            }
            catch (Exception Ex)
            {
                try
                {
                    if (theCom.Transaction != null)
                    {
                        theCom.Transaction.Rollback();
                    }
                }
                catch (Exception InnerEx)
                {
                    throw InnerEx;
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                throw Ex;
            }

            return noOfRowsAffected;
        }
        public static int MSSqlExecuteAction(string command, SqlConnection connection, SqlParameter[] parameters)
        {
            SqlCommand theCom = new SqlCommand(command, connection);
            int noOfRowsAffected = 0;

            try
            {
                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }

                foreach (SqlParameter sqlParam in parameters)
                {
                    theCom.Parameters.Add(sqlParam);
                }

                theCom.Transaction = theCom.Connection.BeginTransaction();

                noOfRowsAffected = theCom.ExecuteNonQuery();

                theCom.Transaction.Commit();
            }
            catch (Exception Ex)
            {
                try
                {
                    if (theCom.Transaction != null)
                    {
                        theCom.Transaction.Rollback();
                    }
                }
                catch (Exception InnerEx)
                {
                    throw InnerEx;
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                throw Ex;
            }

            return noOfRowsAffected;
        }
        public static int MSSqlExecuteActionLTM(string command, SqlConnection connection, SqlParameter[] parameters)
        {
            SqlCommand theCom = new SqlCommand(command, connection);
            int noOfRowsAffected = 0;

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (theCom.Connection.State != ConnectionState.Open)
                    {
                        theCom.Connection.Open();
                    }

                    foreach (SqlParameter sqlParam in parameters)
                    {
                        theCom.Parameters.Add(sqlParam);
                    }

                    //theCom.Transaction = theCom.Connection.BeginTransaction();

                    noOfRowsAffected = theCom.ExecuteNonQuery();

                    //theCom.Transaction.Commit();
                    tx.Complete();
                }
            }
            catch (Exception Ex)
            {
                try
                {
                    if (theCom.Transaction != null)
                    {
                        theCom.Transaction.Rollback();
                    }
                }
                catch (Exception InnerEx)
                {
                    throw InnerEx;
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                throw Ex;
            }

            return noOfRowsAffected;
        }

        public static int MSSqlExecuteBatchAction(string command, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand theCom = new SqlCommand();
            int noOfRowsAffected = 0;

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                theCom = new SqlCommand(command, connection, transaction);

                noOfRowsAffected = theCom.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                try
                {
                    if (theCom.Transaction != null)
                    {
                        theCom.Transaction.Rollback();
                    }
                }
                catch (Exception InnerEx)
                {
                    throw InnerEx;
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                throw Ex;
            }

            return noOfRowsAffected;
        }
        public static int MSSqlExecuteBatchAction(string command, SqlConnection connection, SqlTransaction transaction, SqlParameter[] parameters)
        {
            SqlCommand theCom = new SqlCommand();
            int noOfRowsAffected = 0;

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                theCom = new SqlCommand(command, connection, transaction);

                foreach (SqlParameter sqlParam in parameters)
                {
                    theCom.Parameters.Add(sqlParam);
                }

                noOfRowsAffected = theCom.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                try
                {
                    theCom.Transaction.Rollback();
                }
                catch (Exception InnerEx)
                {
                    throw InnerEx;
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                throw Ex;
            }

            return noOfRowsAffected;
        }
        public static int MSSqlExecuteBatchAction(string command, SqlConnection connection, SqlTransaction transaction, List<SqlParameter> parameters)
        {
            SqlCommand theCom = new SqlCommand();
            int noOfRowsAffected = 0;

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                theCom = new SqlCommand(command, connection, transaction);



                foreach (SqlParameter sqlParam in parameters)
                {
                    theCom.Parameters.Add(sqlParam);
                }

                noOfRowsAffected = theCom.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                try
                {
                    theCom.Transaction.Rollback();
                }
                catch (Exception InnerEx)
                {
                    throw InnerEx;
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                throw Ex;
            }

            return noOfRowsAffected;
        }
        public static int MSSqlExecuteBatchAction(string command, CommandType commandType, SqlConnection connection, SqlTransaction transaction, List<SqlParameter> parameters)
        {
            SqlCommand theCom = new SqlCommand();
            int noOfRowsAffected = 0;

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                theCom = new SqlCommand(command, connection, transaction);
                theCom.CommandType = commandType;


                foreach (SqlParameter sqlParam in parameters)
                {
                    theCom.Parameters.Add(sqlParam);
                }

                noOfRowsAffected = theCom.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                try
                {
                    theCom.Transaction.Rollback();
                }
                catch (Exception InnerEx)
                {
                    throw InnerEx;
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                throw Ex;
            }

            return noOfRowsAffected;
        }
        public static int MSSqlExecuteBatchAction(string command, List<SqlParameter> parameters)
        {
            SqlCommand theCom = new SqlCommand();
            int noOfRowsAffected = 0;

            try
            {
                if (_theCon == null)
                {
                    _theCon = GetConnection();
                }
                if (_theCon.State != ConnectionState.Open)
                {
                    _theCon.Open();
                }
                if (_theTran == null)
                {
                    _theTran = StartTransaction();
                }

                theCom = new SqlCommand(command, _theCon, _theTran);
                if (parameters != null)
                {
                    foreach (SqlParameter sqlParam in parameters)
                    {
                        theCom.Parameters.Add(sqlParam);
                    }
                }

                noOfRowsAffected = theCom.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                try
                {
                    theCom.Transaction.Rollback();
                }
                catch (Exception InnerEx)
                {
                    throw InnerEx;
                }
                finally
                {
                    if (_theCon.State != System.Data.ConnectionState.Closed)
                    {
                        _theCon.Close();
                    }
                }
                throw Ex;
            }

            return noOfRowsAffected;
        }

        public static SqlDataReader MSSqlExecuteQuerry(string command, SqlConnection connection)
        {
            SqlDataReader theReader;
            SqlCommand theCom = new SqlCommand(command, connection);

            try
            {
                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }
                theReader = theCom.ExecuteReader();
            }
            catch (Exception Ex)
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
                throw Ex;
            }

            return theReader;
        }
        public static SqlDataReader MSSqlExecuteQuerry(string command, SqlConnection connection, SqlParameter[] parameters)
        {
            SqlDataReader theReader;
            SqlCommand theCom = new SqlCommand(command, connection);

            try
            {
                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }

                foreach (SqlParameter sqlParam in parameters)
                {
                    theCom.Parameters.Add(sqlParam);
                }

                theReader = theCom.ExecuteReader();
            }
            catch (Exception Ex)
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
                throw Ex;
            }

            return theReader;
        }
        public static SqlDataReader MSSqlExecuteQuerry(string command, List<SqlParameter> parameters)
        {
            SqlDataReader theReader;
            try
            {
                if (_theCon == null)
                {
                    _theCon = GetConnection();
                }
                if (_theCon.State != ConnectionState.Open)
                {
                    _theCon.Open();
                }
                SqlCommand theCom = new SqlCommand(command, _theCon);

                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }
                foreach (SqlParameter sqlParam in parameters)
                {
                    theCom.Parameters.Add(sqlParam);
                }
                theReader = theCom.ExecuteReader();
            }
            catch (Exception Ex)
            {
                if (_theCon.State != System.Data.ConnectionState.Closed)
                {
                    _theCon.Close();
                }
                throw Ex;
            }
            return theReader;
        }

        public static SqlDataReader MSSqlExecuteBatchQuerry(string command, SqlConnection connection, SqlTransaction transaction, SqlParameter[] parameters)
        {
            SqlDataReader theReader;
            SqlCommand theCom = new SqlCommand(command, connection, transaction);

            try
            {
                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }

                foreach (SqlParameter sqlParam in parameters)
                {
                    theCom.Parameters.Add(sqlParam);
                }

                theReader = theCom.ExecuteReader();
            }
            catch (Exception Ex)
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
                throw Ex;
            }

            return theReader;
        }
        public static SqlDataReader MSSqlExecuteBatchQuerry(string command, List<SqlParameter> parameters)
        {
            SqlDataReader theReader;
            if (_theCon == null)
            {
                _theCon = GetConnection();
            }
            if (_theCon.State != ConnectionState.Open)
            {
                _theCon.Open();
            }
            if (_theTran == null)
            {
                _theTran = StartTransaction();
            }

            SqlCommand theCom = new SqlCommand(command, _theCon, _theTran);

            try
            {
                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }

                foreach (SqlParameter sqlParam in parameters)
                {
                    theCom.Parameters.Add(sqlParam);
                }

                theReader = theCom.ExecuteReader();
            }
            catch (Exception Ex)
            {
                theCom.Transaction.Rollback();
                if (_theCon.State != System.Data.ConnectionState.Closed)
                {
                    _theCon.Close();
                }
                throw Ex;
            }

            return theReader;
        }

        public static SqlDataReader MSSqlExecuteQuerry(string command, SqlConnection connection, int commandTimeOutLimit)
        {
            SqlDataReader theReader;
            SqlCommand theCom = new SqlCommand(command, connection);
            theCom.CommandTimeout = commandTimeOutLimit;

            try
            {
                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }
                theReader = theCom.ExecuteReader();
            }
            catch (Exception Ex)
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
                throw Ex;
            }

            return theReader;
        }
        public static SqlDataReader MSSqlExecuteQuerry(string command, SqlConnection connection, int commandTimeOutLimit, SqlParameter[] parameters)
        {
            SqlDataReader theReader;
            SqlCommand theCom = new SqlCommand(command, connection);
            theCom.CommandTimeout = commandTimeOutLimit;

            try
            {
                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }

                foreach (SqlParameter sqlParam in parameters)
                {
                    theCom.Parameters.Add(sqlParam);
                }

                theReader = theCom.ExecuteReader();
            }
            catch (Exception Ex)
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
                throw Ex;
            }

            return theReader;
        }

        public static DataTable MSSqlExecuteQuerry(string command, string tableName, SqlConnection connection)
        {
            DataTable theTable = new DataTable(tableName);

            SqlCommand theCom = new SqlCommand(command, connection);
            SqlDataAdapter theAdapter = new SqlDataAdapter(theCom);

            try
            {
                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }
                theAdapter.Fill(theTable);
            }
            catch (Exception Ex)
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
                throw Ex;
            }
            return theTable;
        }

        public static DataTable MSSqlExecuteQuerry_dt(string command, SqlConnection connection)
        {
            try
            {
                DataTable dataTable = new DataTable();

                SqlCommand sqlCommand = new SqlCommand(command, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

                if (sqlCommand.Connection.State != ConnectionState.Open)
                {
                    sqlCommand.Connection.Open();
                }

                adapter.Fill(dataTable);
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
                return dataTable;
            }
            catch (Exception exception)
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
                throw exception;
            }
        }
        public static DataTable MSSqlExecuteQuerry(string command, string tableName, SqlConnection connection, SqlParameter[] parameters)
        {
            DataTable theTable = new DataTable(tableName);

            SqlCommand theCom = new SqlCommand(command, connection);
            SqlDataAdapter theAdapter = new SqlDataAdapter(theCom);

            try
            {
                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }

                foreach (SqlParameter sqlParam in parameters)
                {
                    theCom.Parameters.Add(sqlParam);
                }

                theAdapter.Fill(theTable);
            }
            catch (Exception Ex)
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
                throw Ex;
            }

            return theTable;
        }

        public static void MSSqlExecuteQuerry(string command, DataSet theSet, string tableName, SqlConnection connection)
        {
            SqlCommand theCom = new SqlCommand(command, connection);
            SqlDataAdapter theAdapter = new SqlDataAdapter(theCom);

            try
            {
                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }
                theAdapter.Fill(theSet, tableName);
            }
            catch (Exception Ex)
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
                throw Ex;
            }
        }

        public static DataTable MSSqlExecuteQuerry(string command, string tableName, SqlConnection connection, int commandTimeOutLimit)
        {
            DataTable theTable = new DataTable(tableName);

            SqlCommand theCom = new SqlCommand(command, connection);
            theCom.CommandTimeout = commandTimeOutLimit;
            SqlDataAdapter theAdapter = new SqlDataAdapter(theCom);

            try
            {
                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }
                theAdapter.Fill(theTable);
            }
            catch (Exception Ex)
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
                throw Ex;
            }

            return theTable;
        }
        public static DataTable MSSqlExecuteQuerry(string command, string tableName, SqlConnection connection, int commandTimeOutLimit, SqlParameter[] parameters)
        {
            DataTable theTable = new DataTable(tableName);

            SqlCommand theCom = new SqlCommand(command, connection);
            theCom.CommandTimeout = commandTimeOutLimit;
            SqlDataAdapter theAdapter = new SqlDataAdapter(theCom);

            try
            {
                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }

                foreach (SqlParameter sqlParam in parameters)
                {
                    theCom.Parameters.Add(sqlParam);
                }

                theAdapter.Fill(theTable);
            }
            catch (Exception Ex)
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
                throw Ex;
            }

            return theTable;
        }

        public static object MSSqlExecuteScalar(string command, SqlConnection connection)
        {
            object dataum = null;

            SqlCommand theCom = new SqlCommand(command, connection);

            try
            {
                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }
                theCom.Transaction = theCom.Connection.BeginTransaction();

                dataum = theCom.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                try
                {
                    if (theCom.Transaction != null)
                    {
                        theCom.Transaction.Rollback();
                    }
                }
                catch (Exception InnerEx)
                {
                    throw InnerEx;
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                throw Ex;
            }

            return dataum;
        }
        public static object MSSqlExecuteScalar(string command, SqlConnection connection, SqlParameter[] parameters)
        {
            object dataum = null;

            SqlCommand theCom = new SqlCommand(command, connection);

            try
            {
                if (theCom.Connection.State != ConnectionState.Open)
                {
                    theCom.Connection.Open();
                }

                theCom.Transaction = theCom.Connection.BeginTransaction();
                foreach (SqlParameter sqlParam in parameters)
                {
                    theCom.Parameters.Add(sqlParam);
                }

                dataum = theCom.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                try
                {
                    if (theCom.Transaction != null)
                    {
                        theCom.Transaction.Rollback();
                    }
                }
                catch (Exception InnerEx)
                {
                    throw InnerEx;
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                throw Ex;
            }

            return dataum;
        }

        public static object MSSqlExecuteBatchScalar(string command, SqlConnection connection, SqlTransaction transaction)
        {
            object dataum = null;
            SqlCommand theCom = new SqlCommand();


            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                theCom = new SqlCommand(command, connection, transaction);

                dataum = theCom.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                try
                {
                    if (theCom.Transaction != null)
                    {
                        theCom.Transaction.Rollback();
                    }
                }
                catch (Exception InnerEx)
                {
                    throw InnerEx;
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                throw Ex;
            }

            return dataum;
        }
        public static object MSSqlExecuteBatchScalar(string command, SqlConnection connection, SqlTransaction transaction, SqlParameter[] parameters)
        {
            object dataum = null;
            SqlCommand theCom = new SqlCommand();


            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                theCom = new SqlCommand(command, connection, transaction);
                foreach (SqlParameter sqlParam in parameters)
                {
                    theCom.Parameters.Add(sqlParam);
                }

                dataum = theCom.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                try
                {
                    if (theCom.Transaction != null)
                    {
                        theCom.Transaction.Rollback();
                    }
                }
                catch (Exception InnerEx)
                {
                    throw InnerEx;
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                throw Ex;
            }

            return dataum;
        }

        public static SqlParameter MSSqlParamGenerator(object param, string name)
        {
            SqlParameter sqlParameter = new SqlParameter();

            sqlParameter.ParameterName = name;
            sqlParameter.Value = param;

            return sqlParameter;
        }
        #endregion


    }
}
