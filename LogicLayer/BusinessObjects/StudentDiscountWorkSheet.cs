using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentDiscountWorkSheet
    {
        public int StdDiscountWorksheetID { get; set; }
        public int StudentID { get; set; }
        public int ProgramID { get; set; }
        public int AcaCalID { get; set; }
        public int AdmissionCalId { get; set; }
        public decimal TotalCrRegInPreviousSession { get; set; }
        public decimal GPAinPreviousSession { get; set; }
        public decimal CGPAupToPreviousSession { get; set; }
        public decimal TotalCrRegInCurrentSession { get; set; }
        public int DiscountTypeId { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
