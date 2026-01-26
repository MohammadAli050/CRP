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
   public class ExamTypeNameManager
    {
        #region Cache

       public static readonly string[] MasterCacheKeyArray = { "ExamTypeNameCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamTypeName> GetCacheAsList(string rawKey)
        {
            List<ExamTypeName> list = (List<ExamTypeName>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamTypeName GetCacheItem(string rawKey)
        {
            ExamTypeName item = (ExamTypeName)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(ExamTypeName examTypeName)
        {
            int id = RepositoryManager.ExamTypeName_Repository.Insert(examTypeName);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamTypeName examTypeName)
        {
            bool isExecute = RepositoryManager.ExamTypeName_Repository.Update(examTypeName);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamTypeName_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamTypeName GetById(int? id)
        {
            string rawKey = "ExamTypeNameID" + id;
            ExamTypeName examTypeName = GetCacheItem(rawKey);

            if (examTypeName == null)
            {
                examTypeName = RepositoryManager.ExamTypeName_Repository.GetById(id);
                if (examTypeName != null)
                    AddCacheItem(rawKey, examTypeName);
            }

            return examTypeName;
        }

        public static List<ExamTypeName> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamTypeNameGetAll";

            List<ExamTypeName> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamTypeName_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
