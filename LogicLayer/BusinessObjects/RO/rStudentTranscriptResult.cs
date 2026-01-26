using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class rStudentTranscriptResult
    {
        public string Roll { get; set; }
        public string SemesterNo { get; set; }
        public string SemesterName { get; set; }
        public string FormalCode { get; set; }
        public string Title { get; set; }
        public string CourseCredit { get; set; }
        public string ObtainedGrade { get; set; }
        public string ObtainedGPA { get; set; }
        public decimal Enrolled { get; set; }
        public decimal Earned { get; set; }
        public decimal GPA { get; set; }
        public string Remarks { get; set; }
        public string CourseSession { get; set; }

    }
}
