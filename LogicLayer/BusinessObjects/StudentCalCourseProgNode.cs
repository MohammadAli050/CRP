using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentCalCourseProgNode
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public int CalCourseProgNodeID { get; set; }
        public bool IsCompleted { get; set; }
        public int OriginalCalID { get; set; }
        public bool IsAutoAssign { get; set; }
        public bool IsAutoOpen { get; set; }
        public bool Isrequisitioned { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsManualOpen { get; set; }
        public int TreeCalendarDetailID { get; set; }
        public int TreeCalendarMasterID { get; set; }
        public int TreeMasterID { get; set; }
        public string CalendarMasterName { get; set; }
        public string CalendarDetailName { get; set; }
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public decimal Credits { get; set; }
        public int Node_CourseID { get; set; }
        public int NodeID { get; set; }
        public bool IsMajorRelated { get; set; }
        public string NodeLinkName { get; set; }
        public string FormalCode { get; set; }
        public string VersionCode { get; set; }
        public string CourseTitle { get; set; }
        public int AcaCal_SectionID { get; set; }
        public string SectionName { get; set; }
        public int ProgramID { get; set; }
        public int DeptID { get; set; }
        public int Priority { get; set; }
        public int RetakeNo { get; set; }
        public decimal ObtainedGPA { get; set; }
        public string ObtainedGrade { get; set; }
        public int AcademicCalenderID { get; set; }
        public int AcaCalYear { get; set; }
        public string BatchCode { get; set; }
        public string AcaCalTypeName { get; set; }
        public decimal CalCrsProgNdCredits { get; set; }
        public bool CalCrsProgNdIsMajorRelated { get; set; }
        public bool IsMultipleACUSpan { get; set; }
        public decimal CompletedCredit { get; set; }
        public decimal CourseCredit { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
