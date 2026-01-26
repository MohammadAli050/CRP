using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentRollSheet
    {
        public int BatchNO { get; set; }
        public int StudentID { get; set; }
        public string Roll { get; set; }
        public string FullName { get; set; }
        public string SectionName { get; set; }
        public string Gender { get; set; }
        public string FormalCode { get; set; }
        public string Title { get; set; }
        public string RegistrationNo { get; set; }
        public string SessionName { get; set; }
        public string ExamCenterName { get; set; }
        public string AffiliateInstitutionName { get; set; }
        
    }
}
