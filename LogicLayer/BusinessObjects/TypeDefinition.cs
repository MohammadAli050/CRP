using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class TypeDefinition
    {
        public int TypeDefinitionID { get; set; }
        public string Type { get; set; }
        public string Definition { get; set; }
        public int AccountsID { get; set; }
        public Nullable<bool> IsCourseSpecific { get; set; }
        public Nullable<bool> IsLifetimeOnce { get; set; }
        public Nullable<bool> IsPerAcaCal { get; set; }
        public int Priority { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
