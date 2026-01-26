using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StdEducationInfo
    {
       public int StdEducationInfoID { get; set; }
       public string DregreeName { get; set; }
       public string GroupName { get; set; }
       public string InstitutionName { get; set; }
       public decimal TotalMarks { get; set; }
       public decimal ObtainedMarks { get; set; }
       public string Division { get; set; }
       public decimal TotalCGPA { get; set; }
       public decimal ObtainedCGPA { get; set; }
       public int StudentID { get; set; }
       public int AddressID { get; set; }
       public int CreatedBy { get; set; }
       public DateTime CreatedDate { get; set; }
       public int ModifiedBy { get; set; }
       public DateTime ModifiedDate { get; set; }
    }
}
