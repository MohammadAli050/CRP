using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rExamRoutineConflict
    {
        public string FormalCode { get; set; }
        public string SectionName { get; set; }
        public int Id { get; set; }
        public string SetName { get; set; }
        public DateTime DayDate { get; set; }
        public string WName { get; set; }
        public string Time { get; set; }
        public string Conflict { get; set; }
       
    }
}
