using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class UserAccessProgram
    {
        	public int ID {get; set; }
		public int User_ID {get; set; }
		public string AccessPattern {get; set; }
		public DateTime AccessStartDate{get; set; }
		public DateTime AccessEndDate{get; set; }
        public bool IsAllCourse { get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public int ModifiedBy {get; set; }
		public DateTime ModifiedDate{get; set; }
    }
}

