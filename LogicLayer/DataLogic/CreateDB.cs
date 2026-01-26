using CommonUtility;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LogicLayer.DataLogic
{
    public class CreateDB
    {
        public static Database GetInstance()
        {
            string str = ConfigurationManager.ConnectionStrings["Connection String"].ConnectionString;
            string desStr = Cryptography.DecryptString(str);

            Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(desStr);
            return db;
        }
    }
}
