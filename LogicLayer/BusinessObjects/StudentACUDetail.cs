using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentACUDetail
    {
        public int StdACUDetailID { get; set; }
        public int StdAcademicCalenderID { get; set; }
        public int StudentID { get; set; }
        public int StatusTypeID { get; set; }
        public int SchSetUpID { get; set; }
        public decimal Credit { get; set; }
        public decimal CGPA { get; set; }
        public decimal GPA { get; set; }
        public decimal TranscriptCredit { get; set; }
        public decimal TranscriptCGPA { get; set; }
        public decimal TranscriptGPA { get; set; }
        public bool IsAllGradeSubmitted {get; set;}
        public string Description { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public AcademicCalender AcademicCalender
        {
            get
            {
                return AcademicCalenderManager.GetById(StdAcademicCalenderID);
            }
        }


        #region Custom Property Not Set
        public string Semester { get; set; }
        public string Roll { get; set; }
        public string Name { get; set; }
        #endregion
    }
}
