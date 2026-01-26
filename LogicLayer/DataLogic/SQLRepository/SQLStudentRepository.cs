using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.RO;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLStudentRepository : IStudentRepository
    {
        Database db = null;

        private string sqlInsert = "StudentInsert";
        private string sqlUpdate = "StudentUpdate";
        private string sqlDelete = "StudentDeleteById";
        private string sqlGetById = "StudentGetById";
        private string sqlGetAll = "StudentGetAll";
        private string sqlGetAllByProgramIdBatchId = "StudentGetAllByProgramIdBatchId";
        private string sqlGetByPersonID = "StudentGetByPersonID";
        private string sqlGetAllByBatchProgram = "StudentGetAllByBatchProgram";
        private string sqlGetAllByNameOrID = "StudentGetAllByNameOrID";
        private string sqlGetByProgramOrBatchOrRoll = "StudentGetByProgramOrBatchOrRoll";

        public int Insert(Student student)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                //db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, student, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "StudentID");

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

        public bool Update(Student student)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, student, isInsert);

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

                db.AddInParameter(cmd, "StudentID", DbType.Int32, id);
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

        public Student GetById(int? id)
        {
            Student _student = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Student> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Student>(sqlGetById, rowMapper);
                _student = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _student;
            }

            return _student;
        }

        public Student GetByPersonID(int personID)
        {
            Student _student = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Student> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Student>(sqlGetByPersonID, rowMapper);
                _student = accessor.Execute(personID).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _student;
            }

            return _student;
        }

        public List<Student> GetAll()
        {
            List<Student> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Student> mapper = GetMaper();

                var accessor = db.CreateSqlStringAccessor<Student>("Select * from Student", mapper);
                IEnumerable<Student> collection = accessor.Execute();

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<Student> GetAllByNameOrID(string searchKey)
        {
            List<Student> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Student> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Student>(sqlGetAllByNameOrID, mapper);
                IEnumerable<Student> collection = accessor.Execute(searchKey);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<Student> GetAllByProgramIdBatchId(int programID, int batchID)
        {
            List<Student> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Student> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Student>(sqlGetAllByProgramIdBatchId, mapper);
                IEnumerable<Student> collection = accessor.Execute(programID, batchID);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Student student, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "StudentID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "StudentID", DbType.Int32, student.StudentID);
            }

            db.AddInParameter(cmd, "Roll", DbType.String, student.Roll);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, student.ProgramID);
            db.AddInParameter(cmd, "PersonID", DbType.Int32, student.PersonID);
            db.AddInParameter(cmd, "BatchId", DbType.Int32, student.BatchId);
            db.AddInParameter(cmd, "SessionId", DbType.Int32, student.SessionId);
            db.AddInParameter(cmd, "GradeMasterId", DbType.Int32, student.GradeMasterId);
            db.AddInParameter(cmd, "AccountHeadsID", DbType.Int32, student.AccountHeadsID);
            db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, student.TreeMasterID);
            db.AddInParameter(cmd, "TreeCalendarMasterID", DbType.Int32, student.TreeCalendarMasterID);
            db.AddInParameter(cmd, "CandidateId", DbType.Int32, student.CandidateId);
            db.AddInParameter(cmd, "Major1NodeID", DbType.Int32, student.Major1NodeID);
            db.AddInParameter(cmd, "Major2NodeID", DbType.Int32, student.Major2NodeID);
            db.AddInParameter(cmd, "Major3NodeID", DbType.Int32, student.Major3NodeID);
            db.AddInParameter(cmd, "Minor1NodeID", DbType.Int32, student.Minor1NodeID);
            db.AddInParameter(cmd, "Minor2NodeID", DbType.Int32, student.Minor2NodeID);
            db.AddInParameter(cmd, "Minor3NodeID", DbType.Int32, student.Minor3NodeID);
            db.AddInParameter(cmd, "History", DbType.String, student.History);
            db.AddInParameter(cmd, "IsCompleted", DbType.Boolean, student.IsCompleted);
            db.AddInParameter(cmd, "CompletedAcaCalId", DbType.Int32, student.CompletedAcaCalId);
            db.AddInParameter(cmd, "TranscriptSerial", DbType.String, student.TranscriptSerial);
            db.AddInParameter(cmd, "Remarks", DbType.String, student.Remarks);
            db.AddInParameter(cmd, "Pre_Math", DbType.Boolean, student.Pre_Math);
            db.AddInParameter(cmd, "Pre_English", DbType.Boolean, student.Pre_English);
            db.AddInParameter(cmd, "IsDiploma", DbType.Boolean, student.IsDiploma);
            db.AddInParameter(cmd, "IsActive", DbType.Boolean, student.IsActive);
            db.AddInParameter(cmd, "IsDeleted", DbType.Boolean, student.IsDeleted);
            db.AddInParameter(cmd, "IsProvisionalAdmission", DbType.Boolean, student.IsProvisionalAdmission);
            db.AddInParameter(cmd, "ValidUptoProAdmissionDate", DbType.DateTime, student.ValidUptoProAdmissionDate);
            db.AddInParameter(cmd, "Attribute1", DbType.String, student.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, student.Attribute2);
            db.AddInParameter(cmd, "Attribute3", DbType.String, student.Attribute3);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, student.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, student.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, student.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, student.ModifiedDate);

            return db;
        }

        private IRowMapper<Student> GetMaper()
        {
            IRowMapper<Student> mapper = MapBuilder<Student>.MapAllProperties()
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.PersonID).ToColumn("PersonID")
            .Map(m => m.BatchId).ToColumn("BatchId")
            .Map(m => m.SessionId).ToColumn("SessionId")
            .Map(m => m.GradeMasterId).ToColumn("GradeMasterId")
            .Map(m => m.AccountHeadsID).ToColumn("AccountHeadsID")
            .Map(m => m.TreeMasterID).ToColumn("TreeMasterID")
            .Map(m => m.TreeCalendarMasterID).ToColumn("TreeCalendarMasterID")
            .Map(m => m.CandidateId).ToColumn("CandidateId")
            .Map(m => m.Major1NodeID).ToColumn("Major1NodeID")
            .Map(m => m.Major2NodeID).ToColumn("Major2NodeID")
            .Map(m => m.Major3NodeID).ToColumn("Major3NodeID")
            .Map(m => m.Minor1NodeID).ToColumn("Minor1NodeID")
            .Map(m => m.Minor2NodeID).ToColumn("Minor2NodeID")
            .Map(m => m.Minor3NodeID).ToColumn("Minor3NodeID")
            .Map(m => m.History).ToColumn("History")
            .Map(m => m.IsCompleted).ToColumn("IsCompleted")
            .Map(m => m.CompletedAcaCalId).ToColumn("CompletedAcaCalId")
            .Map(m => m.TranscriptSerial).ToColumn("TranscriptSerial")
            .Map(m => m.Remarks).ToColumn("Remarks")
            .Map(m => m.Pre_Math).ToColumn("Pre_Math")
            .Map(m => m.Pre_English).ToColumn("Pre_English")
            .Map(m => m.IsDiploma).ToColumn("IsDiploma")
            .Map(m => m.IsActive).ToColumn("IsActive")
            .Map(m => m.IsDeleted).ToColumn("IsDeleted")
            .Map(m => m.IsProvisionalAdmission).ToColumn("IsProvisionalAdmission")
            .Map(m => m.ValidUptoProAdmissionDate).ToColumn("ValidUptoProAdmissionDate")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
            .Map(m => m.Attribute3).ToColumn("Attribute3")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .DoNotMap(m => m.CGPA)
            .DoNotMap(m => m.AutoOpenCr)
            .DoNotMap(m => m.AutoPreRegCr)
            .DoNotMap(m => m.AutoMandaotryCr)
            .DoNotMap(m => m.IsChaked)
            .DoNotMap(m => m.FullName)
            .DoNotMap(m => m.IsRegistered)
            .DoNotMap(m => m.Gender)
            .DoNotMap(m => m.RegistrationNo)
            .DoNotMap(m => m.RegSession)
            .Build();

            return mapper;
        }
        #endregion

        public Student GetByRoll(string roll)
        {
            Student _student = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Student> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Student>("StudentByRoll", rowMapper);
                _student = accessor.Execute(roll).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _student;
            }

            return _student;
        }

        public Student GetByRollOrSerialNo(string roll, int serialNo)
        {
            Student _student = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Student> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Student>("StudentInfoVerifyByStudentIdOrSerialNo", rowMapper);
                _student = accessor.Execute(roll, serialNo).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _student;
            }

            return _student;
        }

        public LoadStudentByIdDTO GetByRollEdit(string roll)
        {
            LoadStudentByIdDTO _studentEdit = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<LoadStudentByIdDTO> rowMapper = GetMaperEdit();

                var accessor = db.CreateSprocAccessor<LoadStudentByIdDTO>("StudentInfoEdit", rowMapper);
                _studentEdit = accessor.Execute(roll).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _studentEdit;
            }

            return _studentEdit;
        }

        private IRowMapper<LoadStudentByIdDTO> GetMaperEdit()
        {
            IRowMapper<LoadStudentByIdDTO> mapper = MapBuilder<LoadStudentByIdDTO>.MapAllProperties()
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.FullName).ToColumn("FullName")
            .Map(m => m.FatherName).ToColumn("FatherName")
            .Map(m => m.MotherName).ToColumn("MotherName")
            .Map(m => m.DOB).ToColumn("DOB")
            .Map(m => m.Gender).ToColumn("Gender")
            .Map(m => m.Phone).ToColumn("Phone")
            .Map(m => m.Email).ToColumn("Email")

            .Build();

            return mapper;

        }

        public List<Student> GetAllFromRegWorksheetByProgramAndBatch(int programId, int acaCalId)
        {
            List<Student> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Student> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Student>("StudentGetAllFromRegWorksheetByProgramAndBatch", mapper);
                IEnumerable<Student> collection = accessor.Execute(programId, acaCalId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<RunningStudent> GetRunningStudentByProgramIdAcaCalId(int programId, int acaCalId, int batchId)
        {
            List<RunningStudent> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RunningStudent> mapper = GetRunningStudentMaper();

                var accessor = db.CreateSprocAccessor<RunningStudent>("RunningStudentByProgramAndAcaCalId", mapper);
                IEnumerable<RunningStudent> collection = accessor.Execute(programId, acaCalId, batchId);

                studentList = collection.ToList();
            }
            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<RunningStudent> GetRunningStudentByProgramIdAcaCalIdNew(int programId, int acaCalId, int batchId)
        {
            List<RunningStudent> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RunningStudent> mapper = GetRunningStudentMaper();

                var accessor = db.CreateSprocAccessor<RunningStudent>("RunningStudentByProgramAndAcaCalIdNew", mapper);
                IEnumerable<RunningStudent> collection = accessor.Execute(programId, acaCalId, batchId);

                studentList = collection.ToList();
            }
            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<rStudentCourse> GetRunningStudentCourseByProgramIdAcaCalId(int programId, int acaCalId, int batchId)
        {
            List<rStudentCourse> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentCourse> mapper = GetRunningStudentCourseMaper();

                var accessor = db.CreateSprocAccessor<rStudentCourse>("RunningStudentCourseListByProgramIdAcaCalId", mapper);
                IEnumerable<rStudentCourse> collection = accessor.Execute(programId, acaCalId, batchId);

                studentList = collection.ToList();
            }
            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        private IRowMapper<rStudentCourse> GetRunningStudentCourseMaper()
        {
            IRowMapper<rStudentCourse> mapper = MapBuilder<rStudentCourse>.MapAllProperties()
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.Name).ToColumn("FullName")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.Title).ToColumn("Title")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.BatchId).ToColumn("BatchId")


            .Build();

            return mapper;
        }

        private IRowMapper<RunningStudent> GetRunningStudentMaper()
        {
            IRowMapper<RunningStudent> mapper = MapBuilder<RunningStudent>.MapAllProperties()
            .Map(m => m.Roll).ToColumn("Roll")


            .Build();

            return mapper;
        }

        //private IRowMapper<RunningStudent> GetRunningStudentMaper()
        //{
        //    IRowMapper<RunningStudent> mapper = MapBuilder<RunningStudent>.MapAllProperties()
        //     .Map(m => m.Roll).ToColumn("Roll")            

        //     .Build();

        //    return mapper;
        //}

        public List<Student> GetFromRegWorksheetByStudentRoll(string roll)
        {
            List<Student> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Student> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Student>("StudentGetAllFromRegWorksheetByStudentRoll", mapper);
                IEnumerable<Student> collection = accessor.Execute(roll);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<Student> GetAllByProgramOrBatchOrRoll(int programId, int acaCalId, string roll)
        {
            List<Student> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Student> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Student>("StudentGetByProgramOrBatchOrRoll", mapper);
                IEnumerable<Student> collection = accessor.Execute(programId, acaCalId, roll);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<Student> GetAllByBatchProgram(string batch, string program)
        {
            List<Student> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Student> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Student>(sqlGetByProgramOrBatchOrRoll, mapper);
                IEnumerable<Student> collection = accessor.Execute(Convert.ToInt32(program), Convert.ToInt32(batch), "");

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<Student> GetAllByProgramOrBatchOrRoll(int programId, int acaCalId, string rollFrom, string rollTo)
        {
            List<Student> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Student> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Student>("StudentGetByProgramOrBatchOrRollRange", mapper);
                IEnumerable<Student> collection = accessor.Execute(programId, acaCalId, rollFrom, rollTo);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<Student> StudentGetAllActiveInactiveWithRegistrationStatus(int programId, int acaCalBatchId, int acaCalSessionId, string roll)
        {
            List<Student> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Student> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Student>("StudentGetAllActiveInactiveWithRegistrationStatus", mapper);
                IEnumerable<Student> collection = accessor.Execute(programId, acaCalBatchId, acaCalSessionId, roll);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<Student> GetAllRegisteredStudentBySession(int acaCalSessionId)
        {
            List<Student> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Student> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Student>("StudentGetAllRegisteredStudentBySession", mapper);
                IEnumerable<Student> collection = accessor.Execute(acaCalSessionId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<rStudentMajorDefine> GetAllStudentByMajorDefine(int programId, int batchId, string roll)
        {
            List<rStudentMajorDefine> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentMajorDefine> mapper = MapBuilder<rStudentMajorDefine>.MapAllProperties()
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.CompletedCr).ToColumn("CompletedCr")
            .Map(m => m.Major1Name).ToColumn("Major1Name")
            .Map(m => m.Major1NodeID).ToColumn("Major1NodeID")
            .Map(m => m.Major2Name).ToColumn("Major2Name")
            .Map(m => m.Major2NodeID).ToColumn("Major2NodeID")
            .Build();

                var accessor = db.CreateSprocAccessor<rStudentMajorDefine>("rptStudentMajorDefine", mapper);
                IEnumerable<rStudentMajorDefine> collection = accessor.Execute(batchId, programId, roll);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<StudentBlockCountByProgramDTO> GetAllBlockStudentByProgram()
        {
            List<StudentBlockCountByProgramDTO> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentBlockCountByProgramDTO> mapper = MapBuilder<StudentBlockCountByProgramDTO>.MapAllProperties()
            .Map(m => m.StudentCount).ToColumn("StudentCount")
            .Map(m => m.Code).ToColumn("Code")
            .Map(m => m.DetailName).ToColumn("DetailName")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.ShortName).ToColumn("ShortName")
            .Build();

                var accessor = db.CreateSprocAccessor<StudentBlockCountByProgramDTO>("StudentBlockCountByProgram", mapper);
                IEnumerable<StudentBlockCountByProgramDTO> collection = accessor.Execute();

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public bool DeleteAllBlockStudentByProgram(int programId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("StudentBlockDeleteAllByProgram");

                db.AddInParameter(cmd, "programId", DbType.Int32, programId);
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

        public List<StudentBlockCountByProgramDTO> GetAllInActiveStudentByProgram()
        {
            List<StudentBlockCountByProgramDTO> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentBlockCountByProgramDTO> mapper = MapBuilder<StudentBlockCountByProgramDTO>.MapAllProperties()
                .Map(m => m.StudentCount).ToColumn("StudentCount")
                .Map(m => m.Code).ToColumn("Code")
                .Map(m => m.DetailName).ToColumn("DetailName")
                .Map(m => m.ProgramID).ToColumn("ProgramID")
                .Map(m => m.ShortName).ToColumn("ShortName")
                .Build();

                var accessor = db.CreateSprocAccessor<StudentBlockCountByProgramDTO>("StudentInActiveCountByProgram", mapper);
                IEnumerable<StudentBlockCountByProgramDTO> collection = accessor.Execute();

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public bool UpdateInActiveToActiveByProgram(int programId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("UpdateInActiveToActiveByProgram");

                db.AddInParameter(cmd, "programId", DbType.Int32, programId);
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

        public List<StudentProbationDTO> GetAllByProbationStatus(int programId, int acaCalBatchId, int? minProb, int? maxProb)
        {
            List<StudentProbationDTO> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentProbationDTO> mapper = MapBuilder<StudentProbationDTO>.MapAllProperties()
                .Map(m => m.AdmissionCalenderID).ToColumn("AdmissionCalenderID")
                .Map(m => m.BatchCode).ToColumn("Code")
                .Map(m => m.CGPA).ToColumn("CGPA")
                .Map(m => m.GPA).ToColumn("GPA")
                .Map(m => m.IsBlock).ToColumn("IsBlock")
                .Map(m => m.Name).ToColumn("Name")
                .Map(m => m.ProbationCount).ToColumn("ProbationCount")
                .Map(m => m.ProgramID).ToColumn("ProgramID")
                .Map(m => m.Remarks).ToColumn("Remarks")
                .Map(m => m.Roll).ToColumn("Roll")
                .Map(m => m.StudentID).ToColumn("StudentID")
                .Build();

                var accessor = db.CreateSprocAccessor<StudentProbationDTO>("StudentGetAllByProbationStatus", mapper);
                IEnumerable<StudentProbationDTO> collection = accessor.Execute(programId, acaCalBatchId, minProb, maxProb);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<rStudentBatchWise> GetStudentProgramOrBatch(int programId, int batchId)
        {
            List<rStudentBatchWise> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentBatchWise> mapper = MapBuilder<rStudentBatchWise>.MapAllProperties()
                .Map(m => m.StudentID).ToColumn("Roll")
                .Map(m => m.BatchNO).ToColumn("BatchNO")
                .Map(m => m.FullName).ToColumn("FullName")
                .Map(m => m.DateOfBirth).ToColumn("DOB")
                .Map(m => m.Email).ToColumn("Email")
                .Map(m => m.Phone).ToColumn("Phone")
                .Map(m => m.Gender).ToColumn("Gender")
                .Map(m => m.PhotoPath).ToColumn("PhotoPath")
                .DoNotMap(m => m.PresentAddress)
                .DoNotMap(m => m.PermanentAddress)
                .Build();

                var accessor = db.CreateSprocAccessor<rStudentBatchWise>("RptStudentByBatchAndProgram2", mapper);
                IEnumerable<rStudentBatchWise> collection = accessor.Execute(batchId, programId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<rStudentBatchWise> GetStudentYearOrSemesterOrCalenderUnitOrProgramOrBatch(string year, int semesterId, int calenderUnitTypeId, int programId, int batchId)
        {
            // Here year is the first two digit of Roll to be mathched with Padded 0

            List<rStudentBatchWise> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentBatchWise> mapper = GetNewMaper();

                var accessor = db.CreateSprocAccessor<rStudentBatchWise>("RptStudentByBatchAndProgram", mapper);
                IEnumerable<rStudentBatchWise> collection = accessor.Execute(year, semesterId, calenderUnitTypeId, batchId, programId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        private IRowMapper<rStudentBatchWise> GetNewMaper()
        {
            IRowMapper<rStudentBatchWise> mapper = MapBuilder<rStudentBatchWise>.MapAllProperties()
            .Map(m => m.StudentID).ToColumn("Roll")
            .Map(m => m.BatchNO).ToColumn("BatchNO")
            .Map(m => m.FullName).ToColumn("FullName")
            .Map(m => m.DateOfBirth).ToColumn("DOB")
            .Map(m => m.Email).ToColumn("Email")
            .Map(m => m.Phone).ToColumn("Phone")
            .Map(m => m.Gender).ToColumn("Gender")
            .Map(m => m.PhotoPath).ToColumn("PhotoPath")
            .Map(m => m.PresentAddress).ToColumn("PresentAddress")
            .Map(m => m.PermanentAddress).ToColumn("PermanentAddress")
            .Build();

            return mapper;
        }

        #region Student Transcript

        public List<rStudentTranscript> GetStudentTrancriptById(string studentId)
        {
            List<rStudentTranscript> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentTranscript> mapper = GetTranscriptMaper();

                var accessor = db.CreateSprocAccessor<rStudentTranscript>("RptStudentTrancriptById", mapper);
                IEnumerable<rStudentTranscript> collection = accessor.Execute(studentId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }
        public List<rStudentTranscriptNew> GetStudentTrancriptByIdNew(string studentId)
        {
            List<rStudentTranscriptNew> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentTranscriptNew> mapper = GetTranscriptMaperNew();

                var accessor = db.CreateSprocAccessor<rStudentTranscriptNew>("RptStudentTrancriptByIdNew", mapper);
                IEnumerable<rStudentTranscriptNew> collection = accessor.Execute(studentId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }
        private IRowMapper<rStudentTranscriptNew> GetTranscriptMaperNew()
        {
            IRowMapper<rStudentTranscriptNew> mapper = MapBuilder<rStudentTranscriptNew>.MapAllProperties()
            .Map(m => m.AcaCalId).ToColumn("AcaCalId")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.Year).ToColumn("Year")
            .Map(m => m.TypeName).ToColumn("TypeName")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.Title).ToColumn("Title")
            .Map(m => m.Credits).ToColumn("Credits")
            .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
            .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
            .Map(m => m.TranscriptCredit).ToColumn("TranscriptCredit")
            .Map(m => m.TranscriptGPA).ToColumn("TranscriptGPA")
            .Map(m => m.TranscriptCGPA).ToColumn("TranscriptCGPA")

            .Build();

            return mapper;
        }

        private IRowMapper<rStudentTranscript> GetTranscriptMaper()
        {
            IRowMapper<rStudentTranscript> mapper = MapBuilder<rStudentTranscript>.MapAllProperties()
            .Map(m => m.Year).ToColumn("Year")
            .Map(m => m.Code).ToColumn("TypeName")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.Title).ToColumn("Title")
            .Map(m => m.Credits).ToColumn("Credits")
            .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
            .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
            .Map(m => m.AcaCalID).ToColumn("AcaCalID")
            .Map(m => m.StudentName).ToColumn("FullName")
            .Map(m => m.DepartmentName).ToColumn("DepartmentName")
            .Map(m => m.ProgrameName).ToColumn("DetailName")
            .Map(m => m.StudentId).ToColumn("Roll")
            .Map(m => m.FatherName).ToColumn("FatherName")
            .Map(m => m.DateOfBirth).ToColumn("DOB")
            .Map(m => m.Major).ToColumn("Major")
            .Map(m => m.OCredits).ToColumn("OCredits")
            .Map(m => m.CATotal).ToColumn("CATotal")
            .Map(m => m.STCreditsTotal).ToColumn("STCreditsTotal")

            .Build();

            return mapper;
        }

        public List<rStudentTranscript> GetStudentTrancriptByIdRunning(string studentId)
        {
            List<rStudentTranscript> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentTranscript> mapper = GetTranscriptRunningMaper();

                var accessor = db.CreateSprocAccessor<rStudentTranscript>("RptStudentTrancriptByIdRunning", mapper);
                IEnumerable<rStudentTranscript> collection = accessor.Execute(studentId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        private IRowMapper<rStudentTranscript> GetTranscriptRunningMaper()
        {
            IRowMapper<rStudentTranscript> mapper = MapBuilder<rStudentTranscript>.MapAllProperties()
            .Map(m => m.Year).ToColumn("Year")
            .Map(m => m.Code).ToColumn("TypeName")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.Title).ToColumn("Title")
            .Map(m => m.Credits).ToColumn("Credits")
            .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
            .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
            .Map(m => m.AcaCalID).ToColumn("AcaCalID")
            .Map(m => m.StudentName).ToColumn("FullName")
            .Map(m => m.DepartmentName).ToColumn("DepartmentName")
            .Map(m => m.ProgrameName).ToColumn("DetailName")
            .Map(m => m.StudentId).ToColumn("Roll")
            .Map(m => m.FatherName).ToColumn("FatherName")
            .Map(m => m.DateOfBirth).ToColumn("DOB")
            .Map(m => m.Major).ToColumn("Major")
            .Map(m => m.OCredits).ToColumn("OCredits")
            .Map(m => m.CATotal).ToColumn("CATotal")
            .Map(m => m.STCreditsTotal).ToColumn("STCreditsTotal")

            .Build();

            return mapper;
        }

        #endregion Student Transcript

        public List<Student> GetAllRegisteredStudentByProgramSessionCourse(int programId, int sessionId, int courseId, int versionId)
        {
            List<Student> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Student> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Student>("StudentGetAllRegisteredStudentByProgramSessionCourse", mapper);
                IEnumerable<Student> collection = accessor.Execute(programId, sessionId, courseId, versionId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<StudentDiscountInitialDTO> GetAllDiscountInitialByProgramBatchRoll(int programId, int batchId, string roll)
        {
            List<StudentDiscountInitialDTO> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDiscountInitialDTO> mapper = GetDiscountInitialMaper();

                var accessor = db.CreateSprocAccessor<StudentDiscountInitialDTO>("StudentDiscountByProgramBatchStudentId", mapper);
                IEnumerable<StudentDiscountInitialDTO> collection = accessor.Execute(programId, batchId, roll);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        private IRowMapper<StudentDiscountInitialDTO> GetDiscountInitialMaper()
        {
            IRowMapper<StudentDiscountInitialDTO> mapper = MapBuilder<StudentDiscountInitialDTO>.MapAllProperties()
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.PersonID).ToColumn("PersonID")
            .Map(m => m.BatchId).ToColumn("BatchId")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.SessionId).ToColumn("SessionId")
            .Map(m => m.StudentDiscountId).ToColumn("StudentDiscountId")
            .Map(m => m.StudentDiscountInitialDetailsId).ToColumn("StudentDiscountInitialDetailsId")
            .Map(m => m.TypeDefinitionId).ToColumn("TypeDefinitionId")
            .Map(m => m.TypePercentage).ToColumn("TypePercentage")
            .Map(m => m.SessionId).ToColumn("SessionId")

            .Build();
            return mapper;
        }

        public List<RunningStudent> GetStudentListByProgramAndBatch(int programId, int batchId)
        {
            List<RunningStudent> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RunningStudent> mapper = GetStudentListByProgramAndBatchMaper();

                var accessor = db.CreateSprocAccessor<RunningStudent>("RptStudentListByProgramAndBatch", mapper);
                IEnumerable<RunningStudent> collection = accessor.Execute(programId, batchId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        private IRowMapper<RunningStudent> GetStudentListByProgramAndBatchMaper()
        {
            IRowMapper<RunningStudent> mapper = MapBuilder<RunningStudent>.MapAllProperties()
            .Map(m => m.Roll).ToColumn("Roll")
            .Build();

            return mapper;
        }

        public List<rStudentTranscript> GetConsolidatedCrAssessment(string studentId)
        {
            List<rStudentTranscript> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentTranscript> mapper = GetConsolidatedCrAssessmentMaper();

                var accessor = db.CreateSprocAccessor<rStudentTranscript>("RptConsolidatedCrAssessment", mapper);
                IEnumerable<rStudentTranscript> collection = accessor.Execute(studentId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rStudentTranscript> GetConsolidatedCrAssessmentMaper()
        {
            IRowMapper<rStudentTranscript> mapper = MapBuilder<rStudentTranscript>.MapAllProperties()
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.Title).ToColumn("Title")
            .Map(m => m.Credits).ToColumn("Credits")
            .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
            .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")

            .DoNotMap(m => m.Year)
            .DoNotMap(m => m.Code)
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.Title).ToColumn("Title")
            .Map(m => m.Credits).ToColumn("Credits")
            .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
            .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
            .DoNotMap(m => m.AcaCalID)
            .Map(m => m.StudentName).ToColumn("FullName")
            .Map(m => m.DepartmentName).ToColumn("DepartmentName")
            .Map(m => m.ProgrameName).ToColumn("DetailName")
            .Map(m => m.StudentId).ToColumn("Roll")
            .Map(m => m.FatherName).ToColumn("FatherName")
            .Map(m => m.DateOfBirth).ToColumn("DOB")
            .Map(m => m.Major).ToColumn("Major")
            .DoNotMap(m => m.OCredits)
            .DoNotMap(m => m.CATotal)
            .DoNotMap(m => m.STCreditsTotal)

            .Build();

            return mapper;
        }

        public List<rStudentGPACGPA> GetStudentGPACGPAByRoll(string studentId)
        {
            List<rStudentGPACGPA> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentGPACGPA> mapper = GetStudentGPACGPAByRollMaper();

                var accessor = db.CreateSprocAccessor<rStudentGPACGPA>("RptStudentGPACGPA", mapper);
                IEnumerable<rStudentGPACGPA> collection = accessor.Execute(studentId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rStudentGPACGPA> GetStudentGPACGPAByRollMaper()
        {
            IRowMapper<rStudentGPACGPA> mapper = MapBuilder<rStudentGPACGPA>.MapAllProperties()
            .Map(m => m.StdAcademicCalenderID).ToColumn("StdAcademicCalenderID")
            .Map(m => m.Credit).ToColumn("Credit")
            .Map(m => m.GPA).ToColumn("GPA")
            .Map(m => m.CompletedCredit).ToColumn("CompletedCredit")
            .Map(m => m.CGPA).ToColumn("CGPA")
            .Map(m => m.FullName).ToColumn("FullName")
            .Map(m => m.Year).ToColumn("Year")
            .Map(m => m.TypeName).ToColumn("TypeName")
            .Map(m => m.TotalCredit).ToColumn("TotalCredit")

            .Build();

            return mapper;
        }

        public List<rStudentByProgramAndBatch> GetStudentByProgramIdBatchId(int programId, int batchId)
        {
            List<rStudentByProgramAndBatch> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentByProgramAndBatch> mapper = GetStudentByProgramIdBatchIdMaper();

                var accessor = db.CreateSprocAccessor<rStudentByProgramAndBatch>("RptStudentByProgramAndBatch", mapper);
                IEnumerable<rStudentByProgramAndBatch> collection = accessor.Execute(programId, batchId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rStudentByProgramAndBatch> GetStudentByProgramIdBatchIdMaper()
        {
            IRowMapper<rStudentByProgramAndBatch> mapper = MapBuilder<rStudentByProgramAndBatch>.MapAllProperties()
           .Map(m => m.Roll).ToColumn("Roll")

           .Build();

            return mapper;
        }

        public rStudentTranscriptGeneralInfo GetStudentTrancriptGeneralInfoById(string studentId)
        {
            rStudentTranscriptGeneralInfo studentGeneralInfo = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentTranscriptGeneralInfo> mapper = GetStudentTrancriptGeneralInfoMaper();

                var accessor = db.CreateSprocAccessor<rStudentTranscriptGeneralInfo>("RptStudentTrancriptGeneralInfo", mapper);
                studentGeneralInfo = accessor.Execute(studentId).SingleOrDefault();
            }

            catch (Exception ex)
            {
                return studentGeneralInfo;
            }

            return studentGeneralInfo;
        }

        private IRowMapper<rStudentTranscriptGeneralInfo> GetStudentTrancriptGeneralInfoMaper()
        {
            IRowMapper<rStudentTranscriptGeneralInfo> mapper = MapBuilder<rStudentTranscriptGeneralInfo>.MapAllProperties()
           .Map(m => m.FullName).ToColumn("FullName")
           .Map(m => m.FatherName).ToColumn("FatherName")
           .Map(m => m.DepartmentName).ToColumn("DepartmentName")
           .Map(m => m.DOB).ToColumn("DOB")
           .Map(m => m.ProgramName).ToColumn("ProgramName")
           .Map(m => m.Major).ToColumn("Major")

           .Build();

            return mapper;
        }

        #region IStudentRepository Members

        public List<StudentDTO> GetAllDTOByProgramOrBatchOrRoll(int programId, int acaCalId, string roll)
        {
            List<StudentDTO> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDTO> mapper = GetDTOMaper();

                var accessor = db.CreateSprocAccessor<StudentDTO>("StudentDTOGetByProgramOrBatchOrRoll", mapper);
                IEnumerable<StudentDTO> collection = accessor.Execute(programId, acaCalId, roll);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        private IRowMapper<StudentDTO> GetDTOMaper()
        {
            IRowMapper<StudentDTO> mapper = MapBuilder<StudentDTO>.MapAllProperties()
               .Map(m => m.StudentID).ToColumn("StudentID")
               .Map(m => m.Roll).ToColumn("Roll")
               .Map(m => m.Name).ToColumn("Name")
               .Map(m => m.PersonID).ToColumn("PersonID")
               .Map(m => m.CandidateId).ToColumn("CandidateId")
               .Map(m => m.ProgramID).ToColumn("ProgramID")
               .Map(m => m.Program).ToColumn("Program")
               .Map(m => m.BatchId).ToColumn("BatchId")
               .Map(m => m.Batch).ToColumn("Batch")
               .Map(m => m.TreeMasterID).ToColumn("TreeMasterID")
               .Map(m => m.TreeCalendarMasterID).ToColumn("TreeCalendarMasterID")
               .Map(m => m.IsBlock).ToColumn("IsBlock")
               .Map(m => m.IsActive).ToColumn("IsActive")
               .Map(m => m.IsDeleted).ToColumn("IsDeleted")
               .Map(m => m.IsDiploma).ToColumn("IsDiploma")
               .Map(m => m.Remarks).ToColumn("Remarks")
               .Map(m => m.AccountHeadsID).ToColumn("AccountHeadsID")
               .Map(m => m.IsCompleted).ToColumn("IsCompleted")
               .Map(m => m.CompletedAcaCalId).ToColumn("CompletedAcaCalId")
               .Map(m => m.TranscriptSerial).ToColumn("TranscriptSerial")
               .Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")
               .Map(m => m.AcademicCalenderYear).ToColumn("AcademicCalenderYear")
               .Map(m => m.CGPA).ToColumn("CGPA")
               .Map(m => m.GPA).ToColumn("GPA")
               .Build();

            return mapper;
        }

        public List<StudentDTO> GetAllDTOHasInitialDiscountGetByProgramOrBatchOrRoll(int programId, int batchId, string roll)
        {
            List<StudentDTO> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDTO> mapper = GetDTOMaper();

                var accessor = db.CreateSprocAccessor<StudentDTO>("StudentDTOHasInitialDiscountGetByProgramOrBatchOrRoll", mapper);
                IEnumerable<StudentDTO> collection = accessor.Execute(programId, batchId, roll);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<StudentDTO> GetAllDTOHasInitialDiscountGetByProgramOrBatchOrRoll(int programId, int batchId, string roll, int sessionId, int resultSessionId)
        {
            List<StudentDTO> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDTO> mapper = GetDTOMaper();

                var accessor = db.CreateSprocAccessor<StudentDTO>("StudentDTOHasInitialDiscountGetByProgramOrBatchOrRollV2", mapper);
                IEnumerable<StudentDTO> collection = accessor.Execute(programId, batchId, roll, sessionId, resultSessionId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<StudentDTO> GetAllDTOForSiblingByProgramOrBatchOrRoll(int programId, int acaCalId, int relationDiscountType, string roll)
        {
            List<StudentDTO> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDTO> mapper = GetDTOMaper();

                var accessor = db.CreateSprocAccessor<StudentDTO>("StudentDTOForSiblingGetByProgramOrBatchOrRoll", mapper);
                IEnumerable<StudentDTO> collection = accessor.Execute(programId, acaCalId, relationDiscountType, roll);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<StudentDTO> GetDTOAllBySiblingGroupId(int groupId)
        {
            List<StudentDTO> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDTO> mapper = GetDTOMaper();

                var accessor = db.CreateSprocAccessor<StudentDTO>("StudentDTOBySiblingGroupId", mapper);
                IEnumerable<StudentDTO> collection = accessor.Execute(groupId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        #endregion

        public List<rStudentByProgramAndBatch> GetCompleteStudentByProgramIdBatchId(int programId, int batchId, int acaCalId)
        {
            List<rStudentByProgramAndBatch> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentByProgramAndBatch> mapper = GetStudentByProgramIdBatchIdMaper();

                var accessor = db.CreateSprocAccessor<rStudentByProgramAndBatch>("StudentGetAllCompletedByProgramAndBatchV2", mapper);
                IEnumerable<rStudentByProgramAndBatch> collection = accessor.Execute(programId, batchId, acaCalId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<StudentDTO> GetAllDTOByProgramBatchResultSessionRoll(int programId, int acaCalId, int sessionId, int resultSessionId, string roll)
        {
            List<StudentDTO> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentDTO> mapper = GetDTOMaper();

                var accessor = db.CreateSprocAccessor<StudentDTO>("StudentDTOByProgramBatchResultSessionRoll", mapper);
                IEnumerable<StudentDTO> collection = accessor.Execute(programId, acaCalId, roll, sessionId, resultSessionId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<rStudentResultProgramBatch> GetStudentGPACGPAByProgramBatchSession(int programId, int BatchId, int sessionId, int SemesterNo)
        {
            List<rStudentResultProgramBatch> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentResultProgramBatch> mapper = MapBuilder<rStudentResultProgramBatch>.MapAllProperties()
                .Map(m => m.GPA).ToColumn("TranscriptGPA")
                .Map(m => m.CGPA).ToColumn("TranscriptCGPA")
                .Map(m => m.FullName).ToColumn("FullName")
                .Map(m => m.Roll).ToColumn("Roll")

                .Build();

                var accessor = db.CreateSprocAccessor<rStudentResultProgramBatch>("RptStudentGPACGPAByProgramBatchSession", mapper);
                IEnumerable<rStudentResultProgramBatch> collection = accessor.Execute(programId, BatchId, sessionId, SemesterNo);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<rStudentRollSheet> GetStudentRollSheetByProgramBatchSession(int programId, int BatchId, int SessoinId, int ExamCenterId, int InstitutionId)
        {
            List<rStudentRollSheet> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentRollSheet> mapper = MapBuilder<rStudentRollSheet>.MapAllProperties()
                .Map(m => m.BatchNO).ToColumn("BatchNO")
                .Map(m => m.Gender).ToColumn("Gender")
                .Map(m => m.FormalCode).ToColumn("FormalCode")
                .Map(m => m.Title).ToColumn("Title")
                .Map(m => m.FullName).ToColumn("FullName")
                .Map(m => m.Roll).ToColumn("Roll")
                .Map(m => m.SectionName).ToColumn("SectionName")
                .Map(m => m.StudentID).ToColumn("StudentID")
                .Map(m => m.RegistrationNo).ToColumn("RegistrationNo")
                .Map(m => m.SessionName).ToColumn("SessionName")
                .Map(m => m.ExamCenterName).ToColumn("ExamCenterName")
                .Map(m => m.AffiliateInstitutionName).ToColumn("AffiliateInstitutionName")

                .Build();

                var accessor = db.CreateSprocAccessor<rStudentRollSheet>("StudentListByProgramSemesterCourses", mapper);
                IEnumerable<rStudentRollSheet> collection = accessor.Execute(programId, BatchId, SessoinId, ExamCenterId, InstitutionId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<StudentCountProgramBatchWise> GetStudentCountProgramBatchWiseByAcaCalIdProgramId(int ProgramId, int AcaCalId)
        {
            List<StudentCountProgramBatchWise> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentCountProgramBatchWise> mapper = MapBuilder<StudentCountProgramBatchWise>.MapAllProperties()
                .Map(m => m.BatchId).ToColumn("BatchId")
                .Map(m => m.BatchNO).ToColumn("BatchNO")
                .Map(m => m.StudentCount).ToColumn("StudentCount")

                .Build();

                var accessor = db.CreateSprocAccessor<StudentCountProgramBatchWise>("StudentCountProgramBatchWiseByProgramIdAcaCalId", mapper);
                IEnumerable<StudentCountProgramBatchWise> collection = accessor.Execute(ProgramId, AcaCalId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<rStudentTabulationSheet> GetStudentTabulationSheetByProgramBatchSession(int programId, int BatchId, int SessoinId)
        {
            List<rStudentTabulationSheet> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentTabulationSheet> mapper = MapBuilder<rStudentTabulationSheet>.MapAllProperties()
                .Map(m => m.BatchNO).ToColumn("BatchNO")
                .Map(m => m.Gender).ToColumn("Gender")
                .Map(m => m.FormalCode).ToColumn("FormalCode")
                .Map(m => m.FullName).ToColumn("FullName")
                .Map(m => m.Roll).ToColumn("Roll")
                .Map(m => m.SectionName).ToColumn("SectionName")
                .Map(m => m.StudentID).ToColumn("StudentID")
                .Map(m => m.Title).ToColumn("Title")
                .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
                .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
                .Map(m => m.CourseCredit).ToColumn("CourseCredit")
                .Map(m => m.SemesterNo).ToColumn("SemesterNo")
                .Map(m => m.Seq).ToColumn("Seq")
                .Map(m => m.GPA).ToColumn("GPA")
                .Map(m => m.EarnedCr).ToColumn("EarnedCr")
                .Map(m => m.EnrolledCr).ToColumn("EnrolledCr")
                .Map(m => m.CGPA).ToColumn("CGPA")
                .Map(m => m.TEarnedCr).ToColumn("TotalEarnedCr")
                .Map(m => m.TEnrolledCr).ToColumn("TotalEnrolledCr")
                .Map(m => m.RegistrationNo).ToColumn("RegistrationNo")
                .Map(m => m.RegistrationSession).ToColumn("RegistrationSession")
                .Map(m => m.TabulationRemarks).ToColumn("TabulationRemarks")
                .Map(m => m.InstitutionName).ToColumn("InstitutionName")
                .Map(m => m.ExamCenterName).ToColumn("ExamCenterName")


                .Build();

                var accessor = db.CreateSprocAccessor<rStudentTabulationSheet>("StudentTabulationListByProgramSemesterCourses", mapper);
                IEnumerable<rStudentTabulationSheet> collection = accessor.Execute(programId, BatchId, SessoinId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<StudentBillCourseCountDTO> GetStudentForBillPosting(int sessionId, int programId, int batchId)
        {
            List<StudentBillCourseCountDTO> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentBillCourseCountDTO> mapper = MapBuilder<StudentBillCourseCountDTO>.MapAllProperties()
                .Map(m => m.StudentId).ToColumn("StudentId")
                .Map(m => m.Roll).ToColumn("Roll")
                .Map(m => m.StudentName).ToColumn("StudentName")
                .Map(m => m.TrimesterBill).ToColumn("TrimesterBill")
                .Map(m => m.TheoryCourseCount).ToColumn("TheoryCourseCount")
                .Map(m => m.LabCourseCount).ToColumn("LabCourseCount")
                .Map(m => m.ProjectCount).ToColumn("ProjectCount")
                .Build();

                var accessor = db.CreateSprocAccessor<StudentBillCourseCountDTO>("StudentForBillPostingByProgramBatchSession", mapper);
                IEnumerable<StudentBillCourseCountDTO> collection = accessor.Execute(programId, batchId, sessionId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<rStudentGradeCertificateInfo> GetStudentGradeCertificateInfoByRoll(string Roll, int sessionId, int semesterId)
        {
            List<rStudentGradeCertificateInfo> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentGradeCertificateInfo> mapper = MapBuilder<rStudentGradeCertificateInfo>.MapAllProperties()
                .Map(m => m.Roll).ToColumn("Roll")
                .Map(m => m.FullName).ToColumn("FullName")
                .Map(m => m.RegistrationNo).ToColumn("RegistrationNo")
                .Map(m => m.BatchInfo).ToColumn("BatchInfo")
                .Map(m => m.SessionInfo).ToColumn("SessionInfo")
                .Map(m => m.Program).ToColumn("Program")
                .Map(m => m.Faculty).ToColumn("Faculty")
                .Map(m => m.TranscriptCGPA).ToColumn("TranscriptCGPA")
                .Map(m => m.AttemptedCredit).ToColumn("AttemptedCredit")
                .Map(m => m.EarnedCredit).ToColumn("EarnedCredit")
                .Map(m => m.SemesterInfo).ToColumn("SemesterInfo")
                .Map(m => m.Remarks).ToColumn("ResultRemarks")

                .Build();

                var accessor = db.CreateSprocAccessor<rStudentGradeCertificateInfo>("RptStudentGradeCertificateGeneralInfoByRoll", mapper);
                IEnumerable<rStudentGradeCertificateInfo> collection = accessor.Execute(Roll, sessionId, semesterId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }
        
        public List<StudentIdCardInfo> GetStudentIdCardInfoByRoll(string Roll)
        {
            List<StudentIdCardInfo> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentIdCardInfo> mapper = MapBuilder<StudentIdCardInfo>.MapAllProperties()
                .Map(m => m.Roll).ToColumn("Roll")
                .Map(m => m.FullName).ToColumn("FullName")
                .Map(m => m.DegreeName).ToColumn("DegreeName")
                .Map(m => m.ShortName).ToColumn("ShortName")
                .Map(m => m.BloodGroup).ToColumn("BloodGroup")
                .Map(m => m.PhotoPath).ToColumn("PhotoPath")
                .Map(m => m.SignaturePath).ToColumn("SignaturePath")


                .Build();

                var accessor = db.CreateSprocAccessor<StudentIdCardInfo>("RptGenerateStudentIDCardInfo", mapper);
                IEnumerable<StudentIdCardInfo> collection = accessor.Execute(Roll);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<StudentRollOnly> GetStudentListRollByProgramBatchSession(int sessionId, int programId, int batchId)
        {
            List<StudentRollOnly> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentRollOnly> mapper = MapBuilder<StudentRollOnly>.MapAllProperties()
                .Map(m => m.Roll).ToColumn("Roll")


                .Build();

                var accessor = db.CreateSprocAccessor<StudentRollOnly>("StudentRollOnlyByProgramBatchSession2", mapper);
                IEnumerable<StudentRollOnly> collection = accessor.Execute(programId, sessionId, batchId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<StudentRollOnly> GetStudentListRollByProgramBatchSessionInstitution(int sessionId, int programId, int batchId, int institutionId)
        {
            List<StudentRollOnly> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentRollOnly> mapper = MapBuilder<StudentRollOnly>.MapAllProperties()
                .Map(m => m.Roll).ToColumn("Roll")


                .Build();

                var accessor = db.CreateSprocAccessor<StudentRollOnly>("StudentRollOnlyByProgramBatchSessionInstitution", mapper);
                IEnumerable<StudentRollOnly> collection = accessor.Execute(programId, sessionId, batchId, institutionId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<StudentRollOnly> GetStudentListRollByProgramBatchSemester(int semesterId, int programId, int batchId)
        {
            List<StudentRollOnly> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentRollOnly> mapper = MapBuilder<StudentRollOnly>.MapAllProperties()
                .Map(m => m.Roll).ToColumn("Roll")


                .Build();

                var accessor = db.CreateSprocAccessor<StudentRollOnly>("StudentRollOnlyByProgramBatchSemester", mapper);
                IEnumerable<StudentRollOnly> collection = accessor.Execute(programId, semesterId, batchId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public List<StudentRollOnly> GetStudentListRollByProgramBatchSemesterInstitution(int semesterId, int programId, int batchId, int institutionId)
        {
            List<StudentRollOnly> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentRollOnly> mapper = MapBuilder<StudentRollOnly>.MapAllProperties()
                .Map(m => m.Roll).ToColumn("Roll")


                .Build();

                var accessor = db.CreateSprocAccessor<StudentRollOnly>("StudentRollOnlyByProgramBatchSemesterInstitution", mapper);
                IEnumerable<StudentRollOnly> collection = accessor.Execute(programId, semesterId, batchId, institutionId);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        } 

        public List<StudentInfoCourseAssign> GetAllRegisteredStudentByProgramYearBatchCourseCompCredit(int ProgramId, int BatchId, int YearId, int CompCr, int courseId, int versionId)
        {
            List<StudentInfoCourseAssign> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StudentInfoCourseAssign> mapper = MapBuilder<StudentInfoCourseAssign>.MapAllProperties()

                .Map(m => m.StudentID).ToColumn("StudentID")
                .Map(m => m.Roll).ToColumn("Roll")
                .Map(m => m.FullName).ToColumn("FullName")
                .Map(m => m.FirstCompCredit).ToColumn("FirstCompCredit")
                .Map(m => m.SecondCompCredit).ToColumn("SecondCompCredit")
                .Map(m => m.ThirdCompCredit).ToColumn("ThirdCompCredit")

                .Build();

                var accessor = db.CreateSprocAccessor<StudentInfoCourseAssign>("GetAllRegisteredStudentByProgramYearBatchCourseCompCredit", mapper);
                IEnumerable<StudentInfoCourseAssign> collection = accessor.Execute(ProgramId, BatchId, YearId, courseId, versionId, CompCr);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }

        public rStudentGeneralInfo GetStudentGeneralInfoById(int studentId)
        {
            rStudentGeneralInfo studentGeneralInfo = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentGeneralInfo> mapper = MapBuilder<rStudentGeneralInfo>.MapAllProperties()

                .Map(m => m.StudentId).ToColumn("StudentId")
                .Map(m => m.RegistrationNo).ToColumn("RegistrationNo")
                .Map(m => m.Roll).ToColumn("Roll")
                .Map(m => m.FullName).ToColumn("FullName")
                .Map(m => m.FullNameInBangla).ToColumn("FullNameInBangla")
                .Map(m => m.FatherName).ToColumn("FatherName")
                .Map(m => m.MotherName).ToColumn("MotherName")
                .Map(m => m.GuardianName).ToColumn("GuardianName")
                .Map(m => m.DOB).ToColumn("DOB")
                .Map(m => m.Phone).ToColumn("Phone")
                .Map(m => m.ProgramName).ToColumn("ProgramName")
                .Map(m => m.ProgNameInBan).ToColumn("ProgNameInBan")
                .Map(m => m.ShortName).ToColumn("ShortName")
                .Map(m => m.PhotoPath).ToColumn("PhotoPath")
                .Map(m => m.YearNo).ToColumn("YearNo")
                

               .Build();

                var accessor = db.CreateSprocAccessor<rStudentGeneralInfo>("RptStudentTrancriptGeneralInfo", mapper);
                studentGeneralInfo = accessor.Execute(studentId).SingleOrDefault();
            }

            catch (Exception ex)
            {
                return studentGeneralInfo;
            }

            return studentGeneralInfo;
        }


        public List<rStudentTranscriptResult> GetTranscriptResultByRoll(string Roll)
        {
            List<rStudentTranscriptResult> studentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentTranscriptResult> mapper = MapBuilder<rStudentTranscriptResult>.MapNoProperties()
                .Map(m => m.Roll).ToColumn("Roll")
                .Map(m => m.SemesterNo).ToColumn("SemesterNo")
                .Map(m => m.CourseSession).ToColumn("CourseSession")
                .Map(m => m.FormalCode).ToColumn("FormalCode")
                .Map(m => m.Title).ToColumn("Title")
                .Map(m => m.CourseCredit).ToColumn("CourseCredit")
                .Map(m => m.ObtainedGrade).ToColumn("ObtainedGrade")
                .Map(m => m.ObtainedGPA).ToColumn("ObtainedGPA")
                .Map(m => m.Enrolled).ToColumn("Enrolled")
                .Map(m => m.Earned).ToColumn("Earned")
                .Map(m => m.GPA).ToColumn("GPA")
                .Map(m => m.Remarks).ToColumn("Remarks")
                .Map(m => m.SemesterName).ToColumn("SemesterName")


                .Build();

                var accessor = db.CreateSprocAccessor<rStudentTranscriptResult>("RptStudentResultForTranscriptByRoll", mapper);
                IEnumerable<rStudentTranscriptResult> collection = accessor.Execute(Roll);

                studentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return studentList;
            }

            return studentList;
        }
    }
}
