using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class SectionDTO
    {
        public int AcaCalSectionID { get; set; }
        public string SectionName { get; set; }
        public string TimeSlot1 { get; set; }
        public string DayOne { get; set; }
        public string TimeSlot2 { get; set; }
        public string DayTwo { get; set; }
        public string Faculty1 { get; set; }
        public string Faculty2 { get; set; }
        public string RoomNo1 { get; set; }
        public string RoomNo2 { get; set; }
        public int Capacity { get; set; }
        public int Occupied { get; set; }
        public int SectionGender { get; set; }
    }
}
