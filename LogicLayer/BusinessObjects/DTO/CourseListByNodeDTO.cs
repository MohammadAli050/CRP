using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class CourseListByNodeDTO
    {
        public int NodeId { get; set; }
        public int CourseId { get; set; }
        public int VersionId { get; set; }
        public int NodeCourseId { get; set; }

        public string CourseName
        {
            get
            {
                Course course = CourseManager.GetByCourseIdVersionId(CourseId, VersionId);
                return course.FormalCode + " - " + course.VersionCode + " - " + course.Title;
            }
        }

        public Course CourseInfo
        {
            get
            {
                Course crs = null;
                try
                {
                    Course course = CourseManager.GetByCourseIdVersionId(CourseId, VersionId);
                    if (course != null)
                        crs = course;
                }
                catch (Exception ex)
                {
                }
                return crs;
            }
        }
    }
}
