using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class RegistrationWorksheetParam
    {
        public int StudentId { get; set; }
       
        public int TreeCalendarMasterID { get; set; }
        public int TreeMasterID { get; set; }
        public int AcademicCalenderID { get; set; }
        public int ProgramID { get; set; }
        public int DepartmentID { get; set; }
        public decimal CrOpenLimit { get; set; }
        public int ReturnValue { get; set; }
        public int BatchID { get; set; }
        public int CourseOpenType { get; set; }
    }    
}
