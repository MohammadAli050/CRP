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
    public partial class SqlExamScheduleRoomInfoRepository : IExamScheduleRoomInfoRepository
    {

        Database db = null;

        private string sqlInsert = "ExamScheduleRoomInfoInsert";
        private string sqlUpdate = "ExamScheduleRoomInfoUpdate";
        private string sqlDelete = "ExamScheduleRoomInfoDeleteById";
        private string sqlGetById = "ExamScheduleRoomInfoGetById";
        private string sqlGetAll = "ExamScheduleRoomInfoGetAll";
        private string sqlGetAllByAcaCalExamSetDayTimeSlot = "ExamScheduleRoomInfoGetAllByAcaCalExamSetDayTimeSlot";
        //private string sqlDeleteByAcaCalExamSetDayTimeSlotRoomInfo = "ExamScheduleRoomInfoDeleteByAcaCalExamSetDayTimeSlotRoomInfo";
               
        public int Insert(ExamScheduleRoomInfo examscheduleroominfo)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examscheduleroominfo, isInsert);
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

        public bool Update(ExamScheduleRoomInfo examscheduleroominfo)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examscheduleroominfo, isInsert);

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

        public ExamScheduleRoomInfo GetById(int id)
        {
            ExamScheduleRoomInfo _examscheduleroominfo = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleRoomInfo> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleRoomInfo>(sqlGetById, rowMapper);
                _examscheduleroominfo = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examscheduleroominfo;
            }

            return _examscheduleroominfo;
        }

        public List<ExamScheduleRoomInfo> GetAll()
        {
            List<ExamScheduleRoomInfo> examscheduleroominfoList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleRoomInfo> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleRoomInfo>(sqlGetAll, mapper);
                IEnumerable<ExamScheduleRoomInfo> collection = accessor.Execute();

                examscheduleroominfoList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examscheduleroominfoList;
            }

            return examscheduleroominfoList;
        }

        public List<ExamScheduleRoomInfo> GetAllByAcaCalExamSetDayTimeSlot(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId)
        {
            List<ExamScheduleRoomInfo> examScheduleRoomInfoList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleRoomInfo> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleRoomInfo>(sqlGetAllByAcaCalExamSetDayTimeSlot, mapper);
                IEnumerable<ExamScheduleRoomInfo> collection = accessor.Execute(acaCalId, examScheduleSetId, dayId, timeSlotId);

                examScheduleRoomInfoList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examScheduleRoomInfoList;
            }

            return examScheduleRoomInfoList;
        }

        //public bool DeleteByAcaCalExamSetDayTimeSlotRoomInfo(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, int roomInfoId)
        //{
        //    bool result = false;

        //    try
        //    {
        //        db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
        //        DbCommand cmd = db.GetStoredProcCommand(sqlDelete);

        //        db.AddInParameter(cmd, "AcaCalId", DbType.Int32, acaCalId);
        //        db.AddInParameter(cmd, "ExamScheduleSetId", DbType.Int32, examScheduleSetId);
        //        db.AddInParameter(cmd, "DayId", DbType.Int32, dayId);
        //        db.AddInParameter(cmd, "TimeSlotId", DbType.Int32, timeSlotId);
        //        db.AddInParameter(cmd, "RoomInfoId", DbType.Int32, roomInfoId);
        //        int rowsAffected = db.ExecuteNonQuery(cmd);

        //        if (rowsAffected > 0)
        //        {
        //            result = true;
        //        }
        //    }
        //    catch
        //    {
        //        result = false;
        //    }

        //    return result;
        //}

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamScheduleRoomInfo examscheduleroominfo, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, examscheduleroominfo.Id);
            }

            	
		db.AddInParameter(cmd,"AcaCalId",DbType.Int32,examscheduleroominfo.AcaCalId);
		db.AddInParameter(cmd,"ExamScheduleSetId",DbType.Int32,examscheduleroominfo.ExamScheduleSetId);
		db.AddInParameter(cmd,"DayId",DbType.Int32,examscheduleroominfo.DayId);
		db.AddInParameter(cmd,"TimeSlotId",DbType.Int32,examscheduleroominfo.TimeSlotId);
		db.AddInParameter(cmd,"RoomInfoId",DbType.Int32,examscheduleroominfo.RoomInfoId);
		db.AddInParameter(cmd,"GenderType",DbType.String,examscheduleroominfo.GenderType);
		db.AddInParameter(cmd,"Attribute1",DbType.String,examscheduleroominfo.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,examscheduleroominfo.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,examscheduleroominfo.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,examscheduleroominfo.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,examscheduleroominfo.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,examscheduleroominfo.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,examscheduleroominfo.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamScheduleRoomInfo> GetMaper()
        {
            IRowMapper<ExamScheduleRoomInfo> mapper = MapBuilder<ExamScheduleRoomInfo>.MapAllProperties()

       	   .Map(m => m.Id).ToColumn("Id")
		.Map(m => m.AcaCalId).ToColumn("AcaCalId")
		.Map(m => m.ExamScheduleSetId).ToColumn("ExamScheduleSetId")
		.Map(m => m.DayId).ToColumn("DayId")
		.Map(m => m.TimeSlotId).ToColumn("TimeSlotId")
		.Map(m => m.RoomInfoId).ToColumn("RoomInfoId")
		.Map(m => m.GenderType).ToColumn("GenderType")
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
        #endregion

    }
}

