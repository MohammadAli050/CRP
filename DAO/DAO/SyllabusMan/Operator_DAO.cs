using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;
namespace DataAccess
{
    public class Operator_DAO : Base_DAO
    {
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

        #region Methods
        private static OperatorEntity operatorMapper(SQLNullHandler nullHandler)
        {
            OperatorEntity opertor = new OperatorEntity();

            opertor.OperatorID = nullHandler.GetInt32("OperatorID");
            opertor.Name = nullHandler.GetString("Name");

            return opertor;
        }
        private static List<OperatorEntity> mapOperators(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<OperatorEntity> operators = null;

            while (theReader.Read())
            {
                if (operators == null)
                {
                    operators = new List<OperatorEntity>();
                }
                OperatorEntity opertor = operatorMapper(nullHandler);
                operators.Add(opertor);
            }

            return operators;
        }
        private static OperatorEntity mapOperator(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            OperatorEntity opertor = null;
            if (theReader.Read())
            {
                opertor = new OperatorEntity();
                opertor = operatorMapper(nullHandler);
            }

            return opertor;
        }
        internal static OperatorEntity GetOperator(int operatorID)
        {
            string cmd = SELECT
                            + "WHERE OperatorID = @OperatorID";

            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter("@OperatorID", operatorID);
            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

            SqlDataReader theReader = QueryHandler.ExecuteSelectQuery(cmd, ps);

            OperatorEntity opertor = mapOperator(theReader);
            theReader.Close();

            return opertor;
        }
        #endregion
    }
}
