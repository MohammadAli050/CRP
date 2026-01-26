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
    public class StdDiscountCurrentManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StdDiscountCurrentCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StdDiscountCurrent> GetCacheAsList(string rawKey)
        {
            List<StdDiscountCurrent> list = (List<StdDiscountCurrent>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StdDiscountCurrent GetCacheItem(string rawKey)
        {
            StdDiscountCurrent item = (StdDiscountCurrent)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StdDiscountCurrent stdDiscountCurrent)
        {
            int id = RepositoryManager.StdDiscountCurrent_Repository.Insert(stdDiscountCurrent);
            InvalidateCache();
            return id;
        }

        public static bool Update(StdDiscountCurrent stdDiscountCurrent)
        {
            bool isExecute = RepositoryManager.StdDiscountCurrent_Repository.Update(stdDiscountCurrent);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StdDiscountCurrent_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StdDiscountCurrent GetById(int? id)
        {
            string rawKey = "StdDiscountCurrentById" + id;
            StdDiscountCurrent stdDiscountCurrent = GetCacheItem(rawKey);

            if (stdDiscountCurrent == null)
            {
                stdDiscountCurrent = RepositoryManager.StdDiscountCurrent_Repository.GetById(id);
                if (stdDiscountCurrent != null)
                    AddCacheItem(rawKey, stdDiscountCurrent);
            }

            return stdDiscountCurrent;
        }

        public static List<StdDiscountCurrent> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StdDiscountCurrentGetAll";

            List<StdDiscountCurrent> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StdDiscountCurrent_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
