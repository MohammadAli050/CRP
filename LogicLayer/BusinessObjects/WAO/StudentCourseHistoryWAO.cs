using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.WAO
{
    [Serializable]
    public class StudentCourseHistoryWAO
    {
        public int StudentId { get; set; }
        public string Roll { get; set; }
        public string SemesterName { get; set; }
        public int SemesterNo { get; set; }
        public string FormalCode { get; set; }
        public string CourseTitle { get; set; }
        public string ObtainedGrade { get; set; }
        public Nullable<decimal> ObtainedGPA { get; set; }
    }
}
