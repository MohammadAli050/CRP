using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    public class StudentResultPublishCourseHistoryDTO
    {
        public int StudentID { get; set; }
        public int CourseHistoryId { get; set; }
        public string StudentName { get; set; }
        public string StudentRoll { get; set; }
        public decimal ObtainedTotalMarks { get; set; }
        public decimal ObtainedGPA { get; set; }
        public string ObtainedGrade { get; set; }
    }
}
