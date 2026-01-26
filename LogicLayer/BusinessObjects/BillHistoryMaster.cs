using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class BillHistoryMaster
    {
        public int BillHistoryMasterId {get; set; }
		public int StudentId {get; set; }
        public decimal Amount { get; set; }
		public string ReferenceNo {get; set; }
		public DateTime BillingDate{get; set; }
		public bool IsDeleted{get; set; }
		public bool IsDue{get; set; }
		public int ParentBillHistroyMasterId {get; set; }
		public int AcaCalId {get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public string Attribute4 {get; set; }
		public int CreatedBy {get; set; }
        public DateTime CreatedDate { get; set; }
		public int ModifiedBy {get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

