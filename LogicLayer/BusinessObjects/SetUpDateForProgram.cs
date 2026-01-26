using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class SetUpDateForProgram
    {
        public int Id {get; set; }
		public int ActivityTypeId {get; set; }
        public string TypeName { get; set; }

		public Nullable<int> AcaCalId {get; set; }
        public string AcaCalName { get; set; }

		public int ProgramId {get; set; }
        public string ProgramName { get; set; }

		public Nullable<bool> IsLock{get; set; }
        public DateTime StartDate { get; set; }
        public Nullable<DateTime> StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public Nullable<DateTime> EndTime { get; set; }		
		public bool IsActive{get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public Nullable<int> ModifiedBy {get; set; }
		public Nullable<DateTime> ModifiedDate{get; set; }
    }
}

