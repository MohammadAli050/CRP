using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable] 
    public class EquivalentCourseDTO
    {
        public string GroupName { get; set; }
        public string GroupNo { get; set; }
        public int EquiCourseDetailId { get; set; }
        public int EquiCourseMasterId { get; set; }
        public int ProgramId { get; set; }
        public int CourseId { get; set; }
        public int VersionId { get; set; }
        public decimal Credits { get; set; }
        public string FormalCode { get; set; }
        public string VersionCode { get; set; }
        public string Title { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
