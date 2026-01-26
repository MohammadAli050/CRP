using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IBillHistoryRepository
    {
        int Insert(BillHistory billhistory);
        bool Update(BillHistory billhistory);
        bool Delete(int BillHistoryId);
        BillHistory GetById(int? BillHistoryId);
        List<BillHistory> GetAll();
        List<BillHistory> GetForBillPrintByBillHistoryMasterId(int billHistoryMasterId);
        List<PaymentPostingDTO> GetBillForPaymentPosting(int programId, int sessionId, int batchId, int studentId);
        List<BillHistory> GetByBillHistoryMasterId(int billHistorymasterId);
        List<BillPaymentHistoryDTO> GetBillPaymentHistoryByBillHistoryMasterId(int billHistoryMasterId);
        List<rStudentBillPaymentDue> GetBillPaymentDueByProgramIdBatchIdSessionId(int programId, int batchId, int sessionId);
        List<BillDeleteDTO> GetBillForDelete(int programId, int batchId, int sessionId, int studentId, DateTime? date);
        bool DeleteByBillHistoryMasterId(int billHistoryMasterId);
        List<BillPaymentHistoryMasterDTO> GetBillPaymentHistoryMasterByStudentId(int studentId);
    }
}

