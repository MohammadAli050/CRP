using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class StudentTranscriptInfo
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Nullable<DateTime> PublicationDate { get; set; }
        public Nullable<DateTime> PreparedDate { get; set; }
        public string ExaminationMonth { get; set; }
        public Nullable<int> Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        public string Roll { get; set; }
        public string FullName { get; set; }
    }
}
