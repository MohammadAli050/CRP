using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rAccountStatement
    {
        public int ID { get; set; }
        public string Roll { get; set; }
        public string FullName { get; set; }
        public string DetailedName { get; set; }
        public int Year { get; set; }
        public string TypeName { get; set; }
        public string Session { get; set; }
        public string Particulars { get; set; }
        public DateTime BillingDate { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal AdvancePay { get; set; }
        public decimal ConcessionAmount { get; set; }
        public string MoneyReciptId { get; set; }
        public decimal ReceivedAmmount { get; set; }
        public decimal PreviousDue { get; set; }
        public decimal FinalCalculation { get; set; }

    }
}
