using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class BillPaymentHistoryDTO
    {
        public int StudentId { get; set; }
        public int BillHistoryMasterId { get; set; }
        public string ReferenceNo { get; set; }
        public int BillHistoryId { get; set; }
        public string Semester { get; set; }
        public int FeeTypeId { get; set; }
        public string FeesName { get; set; }
        public decimal? BillAmount { get; set; }
        public Nullable<DateTime> BillingDate { get; set; }
        public Nullable<decimal> PaymentAmount { get; set; }
        public Nullable<DateTime> PaymentDate { get; set; }

    }
}
