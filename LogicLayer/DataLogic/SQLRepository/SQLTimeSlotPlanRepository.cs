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
   public partial class SQLTimeSlotPlanRepository : ITimeSlotPlanRepository
    {
        Database db = null;

        private string sqlInsert = "TimeSlotPlanInsert";
        private string sqlUpdate = "TimeSlotPlanUpdate";
        private string sqlDelete = "TimeSlotPlanDeleteById";
        private string sqlGetById = "TimeSlotPlanGetById";
        private string sqlGetAll = "TimeSlotPlanGetAll";

        public int Insert(TimeSlotPlanNew timeSlotPlan)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, timeSlotPlan, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TimeSlotPlanID");

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

        public bool Update(TimeSlotPlanNew timeSlotPlan)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, timeSlotPlan, isInsert);

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

                db.AddInParameter(cmd, "TimeSlotPlanID", DbType.Int32, id);
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

        public TimeSlotPlanNew GetById(int id)
        {
            TimeSlotPlanNew timeSlotPlan = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TimeSlotPlanNew> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TimeSlotPlanNew>(sqlGetById, rowMapper);
                timeSlotPlan = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return timeSlotPlan;
            }

            return timeSlotPlan;
        }

        public List<TimeSlotPlanNew> GetAll()
        {
            List<TimeSlotPlanNew> timeSlotPlanList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TimeSlotPlanNew> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TimeSlotPlanNew>(sqlGetAll, mapper);
                IEnumerable<TimeSlotPlanNew> collection = accessor.Execute();

                timeSlotPlanList = collection.ToList();
            }

            catch (Exception ex)
            {
                return timeSlotPlanList;
            }

            return timeSlotPlanList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, TimeSlotPlanNew timeSlotPlan, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "TimeSlotPlanID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "TimeSlotPlanID", DbType.Int32, timeSlotPlan.TimeSlotPlanID);
            }

            db.AddInParameter(cmd, "StartHour", DbType.Int32, timeSlotPlan.StartHour);
            db.AddInParameter(cmd, "StartMin", DbType.Int32, timeSlotPlan.StartMin);
            db.AddInParameter(cmd, "StartAMPM", DbType.Int32, timeSlotPlan.StartAMPM);
            db.AddInParameter(cmd, "EndHour", DbType.Int32, timeSlotPlan.EndHour);
            db.AddInParameter(cmd, "EndMin", DbType.Int32, timeSlotPlan.EndMin);
            db.AddInParameter(cmd, "EndAMPM", DbType.Int32, timeSlotPlan.EndAMPM);
            db.AddInParameter(cmd, "Type", DbType.Int32, timeSlotPlan.Type);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, timeSlotPlan.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, timeSlotPlan.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, timeSlotPlan.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, timeSlotPlan.ModifiedDate);
            return db;
        }

        private IRowMapper<TimeSlotPlanNew> GetMaper()
        {
            IRowMapper<TimeSlotPlanNew> mapper = MapBuilder<TimeSlotPlanNew>.MapAllProperties()
            .Map(m => m.TimeSlotPlanID).ToColumn("TimeSlotPlanID")
            .Map(m => m.StartHour).ToColumn("StartHour")
            .Map(m => m.StartMin).ToColumn("StartMin")
            .Map(m => m.StartAMPM).ToColumn("StartAMPM")
            .Map(m => m.EndHour).ToColumn("EndHour")
            .Map(m => m.EndMin).ToColumn("EndMin")
            .Map(m => m.EndAMPM).ToColumn("EndAMPM")
            .Map(m => m.Type).ToColumn("Type")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Build();

            return mapper;
        }
        #endregion
    }
}
