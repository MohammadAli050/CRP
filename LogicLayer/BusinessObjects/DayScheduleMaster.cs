using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class DayScheduleMaster
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public int SessionId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int DefaultDayId { get; set; }
        public int MakeUpDayId { get; set; }
        public int WeekNo { get; set; }
        public int Attribute1 { get; set; }
        public int Attribute2 { get; set; }
        public int Attribute3 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}

