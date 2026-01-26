using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rAttendance
    {
        public string FullName { get; set; }
        public string Roll { get; set; }
        public string BatchNo { get; set; }
    }
}
