using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentClassRoutine
    {
        public string CourseTitle { get; set; }
        public string FormalCode { get; set; }
        public string Section { get; set; }
        public string Day { get; set; }
        public string Room { get; set; }
        public string Time { get; set; }
    }
}
