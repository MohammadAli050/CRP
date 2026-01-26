using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rClassScheduleForFaculty
    {
        public int ValueID { get; set; }
        public string FormalCode { get; set; }
        public string Title { get; set; }
        public string SectionName { get; set; }
        public string WName { get; set; }
        public string TimeSlot { get; set; }
        public string RoomName { get; set; }
        public string Faculty { get; set; }
        public string FullName { get; set; }
    }
}
