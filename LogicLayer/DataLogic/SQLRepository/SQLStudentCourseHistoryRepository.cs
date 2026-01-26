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
using System.Data.SqlClient;
using LogicLayer.BusinessObjects.WAO;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLStudentCourseHistoryRepository : IStudentCourseHistoryRepository
    {
        Database db = null;

        private string sqlInsert = "StudentCourseHistoryInsert";
        private string sqlUpdate = "StudentCourseHistoryUpdate";
        private string sqlDelete = "StudentCourseHistoryDeleteById";
        private string sqlGetById = "StudentCourseHistoryGetById";
        private string sqlGetAll = "StudentCourseHistoryGetAll";
        private string sqlGetCompletedCredit = "CompletedCreditByRoll";
        private string sqlGetAttemptedCredit = "AttemptedCreditByRoll";
        private string sqlGetAllByAcaCalId = "StudentCourseHistoryGetAllByAcaCalId";
        private string sqlGetAllByAcaCalSectionId = "StudentCourseHistoryGetAllByAcaCalSectionId";
        private string sqlGetAllByStudentId = "StudentCourseHistoryGetAllByStudentId";
        private string sqlGetByStudentIdCourseIdVersionId = "StudentCourseHistoryGetAllByStudentIdCourseIdVersionId";
        private string sqlGetByStudentIdCourseIdVersionIdConsiderGPA = "StudentCourseHistoryGetByStudentIdCourseIdVersionIdConsiderGPA";
        private string sqlGetByStudentIdCourseIdVersionIdAcaCalSecId = "StudentCourseHistoryGetByStudentIdCourseIdVersionIdAcaCalSecId";
        private string sqlGetAllByStudentIdAcaCalId = "StudentCourseHistoryGetAllByStudentIdAcaCalId";
        private string sqlGetByStudentIdAcaCalId = "StudentCourseHistoryGetByStudentIdAcaCalId";
        private string sqlGetCourseHistoryByStudentIdForDegreeValidation = "CourseHistoryByStudentIdForDegreeValidation";

        public int Insert(StudentCourseHistory studentCourseHistory)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, studentCourseHistory, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ID");

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

        public bool Update(StudentCourseHistory studentCourseHistory)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, studentCourseHistory, isInsert);

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

                db.AddInParameter(cmd, "ID", DbType.Int32, id);
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

        public decimal GetCompletedCredit(string Roll)
        {
            decimal completedCredit = 0;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlGetCompletedCredit);

                SqlParameter completedCreditParam = new SqlParameter("@CompletedCredit", SqlDbType.Decimal);
                completedCreditParam.Direction = ParameterDirection.Output;
                completedCreditParam.Precision = 5;
                completedCreditParam.Scale = 2;
                cmd.Parameters.Add(completedCreditParam);
                db.AddInParameter(cmd, "Roll", DbType.String, Roll);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "CompletedCredit");

                if (obj != null)
                {
                    Decimal.TryParse(obj.ToString(), out completedCredit);
                }
            }
            catch (Exception ex)
            {
                completedCredit = 0;
            }
            return completedCredit;
        }

        public decimal GetAttemptedCredit(string Roll)
        {
            decimal attemptedCredit = 0;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlGetAttemptedCredit);

                SqlParameter attemptedCreditParam = new SqlParameter("@AttemptedCredit", SqlDbType.Decimal);
                attemptedCreditParam.Direction = ParameterDirection.Output;
                attemptedCreditParam.Precision = 5;
                attemptedCreditParam.Scale = 2;
                cmd.Parameters.Add(attemptedCreditParam);
                db.AddInParameter(cmd, "Roll", DbType.String, Roll);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "AttemptedCredit");

                if (obj != null)
                {
                    Decimal.TryParse(obj.ToString(), out attemptedCredit);
                }
            }
            catch (Exception ex)
            {
                attemptedCredit = 0;
            }
            return attemptedCredit;
        }

        public StudentCourseHistory GetById(int? id)
        {
            StudentCourseHistory _studentCourseHistory = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistory> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistory>(sqlGetById, rowMapper);
                _studentCourseHistory = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentCourseHistory;
            }

            return _studentCourseHistory;
        }

        public List<StudentCourseHistory> GetAll()
        {
            List<StudentCourseHistory> studentCourseHistoryList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistory>(sqlGetAll, mapper);
                IEnumerable<StudentCourseHistory> collection = accessor.Execute();

                studentCourseHistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentCourseHistoryList;
            }

            return studentCourseHistoryList;
        }

        public List<StudentCourseHistory> GetAllByAcaCalSectionId(int id)
        {
            List<StudentCourseHistory> studentCourseHistoryList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistory>(sqlGetAllByAcaCalSectionId, mapper);
                IEnumerable<StudentCourseHistory> collection = accessor.Execute(id);

                studentCourseHistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentCourseHistoryList;
            }

            return studentCourseHistoryList;
        }

        public List<StudentCourseHistory> GetAllByAcaCalId(int acaCalId)
        {
            List<StudentCourseHistory> studentCourseHistoryList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistory>(sqlGetAllByAcaCalId, mapper);
                IEnumerable<StudentCourseHistory> collection = accessor.Execute(acaCalId);

                studentCourseHistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentCourseHistoryList;
            }

            return studentCourseHistoryList;
        }

        public List<StudentCourseHistoryDTO> GetAllByAcaCalIdCourseId(int acaCalId, int CourseId)
        {
            List<StudentCourseHistoryDTO> studentCourseHistoryList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistoryDTO> mapper = GetStudentCourseHistoryMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistoryDTO>("StudentCourseHistoryDTOGetAllByAcaCalIdCourseId", mapper);
                IEnumerable<StudentCourseHistoryDTO> collection = accessor.Execute(acaCalId, CourseId);

                studentCourseHistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentCourseHistoryList;
            }

            return studentCourseHistoryList;
        }

        public List<StudentCourseHistory> GetAllByStudentId(int studentID)
        {
            List<StudentCourseHistory> studentCourseHistoryList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistory>(sqlGetAllByStudentId, mapper);
                IEnumerable<StudentCourseHistory> collection = accessor.Execute(studentID);

                studentCourseHistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentCourseHistoryList;
            }

            return studentCourseHistoryList;
        }

        public List<StudentCourseHistory> GetAll(int studentId, int courseId, int versionId)
        {
            List<StudentCourseHistory> studentCourseHistoryList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistory>(sqlGetByStudentIdCourseIdVersionId, mapper);
                IEnumerable<StudentCourseHistory> collection = accessor.Execute(studentId, courseId, versionId);

                studentCourseHistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentCourseHistoryList;
            }

            return studentCourseHistoryList;
        }

        public StudentCourseHistory GetBy(int studentId, int courseId, int versionId, bool considerGPA)
        {
            StudentCourseHistory _studentCourseHistory = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistory> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistory>(sqlGetByStudentIdCourseIdVersionIdConsiderGPA, rowMapper);
                _studentCourseHistory = accessor.Execute(studentId, courseId, versionId, considerGPA).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentCourseHistory;
            }

            return _studentCourseHistory;
        }

        public StudentCourseHistory GetBy(int studentId, int courseId, int versionId, int acaCalSecId)
        {
            StudentCourseHistory _studentCourseHistory = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistory> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistory>(sqlGetByStudentIdCourseIdVersionIdAcaCalSecId, rowMapper);
                _studentCourseHistory = accessor.Execute(studentId, courseId, versionId, acaCalSecId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentCourseHistory;
            }

            return _studentCourseHistory;
        }

        public List<StudentCourseHistory> GetAllByStudentIdAcaCalId(int studentID, int acaCalId)
        {
            List<StudentCourseHistory> studentCourseHistoryList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistory>(sqlGetAllByStudentIdAcaCalId, mapper);
                IEnumerable<StudentCourseHistory> collection = accessor.Execute(studentID, acaCalId);

                studentCourseHistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentCourseHistoryList;
            }

            return studentCourseHistoryList;
        }

        public List<StudentCourseHistory> GetByStudentIdAcaCalId(int AcaCalId, int StudentId)
        {
            List<StudentCourseHistory> studentCourseHistoryList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistory>(sqlGetByStudentIdAcaCalId, mapper);
                IEnumerable<StudentCourseHistory> collection = accessor.Execute(StudentId, AcaCalId);

                studentCourseHistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentCourseHistoryList;
            }

            return studentCourseHistoryList;
        }
                
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StudentCourseHistory studentCourseHistory, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ID", DbType.Int32, studentCourseHistory.ID);
            }

            db.AddInParameter(cmd, "StudentID", DbType.Int32, studentCourseHistory.StudentID);
            db.AddInParameter(cmd, "CalCourseProgNodeID", DbType.Int32, studentCourseHistory.CalCourseProgNodeID);
            db.AddInParameter(cmd, "AcaCalSectionID", DbType.String, studentCourseHistory.AcaCalSectionID);
            db.AddInParameter(cmd, "RetakeNo", DbType.Int32, studentCourseHistory.RetakeNo);
            db.AddInParameter(cmd, "ObtainedTotalMarks", DbType.Decimal, studentCourseHistory.ObtainedTotalMarks);
            db.AddInParameter(cmd, "ObtainedGPA", DbType.Decimal, studentCourseHistory.ObtainedGPA);
            db.AddInParameter(cmd, "ObtainedGrade", DbType.String, studentCourseHistory.ObtainedGrade);
            db.AddInParameter(cmd, "GradeId", DbType.Int32, studentCourseHistory.GradeId);
            db.AddInParameter(cmd, "CourseStatusID", DbType.Int32, studentCourseHistory.CourseStatusID);
            db.AddInParameter(cmd, "CourseStatusDate", DbType.DateTime, studentCourseHistory.CourseStatusDate);
            db.AddInParameter(cmd, "AcaCalID", DbType.Int32, studentCourseHistory.AcaCalID);
            db.AddInParameter(cmd, "CourseID", DbType.Int32, studentCourseHistory.CourseID);
            db.AddInParameter(cmd, "VersionID", DbType.Int32, studentCourseHistory.VersionID);
            db.AddInParameter(cmd, "CourseCredit", DbType.Decimal, studentCourseHistory.CourseCredit);
            db.AddInParameter(cmd, "CompletedCredit", DbType.Decimal, studentCourseHistory.CompletedCredit);
            db.AddInParameter(cmd, "Node_CourseID", DbType.Int32, studentCourseHistory.Node_CourseID);
            db.AddInParameter(cmd, "NodeID", DbType.Int32, studentCourseHistory.NodeID);
            db.AddInParameter(cmd, "IsMultipleACUSpan", DbType.Boolean, studentCourseHistory.IsMultipleACUSpan);
            db.AddInParameter(cmd, "IsConsiderGPA", DbType.Boolean, studentCourseHistory.IsConsiderGPA);
            db.AddInParameter(cmd, "CourseWavTransfrID ", DbType.Int32, studentCourseHistory.CourseWavTransfrID);
            db.AddInParameter(cmd, "SemesterNo", DbType.Int32, studentCourseHistory.SemesterNo);
            db.AddInParameter(cmd, "YearNo", DbType.Int32, studentCourseHistory.YearNo);
            db.AddInParameter(cmd, "EqCourseHistoryId", DbType.Int32, studentCourseHistory.EqCourseHistoryId);
            db.AddInParameter(cmd, "Attribute1", DbType.String, studentCourseHistory.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, studentCourseHistory.Attribute2);
            db.AddInParameter(cmd, "Attribute3", DbType.String, studentCourseHistory.Attribute3);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, studentCourseHistory.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, studentCourseHistory.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, studentCourseHistory.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, studentCourseHistory.ModifiedDate);
            db.AddInParameter(cmd, "Remark", DbType.String, studentCourseHistory.Remark);

            return db;
        }
        private IRowMapper<StudentCourseHistoryDTO> GetStudentCourseHistoryMaper()
        {
            IRowMapper<StudentCourseHistoryDTO> mapper = MapBuilder<StudentCourseHistoryDTO>.MapAllProperties()
            .Map(m => m.StudentCourseHistoryId).ToColumn("ID")
            .Map(m => m.StudentId).ToColumn("StudentID")
            .Map(m => m.StudentRoll).ToColumn("Roll")
            .Map(m => m.StudentName).ToColumn("FullName")
            .Build();

            return mapper;
        }

        private IRowMapper<StudentCourseHistory> GetMaper()
        {
            IRowMapper<StudentCourseHistory> mapper = MapBuilder<StudentCourseHistory>.MapAllProperties()
            .Map(m => m.ID).ToColumn("ID")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.CalCourseProgNodeID).ToColumn("CalCourseProgNodeID")
            .Map(m => m.AcaCalSectionID).ToColumn("AcaCalSectionID")
            .Map(m => m.RetakeNo).ToColumn("RetakeNo")
            .Map(m => m.ObtainedTotalMarks).ToColumn("ObtainedTotalMarks")
            .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
            .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
            .Map(m => m.GradeId).ToColumn("GradeId")
            .Map(m => m.CourseStatusID).ToColumn("CourseStatusID")
            .Map(m => m.CourseStatusDate).ToColumn("CourseStatusDate")
            .Map(m => m.AcaCalID).ToColumn("AcaCalID")
            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.VersionID).ToColumn("VersionID")
            .Map(m => m.CourseCredit).ToColumn("CourseCredit")
            .Map(m => m.CompletedCredit).ToColumn("CompletedCredit")
            .Map(m => m.Node_CourseID).ToColumn("Node_CourseID")
            .Map(m => m.NodeID).ToColumn("NodeID")
            .Map(m => m.IsMultipleACUSpan).ToColumn("IsMultipleACUSpan")
            .Map(m => m.IsConsiderGPA).ToColumn("IsConsiderGPA")
            .Map(m => m.CourseWavTransfrID).ToColumn("CourseWavTransfrID")
            .Map(m => m.SemesterNo).ToColumn("SemesterNo")
            .Map(m => m.YearNo).ToColumn("YearNo")
            .Map(m => m.EqCourseHistoryId).ToColumn("EqCourseHistoryId")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
            .Map(m => m.Attribute3).ToColumn("Attribute3")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.Remark).ToColumn("Remark")

            .DoNotMap(m => m.FormalCode)
            .DoNotMap(m => m.CourseTitle)
            .DoNotMap(m => m.SessionFullCode)
            .DoNotMap(m => m.StudentRoll)
            .DoNotMap(m => m.StudentName)

            .DoNotMap(m => m.Semester)
            .DoNotMap(m => m.CourseCode)
            .DoNotMap(m => m.CourseName)
            .DoNotMap(m => m.CourseStatus)
            .DoNotMap(m => m.RegCredit)
            .DoNotMap(m => m.LastCGPA)
            .DoNotMap(m => m.RetakeStatus)

            .Build();

            return mapper;
        }
        #endregion
        
        public bool UpdateSectionBy(int SectionId, int id)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("StudentCourseHistoryUpdateSection");

                db.AddInParameter(cmd, "SectionId", DbType.Int32, SectionId);
                db.AddInParameter(cmd, "Id", DbType.Int32, id);

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

        public List<StudentCourseHistory> GetAllRegisteredStudentByProgramSessionCourse(int programId, int sessionId, int courseId, int versionId)
        {
            List<StudentCourseHistory> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistory>("StudentCourseHistoryGetAllRegisteredStudentByProgramSessionCourse", mapper);
                IEnumerable<StudentCourseHistory> collection = accessor.Execute(programId, sessionId, courseId, versionId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<StudentCourseHistory> GetAllRegisteredStudentByProgramSessionBatchCourse(int programId, int sessionId, int batchId, int courseId, int versionId)
        {
            List<StudentCourseHistory> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistory>("StudentCourseHistoryGetAllRegisteredStudentByProgramSessionBatchCourse", mapper);
                IEnumerable<StudentCourseHistory> collection = accessor.Execute(programId, sessionId, batchId, courseId, versionId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #region IStudentCourseHistoryRepository Members


        public List<StudentCourseHistory> GetAllByProgramSession(int programId, int sessionId)
        {
            List<StudentCourseHistory> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistory>("StudentCourseHistoryGetAllByProgramSession", mapper);
                IEnumerable<StudentCourseHistory> collection = accessor.Execute(programId, sessionId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #endregion

        public List<CourseHistryForDegreeValidation> GetCourseHistoryByStudentIdForDegreeValidation(string Roll)
        {
            List<CourseHistryForDegreeValidation> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseHistryForDegreeValidation> mapper = GetCourseHistryForDegreeValidationMaper();

                var accessor = db.CreateSprocAccessor<CourseHistryForDegreeValidation>(sqlGetCourseHistoryByStudentIdForDegreeValidation, mapper);
                IEnumerable<CourseHistryForDegreeValidation> collection = accessor.Execute(Roll);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;

        }

        private IRowMapper<CourseHistryForDegreeValidation> GetCourseHistryForDegreeValidationMaper()
        {
            IRowMapper<CourseHistryForDegreeValidation> mapper = MapBuilder<CourseHistryForDegreeValidation>.MapAllProperties()
            .Map(m => m.AcaCalId).ToColumn("AcaCalID")
            .Map(m => m.CalendarDetailName).ToColumn("CalenderName")
            .Map(m => m.CourseGroup).ToColumn("CourseGroup")
            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.CourseType).ToColumn("CourseType")
            .Map(m => m.Credits).ToColumn("CourseCredit")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
            .Map(m => m.NodeLinkName).ToColumn("NodeLinkName")
            .Map(m => m.Priority).ToColumn("Priority")
            .Map(m => m.AcaCalCode).ToColumn("AcaCalCode")
            .Map(m => m.Title).ToColumn("Title")
            .Map(m => m.VersionID).ToColumn("VersionID")
            .Map(m => m.HasMultipleACUSpan).ToColumn("HasMultipleACUSpan")
            .Build();

            return mapper;
        }

        public List<StudentCourseHistory> GetAllByProgramSessionBatch(int programId, int sessionId, int batchId, int rangeId)
        {
            List<StudentCourseHistory> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistory>("StudentCourseHistoryGetAllByProgramSessionBatch", mapper);
                IEnumerable<StudentCourseHistory> collection = accessor.Execute(programId, sessionId, batchId, rangeId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<StudentCourseHistory> GetDistinctCourseHistoryByStudentIdAcaCalId(int StudentId, int AcaCalId)
        {
            List<StudentCourseHistory> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StudentCourseHistory>("GetDistinctCourseHistoryByStudentIdAcaCalId", mapper);
                IEnumerable<StudentCourseHistory> collection = accessor.Execute(StudentId, AcaCalId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<GradeSheetInfo> GetAllRegisteredStudentForGradeSheetByProgramSessionBatchCourseExamCenter(int programId, int sessionId, int batchId, int courseId, int versionId, int ExamCeterId, int institutionId)
        {
            List<GradeSheetInfo> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<GradeSheetInfo> mapper = MapBuilder<GradeSheetInfo>.MapAllProperties()
                .Map(m => m.Roll).ToColumn("Roll")
                .Map(m => m.RegistrationNo).ToColumn("RegistrationNo")
                .Map(m => m.SessionName).ToColumn("SessionName") 
                .Build();

                var accessor = db.CreateSprocAccessor<GradeSheetInfo>("sqlRegisteredStudentForGradeSheetByProgramSessionBatchCourseExamCenter", mapper);
                IEnumerable<GradeSheetInfo> collection = accessor.Execute(programId, sessionId, batchId, courseId, versionId,ExamCeterId, institutionId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #region Web Api

        public List<StudentCourseHistoryWAO> GetStudentCourseHistoryWAOByStudentRoll(string roll)
        {
            List<StudentCourseHistoryWAO> studentCourseHistoryList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCourseHistoryWAO> mapper = GetMaperWAO();

                var accessor = db.CreateSprocAccessor<StudentCourseHistoryWAO>("StudentCourseHistoryWAOByStudentRoll", mapper);
                IEnumerable<StudentCourseHistoryWAO> collection = accessor.Execute(roll);

                studentCourseHistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentCourseHistoryList;
            }

            return studentCourseHistoryList;
        }

        private IRowMapper<StudentCourseHistoryWAO> GetMaperWAO()
        {
            IRowMapper<StudentCourseHistoryWAO> mapper = MapBuilder<StudentCourseHistoryWAO>.MapAllProperties()
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.SemesterName).ToColumn("SemesterName")
            .Map(m => m.SemesterNo).ToColumn("SemesterNo")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.CourseTitle).ToColumn("CourseTitle")
            .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
            .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
            .Build();

            return mapper;
        }

        #endregion

    }
}
