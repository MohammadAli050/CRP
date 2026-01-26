using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class CalenderUnitDistribution
    {
        public int CalenderUnitDistributionID { get; set; }
        public int CalenderUnitMasterID { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
