using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class StudentInstitution
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int InstitutionId { get; set; }
        public int ExemCenterId { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        public AffiliatedInstitution AffiliatedInstitution
        {
            get
            {
                AffiliatedInstitution affiliatedInstitution = AffiliatedInstitutionManager.GetById(InstitutionId);
                return affiliatedInstitution;
            }
        }

    }
}

