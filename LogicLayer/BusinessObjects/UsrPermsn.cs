using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class UsrPermsn
    {
        public int UsrPermsn_ID { get; set; }
        public int User_ID { get; set; }
        public string AccessIDPattern { get; set; }
        public Nullable<DateTime> AccessStartDate { get; set; }
        public Nullable<DateTime> AccessEndDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        
    }
}
