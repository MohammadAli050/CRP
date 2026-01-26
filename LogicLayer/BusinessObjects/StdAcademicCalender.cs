using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StdAcademicCalender
    {
       public int StdAcademicCalenderID { get; set; }
       public int StudentID { get; set; }
       public int AcademicCalenderID { get; set; }
       public string Description { get; set; }
       public bool RegStatusType { get; set; }
       public decimal CGPA { get; set; }
       public decimal GPA { get; set; }
       public decimal TotalCreditsPerCalender { get; set; }
       public decimal TotalCreditsComleted { get; set; }
       public int CreatedBy { get; set; }
       public DateTime CreatedDate { get; set; }
       public int ModifiedBy { get; set; }
       public DateTime ModifiedDate { get; set; }
    }
}
