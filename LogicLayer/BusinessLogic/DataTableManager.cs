using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessLogic
{

    public class DataTableManager
    {
        public static DataTable GetDataFromQuery(string spName)
        {
            DataTable table = RepositoryManager.DataTable_Repository.GetDataFromQuery(spName);
            return table;
        }

        public static DataTable GetDataFromQuery(string spName, List<SqlParameter> parameterList)
        {
            DataTable table = RepositoryManager.DataTable_Repository.GetDataFromQuery(spName, parameterList);
            return table;
        }
    }
}
