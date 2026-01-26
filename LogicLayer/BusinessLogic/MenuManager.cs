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
    public class MenuManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "MenuCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Menu> GetCacheAsList(string rawKey)
        {
            List<Menu> list = (List<Menu>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Menu GetCacheItem(string rawKey)
        {
            Menu item = (Menu)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(Menu menu)
        {
            int id = RepositoryManager.Menu_Repository.Insert(menu);
            InvalidateCache();
            return id;
        }

        public static bool Update(Menu menu)
        {
            bool isExecute = RepositoryManager.Menu_Repository.Update(menu);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Menu_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Menu GetById(int? id)
        {
            string rawKey = "MenuById" + id;
            Menu menu = GetCacheItem(rawKey);

            if (menu == null)
            {
                menu = RepositoryManager.Menu_Repository.GetById(id);
                if (menu != null)
                    AddCacheItem(rawKey, menu);
            }

            return menu;
        }

        public static List<Menu> GetAll()
        {
            const string rawKey = "MenuGetAll";

            List<Menu> list = GetCacheAsList(rawKey);

            if (list == null)
            {  
                list = RepositoryManager.Menu_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<Menu> GetByRoleId(int roleId)
        {
            List<Menu> list = RepositoryManager.Menu_Repository.GetByRoleId(roleId);
            return list;

        }
    }
}
