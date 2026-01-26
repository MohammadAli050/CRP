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
    public partial class SqlExamScheduleDayRepository : IExamScheduleDayRepository
    {

        Database db = null;

        private string sqlInsert = "ExamScheduleDayInsert";
        private string sqlUpdate = "ExamScheduleDayUpdate";
        private string sqlDelete = "ExamScheduleDayDeleteById";
        private string sqlGetById = "ExamScheduleDayGetById";
        private string sqlGetAll = "ExamScheduleDayGetAll";
        private string sqlGetAllByExamSet = "ExamScheduleDayGetAllByExamSet";
               
        public int Insert(ExamScheduleDay examscheduleday)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examscheduleday, isInsert);
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

        public bool Update(ExamScheduleDay examscheduleday)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examscheduleday, isInsert);

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

        public ExamScheduleDay GetById(int id)
        {
            ExamScheduleDay _examscheduleday = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleDay> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleDay>(sqlGetById, rowMapper);
                _examscheduleday = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examscheduleday;
            }

            return _examscheduleday;
        }

        public List<ExamScheduleDay> GetAll()
        {
            List<ExamScheduleDay> examscheduledayList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleDay> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleDay>(sqlGetAll, mapper);
                IEnumerable<ExamScheduleDay> collection = accessor.Execute();

                examscheduledayList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examscheduledayList;
            }

            return examscheduledayList;
        }

        public List<ExamScheduleDay> GetAllByExamSet(int examScheduleSetId)
        {
            List<ExamScheduleDay> examScheduleDayList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleDay> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleDay>(sqlGetAllByExamSet, mapper);
                IEnumerable<ExamScheduleDay> collection = accessor.Execute(examScheduleSetId);

                examScheduleDayList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examScheduleDayList;
            }

            return examScheduleDayList;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamScheduleDay examscheduleday, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, examscheduleday.Id);
            }

            	
		db.AddInParameter(cmd,"ExamScheduleSetId",DbType.Int32,examscheduleday.ExamScheduleSetId);
		db.AddInParameter(cmd,"DayNo",DbType.Int32,examscheduleday.DayNo);
		db.AddInParameter(cmd,"DayDate",DbType.DateTime,examscheduleday.DayDate);
		db.AddInParameter(cmd,"Attribute1",DbType.String,examscheduleday.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,examscheduleday.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,examscheduleday.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,examscheduleday.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,examscheduleday.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,examscheduleday.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,examscheduleday.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamScheduleDay> GetMaper()
        {
            IRowMapper<ExamScheduleDay> mapper = MapBuilder<ExamScheduleDay>.MapAllProperties()

       	    .Map(m => m.Id).ToColumn("Id")
		    .Map(m => m.ExamScheduleSetId).ToColumn("ExamScheduleSetId")
		    .Map(m => m.DayNo).ToColumn("DayNo")
		    .Map(m => m.DayDate).ToColumn("DayDate")
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

