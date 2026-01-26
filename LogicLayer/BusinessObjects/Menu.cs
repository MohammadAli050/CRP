using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Menu
    {
        public int Menu_ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int ParentMnu_ID { get; set; }
        public int Tier { get; set; }
        public bool IsSysAdminAccesible { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
