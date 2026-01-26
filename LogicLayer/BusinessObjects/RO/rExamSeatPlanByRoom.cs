using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rExamSeatPlanByRoom
    {
        public string Roll { get; set; }
        public string CourseCode { get; set; }
        public int SequenceNo { get; set; }
        public string Section { get; set; }
        public int RoomNo { get; set; }
        public string Attribute1 { get; set; }
    }
}
