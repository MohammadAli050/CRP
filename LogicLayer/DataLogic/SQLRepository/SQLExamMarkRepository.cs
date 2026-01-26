using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLExamMarkRepository : IExamMarkRepository
    {

        Database db = null;

        private string sqlInsert = "ExamMarkInsert";
        private string sqlUpdate = "ExamMarkUpdate";
        private string sqlDelete = "ExamMarkDeleteById";
        private string sqlGetById = "ExamMarkGetById";
        private string sqlGetAll = "ExamMarkGetAll";
        private string sqlGetAllStudentByCourseSection = "GetExamMarkAllStudent";
        private string sqlGetAllStudentByAcaCalAcaCalSecExam = "ExamMarkGetAllStudentByAcaCalAcaCalSecExam";
        private string sqlGetAllStudentByAcaCalAcaCalSec = "ExamMarkGetAllStudentByAcaCalAcaCalSec";
        private string sqlGetTotalUpdateNumberIsFinalSubmit = "ExamMarkGetTotalUpdateNumberUsingIsFinalSubmit";
        private string sqlGetTotalUpdateNumberIsTransfer = "ExamMarkGetTotalUpdateNumberUsingIsTransfer";
        private string sqlGetAllAcaCalIdProgramIdCourseIdVersionId = "ExamMarkGetAllAcaCalIdProgramIdCourseIdVersionId";
        private string sqlApprovedByExamController = "ExamMarkApprovedByExamController";
        private string sqlApprovedByExamControllerByAcaCalSecRoll = "ExamMarkApprovedByExamControllerByAcaCalSecRoll";
        private string sqlGetShortSummaryReport = "ExamMarkGetShortSummaryReport";
        private string sqlGetResubmitApprovedByExamController = "ExamMarkResubmitApprovedByExamController";
        private string sqlPublishNumberByExamController = "ExamMarkPublishNumberByExamController";


        public int Insert(ExamMark studentresult)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentresult, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "id");

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

        public bool Update(ExamMark studentresult)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentresult, isInsert);

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

                db.AddInParameter(cmd, "id", DbType.Int32, id);
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

        public ExamMark GetById(int id)
        {
            ExamMark _studentresult = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMark> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMark>(sqlGetById, rowMapper);
                _studentresult = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentresult;
            }

            return _studentresult;
        }

        public List<ExamMark> GetAll()
        {
            List<ExamMark> studentresultList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMark> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMark>(sqlGetAll, mapper);
                IEnumerable<ExamMark> collection = accessor.Execute();

                studentresultList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentresultList;
            }

            return studentresultList;
        }


        #region Mapper

        private Database addParam(Database db, DbCommand cmd, ExamMark studentresult, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, studentresult.Id);
            }
            db.AddInParameter(cmd, "CourseHistoryId", DbType.Int32, studentresult.CourseHistoryId);
            db.AddInParameter(cmd, "ExamId", DbType.Int32, studentresult.ExamId);
            db.AddInParameter(cmd, "Mark", DbType.String, studentresult.Mark);
            db.AddInParameter(cmd, "Status", DbType.Int32, studentresult.Status);
            db.AddInParameter(cmd, "IsFinalSubmit", DbType.Boolean, studentresult.IsFinalSubmit);
            db.AddInParameter(cmd, "IsTransfer", DbType.Boolean, studentresult.IsTransfer);
            db.AddInParameter(cmd, "Attribute1", DbType.String, studentresult.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, studentresult.Attribute2);
            db.AddInParameter(cmd, "Attribute3", DbType.String, studentresult.Attribute3);
            db.AddInParameter(cmd, "CreateBy", DbType.Int32, studentresult.CreateBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, studentresult.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, studentresult.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, studentresult.ModifiedDate);

            return db;
        }

        private IRowMapper<ExamMark> GetMaper()
        {
            IRowMapper<ExamMark> mapper = MapBuilder<ExamMark>.MapAllProperties()

            .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.CourseHistoryId).ToColumn("CourseHistoryId")
            .Map(m => m.ExamId).ToColumn("ExamId")
            .Map(m => m.Mark).ToColumn("Mark")
            .Map(m => m.Status).ToColumn("Status")
            .Map(m => m.IsFinalSubmit).ToColumn("IsFinalSubmit")
            .Map(m => m.IsTransfer).ToColumn("IsTransfer")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
            .Map(m => m.Attribute3).ToColumn("Attribute3")
            .Map(m => m.CreateBy).ToColumn("CreateBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")

            .Build();

            return mapper;
        }

        private IRowMapper<ExamMarkDTO> GetExamMarkDTOMaper()
        {
            IRowMapper<ExamMarkDTO> mapper = MapBuilder<ExamMarkDTO>.MapAllProperties()

            .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.CourseHistoryId).ToColumn("CourseHistoryId")
            .Map(m => m.ExamId).ToColumn("ExamId")
            .Map(m => m.Mark).ToColumn("Mark")
            .Map(m => m.Status).ToColumn("Status")
            .Map(m => m.IsFinalSubmit).ToColumn("IsFinalSubmit")
            .Map(m => m.IsTransfer).ToColumn("IsTransfer")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.FullName).ToColumn("FullName")
            .Map(m => m.GradeMasterId).ToColumn("GradeMasterId")

            .Build();

            return mapper;
        }

        private IRowMapper<ExamMarkApproveDTO> GetExamMarkApproveDTOMaper()
        {
            IRowMapper<ExamMarkApproveDTO> mapper = MapBuilder<ExamMarkApproveDTO>.MapAllProperties()

            .Map(m => m.AcaCal_SectionID).ToColumn("AcaCal_SectionID")
            .Map(m => m.Teacher).ToColumn("Teacher")
            .Map(m => m.Title).ToColumn("Title")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.SectionName).ToColumn("SectionName")
            .Map(m => m.TotalStudent).ToColumn("TotalStudent")
            .Map(m => m.IsPublish).ToColumn("IsPublish")
            .Map(m => m.IsFinalSubmit).ToColumn("IsFinalSubmit")
            .Map(m => m.IsTransfer).ToColumn("IsTransfer")

            .Build();

            return mapper;
        }

        #endregion

        public List<ExamMarkApproveDTO> GetAllAcaCalIdProgramIdCourseIdVersionId(int acaCalId, int programId, int courseId, int versionId)
        {
            List<ExamMarkApproveDTO> examMarkApproveDTOList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkApproveDTO> mapper = GetExamMarkApproveDTOMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkApproveDTO>(sqlGetAllAcaCalIdProgramIdCourseIdVersionId, mapper);
                IEnumerable<ExamMarkApproveDTO> collection = accessor.Execute(acaCalId, programId, courseId, versionId).ToList();

                examMarkApproveDTOList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examMarkApproveDTOList;
            }

            return examMarkApproveDTOList;
        }

        public List<ExamMark> GetAllStudentByCourseSection(int courseId, int sectionId, int templateId)
        {
            List<ExamMark> studentresultList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMark> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMark>(sqlGetAllStudentByCourseSection, mapper);
                IEnumerable<ExamMark> collection = accessor.Execute(courseId, sectionId).ToList();

                studentresultList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentresultList;
            }

            return studentresultList;
        }

        public ExamMark GetStudentMarkByExamId(int courseHistoryId, int examId)
        {
            ExamMark _studentresult = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMark> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMark>("GetStudentMarkByExamId", rowMapper);
                _studentresult = accessor.Execute(courseHistoryId, examId).FirstOrDefault();

            }
            catch (Exception ex)
            {
                return _studentresult;
            }

            return _studentresult;
        }

        public List<ExamMarkDTO> GetAllStudentByAcaCalAcaCalSecExam(int acaCalId, int acaCalSecId, int examId)
        {
            List<ExamMarkDTO> studentresultList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkDTO> mapper = GetExamMarkDTOMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkDTO>(sqlGetAllStudentByAcaCalAcaCalSecExam, mapper);
                IEnumerable<ExamMarkDTO> collection = accessor.Execute(acaCalId, acaCalSecId, examId).ToList();

                studentresultList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentresultList;
            }

            return studentresultList;
        }

        public List<ExamMarkDTO> GetAllStudentByAcaCalAcaCalSec(int acaCalId, int acaCalSecId)
        {
            List<ExamMarkDTO> studentresultList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkDTO> mapper = GetExamMarkDTOMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkDTO>(sqlGetAllStudentByAcaCalAcaCalSec, mapper);
                IEnumerable<ExamMarkDTO> collection = accessor.Execute(acaCalId, acaCalSecId).ToList();

                studentresultList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentresultList;
            }

            return studentresultList;
        }

        public int GetTotalUpdateNumberIsFinalSubmit(int acaCalId, int acaCalSecId)
        {
            int count = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlGetTotalUpdateNumberIsFinalSubmit);

                db.AddOutParameter(cmd, "TotalUpdateNumber", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "AcaCalId", DbType.Int32, acaCalId);
                db.AddInParameter(cmd, "AcaCalSecId", DbType.Int32, acaCalSecId);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TotalUpdateNumber");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out count);
                }
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        public int GetTotalUpdateNumberIsTransfer(int acaCalId, int acaCalSecId)
        {
            int count = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlGetTotalUpdateNumberIsTransfer);

                db.AddOutParameter(cmd, "TotalUpdateNumber", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "AcaCalId", DbType.Int32, acaCalId);
                db.AddInParameter(cmd, "AcaCalSecId", DbType.Int32, acaCalSecId);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TotalUpdateNumber");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out count);
                }
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        public List<rExamResultCourseAndTeacherInfo> GetExamResultCourseAndTeacherInfo(int acaCalSecId, int acaCalId)
        {
            List<rExamResultCourseAndTeacherInfo> infoList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rExamResultCourseAndTeacherInfo> mapper = GetInfoListMaper();

                var accessor = db.CreateSprocAccessor<rExamResultCourseAndTeacherInfo>("RptExamResultCourseAndTeacherInfo", mapper);
                IEnumerable<rExamResultCourseAndTeacherInfo> collection = accessor.Execute(acaCalSecId, acaCalId).ToList();

                infoList = collection.ToList();
            }

            catch (Exception ex)
            {
                return infoList;
            }

            return infoList;
        }

        private IRowMapper<rExamResultCourseAndTeacherInfo> GetInfoListMaper()
        {
            IRowMapper<rExamResultCourseAndTeacherInfo> mapper = MapBuilder<rExamResultCourseAndTeacherInfo>.MapAllProperties()

           .Map(m => m.ShortName).ToColumn("ShortName")
           .Map(m => m.DetailedName).ToColumn("DetailedName")
           .Map(m => m.Title).ToColumn("Title")
           .Map(m => m.FormalCode).ToColumn("FormalCode")
           .Map(m => m.Credits).ToColumn("Credits")
           .Map(m => m.FullName).ToColumn("FullName")
           .Map(m => m.Phone).ToColumn("Phone")
           .Map(m => m.Year).ToColumn("Year")
           .Map(m => m.TypeName).ToColumn("TypeName")

           .Build();

            return mapper;
        }

        public List<rStudentGradePrevious> GetStudentGradeReportPrevious(string roll, int acaCalId)
        {
            List<rStudentGradePrevious> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentGradePrevious> mapper = GetStudentGradeReportPreviousMaper();

                var accessor = db.CreateSprocAccessor<rStudentGradePrevious>("RptStudentGradePrevious", mapper);
                IEnumerable<rStudentGradePrevious> collection = accessor.Execute(roll, acaCalId).ToList();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rStudentGradePrevious> GetStudentGradeReportPreviousMaper()
        {
            IRowMapper<rStudentGradePrevious> mapper = MapBuilder<rStudentGradePrevious>.MapAllProperties()

              .Map(m => m.CAttempted).ToColumn("CAttempted")
              .Map(m => m.CEarned).ToColumn("CEarned")
              .Map(m => m.GPATotal).ToColumn("GPATotal")
              .Map(m => m.PSecuredTotal).ToColumn("PSecuredTotal")
              .Map(m => m.CGPA).ToColumn("CGPA")
              .Map(m => m.TranscriptCGPA).ToColumn("TranscriptCGPA")

              .Build();

            return mapper;
        }
        public List<rStudentGradePreviousNew> GetStudentGradeReportPreviousNew(string roll, int acaCalId)
        {
            List<rStudentGradePreviousNew> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentGradePreviousNew> mapper = GetStudentGradeReportPreviousMaperNew();

                var accessor = db.CreateSprocAccessor<rStudentGradePreviousNew>("RptStudentGradePreviousNew", mapper);
                IEnumerable<rStudentGradePreviousNew> collection = accessor.Execute(roll, acaCalId).ToList();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rStudentGradePreviousNew> GetStudentGradeReportPreviousMaperNew()
        {
            IRowMapper<rStudentGradePreviousNew> mapper = MapBuilder<rStudentGradePreviousNew>.MapAllProperties()

              .Map(m => m.CAttempted).ToColumn("CAttempted")
              .Map(m => m.CEarned).ToColumn("CEarned")
              .Map(m => m.GPATotal).ToColumn("SGPA")
              .DoNotMap(m => m.PSecuredTotal)
              .Map(m => m.CGPA).ToColumn("CurrentCGPA")
              .Map(m => m.TranscriptCGPA).ToColumn("TranscriptCGPA")
              .Map(m => m.PreviousTotalGradePoint).ToColumn("PreviousTotalGradePoint")
              .Map(m => m.TotalGradePoint).ToColumn("TotalGradePoint")
              .Map(m => m.TotalCAttemped).ToColumn("TotalAttemptCredit")
              .Map(m => m.TotalCEarned).ToColumn("TotalEarnedCredit")
              .Map(m => m.TotalPreviousGradePointxCr).ToColumn("TotalPreviousGradePointxCr")
              .Map(m => m.TotalGradePointxCr).ToColumn("TotalGradePointxCr")

              .Build();

            return mapper;
        }

        public List<rStudentGradeCurrent> GetStudentGradeReportCurrent(string roll, int acaCalId)
        {
            List<rStudentGradeCurrent> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentGradeCurrent> mapper = GetStudentGradeReportCurrentMaper();

                var accessor = db.CreateSprocAccessor<rStudentGradeCurrent>("RptStudentGradeCurrent", mapper);
                IEnumerable<rStudentGradeCurrent> collection = accessor.Execute(roll, acaCalId).ToList();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }
        public List<rStudentGradeCurrent> GetStudentGradeReportCurrentNew(string roll, int acaCalId)
        {
            List<rStudentGradeCurrent> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentGradeCurrent> mapper = GetStudentGradeReportCurrentMaperNew();

                var accessor = db.CreateSprocAccessor<rStudentGradeCurrent>("RptStudentGradeCurrentNew", mapper);
                IEnumerable<rStudentGradeCurrent> collection = accessor.Execute(roll, acaCalId).ToList();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }
        private IRowMapper<rStudentGradeCurrent> GetStudentGradeReportCurrentMaperNew()
        {
            IRowMapper<rStudentGradeCurrent> mapper = MapBuilder<rStudentGradeCurrent>.MapAllProperties()

             .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
             .DoNotMap(m => m.CAttemped)
             .Map(m => m.FormalCode).ToColumn("FormalCode")
             .Map(m => m.Credits).ToColumn("Credits")
             .Map(m => m.Title).ToColumn("Title")
             .DoNotMap(m => m.Roll)
             .DoNotMap(m => m.Major)
             .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
             .DoNotMap(m => m.FatherName)
             .DoNotMap(m => m.FullName)
             .DoNotMap(m => m.DOB)
             .DoNotMap(m => m.DetailName)
             .DoNotMap(m => m.DepartmentName)
             .DoNotMap(m => m.TypeName)
             .Map(m => m.CEarned).ToColumn("CreditsEarned")
             .DoNotMap(m => m.CAttemptedTotal)
             .DoNotMap(m => m.Year)
             .Build();

            return mapper;
        }
        private IRowMapper<rStudentGradeCurrent> GetStudentGradeReportCurrentMaper()
        {
            IRowMapper<rStudentGradeCurrent> mapper = MapBuilder<rStudentGradeCurrent>.MapAllProperties()

             .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
             .Map(m => m.CAttemped).ToColumn("CAttemped")
             .Map(m => m.FormalCode).ToColumn("FormalCode")
             .Map(m => m.Credits).ToColumn("Credits")
             .Map(m => m.Title).ToColumn("Title")
             .Map(m => m.Roll).ToColumn("Roll")
             .Map(m => m.Major).ToColumn("Major")
             .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
             .Map(m => m.FatherName).ToColumn("FatherName")
             .Map(m => m.FullName).ToColumn("FullName")
             .Map(m => m.DOB).ToColumn("DOB")
             .Map(m => m.DetailName).ToColumn("DetailName")
             .Map(m => m.DepartmentName).ToColumn("DepartmentName")
             .Map(m => m.Year).ToColumn("Year")
             .Map(m => m.TypeName).ToColumn("TypeName")
             .Map(m => m.CEarned).ToColumn("CEarned")
             .Map(m => m.CAttemptedTotal).ToColumn("CAttemptedTotal")

             .Build();

            return mapper;
        }

        public string GetApprovedNumberByExamController(int acaCalSectionID)
        {
            string count = "0-0";

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlApprovedByExamController);

                db.AddOutParameter(cmd, "TotalStudent", DbType.String, Int32.MaxValue);
                db.AddInParameter(cmd, "AcaCalSectionID", DbType.Int32, acaCalSectionID);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TotalStudent");
                count = obj.ToString();
                //if (obj != null)
                //{
                //    int.TryParse(obj.ToString(), out count);
                //}
            }
            catch (Exception ex)
            {
                count = "0-0";
            }

            return count;
        }

        public string GetApprovedNumberByExamControllerByAcaCalSecRoll(int acaCalSectionID, string roll)
        {
            string count = "0-0";

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlApprovedByExamControllerByAcaCalSecRoll);

                db.AddOutParameter(cmd, "FlagStudent", DbType.String, Int32.MaxValue);
                db.AddInParameter(cmd, "AcaCalSectionID", DbType.Int32, acaCalSectionID);
                db.AddInParameter(cmd, "Roll", DbType.String, roll);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "FlagStudent");
                count = obj.ToString();
                //if (obj != null)
                //{
                //    int.TryParse(obj.ToString(), out count);
                //}
            }
            catch (Exception ex)
            {
                count = "0-0";
            }

            return count;
        }

        public string GetShortSummaryReport(int acaCalId, int programId)
        {
            string count = "0-0-0-0";

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlGetShortSummaryReport);

                db.AddOutParameter(cmd, "SummaryReport", DbType.String, Int32.MaxValue);
                db.AddInParameter(cmd, "AcaCalId", DbType.Int32, acaCalId);
                db.AddInParameter(cmd, "ProgramId", DbType.Int32, programId);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "SummaryReport");
                count = obj.ToString();
                //if (obj != null)
                //{
                //    int.TryParse(obj.ToString(), out count);
                //}
            }
            catch (Exception ex)
            {
                count = "0-0-0-0";
            }

            return count;
        }

        public int GetResubmitApprovedByExamController(string actionType, int acaCalSectionID)
        {
            int count = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlGetResubmitApprovedByExamController);

                db.AddOutParameter(cmd, "TotalStudent", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "ActionType", DbType.String, actionType);
                db.AddInParameter(cmd, "AcaCalSectionID", DbType.Int32, acaCalSectionID);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TotalStudent");
                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out count);
                }
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        public int GetPublishNumberByExamController(int acaCalId, int programId)
        {
            int count = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlPublishNumberByExamController);

                db.AddOutParameter(cmd, "TotalNumberStudent", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "AcaCalId", DbType.Int32, acaCalId);
                db.AddInParameter(cmd, "ProgramId", DbType.Int32, programId);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TotalNumberStudent");
                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out count);
                }
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        public List<rGradeOnly> GetGradeOnlyStudentResult(int acaCalSectionId)
        {
            List<rGradeOnly> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rGradeOnly> mapper = GetStudentGradeOnlyMaper();

                var accessor = db.CreateSprocAccessor<rGradeOnly>("RptGradeOnly", mapper);
                IEnumerable<rGradeOnly> collection = accessor.Execute(acaCalSectionId).ToList();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<rGradeOnly> GetTotalMarksStudentResult(int acaCalSectionId)
        {
            List<rGradeOnly> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rGradeOnly> mapper = GetStudentGradeOnlyMaper();

                var accessor = db.CreateSprocAccessor<rGradeOnly>("RptTotalMarks", mapper);
                IEnumerable<rGradeOnly> collection = accessor.Execute(acaCalSectionId).ToList();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rGradeOnly> GetStudentGradeOnlyMaper()
        {
            IRowMapper<rGradeOnly> mapper = MapBuilder<rGradeOnly>.MapAllProperties()

              .Map(m => m.Name).ToColumn("Name")
              .Map(m => m.Roll).ToColumn("Roll")
              .Map(m => m.Total).ToColumn("Total")
              .Map(m => m.Grade).ToColumn("Grade")
              .Map(m => m.Point).ToColumn("Point")

              .Build();

            return mapper;
        }

        public List<StudentGrade> GetGradeReportByRollSesmester(string roll)
        {
            List<StudentGrade> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentGrade> mapper = GetStudentGradeMaper();

                var accessor = db.CreateSprocAccessor<StudentGrade>("GradeReportByRollSesmester", mapper);
                IEnumerable<StudentGrade> collection = accessor.Execute(roll).ToList();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<StudentGrade> GetGradeReportByRollSession(string roll, int SessionId)
        {
            List<StudentGrade> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentGrade> mapper = GetStudentGradeMaper();

                var accessor = db.CreateSprocAccessor<StudentGrade>("GradeReportByRollAndSession", mapper);
                IEnumerable<StudentGrade> collection = accessor.Execute(roll,SessionId).ToList();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<StudentGrade> GetStudentGradeMaper()
        {
            IRowMapper<StudentGrade> mapper = MapBuilder<StudentGrade>.MapAllProperties()

              .Map(m => m.Credits).ToColumn("Credits")
              .Map(m => m.FormalCode).ToColumn("FormalCode")
              .Map(m => m.Title).ToColumn("Title")
              .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
              .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
              .Map(m => m.SessionId).ToColumn("SessionId")
              .Map(m => m.SessionName).ToColumn("SessionName")
              .Map(m => m.SemesterName).ToColumn("SemesterName")
              .Map(m => m.TranscriptCredit).ToColumn("TranscriptCredit")
              .Map(m => m.TranscriptGPA).ToColumn("TranscriptGPA")
              .Map(m => m.EarnedCredit).ToColumn("EarnedCredit")
              .Map(m => m.GradeId).ToColumn("GradeId")


              .Build();

            return mapper;
        }

        public List<rCreditGPA> GetGradeReportCreditGPAByRoll(int ProgramId,int BatchId,string roll)
        {
            List<rCreditGPA> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rCreditGPA> mapper = MapBuilder<rCreditGPA>.MapAllProperties() 
               .Map(m => m.FullName).ToColumn("FullName")
               .Map(m => m.Roll).ToColumn("Roll")
               .Map(m => m.RValue).ToColumn("Value")
               .Map(m => m.Sl).ToColumn("Sl")
               .Map(m => m.SlName).ToColumn("SlN")
               .Map(m => m.Type).ToColumn("Type")
               .Map(m => m.AttemptedCredit).ToColumn("AttemptedCredit")
               .Map(m => m.EarnedCredit).ToColumn("EarnedCredit")
               .Map(m => m.TranscriptCGPA).ToColumn("TranscriptCGPA")
               .Build();

                var accessor = db.CreateSprocAccessor<rCreditGPA>("GetGradeReportCreditGPAByRoll", mapper);
                IEnumerable<rCreditGPA> collection = accessor.Execute(ProgramId,BatchId,roll).ToList();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<StudentGrade> GetGradeReportByRollSemesterNo(string roll, int semesterId)
        {
            List<StudentGrade> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentGrade> mapper = GetStudentGradeMaper();

                var accessor = db.CreateSprocAccessor<StudentGrade>("GradeReportByRollAndSemesterNo", mapper);
                IEnumerable<StudentGrade> collection = accessor.Execute(roll, semesterId).ToList();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

    }
}