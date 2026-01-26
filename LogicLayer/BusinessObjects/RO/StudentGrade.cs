using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class StudentGrade
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public string SemesterName { get; set; }
        public string FormalCode { get; set; }
        public string Title { get; set; }
        public decimal Credits { get; set; }
        public string ObtainedGrade { get; set; }
        public int GradeId { get; set; }
        public decimal TranscriptCredit { get; set; }
        public decimal ObtainedGPA { get; set; }
        public decimal TranscriptGPA { get; set; }
        public decimal EarnedCredit { get; set; }           
        
    }
}
