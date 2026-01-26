using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class FeeType
    {
        public int FeeTypeId {get; set; }
		public string FeeName {get; set; }
		public string FeeDefinition {get; set; }
		public bool IsCourseSpecific{get; set; }
		public bool IsLifetimeOnce{get; set; }
		public bool IsPerSemester{get; set; }
		public int Priority {get; set; }
		public int Sequence {get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public string Attribute4 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public int ModifiedBy {get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

