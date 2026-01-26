using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class ForwardCourses
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int AcaCalId { get; set; }
        public int CourseId { get; set; }
        public int VersionId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}

