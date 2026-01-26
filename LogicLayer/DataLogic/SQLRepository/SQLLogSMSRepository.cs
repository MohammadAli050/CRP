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
    public partial class SQLLogSMSRepository : ILogSMSRepository
    {
        Database db = null;

        private string sqlInsert = "LogSMSInsert";
        private string sqlGetAll = "LogSMSGetAll";
        private string sqlGetByDateRange = "LogSMSGetByDate";
        
        public int Insert(LogSMS logsms)
        {
            int id = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>("ConnectionLoG");
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, logsms);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ID");

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

        public List<LogSMS> GetAll()
        {
            List<LogSMS> logsms = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>("ConnectionLoG");

                IRowMapper<LogSMS> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<LogSMS>(sqlGetAll, mapper);
                IEnumerable<LogSMS> collection = accessor.Execute();

                logsms = collection.ToList();
            }

            catch (Exception ex)
            {
                return logsms;
            }

            return logsms;
        }

        public List<LogSMS> GetByDateRange(DateTime fromDate, DateTime toDate)
        {
            List<LogSMS> logsms = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>("ConnectionLoG");

                IRowMapper<LogSMS> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<LogSMS>(sqlGetByDateRange, mapper);
                IEnumerable<LogSMS> collection = accessor.Execute(fromDate, toDate);

                logsms = collection.ToList();
            }

            catch (Exception ex)
            {
                return logsms;
            }

            return logsms;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, LogSMS logsms)
        {
            db.AddOutParameter(cmd, "ID", DbType.Int32, Int32.MaxValue);
            db.AddInParameter(cmd, "Date", DbType.DateTime, logsms.Date);
            db.AddInParameter(cmd, "Sender", DbType.String, logsms.Sender);
            db.AddInParameter(cmd, "Receipient", DbType.String, logsms.Receipient);
            db.AddInParameter(cmd, "Message", DbType.String, logsms.Message);
            db.AddInParameter(cmd, "Status", DbType.Boolean, logsms.Status);
            return db;
        }

        private IRowMapper<LogSMS> GetMaper()
        {
            IRowMapper<LogSMS> mapper = MapBuilder<LogSMS>.MapAllProperties()

            .Map(m => m.ID).ToColumn("ID")
            .Map(m => m.Date).ToColumn("Date")
            .Map(m => m.Message).ToColumn("Message")
            .Map(m => m.Sender).ToColumn("Sender")
            .Map(m => m.Receipient).ToColumn("Recipient")
            .Map(m=>m.Status).ToColumn("Status")
            .Build();

            return mapper;
        }
        #endregion

    }
}

