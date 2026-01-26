using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rDayScheduleDetails
    {
        public DateTime ScheduleDate { get; set; }
        public string FormalCode { get; set; }
        public string Title { get; set; }
        public int WeekNo { get; set; }
        public string SectionName { get; set; }
        public string TimeSlot { get; set; }
        public string Room { get; set; }
        public string FullName { get; set; }
        public string Topic { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public string Remarks3 { get; set; }
        public string Remarks4 { get; set; }

    }
}
