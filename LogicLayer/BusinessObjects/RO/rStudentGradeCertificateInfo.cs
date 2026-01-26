using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentGradeCertificateInfo
    {
        public string Program { get; set; }
        public string Faculty { get; set; }
        public string BatchInfo { get; set; }
        public string RegistrationNo { get; set; }
        public string SessionInfo { get; set; }
        public string SemesterInfo { get; set; }
        public string Remarks { get; set; }
        public decimal TranscriptCGPA { get; set; }
        public decimal AttemptedCredit { get; set; }
        public decimal EarnedCredit { get; set; }
        public decimal EnrolledCredit { get; set; }
        public int SemesterId { get; set; }
        public int GenderId { get; set; }
        public string Major { get; set; }
        public string ExamMonthYear { get; set; }
        public string Duration { get; set; }
        public string DegreeName { get; set; }
        public decimal RequiredCredit { get; set; }

        public string Roll { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }

        public string GenderNameHeOrShe
        {
            get
            {
                string Type = "he";
                if (GenderId != 1)
                    Type = "she";
                return Type;
            }
        }
        public string ExamName
        {
            get
            {
                string T = "";
                Student std = StudentManager.GetByRoll(Roll);
                if (std != null)
                {
                    LogicLayer.BusinessObjects.DegreeCompletion degreeCompletion = DegreeCompletionManager.GetByStudentId(Convert.ToInt32(std.StudentID));
                    if (degreeCompletion != null)
                        T = degreeCompletion.ExaminationMonth;

                }
                return T;
            }
        }
        public string ExamMonth
        {
            get
            {
                string a = "";
                Student std = StudentManager.GetByRoll(Roll);
                if (std != null)
                {
                    var Info = StudentTranscriptInfoManager.GetByStudentId(std.StudentID);
                    if (Info != null)
                        a = Info.ExaminationMonth;
                }
                return a;
            }
        }
        public string publicationDate
        {
            get
            {
                string a = "-"; //DateTime.Now.ToString("dd/MM/yyyy");

                Student std = StudentManager.GetByRoll(Roll);
                if (std != null)
                {
                    var Info = StudentTranscriptInfoManager.GetByStudentId(std.StudentID);
                    if (Info != null && Info.PublicationDate != null)
                        a = (Convert.ToDateTime(Info.PublicationDate)).ToString("dd/MM/yyyy");
                }
                return a;
            }
        }

    }
}
