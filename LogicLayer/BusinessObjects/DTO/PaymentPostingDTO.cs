using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class PaymentPostingDTO
    {
        public int BillHistoryMasterId { get; set; }
        public int StudentId { get; set; }
        public string Roll { get; set; }
        public string Name { get; set; }
        public string ReferenceNo { get; set; }
        public decimal Amount { get; set; }
        public DateTime BillingDate { get; set; }
        public int CollectionHistoryId { get; set; }
    }
}
