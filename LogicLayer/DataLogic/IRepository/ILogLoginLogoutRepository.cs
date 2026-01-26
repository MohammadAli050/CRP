using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ILogLoginLogoutRepository
    {
        int Insert(LogLoginLogout logloginlogout);        
        List<LogLoginLogout> GetAll();
        List<LogLoginLogout> GetByDateRange(DateTime fromDate,DateTime toDate);
    }
}

