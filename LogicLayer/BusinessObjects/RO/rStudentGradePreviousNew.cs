using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentGradePreviousNew
    {
        public decimal CAttempted { get; set; }
        public decimal CEarned { get; set; }
        public decimal GPATotal { get; set; }
        public decimal PSecuredTotal { get; set; }
        public decimal CGPA { get; set; }
        public decimal TranscriptCGPA { get; set; }
        public decimal TotalCAttemped { get; set; }
        public decimal TotalCEarned { get; set; }
        public decimal TotalGradePoint { get; set; }
        public decimal PreviousTotalGradePoint { get; set; }
        public decimal TotalPreviousGradePointxCr { get; set; }
        public decimal TotalGradePointxCr { get; set; }


    }
}
