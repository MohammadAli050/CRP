using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class PreregistrationCountDTO
    {
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public string FormalCode { get; set; }
        public string CourseTitle { get; set; }
        public int AutoOpen { get; set; }
        public int AutoAssign { get; set; }
    }
}
