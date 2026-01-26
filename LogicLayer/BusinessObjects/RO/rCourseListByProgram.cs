using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class rCourseListByProgram
    {

        public int ParentNodeID { get; set; }
        public int ChildNodeID { get; set; }
        public int NodeID { get; set; }
        public string Name { get; set; }
        public int Node_CourseID { get; set; }
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public string FormalCode { get; set; }
        public string VersionCode { get; set; }
        public string Title { get; set; }
        
    }
}
