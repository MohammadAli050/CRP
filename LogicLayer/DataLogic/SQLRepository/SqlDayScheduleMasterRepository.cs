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
    public partial class SqlDayScheduleMasterRepository : IDayScheduleMasterRepository
    {

        Database db = null;

        private string sqlInsert = "DayScheduleMasterInsert";
        private string sqlUpdate = "DayScheduleMasterUpdate";
        private string sqlDelete = "DayScheduleMasterDelete";
        private string sqlGetById = "DayScheduleMasterGetById";
        private string sqlGetAll = "DayScheduleMasterGetAll";

        public int Insert(DayScheduleMaster dayschedulemaster)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, dayschedulemaster, isInsert);
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

        public bool Update(DayScheduleMaster dayschedulemaster)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, dayschedulemaster, isInsert);

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

        public DayScheduleMaster GetById(int? id)
        {
            DayScheduleMaster _dayschedulemaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DayScheduleMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DayScheduleMaster>(sqlGetById, rowMapper);
                _dayschedulemaster = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _dayschedulemaster;
            }

            return _dayschedulemaster;
        }

        public List<DayScheduleMaster> GetAll()
        {
            List<DayScheduleMaster> dayschedulemasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DayScheduleMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DayScheduleMaster>(sqlGetAll, mapper);
                IEnumerable<DayScheduleMaster> collection = accessor.Execute();

                dayschedulemasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return dayschedulemasterList;
            }

            return dayschedulemasterList;
        }

        public List<DayScheduleMaster> GetAllByProgramSession(int ProgramId, int SessionId)
        {
            List<DayScheduleMaster> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DayScheduleMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DayScheduleMaster>("DayScheduleMasterGetAllByProgramSession", mapper);
                IEnumerable<DayScheduleMaster> collection = accessor.Execute(ProgramId, SessionId);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        public string GenerateDayScheduleMasterByProgramSession(int programId, int SessionId)
        {
            string result = "0-0";

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand("DayScheduleMasterGenerateByProgramSession");

                db.AddOutParameter(cmd, "GenerateResult", DbType.String, Int32.MaxValue); 
                db.AddInParameter(cmd, "ProgramId", DbType.Int32, programId);
                db.AddInParameter(cmd, "SessionId", DbType.Int32, SessionId); 

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "GenerateResult");

                if (obj != null)
                {
                    result = obj.ToString();
                }
            }
            catch (Exception ex)
            {
                result = "0-0";
            }

            return result;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, DayScheduleMaster dayschedulemaster, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, dayschedulemaster.Id);
            }


            db.AddInParameter(cmd, "ProgramId", DbType.Int32, dayschedulemaster.ProgramId);
            db.AddInParameter(cmd, "SessionId", DbType.Int32, dayschedulemaster.SessionId);
            db.AddInParameter(cmd, "ScheduleDate", DbType.DateTime, dayschedulemaster.ScheduleDate);
            db.AddInParameter(cmd, "DefaultDayId", DbType.Int32, dayschedulemaster.DefaultDayId);
            db.AddInParameter(cmd, "MakeUpDayId", DbType.Int32, dayschedulemaster.MakeUpDayId);
            db.AddInParameter(cmd, "WeekNo", DbType.Int32, dayschedulemaster.WeekNo);
            db.AddInParameter(cmd, "Attribute1", DbType.Int32, dayschedulemaster.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.Int32, dayschedulemaster.Attribute2);
            db.AddInParameter(cmd, "Attribute3", DbType.Int32, dayschedulemaster.Attribute3);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, dayschedulemaster.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, dayschedulemaster.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, dayschedulemaster.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, dayschedulemaster.ModifiedDate);

            return db;
        }

        private IRowMapper<DayScheduleMaster> GetMaper()
        {
            IRowMapper<DayScheduleMaster> mapper = MapBuilder<DayScheduleMaster>.MapAllProperties()

           .Map(m => m.Id).ToColumn("Id")
        .Map(m => m.ProgramId).ToColumn("ProgramId")
        .Map(m => m.SessionId).ToColumn("SessionId")
        .Map(m => m.ScheduleDate).ToColumn("ScheduleDate")
        .Map(m => m.DefaultDayId).ToColumn("DefaultDayId")
        .Map(m => m.MakeUpDayId).ToColumn("MakeUpDayId")
        .Map(m => m.WeekNo).ToColumn("WeekNo")
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

