using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LogicLayer.BusinessLogic;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rProbationStudent
    {
        public int StudentId { get; set; }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Roll { get; set; }
        public decimal CGPA { get; set; }
        public decimal GPA { get; set; }
        public int AcaCalId { get; set; }
        public string AcaCalCode { get; set; }
        public int ProbationCount { get; set; }
        public DateTime CreateDate { get; set; }
        public string BatchName
        {
            get
            {
                AcademicCalender academicCalender = AcademicCalenderManager.GetById(AcaCalId);
                if (academicCalender != null)
                    return UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year;
                else
                    return "";
            }
        }
       
    }
}
