using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class ExamCenter
    {
        public int Id { get; set; }
        public string ExamCenterName { get; set; }
        public string ExamCenterAddress { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}

