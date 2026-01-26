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
    public class UserObjectControlManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "UserObjectControlCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<UserObjectControl> GetCacheAsList(string rawKey)
        {
            List<UserObjectControl> list = (List<UserObjectControl>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static UserObjectControl GetCacheItem(string rawKey)
        {
            UserObjectControl item = (UserObjectControl)HttpRuntime.Cache[GetCacheKey(rawKey)];
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
        
        public static int Insert(UserObjectControl userObjectControl)
        {
            int id = RepositoryManager.UserObjectControl_Repository.Insert(userObjectControl);
            InvalidateCache();
            return id;
        }

        public static bool Update(UserObjectControl userObjectControl)
        {
            bool isExecute = RepositoryManager.UserObjectControl_Repository.Update(userObjectControl);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.UserObjectControl_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static UserObjectControl GetById(int id)
        {
            string rawKey = "UserObjectControlByID" + id;
            UserObjectControl userObjectControl = GetCacheItem(rawKey);

            if (userObjectControl == null)
            {
                userObjectControl = RepositoryManager.UserObjectControl_Repository.GetById(id);
                if (userObjectControl != null)
                    AddCacheItem(rawKey, userObjectControl);
            }

            return userObjectControl;
        }

        public static List<UserObjectControl> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "UserObjectControlGetAll";

            List<UserObjectControl> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.UserObjectControl_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<UserObjectControl> GetAll(int userId)
        {
            string rawKey = "UserObjectControlGetAllByUserId" + userId;

            List<UserObjectControl> list = GetCacheAsList(rawKey);

            if (list == null || list.Count() == 0)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.UserObjectControl_Repository.GetAll(userId);
                if (list != null && list.Count() != 0)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
