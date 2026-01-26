using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ILogSMSRepository
    {
        int Insert(LogSMS logsms);
        List<LogSMS> GetAll();
        List<LogSMS> GetByDateRange(DateTime fromDate, DateTime toDate);
    }
}
