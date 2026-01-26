using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IFeeGroupDetailRepository
    {
        int Insert(FeeGroupDetail feegroupdetail);
        bool Update(FeeGroupDetail feegroupdetail);
        bool Delete(int FeeGroupDetailId);
        FeeGroupDetail GetById(int? FeeGroupDetailId);
        List<FeeGroupDetail> GetAll();
        List<FeeGroupDetail> GetByFeeGroupMasterId(int feeGroupMasterId);
    }
}

