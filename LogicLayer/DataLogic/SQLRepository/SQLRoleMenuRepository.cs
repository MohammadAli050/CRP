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
    public partial class SQLRoleMenuRepository : IRoleMenuRepository
    {
        Database db = null;

        private string sqlInsert = "RoleMenuInsert";
        private string sqlUpdate = "RoleMenuUpdate";
        private string sqlDelete = "RoleMenuDeleteById";
        private string sqlGetById = "RoleMenuGetById";
        private string sqlGetAll = "RoleMenuGetAll";
        
        
        public int Insert(RoleMenu roleMenu)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, roleMenu, isInsert);
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

        public bool Update(RoleMenu roleMenu)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, roleMenu, isInsert);

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

        public RoleMenu GetById(int? id)
        {
            RoleMenu _roleMenu = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RoleMenu> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RoleMenu>(sqlGetById, rowMapper);
                _roleMenu = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _roleMenu;
            }

            return _roleMenu;
        }

        public List<RoleMenu> GetAll()
        {
            List<RoleMenu> roleMenuList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RoleMenu> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RoleMenu>(sqlGetAll, mapper);
                IEnumerable<RoleMenu> collection = accessor.Execute();

                roleMenuList = collection.ToList();
            }

            catch (Exception ex)
            {
                return roleMenuList;
            }

            return roleMenuList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, RoleMenu roleMenu, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ID", DbType.Int32, roleMenu.ID);
            }

            db.AddInParameter(cmd, "RoleID", DbType.Int32, roleMenu.RoleID);
            db.AddInParameter(cmd, "MenuID", DbType.Int32, roleMenu.MenuID);
            db.AddInParameter(cmd, "AccessMenuStartDate", DbType.DateTime, roleMenu.AccessMenuStartDate);
            db.AddInParameter(cmd, "AccessMenuEndDate", DbType.DateTime, roleMenu.AccessMenuEndDate);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, roleMenu.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, roleMenu.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, roleMenu.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, roleMenu.ModifiedDate);
            
            return db;
        }

        private IRowMapper<RoleMenu> GetMaper()
        {
            IRowMapper<RoleMenu> mapper = MapBuilder<RoleMenu>.MapAllProperties()
            .Map(m => m.ID).ToColumn("ID")
            .Map(m => m.RoleID).ToColumn("RoleID")
            .Map(m => m.MenuID).ToColumn("MenuID")
            .Map(m => m.AccessMenuStartDate).ToColumn("AccessMenuStartDate")
            .Map(m => m.AccessMenuEndDate).ToColumn("AccessMenuEndDate")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .DoNotMap(m => m.MenuName)
            
            .Build();

            return mapper;
        }
        #endregion
    }
}
