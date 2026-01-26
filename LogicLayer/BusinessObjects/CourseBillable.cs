using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class CourseBillable
    {
        public int BillableCourseID { get; set; }
        public int AcaCalID { get; set; }
        public int ProgramID { get; set; }
        public int BillStartFromRetakeNo { get; set; }
        public bool IsCreditCourse { get; set; }
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
