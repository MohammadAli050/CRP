using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class VNodeSetMaster
    {
        public int VNodeSetMasterID { get; set; }
        public int SetNo { get; set; }
        public int NodeID { get; set; }
        public decimal RequiredUnits { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
