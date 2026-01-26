using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentTranscriptGeneralInfo
    {
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public DateTime DOB { get; set; }
        public string DepartmentName { get; set; }
        public string ProgramName { get; set; }
        public string Major { get; set; }
    }
}
