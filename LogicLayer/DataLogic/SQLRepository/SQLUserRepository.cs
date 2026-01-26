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
    public partial class SQLUserRepository : IUserRepository
    {

        Database db = null;

        private string sqlInsert = "UserInsert";
        private string sqlUpdate = "UserUpdate";
        private string sqlDelete = "UserDeleteById";
        private string sqlGetById = "UserGetById";
        private string sqlGetAll = "UserGetAll";
        private string sqlGetByPersonId = "UserGetByPersonId";
        private string sqlGetByLogInId = "UserGetByLogInId";
        private string sqlGetByUserId = "UserGetByUserId";
        private string sqlGenerateUserByProgramIdBatchId = "UserGenerateUserByProgramIdBatchId";
        private string sqlGetAllByProgramIdBatchId = "UserGetAllByProgramIdBatchId";


        public int Insert(User user)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, user, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "User_ID");

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

        public bool Update(User user)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, user, isInsert);

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

                db.AddInParameter(cmd, "User_ID", DbType.Int32, id);
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

        public User GetById(int? id)
        {
            User _user = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<User> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<User>(sqlGetById, rowMapper);
                _user = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _user;
            }

            return _user;
        }

        public List<User> GetAll()
        {
            List<User> userList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<User> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<User>(sqlGetAll, mapper);
                IEnumerable<User> collection = accessor.Execute();

                userList = collection.ToList();
            }

            catch (Exception ex)
            {
                return userList;
            }

            return userList;
        }

        public List<User> GetByPersonId(int PersonID)
        {
            List<User> userList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<User> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<User>(sqlGetByPersonId, mapper);
                IEnumerable<User> collection = accessor.Execute(PersonID);

                userList = collection.ToList();
            }

            catch (Exception ex)
            {
                return userList;
            }

            return userList;
        }

        public User GetByLogInId(string LogInID)
        {
            User _user = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<User> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<User>(sqlGetByLogInId, rowMapper);
                _user = accessor.Execute(LogInID).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _user;
            }

            return _user;
        }

        public List<User> GetByUserId(string userlogInID)
        {
            List<User> userList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<User> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<User>(sqlGetByUserId, mapper);
                IEnumerable<User> collection = accessor.Execute(userlogInID);

                userList = collection.ToList();
            }

            catch (Exception ex)
            {
                return userList;
            }

            return userList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, User user, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "User_ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "User_ID", DbType.Int32, user.User_ID);
            }

            db.AddInParameter(cmd, "LogInID", DbType.String, user.LogInID);
            db.AddInParameter(cmd, "Password", DbType.String, user.Password);
            db.AddInParameter(cmd, "RoleID", DbType.Int32, user.RoleID);
            db.AddInParameter(cmd, "RoleExistStartDate", DbType.DateTime, user.RoleExistStartDate);
            db.AddInParameter(cmd, "RoleExistEndDate", DbType.DateTime, user.RoleExistEndDate);
            db.AddInParameter(cmd, "IsActive", DbType.Boolean, user.IsActive);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, user.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, user.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, user.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, user.ModifiedDate);

            return db;
        }

        private IRowMapper<User> GetMaper()
        {
            IRowMapper<User> mapper = MapBuilder<User>.MapAllProperties()
            .Map(m => m.User_ID).ToColumn("User_ID")
            .Map(m => m.LogInID).ToColumn("LogInID")
            .Map(m => m.Password).ToColumn("Password")
            .Map(m => m.RoleID).ToColumn("RoleID")
            .Map(m => m.RoleExistStartDate).ToColumn("RoleExistStartDate")
            .Map(m => m.RoleExistEndDate).ToColumn("RoleExistEndDate")
            .Map(m => m.IsActive).ToColumn("IsActive")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .DoNotMap(m => m.RegistrationNo)

            .Build();

            return mapper;
        }

        private IRowMapper<User> GetMaper2()
        {
            IRowMapper<User> mapper = MapBuilder<User>.MapAllProperties()
            .Map(m => m.User_ID).ToColumn("User_ID")
            .Map(m => m.LogInID).ToColumn("LogInID")
            .Map(m => m.Password).ToColumn("Password")
            .Map(m => m.RoleID).ToColumn("RoleID")
            .Map(m => m.RoleExistStartDate).ToColumn("RoleExistStartDate")
            .Map(m => m.RoleExistEndDate).ToColumn("RoleExistEndDate")
            .Map(m => m.IsActive).ToColumn("IsActive")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.RegistrationNo).ToColumn("RegistrationNo")

            .Build();

            return mapper;
        }
        
        #endregion

        public int GenerateUserByProgramIdBatchId(int programId, int batchId)
        {
            int count = 0;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlGenerateUserByProgramIdBatchId);

                db.AddOutParameter(cmd, "TotalUser", DbType.Int32, Int32.MaxValue);
                db.AddInParameter(cmd, "ProgramId", DbType.Int32, programId);
                db.AddInParameter(cmd, "BatchId", DbType.Int32, batchId);

                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TotalUser");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out count);
                }
            }
            catch (Exception ex)
            {
                count = 0;
            }

            return count;
        }

        public List<User> GetAllByProgramIdBatchId(int programId, int batchId)
        {
            List<User> userList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<User> mapper = GetMaper2();

                var accessor = db.CreateSprocAccessor<User>(sqlGetAllByProgramIdBatchId, mapper);
                IEnumerable<User> collection = accessor.Execute(programId, batchId);

                userList = collection.ToList();
            }

            catch (Exception ex)
            {
                return userList;
            }

            return userList;
        }

        public User GetAdminUserByLogInId(string LogInID)
        {
            User _user = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<User> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<User>("UserGetAdminByLogInId", rowMapper);
                _user = accessor.Execute(LogInID).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _user;
            }

            return _user;
        }
         
    }
}
