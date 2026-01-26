using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;


namespace DataAccess
{
    public class Registration_DAO : Base_DAO
    {
       private const string STUDENTID_PA = "@StudentId";
       private const string MODIFIERID_PA = "@ModifierId";

        public static int InsertRequisitionData(int stuId, int modifierID)
        {
          try
            {
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();
                DAOParameters dParam = new DAOParameters();

                string command = @"DECLARE	@return_value int EXEC	@return_value = [dbo].[sp_Registration] @StuId = " + STUDENTID_PA + ", @ModifiedBy = " + MODIFIERID_PA + " SELECT	'Return Value' = @return_value";

                dParam.AddParameter(STUDENTID_PA, stuId);
                dParam.AddParameter(MODIFIERID_PA, modifierID);

                List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);
                int i = QueryHandler.ExecuteSaveBatchScalar(command, sqlParams, sqlConn, sqlTran);
                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection(); //Close DB Connection
                
              return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }        
    }
}
