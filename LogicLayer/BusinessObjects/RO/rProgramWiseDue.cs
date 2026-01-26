using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class rProgramWiseDue
    {
        public int ProgramID { get; set; }
        public string DetailName { get; set; }
        public string ShortName { get; set; }
        public decimal PreviousDue { get; set; }
        public decimal Amount { get; set; }
        public decimal AdmissionFee { get; set; }
        public decimal TutionFee { get; set; }
        public decimal SemesterFee { get; set; }
        public decimal LateFee { get; set; }
        public decimal Others { get; set; }
        public decimal Waiver { get; set; }
        public decimal TotalFee { get; set; }
        public decimal TotalReceivable { get; set; }
        public decimal Received { get; set; }
        public decimal CurrentSemesterDue { get; set; }
        public decimal TotalDue { get; set; }
    }
}
