using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLExamRepository : IExamRepository
    {

        Database db = null;

        private string sqlInsert = "ExamInsert";
        private string sqlUpdate = "ExamUpdate";
        private string sqlDelete = "ExamDeleteById";
        private string sqlGetById = "ExamGetById";
        private string sqlGetAll = "ExamGetAll";
        private string sqlGetExamByExamSetId = "GetExamByExamSetId";
        private string sqlGetTestByCouseTemplateId = "GetExamByCouseTemplateId";
        private string sqlGetMicroTestByCourseId = "GetExamByCourseId";
        private string sqlGetExamByName = "ExamGetByName";
        private string sqlGetAllByExamTemplateId = "ExamGetAllbyExamTemplateId";

        public int Insert(Exam exam)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, exam, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ExamId");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }
            }
            catch (Exception ex)
            {
                id = 0;
            }

            return id;
        }

        public bool Update(Exam exam)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, exam, isInsert);

                int rowsAffected = db.ExecuteNonQuery(cmd);

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDelete);

                db.AddInParameter(cmd, "ExamId", DbType.Int32, id);
                int rowsAffected = db.ExecuteNonQuery(cmd);

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public Exam GetById(int id)
        {
            Exam _microtest = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Exam> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Exam>(sqlGetById, rowMapper);
                _microtest = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _microtest;
            }

            return _microtest;
        }

        public List<Exam> GetAll()
        {
            List<Exam> examList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Exam> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Exam>(sqlGetAll, mapper);
                IEnumerable<Exam> collection = accessor.Execute();

                examList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examList;
            }

            return examList;
        }

        public List<Exam> GetAllExam(int examSetId) 
        {
            List<Exam> microtestList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Exam> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Exam>(sqlGetExamByExamSetId, mapper);
                List<Exam> collection = accessor.Execute(examSetId).ToList();

                microtestList = collection.ToList();
            }

            catch (Exception ex)
            {
                return microtestList;
            }

            return microtestList;
        }

        public List<Exam> GetExamByCouseTemplateId(int courseId, int templateId)
        {
            List<Exam> examList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Exam> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Exam>(sqlGetTestByCouseTemplateId, mapper);
                List<Exam> collection = accessor.Execute(courseId, templateId).ToList();

                examList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examList;
            }

            return examList;
        }

        public List<ExamDTO> GetExamByCourseId(int courseId)
        {
            List<ExamDTO> examList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamDTO> mapper = GetExamMaper();

                var accessor = db.CreateSprocAccessor<ExamDTO>(sqlGetMicroTestByCourseId, mapper);
                List<ExamDTO> collection = accessor.Execute(courseId).ToList();

                examList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examList;
            }

            return examList;
        }

        public Exam GetExamByName(string examName)
        {
            Exam _microtest = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Exam> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Exam>(sqlGetExamByName, rowMapper);
                _microtest = accessor.Execute(examName).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _microtest;
            }

            return _microtest;
        }

        public List<Exam> GetAllByExamTemplateId(int examTemplateId)
        {
            List<Exam> microtestList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Exam> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Exam>(sqlGetAllByExamTemplateId, mapper);
                List<Exam> collection = accessor.Execute(examTemplateId).ToList();

                microtestList = collection.ToList();
            }

            catch (Exception ex)
            {
                return microtestList;
            }

            return microtestList;
        }

        public List<rSemesterResultSummary> GetSemesterResultSummary(int sessionId)
        {
            List<rSemesterResultSummary> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rSemesterResultSummary> mapper = GetSemesterResultSummaryMaper();

                var accessor = db.CreateSprocAccessor<rSemesterResultSummary>("RptSemesterResultSummary", mapper);
                List<rSemesterResultSummary> collection = accessor.Execute(sessionId).ToList();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<rStudentExamRoutine> GetStudentExamRoutine(string roll, int acaCalId)
        {
            List<rStudentExamRoutine> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentExamRoutine> mapper = GetStudentExamRoutineMaper();

                var accessor = db.CreateSprocAccessor<rStudentExamRoutine>("RptStudentExamRoutineBySession", mapper);
                List<rStudentExamRoutine> collection = accessor.Execute(roll, acaCalId).ToList();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<rOfferedCourseExamRoutine> GetOfferedCourseExamRoutineForStudent(string roll, int acaCalId)
        {
            List<rOfferedCourseExamRoutine> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rOfferedCourseExamRoutine> mapper = GetOfferedCourseExamRoutineForStudentMaper();

                var accessor = db.CreateSprocAccessor<rOfferedCourseExamRoutine>("RptOfferedCourseExamRoutineForStudent", mapper);
                List<rOfferedCourseExamRoutine> collection = accessor.Execute(roll, acaCalId).ToList();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<TabulationSheetMajor> GetMajor(int programId)
        {
            List<TabulationSheetMajor> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TabulationSheetMajor> mapper = GetMajorMaper();

                var accessor = db.CreateSprocAccessor<TabulationSheetMajor>("RptTabulationSheetMajor", mapper);
                List<TabulationSheetMajor> collection = accessor.Execute(programId).ToList();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #region Mapper

        private IRowMapper<rOfferedCourseExamRoutine> GetOfferedCourseExamRoutineForStudentMaper()
        {
            IRowMapper<rOfferedCourseExamRoutine> mapper = MapBuilder<rOfferedCourseExamRoutine>.MapAllProperties()

            .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.SetName).ToColumn("SetName")
            .Map(m => m.DayDate).ToColumn("DayDate")
            .Map(m => m.DayNo).ToColumn("DayNo")
            .Map(m => m.DayId).ToColumn("DayId")
            .Map(m => m.WName).ToColumn("WName")
            .Map(m => m.TimeSlotNo).ToColumn("TimeSlotNo")
            .Map(m => m.Time).ToColumn("Time")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.Title).ToColumn("Title")

            .Build();

            return mapper;
        }

        private IRowMapper<rStudentExamRoutine> GetStudentExamRoutineMaper()
        {
            IRowMapper<rStudentExamRoutine> mapper = MapBuilder<rStudentExamRoutine>.MapAllProperties()

            .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.SetName).ToColumn("SetName")
            .Map(m => m.DayDate).ToColumn("DayDate")
            .Map(m => m.DayNo).ToColumn("DayNo")
            .Map(m => m.DayId).ToColumn("DayId")
            .Map(m => m.WName).ToColumn("WName")
            .Map(m => m.TimeSlotNo).ToColumn("TimeSlotNo")
            .Map(m => m.Time).ToColumn("Time")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.Title).ToColumn("Title")

            .Build();

            return mapper;
        }

        private IRowMapper<rSemesterResultSummary> GetSemesterResultSummaryMaper()
        {
            IRowMapper<rSemesterResultSummary> mapper = MapBuilder<rSemesterResultSummary>.MapAllProperties()

            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.GPA).ToColumn("GPA")
            .Map(m => m.ShortName).ToColumn("ShortName")           

            .Build();

            return mapper;
        }
      
        private Database addParam(Database db, DbCommand cmd, Exam exam, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ExamId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ExamId", DbType.Int32, exam.ExamId);
            }


            db.AddInParameter(cmd, "ExamName", DbType.String, exam.ExamName);
            db.AddInParameter(cmd, "Marks", DbType.Int32, exam.Marks);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, exam.CreatedBy);
            db.AddInParameter(cmd, "CreationTime", DbType.DateTime, exam.CreationTime);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, exam.ModifiedBy);
            db.AddInParameter(cmd, "ModificationTime", DbType.DateTime, exam.ModificationTime);

            return db;
        }

        private IRowMapper<Exam> GetMaper()
        {
            IRowMapper<Exam> mapper = MapBuilder<Exam>.MapAllProperties()

            .Map(m => m.ExamId).ToColumn("ExamId")
            .Map(m => m.ExamName).ToColumn("ExamName")
            .Map(m => m.Marks).ToColumn("Marks")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreationTime).ToColumn("CreationTime")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModificationTime).ToColumn("ModificationTime")

            .Build();

            return mapper;
        }

        private IRowMapper<ExamDTO> GetExamMaper()
        {
            IRowMapper<ExamDTO> mapper = MapBuilder<ExamDTO>.MapAllProperties()
            .Map(m => m.ExamId).ToColumn("ExamId")
            .Map(m => m.ExamName).ToColumn("ExamName")
            .Map(m => m.Marks).ToColumn("Marks")
            .Build();

            return mapper;
        }

        private IRowMapper<TabulationSheetMajor> GetMajorMaper()
        {
            IRowMapper<TabulationSheetMajor> mapper = MapBuilder<TabulationSheetMajor>.MapAllProperties()

            .Map(m => m.ChildNodeID).ToColumn("ChildNodeID")
            .Map(m => m.Name).ToColumn("Name")

            .Build();

            return mapper;
        }

        #endregion

        public List<StudentResultPublishCourseHistoryDTO> GetStudentForResultPublish(int programId, int sessionId, int courseId, int versionId, int acaCalSectionId) 
        {
            List<StudentResultPublishCourseHistoryDTO> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentResultPublishCourseHistoryDTO> mapper = GetStudentForResultPublishMaper();

                var accessor = db.CreateSprocAccessor<StudentResultPublishCourseHistoryDTO>("StudentCourseHistoryForResultPublishByParameter", mapper);
                List<StudentResultPublishCourseHistoryDTO> collection = accessor.Execute(programId, sessionId, courseId, versionId, acaCalSectionId).ToList();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<StudentResultPublishCourseHistoryDTO> GetStudentForResultPublishMaper()
        {
            IRowMapper<StudentResultPublishCourseHistoryDTO> mapper = MapBuilder<StudentResultPublishCourseHistoryDTO>.MapAllProperties()

            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.CourseHistoryId).ToColumn("CourseHistoryId")
            .Map(m => m.StudentName).ToColumn("StudentName")
            .Map(m => m.StudentRoll).ToColumn("StudentRoll")
            .Map(m => m.ObtainedTotalMarks).ToColumn("ObtainedTotalMarks")
            .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
            .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")

            .Build();

            return mapper;
        }

    }
}
