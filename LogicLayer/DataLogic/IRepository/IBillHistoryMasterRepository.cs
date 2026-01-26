using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IBillHistoryMasterRepository
    {
        int Insert(BillHistoryMaster billhistorymaster);
        bool Update(BillHistoryMaster billhistorymaster);
        bool Delete(int BillHistoryMasterId);
        BillHistoryMaster GetById(int? BillHistoryMasterId);
        List<BillHistoryMaster> GetAll();
        string GetBillMasterMaxReferenceNo(DateTime date);
        BillHistoryMaster GetByStudentIdAcaCalIdFees(int studentId, int acaCalId, decimal fees, bool isDue);
        List<BillHistoryMaster> GetBillDueCountByStudentIdAcaCalId(int studentId, int acaCalId);
    }
}

