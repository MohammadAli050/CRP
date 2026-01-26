using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentGradePrevious
    {
        public decimal CAttempted { get; set; }
        public decimal CEarned { get; set; }
        public decimal GPATotal { get; set; }
        public decimal PSecuredTotal { get; set; }
        public decimal CGPA { get; set; }
        public decimal TranscriptCGPA { get; set; }
       
    }
}
