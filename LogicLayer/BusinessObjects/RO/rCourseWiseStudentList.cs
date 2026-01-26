using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rCourseWiseStudentList
    {
        public string SectionName { get; set; }
        public int StudentID { get; set; }
        public decimal Credits { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public int Year { get; set; }
        public string TypeName { get; set; }
        public int CourseID { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int Total { get; set; }
        public int CourseStatusID { get; set; }
    }
}
