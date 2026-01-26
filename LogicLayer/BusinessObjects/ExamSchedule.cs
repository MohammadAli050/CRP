using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ExamSchedule
    {
        public int Id {get; set; }
        public int AcaCalId { get; set; }
		public int ExamSetId {get; set; }
		public int ProgramId {get; set; }
		public int CourseId {get; set; }
		public int VersionId {get; set; }
		public int DayId {get; set; }
		public int TimeSlotId {get; set; }
        public string RowFlag { get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public Nullable<int> ModifiedBy {get; set; }
		public Nullable<DateTime> ModifiedDate{get; set; }

        #region Property Not Set
        public string ProgramName { get; set; }
        public string CourseInfo { get; set; }
        public string Day { get; set; }
        public string TimeSlot { get; set; }
        public string SectionList { get; set; }
        public string StudentNo { get; set; }
        public int totalMale { get; set; }
        public int totalFemale { get; set; }
        #endregion
    }
}

