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

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SqlExamScheduleRepository : IExamScheduleRepository
    {

        Database db = null;

        private string sqlInsert = "ExamScheduleInsert";
        private string sqlUpdate = "ExamScheduleUpdate";
        private string sqlDelete = "ExamScheduleDeleteById";
        private string sqlGetById = "ExamScheduleGetById";
        private string sqlGetAll = "ExamScheduleGetAll";
        private string sqlGetAllByAcaCalExamSet = "ExamScheduleGetAllByAcaCalExamSet";
        private string sqlGetAllByAcaCalExamSetDaySlot = "ConflictStudentGetAllByAcaCalExamSetDaySlot";
        private string sqlGetByParameters = "ExamScheduleGetByParameters";
        private string sqlTotalStudentMaleFemale = "ExamScheduleTotalStudentMaleFemale";
        private string sqlGetAllByAcaCalExamSetDayTime = "ExamScheduleGetAllByAcaCalExamSetDayTime";
        private string sqlTotalMaleFemale = "ExamScheduleTotalMaleFemale";
        private string sqlGetAllStudentRollbyExamScheduleGender = "ExamScheduleGetAllStudentRollbyExamScheduleGender";
        private string sqlGetAllStudentRollbyExamSchedule = "ExamScheduleGetAllStudentRollbyExamSchedule";       

        public int Insert(ExamSchedule examschedule)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examschedule, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Id");

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

        public bool Update(ExamSchedule examschedule)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examschedule, isInsert);

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

                db.AddInParameter(cmd, "Id", DbType.Int32, id);
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

        public ExamSchedule GetById(int id)
        {
            ExamSchedule _examschedule = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamSchedule> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamSchedule>(sqlGetById, rowMapper);
                _examschedule = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examschedule;
            }

            return _examschedule;
        }

        public List<ExamSchedule> GetAll()
        {
            List<ExamSchedule> examscheduleList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamSchedule> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamSchedule>(sqlGetAll, mapper);
                IEnumerable<ExamSchedule> collection = accessor.Execute();

                examscheduleList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examscheduleList;
            }

            return examscheduleList;
        }

        public List<ExamSchedule> GetAllByAcaCalExamSet(int acaCalId, int examSet)
        {
            List<ExamSchedule> examScheduleList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamSchedule> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamSchedule>(sqlGetAllByAcaCalExamSet, mapper);
                IEnumerable<ExamSchedule> collection = accessor.Execute(acaCalId, examSet);

                examScheduleList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examScheduleList;
            }

            return examScheduleList;
        }

        public List<ConflictStudentDTO> GetAllByAcaCalExamSetDaySlot(int acaCalId, int examSetId, int dayId, int timeSlotId)
        {
            List<ConflictStudentDTO> conflictStudentDTOList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ConflictStudentDTO> mapper = GetConflictStudentMaper();

                var accessor = db.CreateSprocAccessor<ConflictStudentDTO>(sqlGetAllByAcaCalExamSetDaySlot, mapper);
                IEnumerable<ConflictStudentDTO> collection = accessor.Execute(acaCalId, examSetId, dayId, timeSlotId);

                conflictStudentDTOList = collection.ToList();
            }

            catch (Exception ex)
            {
                return conflictStudentDTOList;
            }

            return conflictStudentDTOList;
        }

        public List<ConflictStudentDTO> GetAllStudentRollbyExamScheduleGender(int examScheduleId, string gender)
        {
            List<ConflictStudentDTO> conflictStudentDTOList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ConflictStudentDTO> mapper = GetConflictStudentMaper();

                var accessor = db.CreateSprocAccessor<ConflictStudentDTO>(sqlGetAllStudentRollbyExamScheduleGender, mapper);
                IEnumerable<ConflictStudentDTO> collection = accessor.Execute(examScheduleId, gender);

                conflictStudentDTOList = collection.ToList();
            }

            catch (Exception ex)
            {
                return conflictStudentDTOList;
            }

            return conflictStudentDTOList;
        }

        public List<ConflictStudentDTO> GetAllStudentRollbyExamSchedule(int examScheduleId)
        {
            List<ConflictStudentDTO> conflictStudentDTOList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ConflictStudentDTO> mapper = GetConflictStudentMaper();

                var accessor = db.CreateSprocAccessor<ConflictStudentDTO>(sqlGetAllStudentRollbyExamSchedule, mapper);
                IEnumerable<ConflictStudentDTO> collection = accessor.Execute(examScheduleId);

                conflictStudentDTOList = collection.ToList();
            }

            catch (Exception ex)
            {
                return conflictStudentDTOList;
            }

            return conflictStudentDTOList;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamSchedule examschedule, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, examschedule.Id);
            }

            db.AddInParameter(cmd, "AcaCalId", DbType.Int32, examschedule.AcaCalId);
		    db.AddInParameter(cmd,"ExamSetId",DbType.Int32,examschedule.ExamSetId);
		    db.AddInParameter(cmd,"ProgramId",DbType.Int32,examschedule.ProgramId);
		    db.AddInParameter(cmd,"CourseId",DbType.Int32,examschedule.CourseId);
		    db.AddInParameter(cmd,"VersionId",DbType.Int32,examschedule.VersionId);
		    db.AddInParameter(cmd,"DayId",DbType.Int32,examschedule.DayId);
		    db.AddInParameter(cmd,"TimeSlotId",DbType.Int32,examschedule.TimeSlotId);
            db.AddInParameter(cmd,"RowFlag", DbType.String, examschedule.RowFlag);
		    db.AddInParameter(cmd,"Attribute1",DbType.String,examschedule.Attribute1);
		    db.AddInParameter(cmd,"Attribute2",DbType.String,examschedule.Attribute2);
		    db.AddInParameter(cmd,"Attribute3",DbType.String,examschedule.Attribute3);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,examschedule.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,examschedule.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,examschedule.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,examschedule.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamSchedule> GetMaper()
        {
            IRowMapper<ExamSchedule> mapper = MapBuilder<ExamSchedule>.MapAllProperties()

       	    .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.AcaCalId).ToColumn("AcaCalId")
		    .Map(m => m.ExamSetId).ToColumn("ExamSetId")
		    .Map(m => m.ProgramId).ToColumn("ProgramId")
		    .Map(m => m.CourseId).ToColumn("CourseId")
		    .Map(m => m.VersionId).ToColumn("VersionId")
		    .Map(m => m.DayId).ToColumn("DayId")
		    .Map(m => m.TimeSlotId).ToColumn("TimeSlotId")
            .Map(m => m.RowFlag).ToColumn("RowFlag")
		    .Map(m => m.Attribute1).ToColumn("Attribute1")
		    .Map(m => m.Attribute2).ToColumn("Attribute2")
		    .Map(m => m.Attribute3).ToColumn("Attribute3")
		    .Map(m => m.CreatedBy).ToColumn("CreatedBy")
		    .Map(m => m.CreatedDate).ToColumn("CreatedDate")
		    .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
		    .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .DoNotMap(m => m.ProgramName)
            .DoNotMap(m => m.CourseInfo)
            .DoNotMap(m => m.Day)
            .DoNotMap(m => m.TimeSlot)
            .DoNotMap(m => m.SectionList)
            .DoNotMap(m => m.StudentNo)
            .DoNotMap(m => m.totalMale)
            .DoNotMap(m => m.totalFemale)
            
            .Build();

            return mapper;
        }

        private IRowMapper<ConflictStudentDTO> GetConflictStudentMaper()
        {
            IRowMapper<ConflictStudentDTO> mapper = MapBuilder<ConflictStudentDTO>.MapAllProperties()

            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.Course).ToColumn("Course")

            .Build();

            return mapper;
        }
        #endregion

        public ExamSchedule GetByParameters(int acaCalId, int examSetId, int dayId, int timeSlotId, int courseId, int versionId)
        {
            ExamSchedule _examschedule = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamSchedule> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamSchedule>(sqlGetByParameters, rowMapper);
                _examschedule = accessor.Execute(acaCalId, examSetId, dayId, timeSlotId, courseId, versionId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examschedule;
            }

            return _examschedule;
        }

        public string GetTotalStudentMaleFemale(int examScheduleId)
        {
            string count = "-";

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlTotalStudentMaleFemale);

                db.AddOutParameter(cmd, "TotalStudentMaleFemale", DbType.String, Int32.MaxValue);
                db.AddInParameter(cmd, "ExamScheduleId", DbType.Int32, examScheduleId);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TotalStudentMaleFemale");

                if (obj != null)
                {
                    count = obj.ToString();
                }
            }
            catch (Exception ex)
            {
                count = "-";
            }

            return count;
        }

        public string GetTotalMaleFemale(int acaCalId, int dayId, int timeSlotId)
        {
            string count = "-";

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlTotalMaleFemale);

                db.AddOutParameter(cmd, "TotalMaleFemale", DbType.String, Int32.MaxValue);
                db.AddInParameter(cmd, "AcaCalId", DbType.Int32, acaCalId);
                db.AddInParameter(cmd, "DayId", DbType.Int32, dayId);
                db.AddInParameter(cmd, "TimeSlotId", DbType.Int32, timeSlotId);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TotalMaleFemale");

                if (obj != null)
                {
                    count = obj.ToString();
                }
            }
            catch (Exception ex)
            {
                count = "-";
            }

            return count;
        }

        public List<ExamSchedule> GetAllByAcaCalExamSetDayTimeSlot(int acaCalId, int examSet, int dayId, int timeSlotId)
        {
            List<ExamSchedule> examScheduleList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamSchedule> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamSchedule>(sqlGetAllByAcaCalExamSetDayTime, mapper);
                IEnumerable<ExamSchedule> collection = accessor.Execute(acaCalId, examSet, dayId, timeSlotId);

                examScheduleList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examScheduleList;
            }

            return examScheduleList;
        }

        public List<rExamAttendanceSheet> GetExamAttendaceListByRoom(int acaCalId, int examSetId, int dayId, int timeSlotId, int roomId)
        {
            List<rExamAttendanceSheet> examExamAttendaceList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rExamAttendanceSheet> mapper = GetExamAttendanceMaper();

                var accessor = db.CreateSprocAccessor<rExamAttendanceSheet>("RptExamAttendance", mapper);
                IEnumerable<rExamAttendanceSheet> collection = accessor.Execute(acaCalId, examSetId, dayId, timeSlotId, roomId);

                examExamAttendaceList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examExamAttendaceList;
            }

            return examExamAttendaceList;
        }

        private IRowMapper<rExamAttendanceSheet> GetExamAttendanceMaper()
        {
            IRowMapper<rExamAttendanceSheet> mapper = MapBuilder<rExamAttendanceSheet>.MapAllProperties()

            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.RoomNo).ToColumn("RoomNo")
            .Map(m => m.CourseCode).ToColumn("CourseCode")
            .Map(m => m.FullName).ToColumn("FullName")
            .Map(m => m.Section).ToColumn("Section")
            .Map(m => m.SeatNo).ToColumn("SeatNo")
            .Map(m => m.Attribute1).ToColumn("Attribute1")

            .Build();

            return mapper;
        }

        public List<rExamSeatPlanByRoom> GetExamSeatPlanByRoom(int acaCalId, int examSetId, int dayId, int timeSlotId, int roomId)
        {
            List<rExamSeatPlanByRoom> examExamSeatPlanList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rExamSeatPlanByRoom> mapper = GetExamSeatPlanMaper();

                var accessor = db.CreateSprocAccessor<rExamSeatPlanByRoom>("RptExamSeatPlan", mapper);
                IEnumerable<rExamSeatPlanByRoom> collection = accessor.Execute(acaCalId, examSetId, dayId, timeSlotId, roomId);

                examExamSeatPlanList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examExamSeatPlanList;
            }

            return examExamSeatPlanList;
        }

        private IRowMapper<rExamSeatPlanByRoom> GetExamSeatPlanMaper()
        {
            IRowMapper<rExamSeatPlanByRoom> mapper = MapBuilder<rExamSeatPlanByRoom>.MapAllProperties()

            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.CourseCode).ToColumn("CourseCode")
            .Map(m => m.SequenceNo).ToColumn("SequenceNo")
            .Map(m => m.Section).ToColumn("Section")
            .Map(m => m.RoomNo).ToColumn("RoomNo")
            .Map(m => m.Attribute1).ToColumn("Attribute1")

            .Build();

            return mapper;
        }

        
    }
}

