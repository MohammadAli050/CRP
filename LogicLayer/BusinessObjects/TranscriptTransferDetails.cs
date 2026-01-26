using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class TranscriptTransferDetails
    {
        public string UniversityName { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitles { get; set; }
        public double Credit { get; set; }
    }
}
