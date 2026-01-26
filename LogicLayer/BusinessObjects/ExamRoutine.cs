using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class ExamRoutine
    {
        public int ExamRoutineID { get; set; }
        public int AcaCal_SectionID { get; set; }
        public Nullable<int> RoomInfoID { get; set; }
        public Nullable<int> TimeSlotPlanID { get; set; }
        public Nullable<DateTime> ExamDate { get; set; }
        public Nullable<int> TeacherID1 { get; set; }
        public Nullable<int> TeacherID2 { get; set; }
        public int ProgramID { get; set; }
        public Nullable<int> ExamTypeID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        #region Custom Property
        public string ProgramName
        {
            get
            {
                Program program = ProgramManager.GetById(ProgramID);
                return program.ShortName;
            }
        }

        public string RoomName
        {   get
            {
                RoomInformation roomInformation = RoomInformationManager.GetById(RoomInfoID);
                return roomInformation.RoomName;
            }
        }

        public string TimeSlotPlanName
        {
            get
            {
                TimeSlotPlanNew timeSlotPlanNew = TimeSlotPlanManager.GetById(Convert.ToInt32(TimeSlotPlanID));
                return timeSlotPlanNew.StartHour + ":" + timeSlotPlanNew.StartMin + " " + (timeSlotPlanNew.StartAMPM == 1 ? "AM" : "PM") + "-" + timeSlotPlanNew.EndHour + ":" + timeSlotPlanNew.EndMin + " " + (timeSlotPlanNew.EndAMPM == 1 ? "AM" : "PM");
            }
        }
        #endregion

        #region Custom Progerty (not set)
        public string TeacherInfoOne { get; set; }
        public string TeacherInfoTwo { get; set; }
        public string RoomInfo { get; set; }
        public string TimeSlotPlanInfo { get; set; }

        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public string Section { get; set; }
        #endregion
    }
}
