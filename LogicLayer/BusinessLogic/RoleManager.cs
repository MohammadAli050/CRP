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
    public class RoleManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "RoleCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Role> GetCacheAsList(string rawKey)
        {
            List<Role> list = (List<Role>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Role GetCacheItem(string rawKey)
        {
            Role item = (Role)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(Role role)
        {
            int id = RepositoryManager.Role_Repository.Insert(role);
            InvalidateCache();
            return id;
        }

        public static bool Update(Role role)
        {
            bool isExecute = RepositoryManager.Role_Repository.Update(role);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Role_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Role GetById(int? id)
        {
            string rawKey = "RoleById" + id;
            Role role = GetCacheItem(rawKey);

            if (role == null)
            {
                role = RepositoryManager.Role_Repository.GetById(id);
                if (role != null)
                    AddCacheItem(rawKey, role);
            }

            return role;
        }


        public static List<Role> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "RoleGetAll";

            List<Role> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Role_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
