using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStdDiscountHistoryRepository
    {
        int Insert(StdDiscountHistory stdDiscountHistory);
        bool Update(StdDiscountHistory stdDiscountHistory);
        bool Delete(int id);
        StdDiscountHistory GetById(int? id);
        List<StdDiscountHistory> GetAll();
    }
}
