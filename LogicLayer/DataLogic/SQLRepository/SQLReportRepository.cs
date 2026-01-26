using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public class SQLReportRepository : IReportRepository
    {
        Database db = null;

        private string sqlGetByAcaCalIDAndProgramID = "rptClassRoutineByAcaCalIDAndProgramID";
        private string sqlStudentCGPAListGetByStudentID = "rptStudentCGPAList";
        private string sqlStudentResultHistoryGetByStudentID = "rptStudentResultHistory";
        private string sqlStudentClassRoutineByStudentIDAndAcaCalID = "rptStudentClassRoutine";
        private string sqlStudentRoadmapGetByStudentID = "rptStudentRoadmapLatest";
        private string sqlGetAttendaceListByAcaCalID = "RptAttendanceSheet";
        private string sqlGetAttendanceCourseInfoByAcaCalID = "RptGetCourseTeacherInfo";
        private string sqlGetDegreeCompletionListByProgramIDAndSessionRange = "RptDegreeCompletionListByProgramIDAndSessionRange";
        private string sqlGetMeritListProgramIDAndSessionRange = "RptMeritListProgramIDAndSessionRange";
        private string sqlGetConsolidatedCreditAssessmentByProgramIDAndSessionRange = "RptConsolidatedCreditAssessmentByProgramIDAndSessionRange";
        public List<rClassRoutine> GetByAcaCalIDAndProgramID(int AcaCalID, int ProgramID)
        {

            List<rClassRoutine> classRoutineList = new List<rClassRoutine>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rClassRoutine> mapper = MapBuilder<rClassRoutine>.MapAllProperties()

                .Map(m => m.CourseCode).ToColumn("CourseCode")
                .Map(m => m.Title).ToColumn("Title")
                .Map(m => m.Credit).ToColumn("Credit")
                .Map(m => m.SectionName).ToColumn("SectionName")
                .Map(m => m.Day).ToColumn("Day")
                .Map(m => m.Time).ToColumn("Time")
                .Map(m => m.Room).ToColumn("Room")
                .Map(m => m.Faculty).ToColumn("Faculty")
                .Map(m => m.Program).ToColumn("Program")
                .Map(m => m.SharedPrograms).ToColumn("SharedPrograms")

                .Build();


                var accessor = db.CreateSprocAccessor<rClassRoutine>(sqlGetByAcaCalIDAndProgramID, mapper);
                IEnumerable<rClassRoutine> collection = accessor.Execute(AcaCalID, ProgramID);

                classRoutineList = collection.ToList();
            }

            catch (Exception ex)
            {
                return classRoutineList;
            }

            return classRoutineList;
        }

        public List<rStudentCGPAList> GetStudentCGPAListByStudentID(string studentId)
        {
            List<rStudentCGPAList> studentCGPAList = new List<rStudentCGPAList>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentCGPAList> mapper = MapBuilder<rStudentCGPAList>.MapAllProperties()

                .Map(m => m.Semester).ToColumn("Semester")
                .Map(m => m.Credit).ToColumn("Credit")
                .Map(m => m.GPA).ToColumn("GPA")
                .Map(m => m.CGPA).ToColumn("CGPA")
                .Build();


                var accessor = db.CreateSprocAccessor<rStudentCGPAList>(sqlStudentCGPAListGetByStudentID, mapper);
                IEnumerable<rStudentCGPAList> collection = accessor.Execute(studentId);

                studentCGPAList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentCGPAList;
            }

            return studentCGPAList;
        }

        public List<rDegreeCompletion> GetDegreeCompletionListByProgramIDAndSessionRange(int programId, int fromSessionId, int toSessionId, int semesterType)
        {
            List<rDegreeCompletion> studentList = new List<rDegreeCompletion>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rDegreeCompletion> mapper = MapBuilder<rDegreeCompletion>.MapAllProperties()

                .Map(m => m.FullName).ToColumn("FullName")
                .Map(m => m.CompletionSemester).ToColumn("CompletionSemester")
                .Map(m => m.CreditEarned).ToColumn("CreditEarned")
                .Map(m => m.TranscriptCGPA).ToColumn("TranscriptCGPA")
                .Map(m => m.Remarks).ToColumn("Remarks")
                .Map(m => m.Code).ToColumn("Code")
                .Build();


                var accessor = db.CreateSprocAccessor<rDegreeCompletion>(sqlGetDegreeCompletionListByProgramIDAndSessionRange, mapper);
                IEnumerable<rDegreeCompletion> collection = accessor.Execute(programId, fromSessionId, toSessionId, semesterType);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<rFinalMeritList> GetMeritListByProgramIDAndSessionRange(int programId, int fromSessionId, int toSessionId)
        {
            List<rFinalMeritList> studentList = new List<rFinalMeritList>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rFinalMeritList> mapper = MapBuilder<rFinalMeritList>.MapAllProperties()

                .Map(m => m.FullName).ToColumn("FullName")
                .Map(m => m.CompletionSemester).ToColumn("CompletionSemester")
                .Map(m => m.CreditEarned).ToColumn("CreditEarned")
                .Map(m => m.TranscriptCGPA).ToColumn("TranscriptCGPA")
                .Map(m => m.MaximumGrade).ToColumn("MaximumGrade")
                .Map(m => m.MinimumGrade).ToColumn("MinimumGrade")
                .Map(m => m.TotalRetakeCount).ToColumn("TotalRetakeCount")
                .Build();


                var accessor = db.CreateSprocAccessor<rFinalMeritList>(sqlGetMeritListProgramIDAndSessionRange, mapper);
                IEnumerable<rFinalMeritList> collection = accessor.Execute(programId, fromSessionId, toSessionId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<rFinalMeritList> GetConsolidatedCreditAssessmentByProgramIDAndSessionRange(int programId, int fromSessionId, int toSessionId, decimal credit)
        {
            List<rFinalMeritList> studentList = new List<rFinalMeritList>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rFinalMeritList> mapper = MapBuilder<rFinalMeritList>.MapAllProperties()

                .Map(m => m.FullName).ToColumn("FullName")
                .Map(m => m.CompletionSemester).ToColumn("CompletionSemester")
                .Map(m => m.CreditEarned).ToColumn("CreditEarned")
                .Map(m => m.TranscriptCGPA).ToColumn("TranscriptCGPA")
                .Map(m => m.MaximumGrade).ToColumn("MaximumGrade")
                .Map(m => m.MinimumGrade).ToColumn("MinimumGrade")
                .Map(m => m.TotalRetakeCount).ToColumn("TotalRetakeCount")
                .Build();


                var accessor = db.CreateSprocAccessor<rFinalMeritList>(sqlGetConsolidatedCreditAssessmentByProgramIDAndSessionRange, mapper);
                IEnumerable<rFinalMeritList> collection = accessor.Execute(programId, fromSessionId, toSessionId, credit);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<rStudentResultHistory> GetStudentResultHistoryListByStudentID(string studentId)
        {
            List<rStudentResultHistory> studentResultHistoryList = new List<rStudentResultHistory>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentResultHistory> mapper = MapBuilder<rStudentResultHistory>.MapAllProperties()

                .Map(m => m.BatchCode).ToColumn("BatchCode")
                .Map(m => m.Course).ToColumn("Course")
                .Map(m => m.CourseName).ToColumn("CourseName")
                .Map(m => m.Credit).ToColumn("Credit")
                .Map(m => m.GPA).ToColumn("GPA")
                .Map(m => m.IsConsiderGPA).ToColumn("IsConsiderGPA")
                .Map(m => m.Grade).ToColumn("Grade")
                .Build();


                var accessor = db.CreateSprocAccessor<rStudentResultHistory>(sqlStudentResultHistoryGetByStudentID, mapper);
                IEnumerable<rStudentResultHistory> collection = accessor.Execute(studentId);

                studentResultHistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentResultHistoryList;
            }

            return studentResultHistoryList;
        }

        public List<rStudentClassRoutine> GetStudentClassRoutineByStudentIDAndAcaCalID(string studentId, int AcaCalID)
        {

            List<rStudentClassRoutine> classRoutineList = new List<rStudentClassRoutine>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentClassRoutine> mapper = MapBuilder<rStudentClassRoutine>.MapAllProperties()

                .Map(m => m.CourseTitle).ToColumn("CourseTitle")
                .Map(m => m.FormalCode).ToColumn("FormalCode")
                .Map(m => m.Section).ToColumn("Section")
                .Map(m => m.Day).ToColumn("Day")
                .Map(m => m.Room).ToColumn("Room")
                .Map(m => m.Time).ToColumn("Time")

                .Build();


                var accessor = db.CreateSprocAccessor<rStudentClassRoutine>(sqlStudentClassRoutineByStudentIDAndAcaCalID, mapper);
                IEnumerable<rStudentClassRoutine> collection = accessor.Execute(studentId, AcaCalID);

                classRoutineList = collection.ToList();
            }

            catch (Exception ex)
            {
                return classRoutineList;
            }

            return classRoutineList;
        }

        public List<rStudentRoadmap> GetStudentRoadmapByStudentID(string studentId)
        {
            List<rStudentRoadmap> studentRoadmapList = new List<rStudentRoadmap>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentRoadmap> mapper = MapBuilder<rStudentRoadmap>.MapAllProperties()

                .Map(m => m.CourseID).ToColumn("CourseID")
                .Map(m => m.VersionID).ToColumn("VersionID")
                .Map(m => m.NodeID).ToColumn("NodeID")
                .Map(m => m.NodeName).ToColumn("NodeName")
                .Map(m => m.Priority).ToColumn("Priority")
                .Map(m => m.Grade).ToColumn("Grade")
                .Map(m => m.SemesterName).ToColumn("SemesterName")
                .Map(m => m.FormalCode).ToColumn("FormalCode")
                .Map(m => m.CourseTitle).ToColumn("CourseTitle")
                .Map(m => m.CreditHr).ToColumn("CreditHr")
                .Map(m => m.SemesterID).ToColumn("SemesterID")
                .Build();


                var accessor = db.CreateSprocAccessor<rStudentRoadmap>(sqlStudentRoadmapGetByStudentID, mapper);
                IEnumerable<rStudentRoadmap> collection = accessor.Execute(studentId);

                studentRoadmapList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentRoadmapList;
            }

            return studentRoadmapList;
        }

        public List<rAttendance> GetAttendanceListByAcaCalID(int AcaCalID)
        {
            List<rAttendance> attList = new List<rAttendance>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rAttendance> mapper = MapBuilder<rAttendance>.MapAllProperties()

                .Map(m => m.FullName).ToColumn("FullName")
                .Map(m => m.Roll).ToColumn("Roll")
                .Map(m => m.BatchNo).ToColumn("BatchNO")
                .Build();


                var accessor = db.CreateSprocAccessor<rAttendance>(sqlGetAttendaceListByAcaCalID, mapper);
                IEnumerable<rAttendance> collection = accessor.Execute(AcaCalID);

                attList = collection.ToList();
            }

            catch (Exception ex)
            {
                return attList;
            }

            return attList;
        }

        public List<rAttendanceClassTeacher> GetAttendanceCourseTeacherByAcaCalID(int AcaCalID)
        {
            List<rAttendanceClassTeacher> teacherList = new List<rAttendanceClassTeacher>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rAttendanceClassTeacher> mapper = MapBuilder<rAttendanceClassTeacher>.MapAllProperties()

                .Map(m => m.TeacherName).ToColumn("TeacherName")
                .Map(m => m.Code).ToColumn("Code")

                .Build();


                var accessor = db.CreateSprocAccessor<rAttendanceClassTeacher>(sqlGetAttendanceCourseInfoByAcaCalID, mapper);
                IEnumerable<rAttendanceClassTeacher> collection = accessor.Execute(AcaCalID);

                teacherList = collection.ToList();
            }

            catch (Exception ex)
            {
                return teacherList;
            }

            return teacherList;
        }

        public List<rOfferedCourseCount> GetOfferedCourseCountByProgramID(int programID)
        {
            List<rOfferedCourseCount> list = new List<rOfferedCourseCount>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rOfferedCourseCount> mapper = MapBuilder<rOfferedCourseCount>.MapAllProperties()

                .Map(m => m.Program).ToColumn("Program")
                .Map(m => m.BatchNO).ToColumn("BatchNO")
                .Map(m => m.FormalCode).ToColumn("FormalCode")
                .Map(m => m.CourseTitle).ToColumn("CourseTitle")
                .Map(m => m.Credits).ToColumn("Credits")
                .Map(m => m.Male).ToColumn("Male")
                .Map(m => m.Female).ToColumn("Female")
                .Map(m => m.Total).ToColumn("Total")
                .Build();

                var accessor = db.CreateSprocAccessor<rOfferedCourseCount>("rptOfferedCourseCount", mapper);
                IEnumerable<rOfferedCourseCount> collection = accessor.Execute(programID);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<RptAdmitCard> GetAdmitCard(int programId, int acaCalId, int batchId, string roll, int institutionId, int examCenterId)
        {
            List<RptAdmitCard> list = new List<RptAdmitCard>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RptAdmitCard> mapper = MapBuilder<RptAdmitCard>.MapAllProperties()

                .Map(m => m.Roll).ToColumn("Roll")
                .Map(m => m.Fullname).ToColumn("Fullname")
                .Map(m => m.FatherName).ToColumn("FatherName")
                .Map(m => m.MotherName).ToColumn("MotherName")
                .Map(m => m.RegistrationNo).ToColumn("RegistrationNo")
                .Map(m => m.SessionName).ToColumn("SessionName")
                .Map(m => m.FormalCode).ToColumn("FormalCode")
                .Map(m => m.Title).ToColumn("Title")
                .Map(m => m.Credits).ToColumn("Credits")
                .Map(m => m.Gender).ToColumn("Gender")
                .Map(m => m.PhotoPath).ToColumn("PhotoPath")
                .Map(m => m.SL).ToColumn("SL")
                .Map(m => m.GroupNo).ToColumn("GroupNo")
                .Map(m => m.BatchInfo).ToColumn("BatchInfo")
                .Map(m => m.Retake).ToColumn("Retake")
                .Map(m => m.ExamCenterName).ToColumn("ExamCenterName")
                .Map(m => m.InstitutionName).ToColumn("InstitutionName")
                .Build();

                var accessor = db.CreateSprocAccessor<RptAdmitCard>("RptAdmitCard", mapper);
                IEnumerable<RptAdmitCard> collection = accessor.Execute(programId, acaCalId, batchId, roll, institutionId, examCenterId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<rStudentClassExamSum> GetStudentRegSummary(string roll, int acaCalId)
        {

            List<rStudentClassExamSum> classRoutineList = new List<rStudentClassExamSum>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentClassExamSum> mapper = MapBuilder<rStudentClassExamSum>.MapAllProperties()

                .Map(m => m.FormalCode).ToColumn("FormalCode")
                .Map(m => m.Title).ToColumn("Title")
                .Map(m => m.Credits).ToColumn("Credits")
                .Map(m => m.StudentId).ToColumn("StudentId")
                .Map(m => m.Semester).ToColumn("Semester")

                .Build();


                var accessor = db.CreateSprocAccessor<rStudentClassExamSum>("RptStudentRegSummary", mapper);
                IEnumerable<rStudentClassExamSum> collection = accessor.Execute(roll, acaCalId);

                classRoutineList = collection.ToList();
            }

            catch (Exception ex)
            {
                return classRoutineList;
            }

            return classRoutineList;
        }

        public List<rClassRoutineConflict> GetClassRoutineConflict(int programId, int acaCalId)
        {
            List<rClassRoutineConflict> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rClassRoutineConflict> mapper = GetClassRoutineConflictMaper();

                var accessor = db.CreateSprocAccessor<rClassRoutineConflict>("RptClassRoutineConflict", mapper);
                IEnumerable<rClassRoutineConflict> collection = accessor.Execute(programId, acaCalId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rClassRoutineConflict> GetClassRoutineConflictMaper()
        {
            IRowMapper<rClassRoutineConflict> mapper = MapBuilder<rClassRoutineConflict>.MapAllProperties()
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.SectionName).ToColumn("SectionName")
            .Map(m => m.Day1).ToColumn("Day1")
            .Map(m => m.TimeSlotPlan1).ToColumn("TimeSlotPlan1")
            .Map(m => m.Day2).ToColumn("Day2")
            .Map(m => m.TimeSlotPlan2).ToColumn("TimeSlotPlan2")
            .Map(m => m.Conflict).ToColumn("Conflict")

            .Build();

            return mapper;
        }

        public List<rExamRoutineConflict> GetExamRoutineConflict(int programId, int acaCalId)
        {
            List<rExamRoutineConflict> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rExamRoutineConflict> mapper = GetExamRoutineConflictMaper();

                var accessor = db.CreateSprocAccessor<rExamRoutineConflict>("RptExamRoutineConflict", mapper);
                IEnumerable<rExamRoutineConflict> collection = accessor.Execute(programId, acaCalId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rExamRoutineConflict> GetExamRoutineConflictMaper()
        {
            IRowMapper<rExamRoutineConflict> mapper = MapBuilder<rExamRoutineConflict>.MapAllProperties()
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.SectionName).ToColumn("SectionName")
            .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.SetName).ToColumn("SetName")
            .Map(m => m.DayDate).ToColumn("DayDate")
            .Map(m => m.WName).ToColumn("WName")
            .Map(m => m.Time).ToColumn("Time")
            .Map(m => m.Conflict).ToColumn("Conflict")

            .Build();

            return mapper;
        }

        public List<rClassRoutineConflictPair> GetClassRoutineConflictPair(int programId, int acaCalId)
        {
            List<rClassRoutineConflictPair> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rClassRoutineConflictPair> mapper = GetClassRoutineConflictPairMaper();

                var accessor = db.CreateSprocAccessor<rClassRoutineConflictPair>("RptClassRoutineConflictPair", mapper);
                IEnumerable<rClassRoutineConflictPair> collection = accessor.Execute(programId, acaCalId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rClassRoutineConflictPair> GetClassRoutineConflictPairMaper()
        {
            IRowMapper<rClassRoutineConflictPair> mapper = MapBuilder<rClassRoutineConflictPair>.MapAllProperties()
            .Map(m => m.CP1).ToColumn("CP1")
            .Map(m => m.CP2).ToColumn("CP2")
            .Map(m => m.CP3).ToColumn("CP3")
            .Map(m => m.ConflictPair).ToColumn("ConflictPair")

            .Build();

            return mapper;
        }

        public List<rExamRoutineConflictPair> GetExamRoutineConflictPair(int programId, int acaCalId)
        {
            List<rExamRoutineConflictPair> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rExamRoutineConflictPair> mapper = GetExamRoutineConflictPairPairMaper();

                var accessor = db.CreateSprocAccessor<rExamRoutineConflictPair>("RptExamRoutineConflictPair", mapper);
                IEnumerable<rExamRoutineConflictPair> collection = accessor.Execute(programId, acaCalId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rExamRoutineConflictPair> GetExamRoutineConflictPairPairMaper()
        {
            IRowMapper<rExamRoutineConflictPair> mapper = MapBuilder<rExamRoutineConflictPair>.MapAllProperties()
            .Map(m => m.CP1).ToColumn("CP1")
            .Map(m => m.CP2).ToColumn("CP2")
            .Map(m => m.CP3).ToColumn("CP3")
            .Map(m => m.ConflictPair).ToColumn("ConflictPair")

            .Build();

            return mapper;
        }

        public List<rOfferedCourseClassRoutine> GetOfferedCourseClassRoutine(string roll, int acaCalId)
        {
            List<rOfferedCourseClassRoutine> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rOfferedCourseClassRoutine> mapper = GetOfferedCourseClassRoutineMaper();

                var accessor = db.CreateSprocAccessor<rOfferedCourseClassRoutine>("RptOfferedCourseClassRoutine", mapper);
                IEnumerable<rOfferedCourseClassRoutine> collection = accessor.Execute(roll, acaCalId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rOfferedCourseClassRoutine> GetOfferedCourseClassRoutineMaper()
        {
            IRowMapper<rOfferedCourseClassRoutine> mapper = MapBuilder<rOfferedCourseClassRoutine>.MapAllProperties()
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.SectionName).ToColumn("SectionName")
            .Map(m => m.Day).ToColumn("Day")
            .Map(m => m.Room).ToColumn("Room")
            .Map(m => m.ClassTime).ToColumn("ClassTime")
            .Map(m => m.Teacher).ToColumn("Teacher")

            .Build();

            return mapper;
        }

        public List<rTabulationSheet> GetTabulationSheet(int programId, int acaCalId, int batchId, string roll, int majorId)
        {
            List<rTabulationSheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rTabulationSheet> mapper = GetTabulationSheetMaper();

                //var accessor = db.CreateSprocAccessor<rTabulationSheet>("RptTabulationSheet", mapper);
                var accessor = db.CreateSprocAccessor<rTabulationSheet>("RptTabulationSheetV2", mapper);
                IEnumerable<rTabulationSheet> collection = accessor.Execute(programId, acaCalId, batchId, roll, majorId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rTabulationSheet> GetTabulationSheetMaper()
        {
            IRowMapper<rTabulationSheet> mapper = MapBuilder<rTabulationSheet>.MapAllProperties()
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.FullName).ToColumn("FullName")
            .Map(m => m.Major).ToColumn("Major")
            .Map(m => m.TCR).ToColumn("TCR")
            .Map(m => m.TCE).ToColumn("TCE")
            .Map(m => m.CGPA).ToColumn("CGPA")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
            .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
            .Map(m => m.CourseCredit).ToColumn("CourseCredit")
            .Map(m => m.PS).ToColumn("PS")
            .Map(m => m.YS).ToColumn("YS")
            .Map(m => m.TranscriptCGPA).ToColumn("TranscriptCGPA")

            .Build();

            return mapper;
        }

        public List<rDeptWiseRegisteredStudent> GetDeptWiseRegisteredStudent(int year)
        {
            List<rDeptWiseRegisteredStudent> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rDeptWiseRegisteredStudent> mapper = GetDeptWiseRegisteredStudentMaper();

                var accessor = db.CreateSprocAccessor<rDeptWiseRegisteredStudent>("RptDeptWiseRegisteredStudent", mapper);
                IEnumerable<rDeptWiseRegisteredStudent> collection = accessor.Execute(year);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rDeptWiseRegisteredStudent> GetDeptWiseRegisteredStudentMaper()
        {
            IRowMapper<rDeptWiseRegisteredStudent> mapper = MapBuilder<rDeptWiseRegisteredStudent>.MapAllProperties()
            .Map(m => m.ProgramName).ToColumn("ProgramName")
            .Map(m => m.SpringA).ToColumn("SpringA")
            .Map(m => m.SummerA).ToColumn("SummerA")
            .Map(m => m.FallA).ToColumn("FallA")
            .Map(m => m.TotalA).ToColumn("TotalA")
            .Map(m => m.SpringR).ToColumn("SpringR")
            .Map(m => m.SummerR).ToColumn("SummerR")
            .Map(m => m.FallR).ToColumn("FallR")
            .Map(m => m.TotalR).ToColumn("TotalR")
            .Map(m => m.DroppedOut).ToColumn("DroppedOut")

            .Build();

            return mapper;
        }

        public List<rProgramWiseRegistrationCount> GetProgramWiseRegistrationCount(int acaCalId)
        {
            List<rProgramWiseRegistrationCount> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rProgramWiseRegistrationCount> mapper = GetProgramWiseRegistrationCountMaper();

                var accessor = db.CreateSprocAccessor<rProgramWiseRegistrationCount>("RptProgramWiseRegistrationCount", mapper);
                IEnumerable<rProgramWiseRegistrationCount> collection = accessor.Execute(acaCalId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rProgramWiseRegistrationCount> GetProgramWiseRegistrationCountMaper()
        {
            IRowMapper<rProgramWiseRegistrationCount> mapper = MapBuilder<rProgramWiseRegistrationCount>.MapAllProperties()

           .Map(m => m.Total).ToColumn("Total")
           .Map(m => m.ShortName).ToColumn("ShortName")
           .Map(m => m.ProgramCode).ToColumn("Code")
           .Map(m => m.BatchNo).ToColumn("BatchNO")
           .Build();

            return mapper;
        }

        public List<rRegisteredStudentList> GetRegisteredStudentList(int acaCalId, int programId, int batchId)
        {
            List<rRegisteredStudentList> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rRegisteredStudentList> mapper = GetRegisteredStudentListMaper();

                var accessor = db.CreateSprocAccessor<rRegisteredStudentList>("RptRegisteredStudentList", mapper);
                IEnumerable<rRegisteredStudentList> collection = accessor.Execute(acaCalId, programId, batchId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rRegisteredStudentList> GetRegisteredStudentListMaper()
        {
            IRowMapper<rRegisteredStudentList> mapper = MapBuilder<rRegisteredStudentList>.MapAllProperties()
           .Map(m => m.StudentId).ToColumn("StudentId")
           .Map(m => m.Roll).ToColumn("Roll")
           .Map(m => m.FullName).ToColumn("FullName")
           .Map(m => m.BatchNO).ToColumn("BatchNO")

           .Build();

            return mapper;
        }

        public List<rDegreeCompletedStudent> GetDegreeCompletedStudentCountSessionRange()
        {
            List<rDegreeCompletedStudent> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rDegreeCompletedStudent> mapper = GetDegreeCompletedStudenMaper();

                var accessor = db.CreateSprocAccessor<rDegreeCompletedStudent>("RptDegreeCompletedStudents", mapper);
                IEnumerable<rDegreeCompletedStudent> collection = accessor.Execute();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rDegreeCompletedStudent> GetDegreeCompletedStudenMaper()
        {
            IRowMapper<rDegreeCompletedStudent> mapper = MapBuilder<rDegreeCompletedStudent>.MapAllProperties()
           .Map(m => m.AcacalID).ToColumn("CompletedSemester")
           .Map(m => m.ProgramID).ToColumn("ProgramID")
           .Map(m => m.TCount).ToColumn("Count")

           .Build();

            return mapper;
        }

        public List<rAdmittedStudentCount> GetAdmittedStudentCount()
        {
            List<rAdmittedStudentCount> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rAdmittedStudentCount> mapper = GetAdmittedStudentCountMaper();

                var accessor = db.CreateSprocAccessor<rAdmittedStudentCount>("RptAdmittedStudentCount", mapper);
                IEnumerable<rAdmittedStudentCount> collection = accessor.Execute();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rAdmittedStudentCount> GetAdmittedStudentCountMaper()
        {
            IRowMapper<rAdmittedStudentCount> mapper = MapBuilder<rAdmittedStudentCount>.MapAllProperties()
           .Map(m => m.Semester).ToColumn("Semester")
           .Map(m => m.ShortName).ToColumn("ShortName")
           .Map(m => m.Gender).ToColumn("Gender")
           .Map(m => m.AdmittedStudentCount).ToColumn("AdmittedStudentCount")
           .Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")

           .Build();

            return mapper;
        }


        public List<rCreditDistribution> GetAllCreditDstributionList()
        {
            List<rCreditDistribution> classRoutineList = new List<rCreditDistribution>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rCreditDistribution> mapper = MapBuilder<rCreditDistribution>.MapAllProperties()

                .Map(m => m.ProgramID).ToColumn("ProgramID")
                .Map(m => m.Code).ToColumn("Code")
                .Map(m => m.Name).ToColumn("Name")
                .Map(m => m.DegreeName).ToColumn("DegreeName")
                .Map(m => m.Duration).ToColumn("Duration")
                .Map(m => m.RequiredCredit).ToColumn("RequiredCredit")
                .Build();


                var accessor = db.CreateSprocAccessor<rCreditDistribution>("RequiredCreditDistributionProgramWise", mapper);
                IEnumerable<rCreditDistribution> collection = accessor.Execute();

                classRoutineList = collection.ToList();
            }

            catch (Exception ex)
            {
                return classRoutineList;
            }

            return classRoutineList;
        }

        public List<rStudentClassExamSum> GetStudentCourseRegSummaryNew(int StudentId, int acaCalId, int userId, int Retake)
        {

            List<rStudentClassExamSum> classRoutineList = new List<rStudentClassExamSum>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentClassExamSum> mapper = MapBuilder<rStudentClassExamSum>.MapAllProperties()

                .Map(m => m.FormalCode).ToColumn("FormalCode")
                .Map(m => m.Title).ToColumn("Title")
                .Map(m => m.Credits).ToColumn("Credits")
                .Map(m => m.StudentId).ToColumn("StudentId")
                .Map(m => m.Semester).ToColumn("Semester")

                .Build();


                var accessor = db.CreateSprocAccessor<rStudentClassExamSum>("RptStudentRegSummaryNew", mapper);
                IEnumerable<rStudentClassExamSum> collection = accessor.Execute(StudentId, acaCalId, userId, Retake);

                classRoutineList = collection.ToList();
            }

            catch (Exception ex)
            {
                return classRoutineList;
            }

            return classRoutineList;
        }

    }
}
