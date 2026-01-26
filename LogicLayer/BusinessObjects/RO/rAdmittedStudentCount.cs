using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.RO
{
    [Serializable]
    public class rAdmittedStudentCount
    {

        public int AcademicCalenderID { get; set; }
        public string Semester { get; set; }
        public string ShortName { get; set; }
        public string Gender { get; set; }

        public int AdmittedStudentCount { get; set; }
    }
}
