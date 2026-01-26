using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StdDiscountHistory
    {
        public int ID { get; set; }
        public int AdmissionID { get; set; }
        public int TypeDefID { get; set; }
        public decimal TypePercentage { get; set; }
        public int EffectiveAcaCalID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
