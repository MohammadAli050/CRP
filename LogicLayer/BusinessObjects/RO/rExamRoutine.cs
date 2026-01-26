using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rExamRoutine
    {
        public int AcaCalId { get; set; }
        public string SetName { get; set; }
        public string ShortName { get; set; }
        public DateTime DayDate { get; set; }
        public int DayNo { get; set; }
        public int DayId { get; set; }
        public string WName { get; set; }
        public int TimeSlotNo {get; set;}
        public string Time { get; set; }
        public string FormalCode { get; set; }
        public string Title { get; set; }
       
    }
}
