using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class StudentCourseHistoryReplica
    {
        public int ID { get; set; }
        public int StudentCourseHistoryID { get; set; }
        public int StudentID { get; set; }
        public Nullable<int> CalCourseProgNodeID { get; set; }
        public Nullable<int> AcaCalSectionID { get; set; }
        public Nullable<int> RetakeNo { get; set; }
        public Nullable<decimal> ObtainedTotalMarks { get; set; }
        public Nullable<decimal> ObtainedGPA { get; set; }
        public string ObtainedGrade { get; set; }
        public Nullable<int> GradeId { get; set; }
        public Nullable<int> CourseStatusID { get; set; }
        public Nullable<DateTime> CourseStatusDate { get; set; }
        public Nullable<int> AcaCalID { get; set; }
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public decimal CourseCredit { get; set; }
        public Nullable<decimal> CompletedCredit { get; set; }
        public Nullable<int> Node_CourseID { get; set; }
        public Nullable<int> NodeID { get; set; }
        public Nullable<bool> IsMultipleACUSpan { get; set; }
        public Nullable<bool> IsConsiderGPA { get; set; }
        public Nullable<int> CourseWavTransfrID { get; set; }
        public Nullable<int> SemesterNo { get; set; }
        public Nullable<int> YearNo { get; set; }
        public Nullable<int> EqCourseHistoryId { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public string Remark { get; set; }


        public Course Course
        {
            get
            {
                Course course = CourseManager.GetByCourseIdVersionId(CourseID, VersionID);
                return course;
            }
        }

        public string CourseStatus
        {
            get
            {
                CourseStatus courseStatus = CourseStatusManager.GetById(CourseStatusID);

                return courseStatus == null ? "" : courseStatus.Description;
            }
        }
    }
}

