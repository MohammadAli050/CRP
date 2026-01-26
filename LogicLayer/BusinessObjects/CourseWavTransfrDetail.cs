using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class CourseWavTransfrDetail
    {
       public int CourseWavTransfrDetailID { get; set; }
       public int CourseWavTransfrMasterID { get; set; }
       public int OwnerNodeCourseID { get; set; }
       public string AgainstCourseInfo { get; set; }
       public int CreatedBy { get; set; }
       public DateTime CreatedDate { get; set; }
       public int ModifiedBy { get; set; }
       public DateTime ModifiedDate { get; set; }
    }
}
