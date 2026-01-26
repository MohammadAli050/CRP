using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class ExamMarkAllDTO
    {
        public int Id1 { get; set; }
        public int CourseHistoryId1 { get; set; }
        public int ExamId1 { get; set; }
        public string Mark1 { get; set; }
        public int Status1 { get; set; }
        public bool IsFinalSubmit1 { get; set; }
        public bool IsTransfer1 { get; set; }

        public int Id2 { get; set; }
        public int CourseHistoryId2 { get; set; }
        public int ExamId2 { get; set; }
        public string Mark2 { get; set; }
        public int Status2 { get; set; }
        public bool IsFinalSubmit2 { get; set; }
        public bool IsTransfer2 { get; set; }

        public int Id3 { get; set; }
        public int CourseHistoryId3 { get; set; }
        public int ExamId3 { get; set; }
        public string Mark3 { get; set; }
        public int Status3 { get; set; }
        public bool IsFinalSubmit3 { get; set; }
        public bool IsTransfer3 { get; set; }

        public int Id4 { get; set; }
        public int CourseHistoryId4 { get; set; }
        public int ExamId4 { get; set; }
        public string Mark4 { get; set; }
        public int Status4 { get; set; }
        public bool IsFinalSubmit4 { get; set; }
        public bool IsTransfer4 { get; set; }

        public int Id5 { get; set; }
        public int CourseHistoryId5 { get; set; }
        public int ExamId5 { get; set; }
        public string Mark5 { get; set; }
        public int Status5 { get; set; }
        public bool IsFinalSubmit5 { get; set; }
        public bool IsTransfer5 { get; set; }

        public int Id6 { get; set; }
        public int CourseHistoryId6 { get; set; }
        public int ExamId6 { get; set; }
        public string Mark6 { get; set; }
        public int Status6 { get; set; }
        public bool IsFinalSubmit6 { get; set; }
        public bool IsTransfer6 { get; set; }

        public int Id7 { get; set; }
        public int CourseHistoryId7 { get; set; }
        public int ExamId7 { get; set; }
        public string Mark7 { get; set; }
        public int Status7 { get; set; }
        public bool IsFinalSubmit7 { get; set; }
        public bool IsTransfer7 { get; set; }

        public string Roll { get; set; }
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public int GradeMasterId { get; set; }
        public decimal TotalMark { get; set; }
        public string Grade { get; set; }
        public decimal GradePoint { get; set; }

        public int forIGrade { get; set; }
    }
}
