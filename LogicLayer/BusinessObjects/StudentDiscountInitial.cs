using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentDiscountInitial
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int TypeDefinitionID { get; set; }
        public decimal Percentage { get; set; }
        public int BatchId { get; set; }
        public int Effective_AcaCalId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}