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
    public partial class SQLRoleRepository : IRoleRepository
    {
        Database db = null;

        private string sqlInsert = "RoleInsert";
        private string sqlUpdate = "RoleUpdate";
        private string sqlDelete = "RoleDeleteById";
        private string sqlGetById = "RoleGetById";
        private string sqlGetAll = "RoleGetAll";
        
        public int Insert(Role role)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, role, isInsert);
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

        public bool Update(Role role)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, role, isInsert);

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

                db.AddInParameter(cmd, "ID", DbType.Int32, id);
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

        public Role GetById(int? id)
        {
            Role _role = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Role> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Role>(sqlGetById, rowMapper);
                _role = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _role;
            }

            return _role;
        }

        public List<Role> GetAll()
        {
            List<Role> roleList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Role> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Role>(sqlGetAll, mapper);
                IEnumerable<Role> collection = accessor.Execute();

                roleList = collection.ToList();
            }

            catch (Exception ex)
            {
                return roleList;
            }

            return roleList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Role role, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ID", DbType.Int32, role.ID);
            }

            db.AddInParameter(cmd, "RoleName", DbType.String, role.RoleName);
            db.AddInParameter(cmd, "SessionTime", DbType.Int32, role.SessionTime);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, role.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, role.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, role.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, role.ModifiedDate);
            
            return db;
        }

        private IRowMapper<Role> GetMaper()
        {
            IRowMapper<Role> mapper = MapBuilder<Role>.MapAllProperties()
            .Map(m => m.ID).ToColumn("ID")
            .Map(m => m.RoleName).ToColumn("RoleName")
            .Map(m => m.SessionTime).ToColumn("SessionTime")
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
