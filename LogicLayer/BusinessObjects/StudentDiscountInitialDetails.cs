using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentDiscountInitialDetails
    {
        public int StudentDiscountInitialDetailsId { get; set; }
        public int StudentDiscountId { get; set; }
        public int TypeDefinitionId { get; set; }
        public decimal TypePercentage { get; set; }
        public int AcaCalSession { get; set; }
        public string Comments { get; set; }

        public TypeDefinition DiscountType 
        {
            get
            {
                TypeDefinition typeDefinition = TypeDefinitionManager.GetById(TypeDefinitionId);
                return typeDefinition;
            }
        }
    }
}

