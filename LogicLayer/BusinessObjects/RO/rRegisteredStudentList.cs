using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rRegisteredStudentList
    {
        public int StudentId { get; set; }
        public string Roll { get; set; }
        public string FullName { get; set; }
        public int BatchNO { get; set; }
    }
}
