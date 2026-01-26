using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class CoursePredictMaster
    {
        public int Id {get; set; }
		public int AcaCalId {get; set; }
		public int ProgramId {get; set; }
		public int BatchNo {get; set; }
		public int AcaCalSessionNo {get; set; }
		public int NoOfActiveStudent {get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public int ModifiedBy {get; set; }
		public DateTime MofifiedDate{get; set; }
    }
}

