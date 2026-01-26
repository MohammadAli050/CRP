using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{

    public class InvigilationSchedule
    {
        public int Serial { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Slot { get; set; }
        public string RoomName { get; set; }
        public DateTime Date { get; set; }
        public string DayName { get; set; }
        public int SlotNo { get; set; }
    }
}
