using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rDailyBillHistory
    {
        public int StudentId { get; set; }
        public string ProgramName { get; set; }
        public string Roll { get; set; }
        public string FullName { get; set; }
        public string TypeName { get; set; }
        public string Year { get; set; }
        public string TypeDefinition { get; set; }
        public decimal Fees { get; set; }
        public string Remark { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
