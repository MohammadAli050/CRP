using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rCreditGPA
    {
        public string Roll { get; set; }
        public string FullName { get; set; }
        public int Sl { get; set; }
        public string SlName { get; set; } 
        public string Type { get; set; }
        public decimal RValue { get; set; } 
        public decimal AttemptedCredit { get; set; }
        public decimal EarnedCredit { get; set; }
        public decimal TranscriptCGPA { get; set; }

    }
}
