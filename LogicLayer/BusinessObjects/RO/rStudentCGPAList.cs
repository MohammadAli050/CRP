using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentCGPAList
    {
        public int Semester { get; set; }
        public decimal Credit { get; set; }
        public decimal GPA { get; set; }
        public decimal CGPA { get; set; }
    }
}
