using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class rStudentTranscriptNew
    {
        public int AcaCalId { get; set; }
        public int StudentID { get; set; }
        public string FormalCode { get; set; }
        public string Title { get; set; }
        public double Credits { get; set; }
        public double ObtainedGPA { get; set; }
        public string ObtainedGrade { get; set; }
        public double TranscriptCredit { get; set; }
        public double TranscriptGPA { get; set; }
        public double TranscriptCGPA { get; set; }
        public string Year { get; set; }
        public string TypeName { get; set; }

    }
}
