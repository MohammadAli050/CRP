using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class TreeDetail
    {
        public int TreeDetailID { get; set; }
        public int TreeMasterID { get; set; }
        public Nullable<int> ParentNodeID { get; set; }
        public Nullable<int> ChildNodeID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
