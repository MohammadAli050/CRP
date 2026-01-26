using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentMeritListForScholarship
    {
        public string Roll { get; set; }
        public string FullName { get; set; }
        public int BatchNO { get; set; }
        public decimal Credit { get; set; }
        public decimal CGPA { get; set; }
        public decimal GPA { get; set; }
        public decimal TranscriptCredit { get; set; }
        public decimal TranscriptCGPA { get; set; }
        public decimal TranscriptGPA { get; set; }
      
    }
}
