using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentRoadmap
    {
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public int NodeID { get; set; }
        public string NodeName { get; set; }
        public int Priority { get; set; }
        public string Grade { get; set; }
        public string SemesterName { get; set; }
        public string FormalCode { get; set; }
        public string CourseTitle { get; set; }
        public decimal CreditHr { get; set; }
        public int SemesterID { get; set; }

        //public string NodeName { get; set; }
        //public string FormalCode { get; set; }
        //public string Title { get; set; }
        //public int Credits { get; set; }
        //public decimal ObtainedGPA { get; set; }
        //public string ObtainedGrade { get; set; }
        //public string CourseStatus { get; set; }
        //public string SemOrTri { get; set; }
        //public string STName { get; set; }
        //public int Sequence { get; set; }
    }
}
