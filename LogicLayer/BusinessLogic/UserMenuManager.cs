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
    public class UserMenuManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "UserMenuCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<UserMenu> GetCacheAsList(string rawKey)
        {
            List<UserMenu> list = (List<UserMenu>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static UserMenu GetCacheItem(string rawKey)
        {
            UserMenu item = (UserMenu)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(UserMenu userMenu)
        {
            int id = RepositoryManager.UserMenu_Repository.Insert(userMenu);
            InvalidateCache();
            return id;
        }

        public static bool Update(UserMenu userMenu)
        {
            bool isExecute = RepositoryManager.UserMenu_Repository.Update(userMenu);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.UserMenu_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static UserMenu GetById(int id)
        {
            string rawKey = "UserMenuByID" + id;
            UserMenu userMenu = GetCacheItem(rawKey);

            if (userMenu == null)
            {
                userMenu = RepositoryManager.UserMenu_Repository.GetById(id);
                if (userMenu != null)
                    AddCacheItem(rawKey, userMenu);
            }

            return userMenu;
        }

        public static List<UserMenu> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "UserMenuGetAll";

            List<UserMenu> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.UserMenu_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<UserMenu> GetAll(int userId)
        {
            string rawKey = "UserMenuGetAllByUserId" + userId;

            List<UserMenu> list = GetCacheAsList(rawKey);

            if (list == null || list.Count() == 0)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.UserMenu_Repository.GetAll(userId);
                if (list != null && list.Count() != 0)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

    }
}
