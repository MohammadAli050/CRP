using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class TranscriptStudentInfo
    {
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public DateTime IssuedDate{get;set;} 
        public string Roll { get; set; }
        public string EnrollmentTri { get; set; }
        public string CompletionTri { get; set; }
        public string Degree { get; set; }
        public string StudentID { get; set; }
    }
}
