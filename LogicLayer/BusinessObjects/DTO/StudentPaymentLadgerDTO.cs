using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class StudentPaymentLadgerDTO
    {
        public int BillHistoryId { get; set; }
        public int StudentCourseHistoryId { get; set; }
        public int StudentId { get; set; }
        public int TypeDefinationId { get; set; }
        public int AcaCalId { get; set; }
        public decimal Fees { get; set; }
        public string Remark { get; set; }
        public DateTime BillingDate { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CollectionHistoryId { get; set; }
        public string FormalCode { get; set; }
        public string CourseTitle { get; set; }
        public decimal Credits{ get; set;}
        public string SemesterName{ get; set;}

        public decimal DiscountAmount { get; set; }
        public decimal Payment { get; set; }
    }
}
