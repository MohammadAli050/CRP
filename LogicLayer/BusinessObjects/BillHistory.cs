using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class BillHistory
    {
        public int BillHistoryId {get; set; }
		public int StudentId {get; set; }
		public int FeeTypeId {get; set; }
        public int FundTypeId { get; set; }
		public int AcaCalId {get; set; }
        public decimal Fees { get; set; }
		public string Remark {get; set; }
		public DateTime BillingDate{get; set; }
		public bool IsDeleted{get; set; }
		public int BillHistoryMasterId {get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public string Attribute4 {get; set; }
		public int CreatedBy {get; set; }
        public DateTime CreatedDate { get; set; }
		public int ModifiedBy {get; set; }
        public DateTime ModifiedDate { get; set; }

        //do not map properties
        public string FeeName { get; set; }
        public string FundAccountNo { get; set; }
    }
}

