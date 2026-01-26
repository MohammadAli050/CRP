using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ExamTemplateCalculationType
    {
        public int ExamCalculationTypeId {get; set; }
		public string ExamCalculationTypeName {get; set; }
		public int Attribute1 {get; set; }
		public int Attribute2 {get; set; }
		public int Attribute3 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public int ModifiedBy {get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

