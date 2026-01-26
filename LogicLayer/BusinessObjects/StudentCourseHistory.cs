using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentCourseHistory
    {
        public int ID { get; set; }
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

        public int ProgramId
        {
            get
            {
                return StudentManager.GetById(StudentID).ProgramID;
            }
        }

        public string FormalCode
        {
            get
            {
                Course course = CourseManager.GetByCourseIdVersionId(CourseID, VersionID);
                return course == null ? "--" : course.FormalCode;
            }
        }

        public string CourseTitle
        {
            get
            {
                Course course = CourseManager.GetByCourseIdVersionId(CourseID, VersionID);
                return course == null ? "--" : course.Title;
            }
        }

        public Course Course
        {
            get
            {
                Course course = CourseManager.GetByCourseIdVersionId(CourseID, VersionID);
                return course;
            }
        }

        public CourseStatus CourseStatus
        {
            get
            {
                CourseStatus courseStatus = CourseStatusManager.GetById(CourseStatusID);
                return courseStatus;
            }
        }

        public string SessionFullCode
        {
            get
            {
                AcademicCalender acaCal = AcademicCalenderManager.GetById(AcaCalID);
                return acaCal == null ? "--" : acaCal.FullCode;
            }
        }

        public AcademicCalender Session
        {
            get
            {
                AcademicCalender acaCal = AcademicCalenderManager.GetById(AcaCalID);
                return acaCal;
            }
        }

        public Student StudentInfo
        {
            get
            {
                Student student = StudentManager.GetById(StudentID);
                return student;
            }
        }

        public string Roll
        {
            get
            {
                Student student = StudentManager.GetById(StudentID);
                return student.Roll;
            }
        }

        public string FullName
        {
            get
            {
                Student student = StudentManager.GetById(StudentID);
                Person person = PersonManager.GetById(student.PersonID);
                return person.FullName;
            }
        }

        public AcademicCalenderSection Section
        {
            get
            {
                AcademicCalenderSection section = AcademicCalenderSectionManager.GetById(Convert.ToInt32(AcaCalSectionID));
                return section;
            }
        }

        public string SectionName
        {
            get
            {
                try
                {
                    AcademicCalenderSection section = AcademicCalenderSectionManager.GetById(Convert.ToInt32(AcaCalSectionID));
                    return section.SectionName;
                }
                catch { return ""; }
            }
        }

        public RegistrationWorksheet RegistrationWorksheet
        {
            get
            {
                RegistrationWorksheet rw = RegistrationWorksheetManager.GetByStudentIdCourseIdVersionId(StudentID, CourseID, VersionID);
                return rw;
            }
        }

        public bool IsRetakeTaken
        {
            get
            {
                if (RegistrationWorksheet != null && RegistrationWorksheet.IsAutoOpen == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsForward
        {
            get
            {
                ForwardCourses fc = ForwardCoursesManager.GetByCourseHistoryId(ID);

                if (fc != null && fc.IsActive == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsCourseRegistered
        {
            get
            {
                if (RegistrationWorksheet != null && RegistrationWorksheet.IsRegistered == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }        

        #region Custom Progerty(Not Set)
        public string Semester { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        // public string CourseStatus { get; set; }
        public string StudentRoll { get; set; }
        public string StudentName { get; set; }
        public string RegCredit { get; set; }
        public string LastCGPA { get; set; }
        public string RetakeStatus { get; set; }
        #endregion
    }
}