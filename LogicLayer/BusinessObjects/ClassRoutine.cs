using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ClassRoutine
    {
        public int ClassRoutineID { get; set; }
        public int AcaCal_CourseID { get; set; }
        public string Section { get; set; }
        public string Capacity { get; set; }
        public int RoomInfoID { get; set; }
        public int TimeSlotPlanID { get; set; }
        public string Day { get; set; }
        public int TeacherID { get; set; }
        public int ProgramID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
