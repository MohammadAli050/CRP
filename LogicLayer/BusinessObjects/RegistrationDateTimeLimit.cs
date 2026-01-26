using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class RegistrationDateTimeLimit
    {
      public int RegistrationDateTimeLimitID { get; set; }
      public int DepartmentID { get; set; }
      public int ProgramID { get; set; }
      public int AcaCalID { get; set; }
      public DateTime PreAdvisingStartDT { get; set; }
      public DateTime PreAdvisingEndDT { get; set; }
      public DateTime SectionSeclationStartDT { get; set; }
      public DateTime SectionSeclationEndDT { get; set; }
      public DateTime RegistrationStartDT { get; set; }
      public DateTime RegistrationEndDT { get; set; }
      public DateTime Step1StartDT { get; set; }
      public DateTime Step1EndDT { get; set; }
      public DateTime Step2StartDT { get; set; }
      public DateTime Step2EndDT { get; set; }
      public int CreatedBy { get; set; }
      public DateTime CreatedDate { get; set; }
      public int ModifiedBy { get; set; }
      public DateTime ModifiedDate { get; set; }
    }
}
