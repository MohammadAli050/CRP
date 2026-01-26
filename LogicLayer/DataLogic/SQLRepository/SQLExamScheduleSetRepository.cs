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
    public partial class SQLExamScheduleSetRepository : IExamScheduleSetRepository
    {

        Database db = null;

        private string sqlInsert = "ExamScheduleSetInsert";
        private string sqlUpdate = "ExamScheduleSetUpdate";
        private string sqlDelete = "ExamScheduleSetDeleteById";
        private string sqlGetById = "ExamScheduleSetGetById";
        private string sqlGetAll = "ExamScheduleSetGetAll";
        private string sqlGetAllByAcaCalId = "ExamScheduleSetGetAllByAcaCalId";
               
        public int Insert(ExamScheduleSet examscheduleset)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examscheduleset, isInsert);
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

        public bool Update(ExamScheduleSet examscheduleset)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examscheduleset, isInsert);

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

        public ExamScheduleSet GetById(int id)
        {
            ExamScheduleSet _examscheduleset = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleSet> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleSet>(sqlGetById, rowMapper);
                _examscheduleset = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examscheduleset;
            }

            return _examscheduleset;
        }

        public ExamScheduleSet GetById(int id, string name)
        {
            ExamScheduleSet _examscheduleset = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleSet> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleSet>("ExamScheduleSetGetByIdandName", rowMapper);
                _examscheduleset = accessor.Execute(id, name).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examscheduleset;
            }

            return _examscheduleset;
        }

        public List<ExamScheduleSet> GetAll()
        {
            List<ExamScheduleSet> examschedulesetList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleSet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleSet>(sqlGetAll, mapper);
                IEnumerable<ExamScheduleSet> collection = accessor.Execute();

                examschedulesetList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examschedulesetList;
            }

            return examschedulesetList;
        }

        public List<ExamScheduleSet> GetAllByAcaCalId(int acaCalId)
        {
            List<ExamScheduleSet> examScheduleSetList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleSet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleSet>(sqlGetAllByAcaCalId, mapper);
                IEnumerable<ExamScheduleSet> collection = accessor.Execute(acaCalId);

                examScheduleSetList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examScheduleSetList;
            }

            return examScheduleSetList;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamScheduleSet examscheduleset, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, examscheduleset.Id);
            }

            	
		db.AddInParameter(cmd,"AcaCalId",DbType.Int32,examscheduleset.AcaCalId);
		db.AddInParameter(cmd,"SetName",DbType.String,examscheduleset.SetName);
		db.AddInParameter(cmd,"TotalDay",DbType.Int32,examscheduleset.TotalDay);
		db.AddInParameter(cmd,"TotalTimeSlot",DbType.Int32,examscheduleset.TotalTimeSlot);
		db.AddInParameter(cmd,"IsActive",DbType.Boolean,examscheduleset.IsActive);
		db.AddInParameter(cmd,"Attribute1",DbType.String,examscheduleset.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,examscheduleset.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,examscheduleset.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,examscheduleset.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,examscheduleset.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,examscheduleset.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,examscheduleset.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamScheduleSet> GetMaper()
        {
            IRowMapper<ExamScheduleSet> mapper = MapBuilder<ExamScheduleSet>.MapAllProperties()

       	   .Map(m => m.Id).ToColumn("Id")
		.Map(m => m.AcaCalId).ToColumn("AcaCalId")
		.Map(m => m.SetName).ToColumn("SetName")
		.Map(m => m.TotalDay).ToColumn("TotalDay")
		.Map(m => m.TotalTimeSlot).ToColumn("TotalTimeSlot")
		.Map(m => m.IsActive).ToColumn("IsActive")
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

