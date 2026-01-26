using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class SchemeSetup
    {
        public int Id {get; set; }
        public string SchemeName { get; set; }
        public Nullable<int> FromBatch { get; set; }
		public Nullable<int> ToBatch {get; set; }
        public int Percentage100 { get; set; }
        public int Percentage50 { get; set; }
        public int Percentage25 { get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public Nullable<int> ModifiedBy {get; set; }
		public Nullable<DateTime> ModifiedDate{get; set; }
    }
}

