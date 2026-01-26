using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
   partial class SQLActiveUserRepository:IActiveUserRepository
    {
        Database db = null;
        private string sqlGetByLogInId = "ActiveUserGetByLogInId";
        private string sqlGetAll = "ActiveUserGetAll";
        private string sqlGetById = "ActiveUserGetById";
        private string sqlUpdate = "ActiveUserUpdate";

        public UserActive GetByLogInId(String LogInId)
        {
            UserActive _activeUser = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserActive> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserActive>(sqlGetByLogInId, rowMapper);
                _activeUser = accessor.Execute(LogInId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _activeUser;
            }

            return _activeUser;
        }

        public List<UserActive> GetAll(int ValueID, int AdmissionCalenderID, string LogInID)
        {
            List<UserActive> activeUserList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserActive> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserActive>(sqlGetAll, mapper);
                IEnumerable<UserActive> collection = accessor.Execute(ValueID, AdmissionCalenderID, LogInID);

                activeUserList = collection.ToList();
            }

            catch (Exception ex)
            {
                return activeUserList;
            }

            return activeUserList;
        }

        public UserActive GetById(int? id)
        {
            UserActive _userActive = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserActive> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserActive>(sqlGetById, rowMapper);
                _userActive = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _userActive;
            }

            return _userActive;
        }

        public bool Update(UserActive userActive)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, userActive, isInsert);

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

        #region Mapper

        private Database addParam(Database db, DbCommand cmd, UserActive userActive, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "User_ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "User_ID", DbType.Int32, userActive.User_ID);
            }
            db.AddInParameter(cmd, "FirstName", DbType.String, userActive.FirstName);
            db.AddInParameter(cmd, "Phone", DbType.String, userActive.Phone);
            db.AddInParameter(cmd, "IsActive", DbType.Boolean, userActive.IsActive);
            db.AddInParameter(cmd, "LogInId", DbType.String, userActive.LogInId);
            return db;
        }


        private IRowMapper<UserActive> GetMaper()
        {
            IRowMapper<UserActive> mapper = MapBuilder<UserActive>.MapAllProperties()
            .Map(m => m.LogInId).ToColumn("LogInId")
            .Map(m => m.FirstName).ToColumn("FirstName")
            .Map(m => m.Phone).ToColumn("Phone")
            .Map(m => m.IsActive).ToColumn("IsActive")
            .Map(m => m.User_ID).ToColumn("User_ID")
            .Build();

            return mapper;
        }
        #endregion
    }
}
