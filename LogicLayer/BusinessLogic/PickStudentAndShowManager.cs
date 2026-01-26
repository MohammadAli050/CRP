using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicLayer.BusinessLogic
{
   public class PickStudentAndShowManager
    {
        #region Cache

       public static readonly string[] MasterCacheKeyArray = { "PickStudentAndShowCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<PickStudentAndShow> GetCacheAsList(string rawKey)
        {
            List<PickStudentAndShow> list = (List<PickStudentAndShow>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static PickStudentAndShow GetCacheItem(string rawKey)
        {
            PickStudentAndShow item = (PickStudentAndShow)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static List<PickStudentAndShow> GetAll(string Roll)
        {

            string rawKey = "PickStudentAndShowGetAll" + Roll.ToString();

            List<PickStudentAndShow> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.PickStudentAndShow_Repository.GetAll(Roll);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
