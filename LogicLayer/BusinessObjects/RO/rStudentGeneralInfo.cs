using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentGeneralInfo
    {
        public int StudentId { get; set; }
        public string RegistrationNo { get; set; }
        public string Roll { get; set; }
        public string FullName { get; set; }
        public string FullNameInBangla { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string GuardianName { get; set; }
        public DateTime DOB { get; set; }        
        public string Phone { get; set; } 
        public string ShortName { get; set; }
        public string ProgramName { get; set; }
        public string ProgNameInBan { get; set; }
        public string PhotoPath { get; set; }
        public int YearNo { get; set; }   
        
    }
}
