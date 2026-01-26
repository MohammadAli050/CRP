using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class DeptRegSetUp
    {
        public int DeptRegSetUpID { get; set; }
        public int ProgramID { get; set; }
        public Decimal LocalCGPA1 { get; set; }
        public Decimal LocalCredit1 { get; set; }
        public Decimal LocalCGPA2 { get; set; }
        public Decimal LocalCredit2 { get; set; }
        public Decimal LocalCGPA3 { get; set; }
        public Decimal LocalCredit3 { get; set; }
        public Decimal ManCGPA1 { get; set; }
        public Decimal ManCredit1 { get; set; }
        public String ManRetakeGradeLimit1 { get; set; }
        public Decimal ManCGPA2 { get; set; }
        public Decimal ManCredit2 { get; set; }
        public String ManRetakeGradeLimit2 { get; set; }
        public Decimal ManCGPA3 { get; set; }
        public Decimal ManCredit3 { get; set; }
        public String ManRetakeGradeLimit3 { get; set; }
        public Decimal MaxCGPA1 { get; set; }
        public Decimal MaxCredit1 { get; set; }
        public Decimal MaxCGPA2 { get; set; }
        public Decimal MaxCredit2 { get; set; }
        public Decimal MaxCGPA3 { get; set; }
        public Decimal MaxCredit3 { get; set; }
        public Decimal ProjectCGPA { get; set; }
        public Decimal ProjectCredit { get; set; }
        public Decimal ThesisCGPA { get; set; }
        public Decimal ThesisCredit { get; set; }
        public Decimal MajorCGPA { get; set; }
        public Decimal MajorCredit { get; set; }
        public int ProbationLock { get; set; }
        public int CourseRetakeLimit { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Decimal AutoPreRegCGPA1 { get; set; }
        public Decimal AutoPreRegCredit1 { get; set; }
        public Decimal AutoPreRegCGPA2 { get; set; }
        public Decimal AutoPreRegCredit2 { get; set; }
        public Decimal AutoPreRegCGPA3 { get; set; }
        public Decimal AutoPreRegCredit3 { get; set; }
    }
}
