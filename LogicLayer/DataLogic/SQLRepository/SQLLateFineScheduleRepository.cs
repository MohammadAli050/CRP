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
    public partial class SQLLateFineScheduleRepository : ILateFineScheduleRepository
    {

        Database db = null;

        private string sqlInsert = "LateFineScheduleInsert";
        private string sqlUpdate = "LateFineScheduleUpdate";
        private string sqlDelete = "LateFineScheduleDeleteById";
        private string sqlGetById = "LateFineScheduleGetById";
        private string sqlGetAll = "LateFineScheduleGetAll";

        public int Insert(LateFineSchedule latefineschedule)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, latefineschedule, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "LateFineScheduleId");

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

        public bool Update(LateFineSchedule latefineschedule)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, latefineschedule, isInsert);

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

                db.AddInParameter(cmd, "LateFineScheduleId", DbType.Int32, id);
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

        public LateFineSchedule GetById(int? id)
        {
            LateFineSchedule _latefineschedule = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<LateFineSchedule> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<LateFineSchedule>(sqlGetById, rowMapper);
                _latefineschedule = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _latefineschedule;
            }

            return _latefineschedule;
        }

        public List<LateFineSchedule> GetAll()
        {
            List<LateFineSchedule> latefinescheduleList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<LateFineSchedule> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<LateFineSchedule>(sqlGetAll, mapper);
                IEnumerable<LateFineSchedule> collection = accessor.Execute();

                latefinescheduleList = collection.ToList();
            }

            catch (Exception ex)
            {
                return latefinescheduleList;
            }

            return latefinescheduleList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, LateFineSchedule latefineschedule, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "LateFineScheduleId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "LateFineScheduleId", DbType.Int32, latefineschedule.LateFineScheduleId);
            }

            db.AddInParameter(cmd, "AcademicCalenderId", DbType.Int32, latefineschedule.AcademicCalenderId);
            db.AddInParameter(cmd, "ProgramId", DbType.Int32, latefineschedule.ProgramId);
            db.AddInParameter(cmd, "ScheduleName", DbType.String, latefineschedule.ScheduleName);
            db.AddInParameter(cmd, "StartDate", DbType.DateTime, latefineschedule.StartDate);
            db.AddInParameter(cmd, "EndDate", DbType.DateTime, latefineschedule.EndDate);
            db.AddInParameter(cmd, "Amount", DbType.Decimal, latefineschedule.Amount);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, latefineschedule.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, latefineschedule.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, latefineschedule.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, latefineschedule.ModifiedDate);

            return db;
        }

        private IRowMapper<LateFineSchedule> GetMaper()
        {
            IRowMapper<LateFineSchedule> mapper = MapBuilder<LateFineSchedule>.MapAllProperties()

            .Map(m => m.LateFineScheduleId).ToColumn("LateFineScheduleId")
            .Map(m => m.AcademicCalenderId).ToColumn("AcademicCalenderId")
            .Map(m => m.ProgramId).ToColumn("ProgramId")
            .Map(m => m.ScheduleName).ToColumn("ScheduleName")
            .Map(m => m.StartDate).ToColumn("StartDate")
            .Map(m => m.EndDate).ToColumn("EndDate")
            .Map(m => m.Amount).ToColumn("Amount")
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

