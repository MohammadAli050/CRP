using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ContactDetails
    {
        public int PersonID { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string PhoneResidential { get; set; }
        public string PhoneOffice { get; set; }
        public string PhoneEmergency { get; set; }
        public string EmailPersonal { get; set; }
        public string EmailOther { get; set; }
        public string EmailOffice { get; set; }
    }
}
