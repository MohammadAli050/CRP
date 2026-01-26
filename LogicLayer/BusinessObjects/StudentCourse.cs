using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentCourse
    {
       public int Student_CourseID { get; set; }
       public int StudentID { get; set; }
       public int StdAcademicCalenderID { get; set; }
       public int DscntSetUpID { get; set; }
       public int RetakeNo { get; set; }
       public int Node_CourseID { get; set; }
       public int CourseID { get; set; }
       public int VersionID { get; set; }
       public int CreatedBy { get; set; }
       public DateTime CreatedDate { get; set; }
       public int ModifiedBy { get; set; }
       public DateTime ModifiedDate { get; set; }
    }
}
