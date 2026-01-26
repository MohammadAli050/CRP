using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStdDiscountCurrentRepository
    {
        int Insert(StdDiscountCurrent stdDiscountCurrent);
        bool Update(StdDiscountCurrent stdDiscountCurrent);
        bool Delete(int id);
        StdDiscountCurrent GetById(int? id);
        List<StdDiscountCurrent> GetAll();
    }
}
