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

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SqlExamScheduleSeatPlanRepository : IExamScheduleSeatPlanRepository
    {

        Database db = null;

        private string sqlInsert = "ExamScheduleSeatPlanInsert";
        private string sqlUpdate = "ExamScheduleSeatPlanUpdate";
        private string sqlDelete = "ExamScheduleSeatPlanDeleteById";
        private string sqlDeleteByExamScheduleId = "ExamScheduleSeatPlanDeleteByExamScheduleId";
        private string sqlGetById = "ExamScheduleSeatPlanGetById";
        private string sqlGetAll = "ExamScheduleSeatPlanGetAll";
        private string sqlGenerateSeatPlan = "ExamScheduleSeatPlanGenerate";
        private string sqlGetAllByAcaCalExamSetDayTimeSlotRoom = "ExamScheduleSeatPlanGetAllByAcaCalExamSetDayTimeSlotRoom";
        private string sqlGetAllByAcaCalExamSetDayTimeSlotCourseCode = "ExamScheduleSeatPlanGetAllByAcaCalExamSetDayTimeSlotCourseCode";
        private string sqlGetAllByAcaCalExamSetDayTimeSlotCourseCodeAbsent = "ExamScheduleSeatPlanGetAllByAcaCalExamSetDayTimeSlotCourseCodeAbsent";

        public int Insert(ExamScheduleSeatPlan examschedueseatplan)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examschedueseatplan, isInsert);
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

        public bool Update(ExamScheduleSeatPlan examschedueseatplan)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examschedueseatplan, isInsert);

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

        public bool DeleteByExamScheduleId(int acaCalId, int examSetId, int dayId, int timeSlotId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDeleteByExamScheduleId);

                db.AddInParameter(cmd, "AcaCalId", DbType.Int32, acaCalId);
                db.AddInParameter(cmd, "ExamSetId", DbType.Int32, examSetId);
                db.AddInParameter(cmd, "DayId", DbType.Int32, dayId);
                db.AddInParameter(cmd, "TimeSlotId", DbType.Int32, timeSlotId);
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

        public ExamScheduleSeatPlan GetById(int id)
        {
            ExamScheduleSeatPlan _examschedueseatplan = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleSeatPlan> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleSeatPlan>(sqlGetById, rowMapper);
                _examschedueseatplan = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examschedueseatplan;
            }

            return _examschedueseatplan;
        }

        public List<ExamScheduleSeatPlan> GetAll()
        {
            List<ExamScheduleSeatPlan> examschedueseatplanList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleSeatPlan> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleSeatPlan>(sqlGetAll, mapper);
                IEnumerable<ExamScheduleSeatPlan> collection = accessor.Execute();

                examschedueseatplanList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examschedueseatplanList;
            }

            return examschedueseatplanList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamScheduleSeatPlan examschedueseatplan, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, examschedueseatplan.Id);
            }

            db.AddInParameter(cmd, "ExamScheduleId", DbType.Int32, examschedueseatplan.ExamScheduleId);
            db.AddInParameter(cmd, "Roll", DbType.String, examschedueseatplan.Roll);
            db.AddInParameter(cmd, "CourseCode", DbType.String, examschedueseatplan.CourseCode);
            db.AddInParameter(cmd, "RowFlag", DbType.String, examschedueseatplan.RowFlag);
            db.AddInParameter(cmd, "SequenceNo", DbType.Int32, examschedueseatplan.SequenceNo);
            db.AddInParameter(cmd, "RoomNo", DbType.Int32, examschedueseatplan.RoomNo);
            db.AddInParameter(cmd, "IsPresent", DbType.Boolean, examschedueseatplan.IsPresent);
            db.AddInParameter(cmd, "Attribute1", DbType.String, examschedueseatplan.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, examschedueseatplan.Attribute2);
            db.AddInParameter(cmd, "Attribute3", DbType.String, examschedueseatplan.Attribute3);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, examschedueseatplan.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, examschedueseatplan.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, examschedueseatplan.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, examschedueseatplan.ModifiedDate);

            return db;
        }

        private IRowMapper<ExamScheduleSeatPlan> GetMaper()
        {
            IRowMapper<ExamScheduleSeatPlan> mapper = MapBuilder<ExamScheduleSeatPlan>.MapAllProperties()

            .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.ExamScheduleId).ToColumn("ExamScheduleId")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.CourseCode).ToColumn("CourseCode")
            .Map(m => m.RowFlag).ToColumn("RowFlag")
            .Map(m => m.SequenceNo).ToColumn("SequenceNo")
            .Map(m => m.RoomNo).ToColumn("RoomNo")
            .Map(m => m.IsPresent).ToColumn("IsPresent")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
            .Map(m => m.Attribute3).ToColumn("Attribute3")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")

            .Build();

            return mapper;
        }

        private IRowMapper<rExamSeatPlan> GetExamSeatPlanMaper()
        {
            IRowMapper<rExamSeatPlan> mapper = MapBuilder<rExamSeatPlan>.MapAllProperties()

            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.CourseCode).ToColumn("CourseCode")
            .Map(m => m.RoomNo).ToColumn("RoomNo")
            .Map(m => m.RowFlag).ToColumn("RowFlag")
            .Map(m => m.SequenceNo).ToColumn("SequenceNo")
            .Map(m => m.AcaCalId).ToColumn("AcaCalId")
            .Map(m => m.SetName).ToColumn("SetName")
            .Map(m => m.TypeName).ToColumn("TypeName")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.DayDate).ToColumn("DayDate")
            .Map(m => m.StartTime).ToColumn("StartTime")

            .Build();

            return mapper;
        }
        #endregion

        public int GenerateSeatPlan(int acaCalId, int examSetId, int dayId, int timeSlotId)
        {
            int count = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlGenerateSeatPlan);

                db.AddOutParameter(cmd, "TotalSeat", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "AcaCalId", DbType.Int32, acaCalId);
                db.AddInParameter(cmd, "ExamSetId", DbType.Int32, examSetId);
                db.AddInParameter(cmd, "DayId", DbType.Int32, dayId);
                db.AddInParameter(cmd, "TimeSlotId", DbType.Int32, timeSlotId);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TotalSeat");

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

        public List<rExamSeatPlan> GetExamSeatPlan(int acaCalId, string examScheduleSetId, int calenderUnitMasterId, int dayId, int timeSlotId)
        {
            List<rExamSeatPlan> examschedueseatplanList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rExamSeatPlan> mapper = GetExamSeatPlanMaper();

                var accessor = db.CreateSprocAccessor<rExamSeatPlan>("RptExamSeatPlan", mapper);
                IEnumerable<rExamSeatPlan> collection = accessor.Execute(acaCalId, examScheduleSetId, calenderUnitMasterId, dayId, timeSlotId);

                examschedueseatplanList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examschedueseatplanList;
            }

            return examschedueseatplanList;
        }

        public List<ExamScheduleSeatPlan> GetAllByAcaCalExamSetDayTimeSlotRoom(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, int roomId)
        {
            List<ExamScheduleSeatPlan> examScheduleSeatPlanList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleSeatPlan> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleSeatPlan>(sqlGetAllByAcaCalExamSetDayTimeSlotRoom, mapper);
                IEnumerable<ExamScheduleSeatPlan> collection = accessor.Execute(acaCalId, examScheduleSetId, dayId, timeSlotId, roomId);

                examScheduleSeatPlanList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examScheduleSeatPlanList;
            }

            return examScheduleSeatPlanList;
        }

        public List<rTopSheetPresent> GetAllByAcaCalExamSetDayTimeSlotCourseCode(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, string courseCode, string sectionId)
        {
            List<rTopSheetPresent> examScheduleSeatPlanList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rTopSheetPresent> mapper = GetTopSheetPresentMaper();

                var accessor = db.CreateSprocAccessor<rTopSheetPresent>(sqlGetAllByAcaCalExamSetDayTimeSlotCourseCode, mapper);
                IEnumerable<rTopSheetPresent> collection = accessor.Execute(acaCalId, examScheduleSetId, dayId, timeSlotId, courseCode, sectionId);

                examScheduleSeatPlanList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examScheduleSeatPlanList;
            }

            return examScheduleSeatPlanList;
        }

        private IRowMapper<rTopSheetPresent> GetTopSheetPresentMaper()
        {
            IRowMapper<rTopSheetPresent> mapper = MapBuilder<rTopSheetPresent>.MapAllProperties()

            .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.ExamScheduleId).ToColumn("ExamScheduleId")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.CourseCode).ToColumn("CourseCode")
            .Map(m => m.RowFlag).ToColumn("RowFlag")
            .Map(m => m.SequenceNo).ToColumn("SequenceNo")
            .Map(m => m.RoomNo).ToColumn("RoomNo")
            .Map(m => m.IsPresent).ToColumn("IsPresent")
            .Map(m => m.Section).ToColumn("Section")
            .Map(m => m.ShortName).ToColumn("ShortName")
            .Map(m => m.DetailName).ToColumn("DetailName")
            .Map(m => m.CTitle).ToColumn("CTitle")

            .Build();

            return mapper;
        }

        public List<rTopSheetAbsent> GetAllByAcaCalExamSetDayTimeSlotCourseCodeAbsent(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, string courseCode, string sectionId)
        {
            List<rTopSheetAbsent> examScheduleSeatPlanList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rTopSheetAbsent> mapper = GetTopSheetAbsentMaper();

                var accessor = db.CreateSprocAccessor<rTopSheetAbsent>(sqlGetAllByAcaCalExamSetDayTimeSlotCourseCodeAbsent, mapper);
                IEnumerable<rTopSheetAbsent> collection = accessor.Execute(acaCalId, examScheduleSetId, dayId, timeSlotId, courseCode, sectionId);

                examScheduleSeatPlanList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examScheduleSeatPlanList;
            }

            return examScheduleSeatPlanList;
        }

        private IRowMapper<rTopSheetAbsent> GetTopSheetAbsentMaper()
        {
            IRowMapper<rTopSheetAbsent> mapper = MapBuilder<rTopSheetAbsent>.MapAllProperties()

            .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.ExamScheduleId).ToColumn("ExamScheduleId")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.CourseCode).ToColumn("CourseCode")
            .Map(m => m.RowFlag).ToColumn("RowFlag")
            .Map(m => m.SequenceNo).ToColumn("SequenceNo")
            .Map(m => m.RoomNo).ToColumn("RoomNo")
            .Map(m => m.IsPresent).ToColumn("IsPresent")
            .Map(m => m.Section).ToColumn("Section")
            .Map(m => m.ShortName).ToColumn("ShortName")
            .Map(m => m.DetailName).ToColumn("DetailName")
            .Map(m => m.Title).ToColumn("Title")

            .Build();

            return mapper;
        }

    }
}

