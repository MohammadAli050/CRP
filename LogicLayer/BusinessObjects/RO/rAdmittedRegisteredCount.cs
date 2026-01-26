using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rAdmittedRegisteredCount
    {
        public int BatchNo { get; set; }
        public int SessionID { get; set; }
        public int SemesterID { get; set; }
        public string SessionName { get; set; }
        public string SemesterName { get; set; }
        public int RegisteredCount { get; set; }
        public int TotalStudent { get; set; }
        public int DroppedStudent { get; set; }
    }
}
