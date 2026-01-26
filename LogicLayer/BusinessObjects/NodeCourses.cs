using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class NodeCourses
    {
        public int CourseID
        {
            get;
            set;
        }
        public int VersionID
        {
            get;
            set;
        }
        public int Node_CourseID
        {
            get;
            set;
        }
        public string FormalCode
        {
            get;
            set;
        }
        public string VersionCode
        {
            get;
            set;
        }
        public string CourseTitle
        {
            get;
            set;
        }
    }
}
