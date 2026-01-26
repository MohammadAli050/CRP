using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLTransferStudentRepository : ITransferStudentRepository
    {
        Database db = null;

        private string sqlTransfer = "TransferFromCandidateToStudent";

        public int Transfer(string batchCode)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlTransfer);

                db = addParam(db, cmd, batchCode, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ReturnValue");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }
            }
            catch (Exception ex)
            {
                id = 0;
            }

            return id;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, string batchCode, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ReturnValue", DbType.Int32, Int32.MaxValue);
            }

            db.AddInParameter(cmd, "BatchCode", DbType.String, batchCode);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, 1);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, DateTime.Now);

            return db;
        }
        #endregion
    }
}
