using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class rEquivalentCourse
    {
        public int ParentCourseID { get; set; }
        public string MainCourseCode { get; set; }
        public string ActualCourseName { get; set; }
        public decimal MainCourseCredits { get; set; }
        public int EquiCourseID { get; set; }
        public string PreCourseCode { get; set; }
        public string PreRequisiteCourseName { get; set; }
        public decimal PreCourseCredits { get; set; }
        public string ProgramName { get; set; }
        public string DepartmentName { get; set; }
    }
}
