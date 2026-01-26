using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using System.Data.SqlClient;

namespace BussinessObject
{
    public class Registration_BAO
    {
        public static int InsertRequisitionData(int stuId, int modifierID)
        {
            return Registration_DAO.InsertRequisitionData(stuId, modifierID);
        }


        // implement DAO here, cause of framework conflict
        public static int GenerateBill(List<Student> students)         
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                foreach (Student obj in students)
                {
                    string command = " DECLARE	@return_value int EXEC	@return_value = [dbo].[usp_RegistrationBillFinal] @StdID = "+obj.Id+", @progID = "+obj.ProgramID+", @CreatorID = "+obj.ModifierID+" SELECT	'Return Value' = @return_value ";

                    counter += (Int32)DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);
                }

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception Ex)
            {
                if (MSSqlConnectionHandler.IsATransactionActive())
                {
                    MSSqlConnectionHandler.RollBackAndClose();
                }
                throw Ex;
            }
        }
    }
}
