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
    public class ClassForceOperationManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ClassForceOperationCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ClassForceOperation> GetCacheAsList(string rawKey)
        {
            List<ClassForceOperation> list = (List<ClassForceOperation>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ClassForceOperation GetCacheItem(string rawKey)
        {
            ClassForceOperation item = (ClassForceOperation)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static List<ClassForceOperation> GetAllByParameters(int programId, string batchId, int semesterId, int courseId, string studentRoll)
        {
            List<ClassForceOperation> list = RepositoryManager.ClassForceOperation_Repository.GetAllByParameters(programId, batchId, semesterId, courseId, studentRoll);

            return list;
        }
    }
}
