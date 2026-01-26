using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLCourseRepository : ICourseRepository
    {
        Database db = null;

        private string sqlInsert = "CourseInsert";
        private string sqlUpdate = "CourseUpdate";
        private string sqlDelete = "CourseDeleteById";
        private string sqlGetById = "CourseGetById";
        private string sqlGetAll = "CourseGetAll";
        private string sqlGetAllOfferedCourse = "GetAllOfferedCourse";
        private string sqlGetAllByProgram = "CourseGetAllByProgram";
        private string sqlGetByCourseIdVersionId = "CourseGetByCourseIdVersionId";
        private string sqlGetAllByTeacherId = "TeacherCoursesByTeacherId";
        private string sqlGetAllFlatCourseByProgram = "RptCourseListByProgram";
        private string sqlGetAllByAcaCalIdProgramId = "CourseGetAllByAcaCalIdProgramId";
        private string sqlGetAllByAcaCalIdStudentRoll = "CourseGetAllByAcaCalIdStudentRoll";

        public int Insert(Course course)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, course, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "CourseID");

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

        public bool Update(Course course)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, course, isInsert);

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

        public List<Course> GetAllByProgram(int ProgramID)
        {
            List<Course> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<Course> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Course>(sqlGetAllByProgram, mapper);
                IEnumerable<Course> collection = accessor.Execute(ProgramID);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<Course> GetAll()
        {
            List<Course> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<Course> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Course>(sqlGetAll, mapper);
                IEnumerable<Course> collection = accessor.Execute();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<Course> GetAllOfferedCourse()
        {
            List<Course> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>(); ;
                IRowMapper<Course> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Course>(sqlGetAllOfferedCourse, mapper);
                IEnumerable<Course> collection = accessor.Execute();

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public Course GetByCourseIdVersionId(int CourseID, int VersionID)
        {
            Course _course = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Course> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Course>(sqlGetByCourseIdVersionId, rowMapper);
                _course = accessor.Execute(CourseID, VersionID).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _course;
            }

            return _course;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Course course, bool isInsert)
        {
            //Must be modification @Sajib
            if (isInsert)
            {
                db.AddOutParameter(cmd, "CourseID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "CourseID", DbType.Int32, course.CourseID);
            }

            //db.AddInParameter(cmd, "CourseID", DbType.Int32, course.CourseID);
            db.AddInParameter(cmd, "VersionID", DbType.Int32, course.VersionID);
            db.AddInParameter(cmd, "FormalCode", DbType.String, course.FormalCode);
            db.AddInParameter(cmd, "VersionCode", DbType.String, course.VersionCode);
            db.AddInParameter(cmd, "Title", DbType.String, course.Title);
            db.AddInParameter(cmd, "AssocCourseID", DbType.Int32, course.AssocCourseID);
            db.AddInParameter(cmd, "AssocVersionID", DbType.Int32, course.AssocVersionID);
            db.AddInParameter(cmd, "StartAcademicCalenderID", DbType.Int32, course.StartAcademicCalenderID);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, course.ProgramID);
            db.AddInParameter(cmd, "CourseContent", DbType.String, course.CourseContent);
            db.AddInParameter(cmd, "IsCreditCourse", DbType.Boolean, course.IsCreditCourse);
            db.AddInParameter(cmd, "Credits", DbType.Decimal, course.Credits);
            db.AddInParameter(cmd, "IsSectionMandatory", DbType.Boolean, course.IsSectionMandatory);
            db.AddInParameter(cmd, "HasEquivalents", DbType.Boolean, course.HasEquivalents);
            db.AddInParameter(cmd, "HasMultipleACUSpan", DbType.Boolean, course.HasMultipleACUSpan);
            db.AddInParameter(cmd, "IsActive", DbType.Boolean, course.IsActive);
            db.AddInParameter(cmd, "TypeDefinitionID", DbType.Int32, course.TypeDefinitionID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, course.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, course.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, course.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, course.ModifiedDate);

            return db;
        }

        private IRowMapper<Course> GetMaper()
        {
            IRowMapper<Course> mapper = MapBuilder<Course>.MapAllProperties()

            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.VersionID).ToColumn("VersionID")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.VersionCode).ToColumn("VersionCode")
            .Map(m => m.Title).ToColumn("Title")
            .Map(m => m.AssocCourseID).ToColumn("AssocCourseID")
            .Map(m => m.AssocVersionID).ToColumn("AssocVersionID")
            .Map(m => m.StartAcademicCalenderID).ToColumn("StartAcademicCalenderID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.CourseContent).ToColumn("CourseContent")
            .Map(m => m.IsCreditCourse).ToColumn("IsCreditCourse")
            .Map(m => m.Credits).ToColumn("Credits")
            .Map(m => m.IsSectionMandatory).ToColumn("IsSectionMandatory")
            .Map(m => m.HasEquivalents).ToColumn("HasEquivalents")
            .Map(m => m.HasMultipleACUSpan).ToColumn("HasMultipleACUSpan")
            .Map(m => m.IsActive).ToColumn("IsActive")
            .Map(m => m.TypeDefinitionID).ToColumn("TypeDefinitionID")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.TranscriptCode).ToColumn("TranscriptCode")
            .Map(m => m.CourseGroup).ToColumn("CourseGroup")
            .Map(m => m.BillTypeDefinitionID).ToColumn("BillTypeDefinitionID")

            .Build();
            return mapper;
        }

        private IRowMapper<RptFlatCourse> GetFlatCourse()
        {
            IRowMapper<RptFlatCourse> mapper = MapBuilder<RptFlatCourse>.MapAllProperties()

            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.ShortName).ToColumn("ShortName")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.Title).ToColumn("Title")

            .Build();

            return mapper;
        }

        private IRowMapper<NodeCourses> GetMaper(string nodeCourses)
        {
            IRowMapper<NodeCourses> mapper = MapBuilder<NodeCourses>.MapAllProperties()

            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.VersionID).ToColumn("VersionID")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.VersionCode).ToColumn("VersionCode")
            .Map(m => m.CourseTitle).ToColumn("CourseTitle")
            .Map(m => m.Node_CourseID).ToColumn("Node_CourseID")
            .Build();

            return mapper;
        }
        #endregion

        public List<NodeCourses> GetAllNodeCoursesByNodeId(int nodeId)
        {
            List<NodeCourses> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<NodeCourses> mapper = GetMaper("NodeCourses");

                var accessor = db.CreateSprocAccessor<NodeCourses>("sp_AllCourseByNode", mapper);
                IEnumerable<NodeCourses> collection = accessor.Execute(nodeId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<CourseListByTeacherDTO> GetCourseByTeacherId(int teacherId)
        {
            List<CourseListByTeacherDTO> courseList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CourseListByTeacherDTO> mapper = GetTeacherCourseMaper();

                var accessor = db.CreateSprocAccessor<CourseListByTeacherDTO>(sqlGetAllByTeacherId, mapper);
                IEnumerable<CourseListByTeacherDTO> collection = accessor.Execute(teacherId);

                courseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return courseList;
            }

            return courseList;
        }

        private IRowMapper<CourseListByTeacherDTO> GetTeacherCourseMaper()
        {
            IRowMapper<CourseListByTeacherDTO> mapper = MapBuilder<CourseListByTeacherDTO>.MapAllProperties()

            .Map(m => m.CourseId).ToColumn("CourseId")
            .Map(m => m.CourseName).ToColumn("CourseName")

            .Build();

            return mapper;
        }

        public List<RptFlatCourse> GetAllFlatCourseByProgram(int programId)
        {
            List<RptFlatCourse> courseList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RptFlatCourse> mapper = GetFlatCourse();

                var accessor = db.CreateSprocAccessor<RptFlatCourse>(sqlGetAllFlatCourseByProgram, mapper);
                IEnumerable<RptFlatCourse> collection = accessor.Execute(programId);

                courseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return courseList;
            }

            return courseList;
        }

        public List<Course> GetAllByProgramAndSessionFromStudentCourseHistoryTable(int programId, int sessionId)
        {
            List<Course> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<Course> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Course>("CourseGetAllByProgramAndSessionFromStudentCourseHistoryTable", mapper);
                IEnumerable<Course> collection = accessor.Execute(programId, sessionId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<Course> GetAllCourseByProgramAndSessionBatchFromStudentCourseHistoryTable(int programId, int sessionId, int BatchId)
        {
            List<Course> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<Course> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Course>("CourseGetAllByProgramAndSessionAndBatchFromStudentCourseHistoryTable", mapper);
                IEnumerable<Course> collection = accessor.Execute(programId, sessionId, BatchId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }


        public List<Course> GetAllCourseByProgramAndSessionBatchExamCenterFromStudentCourseHistoryTable(int programId, int sessionId, int BatchId,int ExamCenterId)
        {
            List<Course> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<Course> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Course>("CourseGetAllByProgramAndSessionAndBatchExamCenterFromStudentCourseHistoryTable", mapper);
                IEnumerable<Course> collection = accessor.Execute(programId, sessionId, BatchId,ExamCenterId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }        
        
        public List<rTreeDistribution> GetTreeDistributionByProgram(int programId, string treeCalendarMasterId)
        {
            List<rTreeDistribution> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<rTreeDistribution> mapper = GetTreeDistributionMaper();

                var accessor = db.CreateSprocAccessor<rTreeDistribution>("RptTreeDistributionByProgram", mapper);
                IEnumerable<rTreeDistribution> collection = accessor.Execute(programId, treeCalendarMasterId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rTreeDistribution> GetTreeDistributionMaper()
        {
            IRowMapper<rTreeDistribution> mapper = MapBuilder<rTreeDistribution>.MapAllProperties()

           .Map(m => m.ProgramID).ToColumn("ProgramID")
           .Map(m => m.TreeMasterID).ToColumn("TreeMasterID")
           .Map(m => m.PassingGPA).ToColumn("PassingGPA")
           .Map(m => m.FormalCode).ToColumn("FormalCode")
           .Map(m => m.SessionName).ToColumn("SessionName")
           .Map(m => m.AcaCalId).ToColumn("AcaCalId")
           .Map(m => m.Sequence).ToColumn("Sequence")
           .Map(m => m.Name).ToColumn("Name")
           .Map(m => m.OfferedByProgramID).ToColumn("OfferedByProgramID")
           .Map(m => m.Credits).ToColumn("Credits")
           .Map(m => m.ProgramID).ToColumn("ProgramID")
           .Map(m => m.FormalCode).ToColumn("FormalCode")
           .Map(m => m.Title).ToColumn("Title")
           .Map(m => m.DetailName).ToColumn("DetailName")
           .Map(m => m.RequiredUnits).ToColumn("RequiredUnits")

           .Build();
            return mapper;
        }

        public List<rCourseRegistrationForm> GetCourseRegistrationForm(int programId, int acaCalId, string roll)
        {
            List<rCourseRegistrationForm> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<rCourseRegistrationForm> mapper = GetCourseRegistrationFormMaper();

                var accessor = db.CreateSprocAccessor<rCourseRegistrationForm>("CourseRegistrationForm", mapper);
                IEnumerable<rCourseRegistrationForm> collection = accessor.Execute(programId, acaCalId, roll);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rCourseRegistrationForm> GetCourseRegistrationFormMaper()
        {
            IRowMapper<rCourseRegistrationForm> mapper = MapBuilder<rCourseRegistrationForm>.MapAllProperties()

           .Map(m => m.Roll).ToColumn("Roll")
           .Map(m => m.ProgramID).ToColumn("ProgramID")
           .Map(m => m.BatchId).ToColumn("BatchId")
           .Map(m => m.AcaCalID).ToColumn("AcaCalID")
           .Map(m => m.DetailedName).ToColumn("DetailedName")
           .Map(m => m.ProgramFullName).ToColumn("ProgramFullName")
           .Map(m => m.Year).ToColumn("Year")
           .Map(m => m.TypeName).ToColumn("TypeName")
           .Map(m => m.Name).ToColumn("Name")
           .Map(m => m.FormalCode).ToColumn("FormalCode")
           .Map(m => m.Title).ToColumn("Title")
           .Map(m => m.Credits).ToColumn("Credits")
           .Map(m => m.Email).ToColumn("Email")
           .Map(m => m.Phone).ToColumn("Phone")
           .Map(m => m.Amount).ToColumn("Amount")
           .Map(m => m.FullName).ToColumn("FullName")

           .Build();
            return mapper;
        }

        public List<rOfferedCourse> GetOfferedCourse(int programId, int acaCalId)
        {
            List<rOfferedCourse> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<rOfferedCourse> mapper = GetOfferedCourseMaper();

                var accessor = db.CreateSprocAccessor<rOfferedCourse>("RptOfferedCourseByProgram", mapper);
                IEnumerable<rOfferedCourse> collection = accessor.Execute(programId, acaCalId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rOfferedCourse> GetOfferedCourseMaper()
        {
            IRowMapper<rOfferedCourse> mapper = MapBuilder<rOfferedCourse>.MapAllProperties()

           .Map(m => m.GroupName).ToColumn("GroupName")
           .Map(m => m.FormalCode).ToColumn("FormalCode")
           .Map(m => m.Title).ToColumn("Title")
           .Map(m => m.Credits).ToColumn("Credits")
           .Map(m => m.PreRequisite).ToColumn("PreRequisite")

           .Build();
            return mapper;
        }

        public List<rCourseWiseStudentList> GetCourseWiseStudentList(int programId, int acaCalId)
        {
            List<rCourseWiseStudentList> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<rCourseWiseStudentList> mapper = GetCourseWiseStudentListMaper();

                var accessor = db.CreateSprocAccessor<rCourseWiseStudentList>("RptCourseWiseStudentCount", mapper);
                IEnumerable<rCourseWiseStudentList> collection = accessor.Execute(programId, acaCalId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rCourseWiseStudentList> GetCourseWiseStudentListMaper()
        {
            IRowMapper<rCourseWiseStudentList> mapper = MapBuilder<rCourseWiseStudentList>.MapAllProperties()

           .Map(m => m.SectionName).ToColumn("SectionName")
           .Map(m => m.StudentID).ToColumn("StudentID")
           .Map(m => m.Credits).ToColumn("Credits")
           .Map(m => m.Title).ToColumn("Title")
           .Map(m => m.ShortName).ToColumn("ShortName")
           .Map(m => m.Year).ToColumn("Year")
           .Map(m => m.TypeName).ToColumn("TypeName")
           .Map(m => m.CourseID).ToColumn("CourseID")
           .Map(m => m.Male).ToColumn("Male")
           .Map(m => m.Female).ToColumn("Female")
           .Map(m => m.Total).ToColumn("Total")
           .Map(m => m.CourseStatusID).ToColumn("CourseStatusID")

           .Build();
            return mapper;
        }

        public List<rTopSheet> LoadTopSheet(int examScheduleSetId, int dayId, int timeSlotId)
        {
            List<rTopSheet> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<rTopSheet> mapper = GetCourseListMaper();

                var accessor = db.CreateSprocAccessor<rTopSheet>("RptCourseList", mapper);
                IEnumerable<rTopSheet> collection = accessor.Execute(examScheduleSetId, dayId, timeSlotId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rTopSheet> GetCourseListMaper()
        {
            IRowMapper<rTopSheet> mapper = MapBuilder<rTopSheet>.MapAllProperties()

           .Map(m => m.CourseCode).ToColumn("CourseCode")

           .Build();
            return mapper;
        }

        public List<rExamSection> LoadExamSection(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, string courseId, int teacherId)
        {
            List<rExamSection> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<rExamSection> mapper = GetSectionListMaper();

                var accessor = db.CreateSprocAccessor<rExamSection>("RptExamSectionByCourse", mapper);
                IEnumerable<rExamSection> collection = accessor.Execute(acaCalId, examScheduleSetId, dayId, timeSlotId, courseId, teacherId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rExamSection> GetSectionListMaper()
        {
            IRowMapper<rExamSection> mapper = MapBuilder<rExamSection>.MapAllProperties()

           .Map(m => m.Section).ToColumn("Section")

           .Build();
            return mapper;
        }

        public List<Course> GetAllByAcaCalIdProgramId(int acaCalId, int programId)
        {
            List<Course> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<Course> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Course>(sqlGetAllByAcaCalIdProgramId, mapper);
                IEnumerable<Course> collection = accessor.Execute(acaCalId, programId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<Course> GetAllByAcaCalIdProgramIdFromCourseHistory(int acaCalId, int programId)
        {
            List<Course> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<Course> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Course>("CourseGetAllByAcaCalIdProgramIdFromCourseHistory", mapper);
                IEnumerable<Course> collection = accessor.Execute(acaCalId, programId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<Course> GetAllByAcaCalIdStudentRoll(int acaCalId, string studentRoll)
        {
            List<Course> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<Course> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Course>(sqlGetAllByAcaCalIdStudentRoll, mapper);
                IEnumerable<Course> collection = accessor.Execute(acaCalId, studentRoll);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<Course> GetAllByFormalCode(string formalcode)
        {
            List<Course> courseList = new List<Course>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Course> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Course>("CourseGetAllByFormalCode", mapper);
                IEnumerable<Course> collection = accessor.Execute(formalcode).ToList();

                courseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return courseList;
            }

            return courseList;
        }

        public List<Course> GetAllByVersionCode(string versioncode)
        {
            List<Course> courseList = new List<Course>();

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Course> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Course>("CourseGetAllByVersionCode", mapper);
                IEnumerable<Course> collection = accessor.Execute(versioncode).ToList();

                courseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return courseList;
            }

            return courseList;
        }

        public bool Delete(int courseId, int versionId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDelete);

                db.AddInParameter(cmd, "CourseId", DbType.Int32, courseId);
                db.AddInParameter(cmd, "VersionId", DbType.Int32, versionId);
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

        public List<Course> GetAllCourseByProgramBatchYearFromCourseHistory(int programId, int batchId, int yearId)
        {
            List<Course> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<Course> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Course>("CourseGetAllByProgramBatchYearIdFromCourseHistory", mapper);
                IEnumerable<Course> collection = accessor.Execute(programId, batchId, yearId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<Course> GetAllRetakeOrRegularOpenCourseByStudentIdCourseTypeId(int StudentId, int TypeId)
        {
            List<Course> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<Course> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Course>("GetAllRetakeOrRegularOpenCourseByStudentIdCourseTypeId", mapper);
                IEnumerable<Course> collection = accessor.Execute(StudentId, TypeId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #region ICourseRepository Members


        public List<Course> GetOfferedCourseByProgramSession(int programId, int sessionId)
        {
            List<Course> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<Course> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Course>("OfferedCourseGetAllByProgramSession", mapper);
                IEnumerable<Course> collection = accessor.Execute(programId, sessionId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<rTeacherList> LoadTeacherList(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, string courseId)
        {
            List<rTeacherList> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<rTeacherList> mapper = GetTeacherListMaper();

                var accessor = db.CreateSprocAccessor<rTeacherList>("RptTeacherListByExamDaySlotAndCourse", mapper);
                IEnumerable<rTeacherList> collection = accessor.Execute(acaCalId, examScheduleSetId, dayId, timeSlotId, courseId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        private IRowMapper<rTeacherList> GetTeacherListMaper()
        {
            IRowMapper<rTeacherList> mapper = MapBuilder<rTeacherList>.MapAllProperties()

           .Map(m => m.TeacherId).ToColumn("TeacherId")
           .Map(m => m.TeacherName).ToColumn("TeacherName")

           .Build();
            return mapper;
        }

        #endregion
    }
}
