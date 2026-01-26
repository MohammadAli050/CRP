using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    public class BillPaymentHistoryMasterDTO
    {
        public int BillHistoryMasterId { get; set; }
        public string Roll { get; set; }
        public int StudentId { get; set; }
        public string Code { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime CollectionDate { get; set; }
        public decimal BillAmount { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
