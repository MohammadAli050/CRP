using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class OpeningDue
    {
        	public int OpeningDueId {get; set; }
		public int StudentId {get; set; }
		public decimal DueAmount{get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public int ModifiedBy {get; set; }
		public DateTime ModifiedDate{get; set; }
    }
}

