using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class TeacherCourseSection
    {
        public int Id {get; set; }
		public int TeacherId {get; set; }
		public int CourseId {get; set; }
		public int SectionId {get; set; }
		public DateTime CreatedDate{get; set; }
		public int CreatedBy {get; set; }
		public DateTime ModifiedDate{get; set; }
		public int ModifiedBy {get; set; }
    }
}

