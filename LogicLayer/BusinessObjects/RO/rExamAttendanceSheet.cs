using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rExamAttendanceSheet
    {
        public string Roll { get; set; }
        public int RoomNo { get; set; }
        public string CourseCode { get; set; }
        public string FullName { get; set; }
        public string Section { get; set; }
        public int SeatNo { get; set; }
        public string Attribute1 { get; set; }
    }
}
