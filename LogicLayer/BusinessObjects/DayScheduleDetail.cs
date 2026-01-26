using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class DayScheduleDetail
    {
        public int Id { get; set; }
        public int DayScheduleMasterId { get; set; }
        public int AcaSecId { get; set; }
        public int AcaCalId { get; set; }
        public int CourseId { get; set; }
        public int VersionId { get; set; }
        public string SectionName { get; set; }
        public int TimeSlotId { get; set; }
        public bool IsActive { get; set; }
        public int Attribute { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        #region Custom Property

        public string FullCourseCode
        {
            get
            {
                Course Course = CourseManager.GetByCourseIdVersionId(CourseId, VersionId);
                return Course.FormalCode + " " + Course.Title;

            }
        }


        public string TimeSlotInfoOne
        {
            get
            {
                TimeSlotPlanNew tsp = TimeSlotPlanManager.GetById(TimeSlotId);
                if (tsp != null)
                {
                    return tsp.StartHour + ":" + tsp.StartMin + " " + (tsp.StartAMPM == 1 ? "AM" : "PM") + "-" + tsp.EndHour + ":" + tsp.EndMin + " " + (tsp.EndAMPM == 1 ? "AM" : "PM");
                }
                else
                    return "";
            }
        }

        #endregion
    }
}

