using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentClassExamSum
    {
        public int StudentId { get; set; }
        public string FormalCode { get; set; }
        public string Title { get; set; }
        public decimal Credits { get; set; } 
        public string Semester { get; set; } 
        
    }
}
