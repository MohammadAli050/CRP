using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rFeeSetup
    {
        public string Type { get; set; }
        public string Definition { get; set; }
        public decimal Amount { get; set; }
        public int ProgramID { get; set; }
        public int BatchID { get; set; }
        public int BatchNO { get; set; }
    }
}
