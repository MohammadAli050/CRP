using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class rStudentBillPaymentDue
    {
        public int StudentId { get; set; }
        public string Roll { get; set; }
        public string StudentName { get; set; }
        public decimal Bill { get; set; }
        public decimal Payment { get; set; }
        public decimal Due { get; set; }
    }
}
