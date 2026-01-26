using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ExamScheduleTimeSlot
    {
		public int Id {get; set; }
		public int ExamScheduleSetId {get; set; }
		public int TimeSlotNo {get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public Nullable<int> ModifiedBy {get; set; }
		public Nullable<DateTime> ModifiedDate{get; set; }

        public string ExamScheduleSetName { get; set; }
    }
}

