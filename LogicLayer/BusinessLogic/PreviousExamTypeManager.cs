using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using System.Web;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class PreviousExamTypeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "PreviousExamTypeManagerCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<PreviousExamType> GetCacheAsList(string rawKey)
        {
            List<PreviousExamType> list = (List<PreviousExamType>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static PreviousExamType GetCacheItem(string rawKey)
        {
            PreviousExamType item = (PreviousExamType)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return item;
        }

        public static void AddCacheItem(string rawKey, object value)
        {
            System.Web.Caching.Cache DataCache = HttpRuntime.Cache;
            if (DataCache[MasterCacheKeyArray[0]] == null)
                DataCache[MasterCacheKeyArray[0]] = DateTime.Now;
            System.Web.Caching.CacheDependency dependency = new System.Web.Caching.CacheDependency(null, MasterCacheKeyArray);
            DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddMinutes(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public static void InvalidateCache()
        {
            // Remove the cache dependency
            HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        }

        #endregion

        public static int Insert(PreviousExamType exam)
        {
            int id = RepositoryManager.PreviousExamType_Repository.Insert(exam);
            InvalidateCache();
            return id;
        }

        public static bool Update(PreviousExamType exam)
        {
            bool isExecute = RepositoryManager.PreviousExamType_Repository.Update(exam);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.PreviousExamType_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static PreviousExamType GetById(int id)
        {
            string rawKey = "PreviousExamType" + id;
            PreviousExamType exam = GetCacheItem(rawKey);

            if (exam == null)
            {
                exam = RepositoryManager.PreviousExamType_Repository.GetById(id);
                if (exam != null)
                    AddCacheItem(rawKey, exam);
            }

            return exam;
        }

        public static List<PreviousExamType> GetAll()
        {
            List<PreviousExamType> list = RepositoryManager.PreviousExamType_Repository.GetAll();
            return list;
        }
    }
}
