using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rClassRoutineConflict
    {
        public string FormalCode { get; set; }
        public string SectionName { get; set; }
        public string Day1 { get; set; }
        public string TimeSlotPlan1 { get; set; }
        public string Day2 { get; set; }
        public string TimeSlotPlan2 { get; set; }
        public string Conflict { get; set; }
  
    }
}
