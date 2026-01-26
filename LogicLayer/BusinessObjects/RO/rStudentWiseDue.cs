using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class rStudentWiseDue
    {
        public int StudentID {get; set;}
        public string Roll{get; set;} 
        public decimal AdmissionFee{get; set;} 
        public decimal TutionFee{get; set;} 
        public decimal SemesterFee{get; set;} 
        public decimal LateFee{get; set;} 
        public decimal OtherFee{get; set;} 
        public decimal WaiverAmount{get; set;} 
        public decimal CurrentBill{get; set;} 
        public decimal CurrentPayment{get; set;} 
        public decimal CurrentDue{get; set;} 
        public decimal PreviousDue{get; set;}
        public decimal TotalDue { get; set; }
    }
}
