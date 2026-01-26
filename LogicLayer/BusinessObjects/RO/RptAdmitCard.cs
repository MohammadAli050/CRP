using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class RptAdmitCard
    {
        public int GroupNo { get; set; }
        public int SL { get; set; }
        public string Roll { get; set; }
        public string BatchInfo { get; set; }
        public string Fullname { get; set; } 
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string RegistrationNo { get; set; }
        public string SessionName { get; set; }  
        public string FormalCode { get; set; }
        public string Title { get; set; }
        public decimal Credits { get; set; } 
        public string Gender { get; set; }
        public string PhotoPath { get; set; }
        public string Retake { get; set; }
        public string ExamCenterName { get; set; }
        public string InstitutionName { get; set; }

    }
}
