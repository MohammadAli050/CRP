using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.BusinessLogic
{
    public class DailyCollectionManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "DailyCollectionCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<rDailyCollection> GetCacheAsList(string rawKey)
        {
            List<rDailyCollection> list = (List<rDailyCollection>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
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

        public static List<rDailyCollection> GetDailyCollectionByProgramAndDate(DateTime fromDate, DateTime toDate, int programId)
        {
           
            List<rDailyCollection> list = RepositoryManager.DailyCollection_Repository.GetDailyCollectionByProgramAndDate(fromDate, toDate, programId);

            return list;
        }
        public static List<rDailyBillHistory> GetDailyBillHistoryByProgramAndDate(DateTime fromDate, DateTime toDate, int programId)
        {
            List<rDailyBillHistory> list = RepositoryManager.DailyCollection_Repository.GetDailyBillHistoryByProgramAndDate(fromDate, toDate, programId);

            return list;
        }

       
    }
}

