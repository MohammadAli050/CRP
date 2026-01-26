using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IFundTypeRepository
    {
        int Insert(FundType fundtype);
        bool Update(FundType fundtype);
        bool Delete(int FundTypeId);
        FundType GetById(int? FundTypeId);
        List<FundType> GetAll();
    }
}

