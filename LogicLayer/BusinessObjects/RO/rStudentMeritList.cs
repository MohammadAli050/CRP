using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    public class rStudentMeritList
    {
        public string Roll { get; set; }
        public string StudentName { get; set; }
        public decimal GPA { get; set; }
        public string TranscriptCGPA { get; set; }
        public int Position { get; set; }
    }
}
