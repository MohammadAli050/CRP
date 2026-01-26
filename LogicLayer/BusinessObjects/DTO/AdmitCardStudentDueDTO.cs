using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class AdmitCardStudentDueDTO
    {
        public int StudentID { get; set; }
        public string Roll { get; set; }
        public string FullName { get; set; }
        public decimal CurrentTrimesterTotalBill { get; set; }
        public decimal CurrentTrimesterHalfBill { get; set; }
        public decimal TotalPreviousBill { get; set; }
        public decimal Due { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal FinalDue { get; set; }
        public bool IsAdmitCardBlock { get; set; }
        
    }
}
