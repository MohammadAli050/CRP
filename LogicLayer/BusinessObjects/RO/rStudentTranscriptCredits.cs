using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentTranscriptCredits
    {
        public int Year { get; set; }
        public string TypeName { get; set; }
        public double Credits { get; set; }
    }
}
