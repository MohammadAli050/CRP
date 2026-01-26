using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class rStudentGradeDetail
    {
        public string MarksRange { get; set; }
        public string Grade { get; set; }
        public Double GradePoint { get; set; }
       
    }
}
