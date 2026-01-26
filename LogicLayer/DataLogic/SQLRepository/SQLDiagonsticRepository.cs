using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLDiagonsticRepository : IDiagonsticRepository
    {
        Database db = null;
        public DataTable GetData(int queryNo, int acaCalId)
        {
            DataTable dt = new DataTable();
            //Database objDB = DatabaseFactory.CreateDatabase();
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("DiagonosticByAcaCalId");
                db.AddInParameter(cmd, "@AcaCalId", DbType.Int32, acaCalId);
                db.AddInParameter(cmd, "@QueryNo", DbType.Int32, queryNo);
                dt = db.ExecuteDataSet(cmd).Tables[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
    }
}
