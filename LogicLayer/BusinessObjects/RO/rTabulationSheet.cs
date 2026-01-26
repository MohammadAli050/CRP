using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rTabulationSheet
    {
        public string Roll { get; set; }
        public string FullName { get; set; }
        public string Major { get; set; }
        public double TCR { get; set; }
        public double TCE { get; set; }
        public double CGPA { get; set; }
        public string FormalCode { get; set; }
        public string ObtainedGrade { get; set; }
        public double ObtainedGPA { get; set; }
        public double CourseCredit { get; set; }
        public double PS { get; set; }
        public string YS { get; set; }
        public decimal TranscriptCGPA { get; set; }
    }
}
