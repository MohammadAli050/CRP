using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class StudentDiscountInitialDTO
    {
        public int StudentID { get; set; }
        public string Roll { get; set; }
        public int PersonID { get; set; }
        public int BatchId { get; set; }
        public Nullable<int> ProgramID { get; set; }
        public int SessionId { get; set; }
        public int StudentDiscountId { get; set; }
        public int StudentDiscountInitialDetailsId { get; set; }
        public int TypeDefinitionId { get; set; }
        public decimal TypePercentage { get; set; }
        public int AcaCalSession { get; set; }
    }
}
