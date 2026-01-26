using LogicLayer.BusinessObjects;
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
    public partial class SQLAcademicCalenderSectionRepository : IAcademicCalenderSectionRepository
    {
        Database db = null;

        private string sqlInsert = "AcademicCalenderSectionInsert";
        private string sqlUpdate = "AcademicCalenderSectionUpdate";
        private string sqlDelete = "AcademicCalenderSectionDeleteById";
        private string sqlGetById = "AcademicCalenderSectionGetById";
        private string sqlGetByCourseVersionSecFac = "AcademicCalenderSectionGetByCourseVersionSecFac";
        private string sqlGetAll = "AcademicCalenderSectionGetAll";
        private string sqlGetAllByAcaCalId = "AcademicCalenderSectionGetAllByAcaCalId";
        private string sqlGetAllByAcaCalIdState = "AcademicCalenderSectionGetAllByAcaCalIdState";
        private string sqlGetAllByStudentAcaCal = "AcademicCalenderSectionGetAllByStudentAcaCal";
        private string sqlGetByRoomDayTime = "AcademicCalenderSectionGetByRoomDayTime";
        private string sqlGetByAcaCalCourseVersionSection = "AcademicCalenderSectionGetByAcaCalCourseVersionSection";
        private string sqlGetByAcaCalCourseVersion = "AcademicCalenderSectionGetByAcaCalCourseVersion";
        private string sqlGetAllByAcaCalProgram = "AcademicCalenderSectionGetAllByAcaCalProgram";
        private string sqlGetAllByAcaCalIdStudentRoll = "AcademicCalenderSectionGetAllByAcaCalIdStudentRoll";
        private string sqlGetAllCourseWithSectionByAcaCalAndProgramAndTeacher = "GetAllCourseWithSectionByAcaCalAndProgramAndTeacher";
        private string sqlGetAllTeacherListByAcaCalIdAndProgramId = "GetTeacherListBySessionAndProgram";
        public int Insert(AcademicCalenderSection academicCalenderSection)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, academicCalenderSection, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "AcaCal_SectionID");

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

        public bool Update(AcademicCalenderSection academicCalenderSection)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, academicCalenderSection, isInsert);

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

                db.AddInParameter(cmd, "AcaCal_SectionID", DbType.Int32, id);
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

        public AcademicCalenderSection GetById(int? id)
        {
            AcademicCalenderSection _academicCalenderSection = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalenderSection> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSection>(sqlGetById, rowMapper);
                _academicCalenderSection = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _academicCalenderSection;
            }

            return _academicCalenderSection;
        }

        public List<AcademicCalenderSection> GetAll()
        {
            List<AcademicCalenderSection> academicCalenderSectionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalenderSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSection>(sqlGetAll, mapper);
                IEnumerable<AcademicCalenderSection> collection = accessor.Execute();

                academicCalenderSectionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return academicCalenderSectionList;
            }

            return academicCalenderSectionList;
        }

        public List<AcademicCalenderSection> GetAllByAcaCalId(int id)
        {
            List<AcademicCalenderSection> academicCalenderSectionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalenderSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSection>(sqlGetAllByAcaCalId, mapper);
                IEnumerable<AcademicCalenderSection> collection = accessor.Execute(id);

                academicCalenderSectionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return academicCalenderSectionList;
            }

            return academicCalenderSectionList;
        }

        public List<AcademicCalenderSection> GetAllByAcaCalIdStudentRoll(int id, string studentRoll)
        {
            List<AcademicCalenderSection> academicCalenderSectionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalenderSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSection>(sqlGetAllByAcaCalIdStudentRoll, mapper);
                IEnumerable<AcademicCalenderSection> collection = accessor.Execute(id, studentRoll);

                academicCalenderSectionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return academicCalenderSectionList;
            }

            return academicCalenderSectionList;
        }

        public List<AcademicCalenderSection> GetAllByAcaCalIdState(int id, string state)
        {
            List<AcademicCalenderSection> academicCalenderSectionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalenderSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSection>(sqlGetAllByAcaCalIdState, mapper);
                IEnumerable<AcademicCalenderSection> collection = accessor.Execute(id, state);

                academicCalenderSectionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return academicCalenderSectionList;
            }

            return academicCalenderSectionList;
        }

        public List<AcademicCalenderSection> GetAllByRoomDayTime(int Room1, int Room2, int Day1, int Day2, int Time1, int Time2)
        {
            List<AcademicCalenderSection> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<AcademicCalenderSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSection>(sqlGetByRoomDayTime, mapper);
                IEnumerable<AcademicCalenderSection> collection = accessor.Execute(Room1, Room2, Day1, Day2, Time1, Time2);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public AcademicCalenderSection GetByCourseVersionSecFac(int courseId, int versionId, string section, int facultyId)
        {
            AcademicCalenderSection _academicCalenderSection = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalenderSection> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSection>(sqlGetByCourseVersionSecFac, rowMapper);
                _academicCalenderSection = accessor.Execute(courseId, versionId, section, facultyId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _academicCalenderSection;
            }

            return _academicCalenderSection;
        }

        public AcademicCalenderSection GetByAcaCalCourseVersionSection(int acaCalId, int courseId, int versionId, string sectionName)
        {
            AcademicCalenderSection _academicCalenderSection = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AcademicCalenderSection> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSection>(sqlGetByAcaCalCourseVersionSection, rowMapper);
                _academicCalenderSection = accessor.Execute(acaCalId, courseId, versionId, sectionName).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _academicCalenderSection;
            }

            return _academicCalenderSection;
        }

        public List<AcademicCalenderSection> GetAllByAcaCalProgram(int acaCalId, int programId)
        {
            List<AcademicCalenderSection> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<AcademicCalenderSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSection>(sqlGetAllByAcaCalProgram, mapper);
                IEnumerable<AcademicCalenderSection> collection = accessor.Execute(acaCalId, programId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, AcademicCalenderSection academicCalenderSection, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "AcaCal_SectionID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "AcaCal_SectionID", DbType.Int32, academicCalenderSection.AcaCal_SectionID);
            }

            db.AddInParameter(cmd, "AcademicCalenderID", DbType.Int32, academicCalenderSection.AcademicCalenderID);
            db.AddInParameter(cmd, "CourseID", DbType.Int32, academicCalenderSection.CourseID);
            db.AddInParameter(cmd, "VersionID", DbType.Int32, academicCalenderSection.VersionID);
            db.AddInParameter(cmd, "SectionName", DbType.String, academicCalenderSection.SectionName);
            db.AddInParameter(cmd, "Capacity", DbType.Int32, academicCalenderSection.Capacity);

            db.AddInParameter(cmd, "RoomInfoOneID", DbType.Int32, academicCalenderSection.RoomInfoOneID);
            db.AddInParameter(cmd, "RoomInfoTwoID", DbType.Int32, academicCalenderSection.RoomInfoTwoID);
            db.AddInParameter(cmd, "RoomInfoThreeID", DbType.Int32, academicCalenderSection.RoomInfoThreeID);

            db.AddInParameter(cmd, "DayOne", DbType.Int32, academicCalenderSection.DayOne);
            db.AddInParameter(cmd, "DayTwo", DbType.Int32, academicCalenderSection.DayTwo);
            db.AddInParameter(cmd, "DayThree", DbType.Int32, academicCalenderSection.DayThree);

            db.AddInParameter(cmd, "TimeSlotPlanOneID", DbType.Int32, academicCalenderSection.TimeSlotPlanOneID);
            db.AddInParameter(cmd, "TimeSlotPlanTwoID", DbType.Int32, academicCalenderSection.TimeSlotPlanTwoID);
            db.AddInParameter(cmd, "TimeSlotPlanThreeID", DbType.Int32, academicCalenderSection.TimeSlotPlanThreeID);

            db.AddInParameter(cmd, "TeacherOneID", DbType.Int32, academicCalenderSection.TeacherOneID);
            db.AddInParameter(cmd, "TeacherTwoID", DbType.Int32, academicCalenderSection.TeacherTwoID);
            db.AddInParameter(cmd, "TeacherThreeID", DbType.Int32, academicCalenderSection.TeacherThreeID);

            db.AddInParameter(cmd, "DeptID", DbType.Int32, academicCalenderSection.DeptID);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, academicCalenderSection.ProgramID); 
            db.AddInParameter(cmd, "TypeDefinitionID", DbType.Int32, academicCalenderSection.TypeDefinitionID);
            db.AddInParameter(cmd, "Occupied", DbType.Int32, academicCalenderSection.Occupied);
            db.AddInParameter(cmd, "ShareSection", DbType.Int32, academicCalenderSection.ShareSection);
            db.AddInParameter(cmd, "BasicExamTemplateId", DbType.Int32, academicCalenderSection.BasicExamTemplateId);
            db.AddInParameter(cmd, "CalculativeExamTemplateId", DbType.Int32, academicCalenderSection.CalculativeExamTemplateId);
            db.AddInParameter(cmd, "SectionDefination", DbType.String, academicCalenderSection.SectionDefination);
            db.AddInParameter(cmd, "OnlineGradeSheetTemplateID", DbType.Int32, academicCalenderSection.OnlineGradeSheetTemplateID);
            db.AddInParameter(cmd, "SectionGenderID", DbType.Int32, academicCalenderSection.SectionGenderID);
            db.AddInParameter(cmd, "Remarks", DbType.String, academicCalenderSection.Remarks);
            db.AddInParameter(cmd, "RoomConflict", DbType.String, academicCalenderSection.RoomConflict);
            db.AddInParameter(cmd, "FacultyConflict", DbType.String, academicCalenderSection.FacultyConflict);

            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, academicCalenderSection.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, academicCalenderSection.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, academicCalenderSection.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, academicCalenderSection.ModifiedDate);

            return db;
        }

        private IRowMapper<AcademicCalenderSection> GetMaper()
        {
            IRowMapper<AcademicCalenderSection> mapper = MapBuilder<AcademicCalenderSection>.MapAllProperties()
            .Map(m => m.AcaCal_SectionID).ToColumn("AcaCal_SectionID")
            .Map(m => m.AcademicCalenderID).ToColumn("AcademicCalenderID")
            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.VersionID).ToColumn("VersionID")
            .Map(m => m.SectionName).ToColumn("SectionName")
            .Map(m => m.Capacity).ToColumn("Capacity")

            .Map(m => m.RoomInfoOneID).ToColumn("RoomInfoOneID")
            .Map(m => m.RoomInfoTwoID).ToColumn("RoomInfoTwoID")
            .Map(m => m.RoomInfoThreeID).ToColumn("RoomInfoThreeID")

            .Map(m => m.DayOne).ToColumn("DayOne")
            .Map(m => m.DayTwo).ToColumn("DayTwo")
             .Map(m => m.DayThree).ToColumn("DayThree")

            .Map(m => m.TimeSlotPlanOneID).ToColumn("TimeSlotPlanOneID")
            .Map(m => m.TimeSlotPlanTwoID).ToColumn("TimeSlotPlanTwoID")
            .Map(m => m.TimeSlotPlanThreeID).ToColumn("TimeSlotPlanThreeID")

            .Map(m => m.TeacherOneID).ToColumn("TeacherOneID")
            .Map(m => m.TeacherTwoID).ToColumn("TeacherTwoID")
            .Map(m => m.TeacherThreeID).ToColumn("TeacherThreeID")

            .Map(m => m.DeptID).ToColumn("DeptID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.TypeDefinitionID).ToColumn("TypeDefinitionID")
            .Map(m => m.Occupied).ToColumn("Occupied")
            .Map(m => m.ShareSection).ToColumn("ShareSection")
            .Map(m => m.BasicExamTemplateId).ToColumn("BasicExamTemplateId")
            .Map(m => m.CalculativeExamTemplateId).ToColumn("CalculativeExamTemplateId")
            .Map(m => m.SectionDefination).ToColumn("SectionDefination")
            .Map(m => m.OnlineGradeSheetTemplateID).ToColumn("OnlineGradeSheetTemplateID")
            .Map(m => m.SectionGenderID).ToColumn("SectionGenderID")
            .Map(m => m.Remarks).ToColumn("Remarks")

            .Map(m => m.RoomConflict).ToColumn("RoomConflict")
            .Map(m => m.FacultyConflict).ToColumn("FacultyConflict")

            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")

            .DoNotMap(m => m.CourseInfo)
            .DoNotMap(m => m.RoomInfoOne)
            .DoNotMap(m => m.RoomInfoTwo)
            .DoNotMap(m => m.DayInfoOne)
            .DoNotMap(m => m.DayInfoTwo)
            .DoNotMap(m => m.ProgramName)
            .DoNotMap(m => m.TimeSlotPlanInfoOne)
            .DoNotMap(m => m.TimeSlotPlanInfoTwo)
            .DoNotMap(m => m.TeacherInfoOne)
            .DoNotMap(m => m.TeacherInfoTwo)
            .DoNotMap(m => m.ShareProgInfoOne)
            .DoNotMap(m => m.ShareProgInfoTwo)
            .DoNotMap(m => m.ShareProgInfoThree)
            .DoNotMap(m => m.GradeSheetTemplate)
            .DoNotMap(m => m.TotalStudent)
            .DoNotMap(m => m.EvaluationCompleteStudent)

            .Build();

            return mapper;
        }

        private IRowMapper<AcademicCalenderSectionWithCourse> GetCourseWithSectionMapper()
        {
            IRowMapper<AcademicCalenderSectionWithCourse> mapper = MapBuilder<AcademicCalenderSectionWithCourse>.MapAllProperties()
            .Map(m => m.AcaCal_SectionID).ToColumn("AcaCal_SectionID")
            .Map(m => m.Title).ToColumn("Title")
            .Map(m => m.SectionName).ToColumn("SectionName")
            .Map(m => m.TotalStudent).ToColumn("TotalStudent")
            .Map(m => m.TotalParticipant).ToColumn("TotalParticipant")

            .Build();

            return mapper;
        }

        private IRowMapper<TeacherInfo> GetTeacherInfoMapper()
        {
            IRowMapper<TeacherInfo> mapper = MapBuilder<TeacherInfo>.MapAllProperties()
            .Map(m => m.TeacherName).ToColumn("TeacherName")
            .Map(m => m.TeacherId).ToColumn("TeacherID")

            .DoNotMap(m => m.BasicInfo)
            .DoNotMap(m => m.Email)
            .DoNotMap(m => m.EmployeeObj)
            .DoNotMap(m => m.MaxNoTobeAdvised)
            .DoNotMap(m => m.Phone)
            .DoNotMap(m => m.Publish)
            .DoNotMap(m => m.UserID)
            .DoNotMap(m => m.WebAddress)
            .DoNotMap(m => m.AcademicBackground)

            .Build();

            return mapper;
        }
        #endregion
        
        public List<AcademicCalenderSection> GetAllByRoomInRegSession(int roomId)
        {
            List<AcademicCalenderSection> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<AcademicCalenderSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSection>("AcademicCalenderSectionGetAllByRoomId", mapper);
                IEnumerable<AcademicCalenderSection> collection = accessor.Execute(roomId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }
        
        public List<AcademicCalenderSection> GetAllByTeacherInRegSession(int teacherId)
        {
            List<AcademicCalenderSection> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<AcademicCalenderSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSection>("AcademicCalenderSectionGetAllByTeacherId", mapper);
                IEnumerable<AcademicCalenderSection> collection = accessor.Execute(teacherId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<AcademicCalenderSection> GetAll(int studentId, int acaCalId)
        {
            List<AcademicCalenderSection> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<AcademicCalenderSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSection>(sqlGetAllByStudentAcaCal, mapper);
                IEnumerable<AcademicCalenderSection> collection = accessor.Execute(studentId, acaCalId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<AcademicCalenderSection> GetByAcaCalCourseVersion(int acaCalId, int courseId, int versionId)
        {
            List<AcademicCalenderSection> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<AcademicCalenderSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSection>(sqlGetByAcaCalCourseVersion, mapper);
                IEnumerable<AcademicCalenderSection> collection = accessor.Execute(acaCalId, courseId, versionId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<AcademicCalenderSection> GetAllByAcaCalAndProgram(int programId, int sessionId)
        {
            List<AcademicCalenderSection> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<AcademicCalenderSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSection>("AcademicCalenderSectionGetAllByAcaCalAndProgram", mapper);
                IEnumerable<AcademicCalenderSection> collection = accessor.Execute(programId, sessionId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<AcademicCalenderSectionWithCourse> GetAllCourseWithSectionByAcaCalAndProgramAndTeacher(int acaCalId, int programId, int teacherId)
        {
            List<AcademicCalenderSectionWithCourse> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<AcademicCalenderSectionWithCourse> mapper = GetCourseWithSectionMapper();

                var accessor = db.CreateSprocAccessor<AcademicCalenderSectionWithCourse>(sqlGetAllCourseWithSectionByAcaCalAndProgramAndTeacher, mapper);
                IEnumerable<AcademicCalenderSectionWithCourse> collection = accessor.Execute(acaCalId, programId, teacherId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public List<TeacherInfo> GetAllTeacherByAcaCalAndProgram(int acaCalId, int programId)
        {
            List<TeacherInfo> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<TeacherInfo> mapper = GetTeacherInfoMapper();

                var accessor = db.CreateSprocAccessor<TeacherInfo>(sqlGetAllTeacherListByAcaCalIdAndProgramId, mapper);
                IEnumerable<TeacherInfo> collection = accessor.Execute(acaCalId, programId);

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
