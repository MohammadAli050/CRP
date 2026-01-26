using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rBillHistoryFeeTypeWise
    {
        public int StudentID { get; set; }
        public string Roll { get; set; }
        public string FullName { get; set; }
        public int BatchNo { get; set; }
        public string ProgramShortName { get; set; }
        public string Definition { get; set; }
		public int AcaCalId {get; set; }
		public decimal Fees{get; set; }
        public string Remark { get; set; }
		public DateTime CreatedDate{get; set; }
        public string Semester { get; set; }
    }
}

