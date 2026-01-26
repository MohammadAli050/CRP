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
    public partial class SqlDayScheduleDetailRepository : IDayScheduleDetailRepository
    {

        Database db = null;

        private string sqlInsert = "DayScheduleDetailInsert";
        private string sqlUpdate = "DayScheduleDetailUpdate";
        private string sqlDelete = "DayScheduleDetailDelete";
        private string sqlGetById = "DayScheduleDetailGetById";
        private string sqlGetAll = "DayScheduleDetailGetAll";

        public int Insert(DayScheduleDetail dayscheduledetail)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, dayscheduledetail, isInsert);
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

        public bool Update(DayScheduleDetail dayscheduledetail)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, dayscheduledetail, isInsert);

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

        public DayScheduleDetail GetById(int? id)
        {
            DayScheduleDetail _dayscheduledetail = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DayScheduleDetail> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DayScheduleDetail>(sqlGetById, rowMapper);
                _dayscheduledetail = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _dayscheduledetail;
            }

            return _dayscheduledetail;
        }

        public List<DayScheduleDetail> GetAll()
        {
            List<DayScheduleDetail> dayscheduledetailList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DayScheduleDetail> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DayScheduleDetail>(sqlGetAll, mapper);
                IEnumerable<DayScheduleDetail> collection = accessor.Execute();

                dayscheduledetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return dayscheduledetailList;
            }

            return dayscheduledetailList;
        }

        public List<DayScheduleDetail> GetAllByDayScheduleMasterId(int DayScheduleMasterId)
        {
            List<DayScheduleDetail> dayscheduledetailList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DayScheduleDetail> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DayScheduleDetail>("DayScheduleDetailGetByDayScheduleMasterId", mapper);
                IEnumerable<DayScheduleDetail> collection = accessor.Execute(DayScheduleMasterId);

                dayscheduledetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return dayscheduledetailList;
            }

            return dayscheduledetailList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, DayScheduleDetail dayscheduledetail, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, dayscheduledetail.Id);
            }


            db.AddInParameter(cmd, "DayScheduleMasterId", DbType.Int32, dayscheduledetail.DayScheduleMasterId);
            db.AddInParameter(cmd, "AcaSecId", DbType.Int32, dayscheduledetail.AcaSecId);
            db.AddInParameter(cmd, "AcaCalId", DbType.Int32, dayscheduledetail.AcaCalId);
            db.AddInParameter(cmd, "CourseId", DbType.Int32, dayscheduledetail.CourseId);
            db.AddInParameter(cmd, "VersionId", DbType.Int32, dayscheduledetail.VersionId);
            db.AddInParameter(cmd, "SectionName", DbType.String, dayscheduledetail.SectionName);
            db.AddInParameter(cmd, "TimeSlotId", DbType.Int32, dayscheduledetail.TimeSlotId);
            db.AddInParameter(cmd, "IsActive", DbType.Boolean, dayscheduledetail.IsActive);
            db.AddInParameter(cmd, "Attribute", DbType.Int32, dayscheduledetail.Attribute);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, dayscheduledetail.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, dayscheduledetail.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, dayscheduledetail.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, dayscheduledetail.ModifiedDate);

            return db;
        }

        private IRowMapper<DayScheduleDetail> GetMaper()
        {
            IRowMapper<DayScheduleDetail> mapper = MapBuilder<DayScheduleDetail>.MapAllProperties()

           .Map(m => m.Id).ToColumn("Id")
        .Map(m => m.DayScheduleMasterId).ToColumn("DayScheduleMasterId")
        .Map(m => m.AcaSecId).ToColumn("AcaSecId")
        .Map(m => m.AcaCalId).ToColumn("AcaCalId")
        .Map(m => m.CourseId).ToColumn("CourseId")
        .Map(m => m.VersionId).ToColumn("VersionId")
        .Map(m => m.SectionName).ToColumn("SectionName")
        .Map(m => m.TimeSlotId).ToColumn("TimeSlotId")
        .Map(m => m.IsActive).ToColumn("IsActive")
        .Map(m => m.Attribute).ToColumn("Attribute")
        .Map(m => m.CreatedBy).ToColumn("CreatedBy")
        .Map(m => m.CreatedDate).ToColumn("CreatedDate")
        .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
        .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")

            .Build();

            return mapper;
        }
        #endregion

        public List<rDayScheduleDetails> GetDayScheduleDetailReportByProgramIdSessionId(int ProgramId, int SessionId)
        {
            List<rDayScheduleDetails> dayscheduledetailList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rDayScheduleDetails> mapper = MapBuilder<rDayScheduleDetails>.MapAllProperties()

                .Map(m => m.FullName).ToColumn("FullName")
                .Map(m => m.FormalCode).ToColumn("FormalCode")
                .Map(m => m.Room).ToColumn("Room")
                .Map(m => m.ScheduleDate).ToColumn("ScheduleDate")
                .Map(m => m.SectionName).ToColumn("SectionName")
                .Map(m => m.TimeSlot).ToColumn("TimeSlot")
                .Map(m => m.Title).ToColumn("Title")
                .Map(m => m.WeekNo).ToColumn("WeekNo")
                .Map(m => m.Topic).ToColumn("Topic")
                .Map(m => m.Remarks1).ToColumn("Remarks1")
                .Map(m => m.Remarks2).ToColumn("Remarks2")
                .Map(m => m.Remarks3).ToColumn("Remarks3")
                .Map(m => m.Remarks4).ToColumn("Remarks4") 

                .Build();

                var accessor = db.CreateSprocAccessor<rDayScheduleDetails>("DayScheduleMasterReportGetByProgramSession", mapper);
                IEnumerable<rDayScheduleDetails> collection = accessor.Execute(ProgramId, SessionId);

                dayscheduledetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return dayscheduledetailList;
            }

            return dayscheduledetailList;
        }

    }
}

