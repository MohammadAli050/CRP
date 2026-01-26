using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class rDegreeCompletion
    {
        public string Roll { get; set; }
        public string FullName { get; set; }
        public decimal CreditEarned { get; set; }
        public decimal TranscriptCGPA { get; set; }
        public string CompletionSemester { get; set; }
        public string Remarks { get; set; }
        public string Code { get; set; }
    }
}
