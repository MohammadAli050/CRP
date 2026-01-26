using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class AcacalSectionResultPublishDTO
    {
        public int AcaCal_SectionID { get; set; }
        public int AcademicCalenderID { get; set; }
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public string CourseName { get; set; }
        public string SectionName { get; set; }
        public int StudentCount { get; set; }
        public DateTime ApprovedDate { get; set; }
        public bool IsApproved { get; set; }
        public DateTime FinalSubmitDate { get; set; }
        public bool IsFinalSubmit { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsPublished { get; set; }
        
    }
}
