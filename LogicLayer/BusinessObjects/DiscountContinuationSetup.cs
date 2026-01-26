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
    public class DiscountContinuationSetup
    {
        public int DiscountContinuationID { get; set; }
        public int BatchAcaCalID { get; set; }
        public int ProgramID { get; set; }
        public int TypeDefinitionID { get; set; }
        public Nullable<decimal> MinCredits { get; set; }
        public Nullable<decimal> MaxCredits { get; set; }
        public Nullable<decimal> MinCGPA { get; set; }
        public string Range { get; set; }
        public Nullable<decimal> PercentMin { get; set; }
        public Nullable<decimal> PercentMax { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        #region Custom Property
        public string DiscountType
        {
            get
            {
                TypeDefinition typeDefinition = TypeDefinitionManager.GetById(TypeDefinitionID);
                if (typeDefinition != null)
                    return typeDefinition.Definition;
                else
                    return "";
            }
        }
        public string BatchName
        {
            get
            {
                AcademicCalender academicCalender = AcademicCalenderManager.GetById(BatchAcaCalID);
                if (academicCalender != null)
                    return "[" + academicCalender.Code + "] " + UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year+" "+academicCalender.AcademicCalenderID.ToString();
                else
                    return "";
            }
        }
        public string ProgramName
        {
            get
            {
                Program pro = ProgramManager.GetById(ProgramID); 
                if (pro != null)
                    return pro.ShortName;
                else
                    return "";
            }
        }
        #endregion
    }
}
