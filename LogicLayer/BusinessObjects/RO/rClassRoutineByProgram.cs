using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rClassRoutineByProgram
    {
        public int ProgramID { get; set; }
        public string DetailedName { get; set; }
        public string ShortName { get; set; }
        public int AcademicCalenderID { get; set; }
        public string TypeName { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public string WName { get; set; }
        public string RoomName { get; set; }
        public string TimeSlot { get; set; }
        public int Time { get; set; }
        public int Day { get; set; }
        public string SectionName { get; set; }
        public string FormalCode { get; set; }
        public int Room { get; set; }
        public string Code { get; set; }
        
        
    }
}
