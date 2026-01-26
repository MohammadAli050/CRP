using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class rClassAttendance
    {
        public string Roll { get; set; }
        public string Name { get; set; }
        public int PreasentCount { get; set; }
        public int AbsentCount { get; set; }
        public int LateCount { get; set; }
        public string Status { get; set; }
        public Nullable<DateTime> AttendanceDate { get; set; }
    }
}
