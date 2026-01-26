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
    public class PreviousExamManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "PreviousExamManagerCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<PreviousExam> GetCacheAsList(string rawKey)
        {
            List<PreviousExam> list = (List<PreviousExam>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static PreviousExam GetCacheItem(string rawKey)
        {
            PreviousExam item = (PreviousExam)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(PreviousExam exam)
        {
            int id = RepositoryManager.PreviousExam_Repository.Insert(exam);
            InvalidateCache();
            return id;
        }

        public static bool Update(PreviousExam exam)
        {
            bool isExecute = RepositoryManager.PreviousExam_Repository.Update(exam);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.PreviousExam_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static PreviousExam GetById(int id)
        {
            string rawKey = "PreviousExam" + id;
            PreviousExam exam = GetCacheItem(rawKey);

            if (exam == null)
            {
                exam = RepositoryManager.PreviousExam_Repository.GetById(id);
                if (exam != null)
                    AddCacheItem(rawKey, exam);
            }

            return exam;
        }

        public static List<PreviousExam> GetAll()
        {
            List<PreviousExam> list = RepositoryManager.PreviousExam_Repository.GetAll();
            return list;
        }

    }
}
