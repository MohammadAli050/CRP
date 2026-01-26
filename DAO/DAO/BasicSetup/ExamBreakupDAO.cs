using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ExamBreakupDAO : BaseDAO
    {
        public static bool Save(ExamTypeNameEntity _examTypeName, List<ExamMarksAllocationEntity> _exmMarkAllocations)
        {
            try
            {
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                int examTypeNameID = ExamTypeNameDAO.GetMaxExamTypeNameID(sqlConn, sqlTran);                
                if (examTypeNameID < 1) { throw new Exception(); }

                _examTypeName.Id = examTypeNameID;
                int numOfRows = ExamTypeNameDAO.Save(_examTypeName, sqlConn, sqlTran);
                if (numOfRows < 1)  { throw new Exception(); }

                numOfRows = ExamMarksAllocationDAO.Save(_exmMarkAllocations, _examTypeName, sqlConn, sqlTran);
                if (numOfRows < 1) { throw new Exception(); }

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection(); //Close DB Connection

                return true;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                //throw ex;
                return false;
            }                         
        }

        public static bool Update(ExamTypeNameEntity _examTypeName, List<ExamMarksAllocationEntity> _exmMarkAllocations)
        {
            try
            {
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();
                                
                int numOfRows = ExamTypeNameDAO.Update(_examTypeName, sqlConn, sqlTran);
                if (numOfRows < 1) { throw new Exception(); }

                numOfRows = ExamMarksAllocationDAO.Delete(_examTypeName.Id, sqlConn, sqlTran);

                numOfRows = ExamMarksAllocationDAO.Save(_exmMarkAllocations, _examTypeName, sqlConn, sqlTran);
                if (numOfRows < 1) { throw new Exception(); }

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection(); //Close DB Connection

                return true;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                //throw ex;
                return false;
            }
        }
    }
}
