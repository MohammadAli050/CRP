using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rOfferedCourseCount
    {
        public string Program { get; set; }
        public string BatchNO { get; set; }
        public string FormalCode { get; set; }
        public string CourseTitle { get; set; }
        public decimal Credits { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int Total { get; set; }
    }
}
