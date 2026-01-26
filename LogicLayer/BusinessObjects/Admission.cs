using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Admission
    {
        public int AdmissionID { get; set; }
        public int StudentID { get; set; }
        public int AdmissionCalenderID { get; set; }
        public string Remarks { get; set; }
        public bool IsRule { get; set; }
        public bool IsLastAdmission { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }


        public AcademicCalender AcademicCalender
        {
            get
            {
                return AcademicCalenderManager.GetById(AdmissionCalenderID);
            }
        }
    }
}
