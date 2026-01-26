using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rDailyCollection
    {
        public string ProgramName { get; set; }
        public string Roll { get; set; }
        public string FullName { get; set; }
        public decimal Amount { get; set; }
        public string MoneyReceiptId { get; set; }
        public DateTime CollectionDate { get; set; }
        public string Semester { get; set; }
        public int PaymentType { get; set; }

        public decimal Cash
        {
            get
            {
                if (PaymentType == 1)
                {
                    return Amount;
                }
                else return 0;
            }
        }
        public decimal Bank
        {
            get
            {
                if (PaymentType == 2)
                {
                    return Amount;
                }
                else return 0;
            }
        }
    }
}
