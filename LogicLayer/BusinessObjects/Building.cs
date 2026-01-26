using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Building
    {
        public int BuildingId {get; set; }
		public string BuildingName {get; set; }
		public Nullable<int> CampusId {get; set; }
		public Nullable<int> CreatedBy {get; set; }
		public Nullable<DateTime> CreatedDate{get; set; }
		public Nullable<int> ModifiedBy {get; set; }
		public Nullable<DateTime> ModifiedDate{get; set; }
    }
}

