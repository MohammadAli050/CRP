using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IDiscountContinuationSetupRepository
    {
        int Insert(DiscountContinuationSetup discountContinuationSetup);
        bool Update(DiscountContinuationSetup discountContinuationSetup);
        bool Delete(int id);
        DiscountContinuationSetup GetById(int id);
        List<DiscountContinuationSetup> GetAll();
        List<DiscountContinuationSetup> GetAll(int batchId, int programId);
    }
}
