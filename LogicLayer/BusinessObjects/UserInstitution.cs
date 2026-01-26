using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class UserInstitution
    {
        	public int Id {get; set; }
		public int User_ID {get; set; }
		public int InstitutionID {get; set; }
		public int ProgramID {get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public int ModifiedBy {get; set; }
		public DateTime ModifiedDate{get; set; }

        public string InstitutionName
        {
            get
            {
                AffiliatedInstitution institution = AffiliatedInstitutionManager.GetById(InstitutionID);
                return institution.Name;
            }
        }
    }
}

