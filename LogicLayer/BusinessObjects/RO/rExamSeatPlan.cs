using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rExamSeatPlan
    {
        public string Roll { get; set; }
        public string CourseCode { get; set; }
        public int RoomNo { get; set; }
        public string RowFlag { get; set; }
        public string SequenceNo { get; set; }
        public int AcaCalId { get; set; }
        public string SetName { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public DateTime DayDate { get; set; }
        public string StartTime { get; set; }
    }
}
