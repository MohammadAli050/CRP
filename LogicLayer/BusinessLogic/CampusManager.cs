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
    public class CampusManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "CampusCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Campus> GetCacheAsList(string rawKey)
        {
            List<Campus> list = (List<Campus>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Campus GetCacheItem(string rawKey)
        {
            Campus item = (Campus)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(Campus campus)
        {
            int id = RepositoryManager.Campus_Repository.Insert(campus);
            InvalidateCache();
            return id;
        }

        public static bool Update(Campus campus)
        {
            bool isExecute = RepositoryManager.Campus_Repository.Update(campus);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Campus_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Campus GetById(int id)
        {
            string rawKey = "CampusByID" + id;
            Campus campus = GetCacheItem(rawKey);

            if (campus == null)
            {
                campus = RepositoryManager.Campus_Repository.GetById(id);
                if (campus != null)
                    AddCacheItem(rawKey,campus);
            }

            return campus;
        }

        public static List<Campus> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "CampusGetAll";

            List<Campus> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Campus_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

