using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentProbationDTO
    {
        public int StudentID { get; set; }
        public string Roll { get; set; }
        public string Name { get; set; }
        public int ProgramID { get; set; }
        public int AdmissionCalenderID { get; set; }
        public string BatchCode { get; set; }
        public string Remarks { get; set; }
        public decimal GPA { get; set; }
        public decimal CGPA { get; set; }
        public int ProbationCount { get; set; }        
        public bool IsBlock { get; set; }         
    }
}
