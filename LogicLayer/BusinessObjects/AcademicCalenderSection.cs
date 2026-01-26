using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class AcademicCalenderSection
    {
        public int AcaCal_SectionID { get; set; }
        public int AcademicCalenderID { get; set; }
        public int CourseID { get; set; }
        public int VersionID { get; set; }
        public string SectionName { get; set; }
        public int Capacity { get; set; }
        public int RoomInfoOneID { get; set; }
        public int RoomInfoTwoID { get; set; }
        public int RoomInfoThreeID { get; set; }
        public int DayOne { get; set; }
        public int DayTwo { get; set; }
        public int DayThree { get; set; }
        public int TimeSlotPlanOneID { get; set; }
        public int TimeSlotPlanTwoID { get; set; }
        public int TimeSlotPlanThreeID { get; set; }
        public int TeacherOneID { get; set; }
        public int TeacherTwoID { get; set; }
        public int TeacherThreeID { get; set; }
        public int DeptID { get; set; }
        public int ProgramID { get; set; }
        public int TypeDefinitionID { get; set; }
        public int Occupied { get; set; }
        public int ShareSection { get; set; }
        public int BasicExamTemplateId { get; set; }
        public int CalculativeExamTemplateId { get; set; }

        public string SectionDefination { get; set; }
        public int OnlineGradeSheetTemplateID { get; set; }
        public int SectionGenderID { get; set; }
        public string Remarks { get; set; }

        public string RoomConflict { get; set; }
        public string FacultyConflict { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        #region Custom Property
        public Program Program
        {
            get
            {
                return ProgramManager.GetById(ProgramID);
            }
        }
        public string FcSectionTitle
        {
            get
            {
                Course course = CourseManager.GetByCourseIdVersionId(CourseID, VersionID);

                return course.FormalCode + "(" + SectionName + ")-" + course.Title;
            }
        }
        public List<Program> ShareProgram
        {
            get
            {
                List<Program> programs = null;
                programs = ProgramManager.GetByAcaCalSectionID(AcaCal_SectionID);
                return programs == null ? new List<Program>() : programs;
            }
        }

        public List<Batch> ShareBatch
        {
            get
            {
                List<Batch> Batches = null;
                Batches = BatchManager.GetByAcaCalSectionID(AcaCal_SectionID);
                return Batches == null ? new List<Batch>() : Batches;
            }
        }
        public string AllShareProgram
        {
            get
            {
                List<Program> programs = null;
                string allShareProgram = "";
                int f = 0;

                programs = ProgramManager.GetByAcaCalSectionID(AcaCal_SectionID);

                foreach (Program item in programs)
                {
                    if (f > 0)
                    {
                        allShareProgram += " & ";
                    }

                    allShareProgram += item.ShortName;
                    f = 1;
                }

                return allShareProgram;
            }
        }
        public Value SectionGender
        {
            get
            {
                Value value = ValueManager.GetById(SectionGenderID);

                return value;
            }
        }
        public RoomInformation Room1
        {
            get
            {
                RoomInformation room = RoomInformationManager.GetById(RoomInfoOneID);
                return room;
            }
        }
        public RoomInformation Room2
        {
            get
            {
                RoomInformation room = RoomInformationManager.GetById(RoomInfoTwoID);
                return room;
            }
        }
        public Course Course
        {
            get
            {
                Course course = CourseManager.GetByCourseIdVersionId(CourseID, VersionID);
                return course;
            }
        }
        public ExamTemplate ExamTemplate
        {
            get
            {
                ExamTemplate examTemplate = ExamTemplateManager.GetById(OnlineGradeSheetTemplateID);
                return examTemplate;
            }
        }

        public ExamTemplateMaster ExamTemplateMaster
        {
            get
            {
                ExamTemplateMaster examTemplateMaster = new ExamTemplateMaster();
                if (BasicExamTemplateId > 0)
                {
                    examTemplateMaster = ExamTemplateMasterManager.GetById(BasicExamTemplateId);
                }
                return examTemplateMaster;
            }
        }
        public string CalculativeExamTemplateMasterName
        {
            get
            {
                ExamTemplateMaster examTemplateMaster = new ExamTemplateMaster();
                if (CalculativeExamTemplateId > 0)
                {
                    examTemplateMaster = ExamTemplateMasterManager.GetById(CalculativeExamTemplateId);
                }
                if (examTemplateMaster != null)
                    return examTemplateMaster.ExamTemplateMasterName;
                else return "";
            }
        }
        public int RegisteredCount
        {
            get
            {
                int count = 0;

                count = StudentCourseHistoryManager.GetAllByAcaCalSectionId(AcaCal_SectionID).Count;

                return count;

            }
        }

        #endregion

        #region Custom Progerty(Not Set)
        public string CourseInfo { get; set; }
        public string RoomInfoOne { get; set; }
        public string RoomInfoTwo { get; set; }
        public string DayInfoOne { get; set; }
        public string DayInfoTwo { get; set; }
        public string ProgramName { get; set; }
        public string TimeSlotPlanInfoOne { get; set; }
        public string TimeSlotPlanInfoTwo { get; set; }
        public string TeacherInfoOne { get; set; }
        public string TeacherInfoTwo { get; set; }
        public string ShareProgInfoOne { get; set; }
        public string ShareProgInfoTwo { get; set; }
        public string ShareProgInfoThree { get; set; }
        public string GradeSheetTemplate { get; set; }

        public int TotalStudent { get; set; }
        public int EvaluationCompleteStudent { get; set; }
        #endregion
    }
}
