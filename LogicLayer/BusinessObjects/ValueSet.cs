using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ValueSet
    {
        public int ValueSetID { get; set; }
        public string ValueSetName { get; set; }
        public string ValueSetCode { get; set; }
        public string Remarks { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
