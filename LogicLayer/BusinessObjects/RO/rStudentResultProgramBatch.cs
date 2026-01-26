using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentResultProgramBatch
    {
        public string Roll { get; set; }
        public string FullName { get; set; } 
        public decimal GPA { get; set; } 
        public decimal CGPA { get; set; } 
    }
}
