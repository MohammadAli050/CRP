using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class rFinalMeritList
    {
        public string Roll { get; set; }
        public string FullName { get; set; }
        public decimal CreditEarned { get; set; }
        public decimal TranscriptCGPA { get; set; }
        public string CompletionSemester { get; set; }
        public string MaximumGrade { get; set; }
        public string MinimumGrade { get; set; }
        public int TotalRetakeCount { get; set; }


    }
}
