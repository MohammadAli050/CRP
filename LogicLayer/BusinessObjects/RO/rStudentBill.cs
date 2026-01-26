using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentBill
    {
        public string Roll { get; set; }
        public string FullName { get; set; }
        public decimal TotalFees { get; set; }
        public decimal Discount { get; set; }
        public decimal PreviousDue { get; set; }
        public decimal ReceivedAmount { get; set; }
        public DateTime CollectionDate { get; set; }
        public string MoneyReciptId { get; set; }
        public string PaymentType { get; set; }
       
    }
}
