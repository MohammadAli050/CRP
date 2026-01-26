using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class AcademicCalenderExamScheduler
    {
        public int AcademicCalenderExamSchedulerID { get; set; }
        public int AcaCal_SectionID { get; set; }
        public int RoomInfoOneID { get; set; }
        public int DayOne { get; set; }
        public int TimeSlotPlanOneID { get; set; }
        public int TeacherOneID { get; set; }
        public int TeacherTwoID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public int TypeDefinitionID { get; set; }
        public int Occupied { get; set; }
        public DateTime Date { get; set; }
    }
}
