using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicLayer.BusinessLogic
{
    public class ExamSetManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "MicroTestSetCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamSet> GetCacheAsList(string rawKey)
        {
            List<ExamSet> list = (List<ExamSet>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamSet GetCacheItem(string rawKey)
        {
            ExamSet item = (ExamSet)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamSet examSet)
        {
            int id = RepositoryManager.ExamSet_Repository.Insert(examSet);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamSet examSet)
        {
            bool isExecute = RepositoryManager.ExamSet_Repository.Update(examSet);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamSet_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamSet GetById(int? id)
        {

            ExamSet examSet = RepositoryManager.ExamSet_Repository.GetById(id);
            return examSet;
        }

        public static List<ExamSet> GetAll()
        {
            List<ExamSet> examSetList = RepositoryManager.ExamSet_Repository.GetAll();
            return examSetList;
        }

        public static bool GetExamSetByName(string examSetName) 
        {
            ExamSet examSetObj = RepositoryManager.ExamSet_Repository.GetExamSetByName(examSetName);
            if (examSetObj == null) { return true; }
            else { return false; }
        }
    }
}
