using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ExamMarkEquationColumnOrder
    {
        public int Id {get; set; }
		public int TemplateItemId {get; set; }
		public int ColumnSequence {get; set; }
		public int SumColumnNo {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public int ModifiedBy {get; set; }
		public DateTime ModifiedDate{get; set; }
    }
}

