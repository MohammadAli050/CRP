using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class TabulationResultRemarks
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int AcaCalId { get; set; }
        public string TabulationRemarks { get; set; }
        public string ResultRemarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}

