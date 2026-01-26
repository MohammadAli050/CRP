using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class Operator
    {
        #region Variables
        private int _operatorID;
        private string _name;
        #endregion

        #region Constants
        private const string SELECT = "SELECT "
                            + "[OperatorID], "
                            + "[Name] "
                            + "FROM [Operator] ";

        private const string INSERT = "INSERT INTO [Operator] "
                             + "[Name] "
                             + "VALUES "
                             + "(@Name) ";

        private const string DELETE = "DELETE FROM [Operator] ";
        #endregion

        #region Constructor
        public Operator():base()
        {
            _name = string.Empty;
        }
        #endregion

        #region Properties
        public int OperatorID
        {
            get { return _operatorID; }
            set { _operatorID = value; }
        }
        private SqlParameter OperatorIDParam
        {
            get
            {
                SqlParameter ownerNodeIDParam = new SqlParameter();
                ownerNodeIDParam.ParameterName = "@OperatorID";
                ownerNodeIDParam.Value = _operatorID;
                return ownerNodeIDParam;
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
                nameParam.ParameterName = "@Name";
                nameParam.Value = _name;
                return nameParam;
            }
        }
        #endregion

        #region Methods
        private static Operator operatorMapper(SQLNullHandler nullHandler)
        {
            Operator opertor = new Operator();

            opertor.OperatorID = nullHandler.GetInt32("OperatorID");
            opertor.Name = nullHandler.GetString("Name");

            return opertor;
        }
        private static List<Operator> mapOperators(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Operator> operators = null;

            while (theReader.Read())
            {
                if (operators == null)
                {
                    operators = new List<Operator>();
                }
                Operator opertor = operatorMapper(nullHandler);
                operators.Add(opertor);
            }

            return operators;
        }
        private static Operator mapOperator(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Operator opertor = null;
            if (theReader.Read())
            {
                opertor = new Operator();
                opertor = operatorMapper(nullHandler);
            }

            return opertor;
        }

        public static List<Operator> GetOperators()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<Operator> operators = mapOperators(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return operators;
        }

        public static Operator GetOperator(int operatorID)
        {
            string command = SELECT
                            + "WHERE OperatorID = @OperatorID";

            SqlParameter ownerNodeIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(operatorID, "@OperatorID");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { ownerNodeIDParam });

            Operator opertor = mapOperator(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return opertor;
        }
        #endregion
    }
}
