using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ExamScheduleRoomInfo
    {
        public int Id {get; set; }
		public int AcaCalId {get; set; }
		public int ExamScheduleSetId {get; set; }
		public int DayId {get; set; }
		public int TimeSlotId {get; set; }
		public int RoomInfoId {get; set; }
		public string GenderType {get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public Nullable<int> ModifiedBy {get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}