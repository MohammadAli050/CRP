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
   public class ExamMarksAllocationManager
    {
        #region Cache

       public static readonly string[] MasterCacheKeyArray = { "ExamMarksAllocationCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamMarksAllocation> GetCacheAsList(string rawKey)
        {
            List<ExamMarksAllocation> list = (List<ExamMarksAllocation>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamMarksAllocation GetCacheItem(string rawKey)
        {
            ExamMarksAllocation item = (ExamMarksAllocation)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamMarksAllocation examMarksAllocation)
        {
            int id = RepositoryManager.ExamMarksAllocation_Repository.Insert(examMarksAllocation);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamMarksAllocation examMarksAllocation)
        {
            bool isExecute = RepositoryManager.ExamMarksAllocation_Repository.Update(examMarksAllocation);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamMarksAllocation_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamMarksAllocation GetById(int? id)
        {
            string rawKey = "ExamMarksAllocationID" + id;
            ExamMarksAllocation examMarksAllocation = GetCacheItem(rawKey);

            if (examMarksAllocation == null)
            {
                examMarksAllocation = RepositoryManager.ExamMarksAllocation_Repository.GetById(id);
                if (examMarksAllocation != null)
                    AddCacheItem(rawKey, examMarksAllocation);
            }

            return examMarksAllocation;
        }

        public static List<ExamMarksAllocation> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamMarksAllocationGetAll";

            List<ExamMarksAllocation> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamMarksAllocation_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

    }
}
