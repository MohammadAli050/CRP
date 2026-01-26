using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class CollectionHistory
    {
        public int CollectionHistoryId { get; set; }
        public int StudentId { get; set; }
        public int BillHistoryMasterId { get; set; }
        public int BillHistoryId { get; set; }
        public string MoneyReciptSerialNo { get; set; }
        public int AcaCalId { get; set; }
        public int TypeDefinitionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CollectionDate { get; set; }
        public string PaymentType { get; set; }
        public int CounterId { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public string ReferenceNo { get; set; }
        public string Comments { get; set; }
        public bool IsDeleted { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public string Attribute4 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}

