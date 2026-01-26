using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IDataTableRepository
    {
        DataTable GetDataFromQuery(string spName);
        DataTable GetDataFromQuery(string SPName, List<SqlParameter> parameterList);
    }
}
