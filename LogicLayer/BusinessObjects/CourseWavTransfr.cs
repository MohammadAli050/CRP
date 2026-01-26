using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class CourseWavTransfr
    {
       public int CourseWavTransfrID { get; set; }
       public int StudentID { get; set; }
       public string UniversityName { get; set; }
       public DateTime? FromDate { get; set; }
       public DateTime? ToDate { get; set; }
       public int? DivisionType { get; set; }
       public int CourseStatusID { get; set; }
       public string Remarks { get; set; }
       public int CreatedBy { get; set; }
       public DateTime CreatedDate { get; set; }
       public int? ModifiedBy { get; set; }
       public DateTime? ModifiedDate { get; set; }
    }
}
