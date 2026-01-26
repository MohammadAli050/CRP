using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rOfferedCourse
    {
        public string GroupName { get; set; }
        public string FormalCode { get; set; }
        public string Title { get; set; }
        public decimal Credits { get; set; }
        public string PreRequisite { get; set; }
        
    }
}
