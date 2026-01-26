using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IFeeGroupMasterRepository
    {
        int Insert(FeeGroupMaster feegroupmaster);
        bool Update(FeeGroupMaster feegroupmaster);
        bool Delete(int FeeGroupMasterId);
        FeeGroupMaster GetById(int? FeeGroupMasterId);
        List<FeeGroupMaster> GetAll();
    }
}

