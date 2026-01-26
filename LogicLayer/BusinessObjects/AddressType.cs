using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class AddressType
    {
        public int AddressTypeId {get; set; }
		public string TypeName {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedOn{get; set; }
		public int ModifiedBy {get; set; }
		public DateTime ModifiedOn{get; set; }
    }
}

