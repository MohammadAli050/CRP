using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class VNodeSet
    {
        public int VNodeSetID { get; set; }
        public int VNodeSetMasterID { get; set; }
        public int NodeID { get; set; }
        public int SetNo { get; set; }
        public int OperandNodeID { get; set; }
        public int OperandCourseID { get; set; }
        public int OperandVersionID { get; set; }
        public int NodeCourseID { get; set; }
        public int OperatorID { get; set; }
        public string WildCard { get; set; }
        public bool IsStudntSpec { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
