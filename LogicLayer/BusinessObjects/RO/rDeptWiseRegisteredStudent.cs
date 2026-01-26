using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rDeptWiseRegisteredStudent
    {
        public string ProgramName { get; set; }
        public string SpringA { get; set; }
        public string SummerA { get; set; }
        public string FallA { get; set; }
        public string TotalA { get; set; }
        public string SpringR { get; set; }
        public string SummerR { get; set; }
        public string FallR { get; set; }
        public string TotalR { get; set; }
        public string DroppedOut { get; set; }
       
    }
}
