using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rOfferedCourseClassRoutine
    {
        public string FormalCode { get; set; }
        public string SectionName { get; set; }
        public string Day { get; set; }
        public string Room { get; set; }
        public string ClassTime { get; set; }
        public string Teacher { get; set; }
       
    }
}
