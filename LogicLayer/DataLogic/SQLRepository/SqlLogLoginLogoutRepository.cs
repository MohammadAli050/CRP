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
    public partial class SqlLogLoginLogoutRepository : ILogLoginLogoutRepository
    {

        Database db = null;

        private string sqlInsert = "LogLoginLogoutInsert";
        private string sqlGetAll = "LogLoginLogoutGetAll";
        private string sqlGetByDate = "LogLoginLogoutGetByDate";
        public int Insert(LogLoginLogout logloginlogout)
        {
            int id = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>("ConnectionLoG");
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, logloginlogout);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "SL");

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


        public List<LogLoginLogout> GetAll()
        {
            List<LogLoginLogout> logloginlogoutList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>("ConnectionLoG");

                IRowMapper<LogLoginLogout> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<LogLoginLogout>(sqlGetAll, mapper);
                IEnumerable<LogLoginLogout> collection = accessor.Execute();

                logloginlogoutList = collection.ToList();
            }

            catch (Exception ex)
            {
                return logloginlogoutList;
            }

            return logloginlogoutList;
        }

        public List<LogLoginLogout> GetByDateRange(DateTime fromDate,DateTime toDate)
        {
            List<LogLoginLogout> logloginlogoutList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>("ConnectionLoG");

                IRowMapper<LogLoginLogout> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<LogLoginLogout>(sqlGetByDate, mapper);
                IEnumerable<LogLoginLogout> collection = accessor.Execute(fromDate,toDate);

                logloginlogoutList = collection.ToList();
            }

            catch (Exception ex)
            {
                return logloginlogoutList;
            }

            return logloginlogoutList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, LogLoginLogout logloginlogout)
        {
            db.AddOutParameter(cmd, "SL", DbType.Int32, Int32.MaxValue);
            db.AddInParameter(cmd, "DateTime", DbType.DateTime, logloginlogout.DateTime);
            db.AddInParameter(cmd, "LoginID", DbType.String, logloginlogout.LoginID);
            db.AddInParameter(cmd, "PasswordAttempts", DbType.String, logloginlogout.PasswordAttempts);
            db.AddInParameter(cmd, "loginStatus", DbType.String, logloginlogout.loginStatus);
            db.AddInParameter(cmd, "Message", DbType.String, logloginlogout.Message);
            db.AddInParameter(cmd, "LogInLogOut", DbType.String, logloginlogout.LogInLogOut);

            return db;
        }

        private IRowMapper<LogLoginLogout> GetMaper()
        {
            IRowMapper<LogLoginLogout> mapper = MapBuilder<LogLoginLogout>.MapAllProperties()

            .Map(m => m.DateTime).ToColumn("DateTime")
            .Map(m => m.LoginID).ToColumn("LoginID")
            .Map(m => m.PasswordAttempts).ToColumn("PasswordAttempts")
            .Map(m => m.loginStatus).ToColumn("loginStatus")
            .Map(m => m.Message).ToColumn("Message")
            .Map(m => m.LogInLogOut).ToColumn("LogInLogOut")

            .Build();

            return mapper;
        }
        #endregion

    }
}

