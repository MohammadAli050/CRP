using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class DegreeCompletion
    {
        public int DegreeCompletionId { get; set; }
        public int StudentId { get; set; }
        public string DegreeTranscriptNumber { get; set; }
        public DateTime? DegreeTranscriptGenerateDate { get; set; }
        public string DegreeCertificateNumber { get; set; }
        public DateTime? DegreeCertificateGenerateDate { get; set; }
        public Boolean? IsDegreeComplete { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public int StudentTranscriptInfoId { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string ExaminationMonth { get; set; }
    }
}
