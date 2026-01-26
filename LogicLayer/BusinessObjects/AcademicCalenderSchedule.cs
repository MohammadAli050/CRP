using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class AcademicCalenderSchedule
    {
        public int AcademicCalenderScheduleId { get; set; }
        public int AcademicCalenderID { get; set; }
        public int ProgramId { get; set; }
        public DateTime ClassStartDate { get; set; }
        public DateTime MarkSheetSubmissionLastDate { get; set; }
        public DateTime AnswerScriptSubmissionLastDate { get; set; }
        public DateTime ResultPublicationDate { get; set; }
        public DateTime RegistrationPamentDateWithoutFine { get; set; }
        public DateTime OrientationDate { get; set; }
        public DateTime RegistrationPamentDateWithFine { get; set; }
        public DateTime MidExamStartDate { get; set; }
        public DateTime MidExamEndDate { get; set; }
        public DateTime AdvisingStartDate { get; set; }
        public DateTime AdvisingEnd { get; set; }
        public DateTime RegistrationStartDate { get; set; }
        public DateTime RegistrationEndDate { get; set; }
        public DateTime ClassEndDate { get; set; }
        public DateTime FinalExamDate { get; set; }
        public DateTime FinalEndDate { get; set; }
        public DateTime SessionVacationStartDate { get; set; }
        public DateTime SessionVacationEndDate { get; set; }
        public DateTime AdmissionStartDate { get; set; }
        public DateTime AdmissionEndDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

