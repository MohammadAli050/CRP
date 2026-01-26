using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStdAdmissionDiscountRepository
    {
        int Insert(StdAdmissionDiscount stdAdmissionDiscount);
        bool Update(StdAdmissionDiscount stdAdmissionDiscount);
        bool Delete(int id);
        StdAdmissionDiscount GetById(int? id);
        List<StdAdmissionDiscount> GetAll();
    }
}
