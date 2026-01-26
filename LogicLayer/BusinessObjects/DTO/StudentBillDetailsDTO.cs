using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class StudentBillDetailsDTO
    {
        public string Roll { get; set; }
        public int StudentCourseHistoryId { get; set; }
        public int StudentID { get; set; }
        public int TypeDefinitionID { get; set; }
        public int AcaCalId { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
        public DateTime BillingDate { get; set; }
    }
}
