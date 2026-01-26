using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentInfoCourseAssign
    {
        public int StudentID { get; set; }
        public string Roll { get; set; }
        public string FullName { get; set; }
        public decimal FirstCompCredit { get; set; }
        public decimal SecondCompCredit { get; set; }
        public decimal ThirdCompCredit { get; set; }
        
    }
}

