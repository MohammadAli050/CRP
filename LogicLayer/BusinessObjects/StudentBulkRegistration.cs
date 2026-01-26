using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentBulkRegistration
    {
        public int StudentID { get; set; }
        public string Roll { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string CourseWithSection { get; set; }
        public string RegisteredCourseSection { get; set; }

        public string CourseAndSection
        {
            get
            {
                if (!string.IsNullOrEmpty(CourseWithSection))
                {
                    string course = CourseWithSection.Replace(",", "<br/>");
                    return course;
                }
                else return "";
            }
        }
        public string RegCourseAndSection
        {
            get
            {
                if(!string.IsNullOrEmpty(RegisteredCourseSection))
                {
                    string course = RegisteredCourseSection.Replace(",", "<br/>");
                    return course;
                }
                else
                    return "";
            }
        }
    }
}

