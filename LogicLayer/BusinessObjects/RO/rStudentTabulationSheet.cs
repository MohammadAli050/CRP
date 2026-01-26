using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentTabulationSheet
    {
        public int BatchNO { get; set; }
        public int StudentID { get; set; }
        public string Roll { get; set; }
        public string FullName { get; set; }
        public int Seq { get; set; }
        public string SectionName { get; set; }
        public string Gender { get; set; }
        public string FormalCode { get; set; }
        public string Title { get; set; }
        public decimal CourseCredit { get; set; }
        public string ObtainedGrade { get; set; }
        public string ObtainedGPA { get; set; }
        public decimal GPA { get; set; }
        public int SemesterNo { get; set; }
        public decimal EarnedCr { get; set; }
        public decimal EnrolledCr { get; set; }
        public decimal TEarnedCr { get; set; }
        public decimal TEnrolledCr { get; set; }
        public decimal CGPA { get; set; }
        public string RegistrationNo { get; set; }
        public string RegistrationSession { get; set; }
        public string TabulationRemarks { get; set; }
        public string InstitutionName { get; set; }
        public string ExamCenterName { get; set; }

    }
}
