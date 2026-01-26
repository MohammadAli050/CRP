using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentDiscountAndScholarshipPerSessionCount
    {
        public int StudentCount { get; set; }
        public int ProgramId { get; set; }
        public string Program { get; set; }
        public int AcaCalId { get; set; }
        public string BatchCode { get; set; }
        public string Year { get; set; }
        public string UnitTypeName { get; set; }
        public string Batch { get { return "[" + BatchCode + "] " + UnitTypeName + Year; } }
    }
}

