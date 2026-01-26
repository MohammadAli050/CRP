using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IFeeTypeRepository
    {
        int Insert(FeeType feetype);
        bool Update(FeeType feetype);
        bool Delete(int FeeTypeId);
        FeeType GetById(int? FeeTypeId);
        List<FeeType> GetAll();
    }
}

