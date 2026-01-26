using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class TimeSlotPlanNew
    {
        public int TimeSlotPlanID { get; set; }
        public int StartHour { get; set; }
        public int StartMin { get; set; }
        public int StartAMPM { get; set; }
        public int EndHour { get; set; }
        public int EndMin { get; set; } 
        public int EndAMPM { get; set; }
        public int Type { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
