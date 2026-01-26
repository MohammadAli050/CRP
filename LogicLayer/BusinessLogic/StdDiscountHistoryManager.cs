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
    public class StdDiscountHistoryManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StdDiscountHistoryCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StdDiscountHistory> GetCacheAsList(string rawKey)
        {
            List<StdDiscountHistory> list = (List<StdDiscountHistory>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StdDiscountHistory GetCacheItem(string rawKey)
        {
            StdDiscountHistory item = (StdDiscountHistory)HttpRuntime.Cache[GetCacheKey(rawKey)];
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
        
        
        public static int Insert(StdDiscountHistory stdDiscountHistory)
        {
            int id = RepositoryManager.StdDiscountHistory_Repository.Insert(stdDiscountHistory);
            InvalidateCache();
            return id;
        }

        public static bool Update(StdDiscountHistory stdDiscountHistory)
        {
            bool isExecute = RepositoryManager.StdDiscountHistory_Repository.Update(stdDiscountHistory);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StdDiscountHistory_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StdDiscountHistory GetById(int? id)
        {
            string rawKey = "StdDiscountHistoryById" + id;
            StdDiscountHistory stdDiscountHistory = GetCacheItem(rawKey);

            if (stdDiscountHistory == null)
            {
                stdDiscountHistory = RepositoryManager.StdDiscountHistory_Repository.GetById(id);
                if (stdDiscountHistory != null)
                    AddCacheItem(rawKey, stdDiscountHistory);
            }

            return stdDiscountHistory;
        }

        public static List<StdDiscountHistory> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StdDiscountHistoryGetAll";

            List<StdDiscountHistory> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StdDiscountHistory_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
