using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rSemesterResultSummary
    {
        public int StudentID { get; set; }
        public decimal GPA { get; set; }
        public string ShortName { get; set; }
    }
}
