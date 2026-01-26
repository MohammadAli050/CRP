using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IDailyCollection
    {
        List<rDailyCollection> GetDailyCollectionByProgramAndDate(DateTime fromDate, DateTime toDate, int programId);
        List<rDailyBillHistory> GetDailyBillHistoryByProgramAndDate(DateTime fromDate, DateTime toDate, int programId);
    }
}
