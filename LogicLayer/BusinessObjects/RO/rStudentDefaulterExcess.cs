using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class rStudentDefaulterExcess
    {
        public int StudentID { get; set; }
        public string Roll { get; set; }
        public string FullName { get; set; }
        public decimal PayableAmount { get; set; }
        public decimal Waiver { get; set; }
        public decimal Received { get; set; }
        public decimal Due { get; set; }
    }
}
