using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ClassForceOperation
    {
        public int ID { get; set; }
        public string StudentID { get; set; }
        public string CourseStatus { get; set; }
        public string Grade { get; set; }
        public string StudentName { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string PreRequisiteCourseName { get; set; }
        public decimal CourseCredit { get; set; }
        public string Semester { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsAutoAssign { get; set; }
        public bool IsAutoOpen { get; set; }
        public int Priority { get; set; }
        public int SequenceNo { get; set; }
    }
}
