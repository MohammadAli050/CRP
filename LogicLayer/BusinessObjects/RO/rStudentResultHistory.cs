using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentResultHistory
    {
        public int BatchCode { get; set; }
        public string Course { get; set; }
        public string CourseName { get; set; }
        public decimal Credit { get; set; }
        public decimal GPA { get; set; }
        public bool IsConsiderGPA { get; set; }
        public string Grade { get; set; }
        
    }
}
