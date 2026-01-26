using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class TranscriptResultDetails
    {
        public string Trimesters { get; set; }
        public string CourseTitles { get; set; }
        public double GPA { get; set; }
        public double ObtainedGPA { get; set; }
        public double CGPA { get; set; }
        public string Grade { get; set; }
        public double Credit { get; set; }
        public string CourseCode { get; set; }
        public int TriID { get; set; }
    }
}
