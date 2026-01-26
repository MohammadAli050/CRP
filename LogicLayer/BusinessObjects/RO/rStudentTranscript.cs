using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class rStudentTranscript
    {
        public int Year { get; set; }
        public string Code { get; set; }
        public string FormalCode { get; set; }
        public string Title { get; set; }
        public double Credits { get; set; }
        public double ObtainedGPA { get; set; }
        public string ObtainedGrade { get; set; }
        public int AcaCalID { get; set; }
        public string StudentName { get; set; }
        public string DepartmentName { get; set; }
        public string ProgrameName { get; set; }
        public string StudentId { get; set; }
        public string FatherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Major { get; set; }
        public double OCredits { get; set; }
        public double CATotal { get; set; }
        public double STCreditsTotal { get; set; }
       

    }
}
