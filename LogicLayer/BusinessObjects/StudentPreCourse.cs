using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System.Globalization;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentPreCourse
    {
        public int StudentPreCourseId { get; set; }
        public int StudentId { get; set; }
        public int PreCourseId { get; set; }
        public int PreVersionId { get; set; }
        public int PreNodeCourseId { get; set; }
        public int MainCourseId { get; set; }
        public int MainVersionId { get; set; }
        public int ManiNodeCourseId { get; set; }
        public bool IsBundle { get; set; }
        public string Remarks { get; set; }
        public bool IsBool { get; set; }
        public int Number { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        #region Custom Property
        public String PreCourse 
        {
            get 
            {
                Course course = CourseManager.GetByCourseIdVersionId(PreCourseId, PreVersionId);

                if (course != null)
                    return course.FormalCode + " - " + course.Title;
                else
                    return "Not Found.";
            }
        }
        public String MainCourse 
        {
            get
            {
                Course course = CourseManager.GetByCourseIdVersionId(MainCourseId, MainVersionId);

                if (course != null)
                    return course.FormalCode + " - " + course.Title;
                else
                    return "Not Found.";
            }
        }

        public string StudentRoll
        {
            get
            {
                Student student = StudentManager.GetById(StudentId);
                if (student != null)
                    return student.Roll;
                else
                    return "";
            }
        }

        public string StudentName
        {
            get
            {
                Student student = StudentManager.GetById(StudentId);
                if (student != null)
                {
                    Person person = PersonManager.GetById(student.PersonID);
                    if (person != null)
                        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(person.FullName.ToString().ToLower());
                    else
                        return "";
                }
                else
                    return "";
            }
        }
        #endregion
    }
}
