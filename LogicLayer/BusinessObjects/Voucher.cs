using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Voucher
    {
        public int VoucherID { get; set; }
        public string VoucherCode { get; set; }
        public string Prefix { get; set; }
        public Int64 SLNO { get; set; }
        public int AccountHeadsID { get; set; }
        public int AccountTypeID { get; set; }
        public decimal Amount { get; set; }
        public string PostedBy { get; set; }
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public string Remarks { get; set; }
        public int AcaCalID { get; set; }
        public string ReferenceNo { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeBankName { get; set; }
        public DateTime? ChequeDate { get; set; }
        public bool IsChequeCleare { get; set; }
        public DateTime? ChequeCleareDate { get; set; }
        public int Adjust { get; set; }
        public Guid GUID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Attribute1 { get; set; }
        public int Attribute2 { get; set; }
        public int Attribute3 { get; set; }
        public string Attribute4 { get; set; }
        public string Attribute5 { get; set; }
        public string Attribute6 { get; set; }
    }
}