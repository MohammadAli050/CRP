using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class RoleMenuManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "RoleMenuCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<RoleMenu> GetCacheAsList(string rawKey)
        {
            List<RoleMenu> list = (List<RoleMenu>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static RoleMenu GetCacheItem(string rawKey)
        {
            RoleMenu item = (RoleMenu)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return item;
        }

        public static void AddCacheItem(string rawKey, object value)
        {
            System.Web.Caching.Cache DataCache = HttpRuntime.Cache;

            // Make sure MasterCacheKeyArray[0] is in the cache - if not, add it
            if (DataCache[MasterCacheKeyArray[0]] == null)
                DataCache[MasterCacheKeyArray[0]] = DateTime.Now;

            // Add a CacheDependency
            System.Web.Caching.CacheDependency dependency = new System.Web.Caching.CacheDependency(null, MasterCacheKeyArray);
            DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddMinutes(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        }



        public static void InvalidateCache()
        {
            // Remove the cache dependency
            HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        }

        #endregion



        public static int Insert(RoleMenu roleMenu)
        {
            int id = RepositoryManager.RoleMenu_Repository.Insert(roleMenu);
            InvalidateCache();
            return id;
        }

        public static bool Update(RoleMenu roleMenu)
        {
            bool isExecute = RepositoryManager.RoleMenu_Repository.Update(roleMenu);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.RoleMenu_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static RoleMenu GetById(int? id)
        {
            string rawKey = "RoleMenuById" + id;
            RoleMenu roleMenu = GetCacheItem(rawKey);

            if (roleMenu == null)
            {
                roleMenu = RepositoryManager.RoleMenu_Repository.GetById(id);
                if (roleMenu != null)
                    AddCacheItem(rawKey, roleMenu);
            }

            return roleMenu;
        }

        public static List<RoleMenu> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "RoleMenuGetAll";

            List<RoleMenu> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RoleMenu_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
