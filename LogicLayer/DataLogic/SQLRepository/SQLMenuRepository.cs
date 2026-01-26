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
    public partial class SQLMenuRepository : IMenuRepository
    {
        Database db = null;

        private string sqlInsert = "MenuInsert";
        private string sqlUpdate = "MenuUpdate";
        private string sqlDelete = "MenuDeleteById";
        private string sqlGetById = "MenuGetById";
        private string sqlGetAll = "MenuGetAll";
        
        public int Insert(Menu menu)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, menu, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Menu_ID");

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

        public bool Update(Menu menu)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, menu, isInsert);

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

                db.AddInParameter(cmd, "Menu_ID", DbType.Int32, id);
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

        public Menu GetById(int? id)
        {
            Menu _menu = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Menu> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Menu>(sqlGetById, rowMapper);
                _menu = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _menu;
            }

            return _menu;
        }

        public List<Menu> GetAll()
        {
            List<Menu> menuList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Menu> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Menu>(sqlGetAll, mapper);
                IEnumerable<Menu> collection = accessor.Execute();

                menuList = collection.ToList();
            }

            catch (Exception ex)
            {
                return menuList;
            }

            return menuList;
        }
                
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Menu menu, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Menu_ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Menu_ID", DbType.Int32, menu.Menu_ID);
            }

            db.AddInParameter(cmd, "Name", DbType.String, menu.Name);
            db.AddInParameter(cmd, "URL", DbType.String, menu.URL);
            db.AddInParameter(cmd, "ParentMnu_ID", DbType.Int32, menu.ParentMnu_ID);
            db.AddInParameter(cmd, "Tier", DbType.Int32, menu.Tier);
            db.AddInParameter(cmd, "IsSysAdminAccesible", DbType.Boolean, menu.IsSysAdminAccesible);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, menu.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, menu.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, menu.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, menu.ModifiedDate);
           
            return db;
        }

        private IRowMapper<Menu> GetMaper()
        {
            IRowMapper<Menu> mapper = MapBuilder<Menu>.MapAllProperties()
            .Map(m => m.Menu_ID).ToColumn("Menu_ID")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.URL).ToColumn("URL")
            .Map(m => m.ParentMnu_ID).ToColumn("ParentMnu_ID")
            .Map(m => m.Tier).ToColumn("Tier")
            .Map(m => m.IsSysAdminAccesible).ToColumn("IsSysAdminAccesible")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            
            .Build();

            return mapper;
        }
        #endregion

        public List<Menu> GetByRoleId(int roleId)
        {
            List<Menu> menuList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Menu> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Menu>("MenuGetAllByRoleId", mapper);
                IEnumerable<Menu> collection = accessor.Execute();

                menuList = collection.ToList();
            }

            catch (Exception ex)
            {
                return menuList;
            }

            return menuList;
        }
    }
}
