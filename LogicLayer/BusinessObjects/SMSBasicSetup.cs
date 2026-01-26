using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class SMSBasicSetup
    {
        public int ID { get; set; }
        public int RemainingSMS { get; set; }
        public bool RegistrationStatus { get; set; }
        public bool BillCollectionStatus { get; set; }
        public bool LateFineStatus { get; set; }
        public bool WaiverPostingStatus { get; set; }
        public bool AdmitCardStatus { get; set; }
        public bool ResultPubStatus { get; set; }
        public bool ResultCorrectionStatus { get; set; }
        public bool CustomSmsStatus { get; set; }
    }
}
