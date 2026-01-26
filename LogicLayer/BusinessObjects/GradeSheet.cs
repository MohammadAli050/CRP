using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class GradeSheet
    {
        public int GradeSheetId { get; set; }
        public int ExamMarksAllocationID { get; set; }
        public int ProgramID { get; set; }
        public int AcademicCalenderID { get; set; }
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public int StudentID { get; set; }
        public int AcaCal_SectionID { get; set; }
        public int TeacherID { get; set; }
        public decimal ObtainMarks { get; set; }
        public string ObtainGrade { get; set; }
        public int GradeId { get; set; }
        public bool IsFinalSubmit { get; set; }
        public bool IsTransfer { get; set; }
        public bool IsConflictWithRetake { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        #region Custom Property Not Set
        public string StudentRoll { get; set; }
        public string StudentName { get; set; }

        public bool IsCurrentGrade { get; set; }
        public bool IsPreviousGrade { get; set; }
        public int CourseHistoryId { get; set; }
        public string CourseHistoryGrade { get; set; }
        public string PreviousRecord { get; set; }
        #endregion
    }
}
