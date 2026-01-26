using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rCreditDistribution
    {
        public int ProgramID { get; set; }
        public string Code { get; set; }
        public string DegreeName { get; set; }
        public int Duration { get; set; }
        public string Name { get; set; }
        public decimal RequiredCredit { get; set; }
    }
}
