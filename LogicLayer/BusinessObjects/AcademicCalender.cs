using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class AcademicCalender
    {
        public int AcademicCalenderID { get; set; }
        public int CalenderUnitTypeID { get; set; }
        public int Sequence { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Nullable<int> TotalWeek { get; set; }
        public int Year { get; set; }
        public string Code { get; set; }
        public Nullable<bool> IsCurrent { get; set; }
        public Nullable<bool> IsNext { get; set; }
        public Nullable<DateTime> AdmissionStartDate { get; set; }
        public Nullable<DateTime> AdmissionEndDate { get; set; }
        public Nullable<bool> IsActiveAdmission { get; set; }
        public Nullable<DateTime> RegistrationStartDate { get; set; }
        public Nullable<DateTime> RegistrationEndDate { get; set; }
        public Nullable<bool> IsActiveRegistration { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        #region Custom Property
        public string CalendarUnitType_TypeName
        {
            get
            {
                CalenderUnitType calenderUnitType = CalenderUnitTypeManager.GetById(CalenderUnitTypeID);
                return calenderUnitType.TypeName;
            }
        }
        public string FullCode
        {
            get
            {
                return CalendarUnitType_TypeName + " " + Year.ToString();
                //return Year.ToString() + ", " + CalendarUnitType_TypeName + " - " + BatchCode;
            }
        }

        public CalenderUnitType CalenderUnitType
        {
            get
            {
                CalenderUnitType calenderUnitType = CalenderUnitTypeManager.GetById(CalenderUnitTypeID);
                return calenderUnitType;
            }
        }

        public CalenderUnitMaster calenderUnitMaster
        {
            get
            {

                if (CalenderUnitType != null)
                {
                    CalenderUnitMaster calenderUnitMaster = CalenderUnitMasterManager.GetById(CalenderUnitType.CalenderUnitMasterID);

                    return calenderUnitMaster;
                }
                else
                    return null;
            }
        }

        public string FullCodeWithType
        {
            get
            {
                return CalendarUnitType_TypeName + " " + Year.ToString() + "(" + calenderUnitMaster.Name + ")";
                //return Year.ToString() + ", " + CalendarUnitType_TypeName + " - " + BatchCode;
            }
        }

        #endregion
    }
}
