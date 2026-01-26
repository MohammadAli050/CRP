using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class CalCourseProgNode
    {
        public int CalCorProgNodeID { get; set; }
        public int OfferedinTrimesterID { get; set; }
        public int TreeCalendarDetailID { get; set; }
        public int OfferedByProgramID { get; set; }
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public int Node_CourseID { get; set; }
        public int? NodeID { get; set; }
        public string NodeLinkName { get; set; }
        public int Priority { get; set; }
        public decimal Credits { get; set; }
        public bool IsMajorRelated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public int? PostMajorLevel { get; set; }
        public int? TopNodeId { get; set; }

        #region Custom Property
        public Course CalCourseProgNodeCourse
        {
            get
            {
                Course course = new Course();

                if (CourseID > 0 && VersionID > 0)
                {
                    course = CourseManager.GetByCourseIdVersionId(CourseID, VersionID);
                }

                return course;
            }
        }

        public Node CalCourseProgNodeNode
        {
            get
            {
                Node node = new Node();

                if (NodeID > 0)
                {
                    node = NodeManager.GetById(NodeID);
                }

                return node;
            }
        }
        #endregion
    }
}
