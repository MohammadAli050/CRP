using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentGPACGPA
    {
        public int StdAcademicCalenderID { get; set; }
        public decimal Credit { get; set; }
        public decimal GPA { get; set; }
        public decimal CompletedCredit { get; set; }
        public decimal CGPA { get; set; }
        public string FullName { get; set; }
        public int Year { get; set; }
        public string TypeName { get; set; }
        public decimal TotalCredit { get; set; }
    }
}
