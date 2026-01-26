using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Certificates
    {
        public int ID { get; set; }
        public int SerialNo { get; set; }
        public int StudentID { get; set; }
        public int TypeID { get; set; }
        public bool IsCancelled { get; set;}
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

    }
}
