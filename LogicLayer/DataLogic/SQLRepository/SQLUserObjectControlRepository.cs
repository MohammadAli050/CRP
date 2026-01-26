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
    public partial class SQLUserObjectControlRepository : IUserObjectControlRepository
    {
        Database db = null;

        private string sqlInsert = "UserObjectControlInsert";
        private string sqlUpdate = "UserObjectControlUpdate";
        private string sqlDelete = "UserObjectControlDelete";
        private string sqlGetById = "UserObjectControlGetById";
        private string sqlGetAll = "UserObjectControlGetAll";
        private string sqlGetAllByUserId = "UserObjectControlGetAllByUserId";

        public int Insert(UserObjectControl userObjectControl)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, userObjectControl, isInsert);
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

        public bool Update(UserObjectControl userObjectControl)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, userObjectControl, isInsert);

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

        public UserObjectControl GetById(int id)
        {
            UserObjectControl _userObjectControl = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserObjectControl> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserObjectControl>(sqlGetById, rowMapper);
                _userObjectControl = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _userObjectControl;
            }

            return _userObjectControl;
        }

        public List<UserObjectControl> GetAll()
        {
            List<UserObjectControl> userObjectControlList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserObjectControl> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserObjectControl>(sqlGetAll, mapper);
                IEnumerable<UserObjectControl> collection = accessor.Execute();

                userObjectControlList = collection.ToList();
            }

            catch (Exception ex)
            {
                return userObjectControlList;
            }

            return userObjectControlList;
        }

        public List<UserObjectControl> GetAll(int UserId)
        {
            List<UserObjectControl> userObjectControlList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserObjectControl> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserObjectControl>(sqlGetAllByUserId, mapper);
                IEnumerable<UserObjectControl> collection = accessor.Execute(UserId);

                userObjectControlList = collection.ToList();
            }

            catch (Exception ex)
            {
                return userObjectControlList;
            }

            return userObjectControlList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, UserObjectControl userObjectControl, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, userObjectControl.Id);
            }

            db.AddInParameter(cmd, "ObjectControlId", DbType.Int32, userObjectControl.ObjectControlId);
            db.AddInParameter(cmd, "UserId", DbType.Int32, userObjectControl.UserId);
            db.AddInParameter(cmd, "ValidFrom", DbType.DateTime, userObjectControl.ValidFrom);
            db.AddInParameter(cmd, "ValidTo", DbType.DateTime, userObjectControl.ValidTo);
            db.AddInParameter(cmd, "ProgramId", DbType.Int32, userObjectControl.ProgramId);
            db.AddInParameter(cmd, "DeptId", DbType.Int32, userObjectControl.DeptId);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, userObjectControl.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, userObjectControl.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, userObjectControl.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, userObjectControl.ModifiedDate);

            return db;
        }

        private IRowMapper<UserObjectControl> GetMaper()
        {
            IRowMapper<UserObjectControl> mapper = MapBuilder<UserObjectControl>.MapAllProperties()

            .Map(m => m.Id).ToColumn("Id")
            .Map(m => m.ObjectControlId).ToColumn("ObjectControlId")
            .Map(m => m.UserId).ToColumn("UserId")
            .Map(m => m.ValidFrom).ToColumn("ValidFrom")
            .Map(m => m.ValidTo).ToColumn("ValidTo")
            .Map(m => m.ProgramId).ToColumn("ProgramId")
            .Map(m => m.DeptId).ToColumn("DeptId")
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
