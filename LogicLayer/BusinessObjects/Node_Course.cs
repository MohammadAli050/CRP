using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Node_Course
    {
        public int Node_CourseID { get; set; }
        public int NodeID { get; set; }
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public decimal PassingGPA { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Course Course
        {
            get
            {
                return CourseManager.GetByCourseIdVersionId(CourseID, VersionID);
            }
        }
    }
}
