using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class AccountHeads
    {
        public int AccountsID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int ParentID { get; set; }
        public string Tag { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsLeaf { get; set; }
        public Nullable<bool> SysMandatory { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
