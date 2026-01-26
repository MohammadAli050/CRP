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
    public partial class SqlExamScheduleTimeSlotRepository : IExamScheduleTimeSlotRepository
    {

        Database db = null;

        private string sqlInsert = "ExamScheduleTimeSlotInsert";
        private string sqlUpdate = "ExamScheduleTimeSlotUpdate";
        private string sqlDelete = "ExamScheduleTimeSlotDeleteById";
        private string sqlGetById = "ExamScheduleTimeSlotGetById";
        private string sqlGetAll = "ExamScheduleTimeSlotGetAll";
        private string sqlGetAllByExamSet = "ExamScheduleTimeSlotGetAllByExamSet";
               
        public int Insert(ExamScheduleTimeSlot examscheduletimeslot)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examscheduletimeslot, isInsert);
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

        public bool Update(ExamScheduleTimeSlot examscheduletimeslot)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examscheduletimeslot, isInsert);

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

        public ExamScheduleTimeSlot GetById(int id)
        {
            ExamScheduleTimeSlot _examscheduletimeslot = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleTimeSlot> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleTimeSlot>(sqlGetById, rowMapper);
                _examscheduletimeslot = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examscheduletimeslot;
            }

            return _examscheduletimeslot;
        }

        public List<ExamScheduleTimeSlot> GetAll()
        {
            List<ExamScheduleTimeSlot> examscheduletimeslotList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleTimeSlot> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleTimeSlot>(sqlGetAll, mapper);
                IEnumerable<ExamScheduleTimeSlot> collection = accessor.Execute();

                examscheduletimeslotList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examscheduletimeslotList;
            }

            return examscheduletimeslotList;
        }

        public List<ExamScheduleTimeSlot> GetAllByExamSet(int examScheduleSetId)
        {
            List<ExamScheduleTimeSlot> examScheduleTimeSlotList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleTimeSlot> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleTimeSlot>(sqlGetAllByExamSet, mapper);
                IEnumerable<ExamScheduleTimeSlot> collection = accessor.Execute(examScheduleSetId);

                examScheduleTimeSlotList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examScheduleTimeSlotList;
            }

            return examScheduleTimeSlotList;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamScheduleTimeSlot examscheduletimeslot, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, examscheduletimeslot.Id);
            }

            	
		db.AddInParameter(cmd,"ExamScheduleSetId",DbType.Int32,examscheduletimeslot.ExamScheduleSetId);
		db.AddInParameter(cmd,"TimeSlotNo",DbType.Int32,examscheduletimeslot.TimeSlotNo);
        db.AddInParameter(cmd, "StartTime", DbType.String, examscheduletimeslot.StartTime);
        db.AddInParameter(cmd, "EndTime", DbType.String, examscheduletimeslot.EndTime);		
		db.AddInParameter(cmd,"Attribute1",DbType.String,examscheduletimeslot.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,examscheduletimeslot.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,examscheduletimeslot.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,examscheduletimeslot.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,examscheduletimeslot.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,examscheduletimeslot.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,examscheduletimeslot.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamScheduleTimeSlot> GetMaper()
        {
            IRowMapper<ExamScheduleTimeSlot> mapper = MapBuilder<ExamScheduleTimeSlot>.MapAllProperties()

       	    .Map(m => m.Id).ToColumn("Id")
		    .Map(m => m.ExamScheduleSetId).ToColumn("ExamScheduleSetId")
		    .Map(m => m.TimeSlotNo).ToColumn("TimeSlotNo")
		    .Map(m => m.StartTime).ToColumn("StartTime")
		    .Map(m => m.EndTime).ToColumn("EndTime")
		    .Map(m => m.Attribute1).ToColumn("Attribute1")
		    .Map(m => m.Attribute2).ToColumn("Attribute2")
		    .Map(m => m.Attribute3).ToColumn("Attribute3")
		    .Map(m => m.CreatedBy).ToColumn("CreatedBy")
		    .Map(m => m.CreatedDate).ToColumn("CreatedDate")
		    .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
		    .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .DoNotMap(m => m.ExamScheduleSetName)
            
            .Build();

            return mapper;
        }
        #endregion

    }
}

