using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentDiscountInitialDetailsDTO
    {
        public int StudentID { get; set; }
        public string Roll { get; set; }
        public string Name { get; set; }
        public int BatchId { get; set; }
        public string BatchCode { get; set; }
        public int ProgramId { get; set; }
        public string Program { get; set; }
        public int StudentDiscountId { get; set; }
        public int StudentDiscountInitialDetailsId { get; set; }
        public int TypeDefinitionId { get; set; }
        public string DiscountType { get; set; }
        public decimal TypePercentage { get; set; }
        public string Comments { get; set; }
    }
}
