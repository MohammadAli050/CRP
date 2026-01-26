using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class GradeWiseRetakeDiscount
    {
        public int GradeWiseRetakeDiscountId { get; set; }
        public int GradeId { get; set; }
        public int SessionId { get; set; }
        public int ProgramId { get; set; }
        public decimal RetakeDiscount { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal RetakeDiscountOnTrOrWav { get; set; }

        public GradeDetails Grade
        {
            get { return GradeDetailsManager.GetById(GradeId); }
        }
        public string GradeName
        {
            get
            {
                return Grade.Grade;
            }
        }
        public decimal GradePoint
        {
            get
            {
                return Grade.GradePoint;
            }
        }
        public string BatchName
        {
            get
            {
                AcademicCalender academicCalender = AcademicCalenderManager.GetById(SessionId);
                if (academicCalender != null)
                    return "[" + academicCalender.Code + "] " + UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year + " " + academicCalender.AcademicCalenderID.ToString();
                else
                    return "";
            }
        }
        public string ProgramName
        {
            get
            {
                Program pro = ProgramManager.GetById(ProgramId);
                if (pro != null)
                    return pro.ShortName;
                else
                    return "";
            }
        }
    }
}