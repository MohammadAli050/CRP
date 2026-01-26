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
    public partial class SQLUserMenuRepository : IUserMenuRepository
    {
        Database db = null;

        private string sqlInsert = "UserMenuInsert";
        private string sqlUpdate = "UserMenuUpdate";
        private string sqlDelete = "UserMenuDelete";
        private string sqlGetById = "UserMenuGetById";
        private string sqlGetAll = "UserMenuGetAll";
        private string sqlGetAllByUserId = "UserMenuGetAllByUserId";
               
        public int Insert(UserMenu userMenu)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, userMenu, isInsert);
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

        public bool Update(UserMenu userMenu)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, userMenu, isInsert);

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

        public UserMenu GetById(int id)
        {
            UserMenu _userMenu = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserMenu> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserMenu>(sqlGetById, rowMapper);
                _userMenu = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _userMenu;
            }

            return _userMenu;
        }

        public List<UserMenu> GetAll()
        {
            List<UserMenu> userMenuList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserMenu> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserMenu>(sqlGetAll, mapper);
                IEnumerable<UserMenu> collection = accessor.Execute();

                userMenuList = collection.ToList();
            }

            catch (Exception ex)
            {
                return userMenuList;
            }

            return userMenuList;
        }

        public List<UserMenu> GetAll(int UserId)
        {
            List<UserMenu> userMenuList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserMenu> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserMenu>(sqlGetAllByUserId, mapper);
                IEnumerable<UserMenu> collection = accessor.Execute(UserId);

                userMenuList = collection.ToList();
            }

            catch (Exception ex)
            {
                return userMenuList;
            }

            return userMenuList;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, UserMenu userMenu, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, userMenu.Id);
            }

            db.AddInParameter(cmd, "MenuId", DbType.Int32, userMenu.MenuId);
            db.AddInParameter(cmd, "UserId", DbType.Int32, userMenu.UserId);
            db.AddInParameter(cmd, "ValidFrom", DbType.DateTime, userMenu.ValidFrom);
            db.AddInParameter(cmd, "ValidTo", DbType.DateTime, userMenu.ValidTo);
            db.AddInParameter(cmd, "AddRemove", DbType.Int32, userMenu.AddRemove);
            db.AddInParameter(cmd, "ProgramId", DbType.Int32, userMenu.ProgramId);
            db.AddInParameter(cmd, "DeptId", DbType.Int32, userMenu.DeptId);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, userMenu.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, userMenu.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, userMenu.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, userMenu.ModifiedDate);
            
            return db;
        }

        private IRowMapper<UserMenu> GetMaper()
        {
            IRowMapper<UserMenu> mapper = MapBuilder<UserMenu>.MapAllProperties()

       	    .Map(m => m.Id).ToColumn("Id")
		    .Map(m => m.MenuId).ToColumn("MenuId")
		    .Map(m => m.UserId).ToColumn("UserId")
		    .Map(m => m.ValidFrom).ToColumn("ValidFrom")
		    .Map(m => m.ValidTo).ToColumn("ValidTo")
            .Map(m => m.AddRemove).ToColumn("AddRemove")
		    .Map(m => m.ProgramId).ToColumn("ProgramId")
		    .Map(m => m.DeptId).ToColumn("DeptId")
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