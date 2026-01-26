using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable] 
    public class PrerequisiteMasterV2
    {
        public int PreRequisiteMasterId {get; set; }
		public int ProgramId {get; set; }
		public int NodeId {get; set; }
		public int CourseId {get; set; }
		public int VersionId {get; set; }
		public string Remark {get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public int ModifiedBy {get; set; }
		public DateTime ModifiedDate{get; set; }
    }
}

