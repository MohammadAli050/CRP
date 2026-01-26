using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class PrerequisiteDetail
    {
        public int PrerequisiteID { get; set; }
        public int PrerequisiteMasterID { get; set; }
        public int NodeCourseID { get; set; }
        public int PreReqNodeCourseID { get; set; }
        public int OperatorID { get; set; }
        public int OperatorIDMinOccurance { get; set; }
        public decimal ReqCredits { get; set; }
        public int NodeID { get; set; }
        public int PreReqNodeID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        public Node_Course NodeCourse
        {
            get
            {
                Node_Course node_Course = Node_CourseManager.GetById(NodeCourseID);
                return node_Course;
            }
        }

        public Node_Course PreReqNodeCourse {
            get
            {
                Node_Course node_Course = Node_CourseManager.GetById(PreReqNodeCourseID);
                return node_Course;
            }
        }

        public Node Node
        {
            get
            {
                Node node = NodeManager.GetById(NodeID);
                return node;
            }
        }
    }
}
