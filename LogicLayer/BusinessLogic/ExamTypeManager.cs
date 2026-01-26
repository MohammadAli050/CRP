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
    public class ExamTypeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamTypeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamType> GetCacheAsList(string rawKey)
        {
            List<ExamType> list = (List<ExamType>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamType GetCacheItem(string rawKey)
        {
            ExamType item = (ExamType)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamType examtype)
        {
            int id = RepositoryManager.ExamType_Repository.Insert(examtype);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamType examtype)
        {
            bool isExecute = RepositoryManager.ExamType_Repository.Update(examtype);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamType_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamType GetById(int? id)
        {
            string rawKey = "ExamTypeByID" + id;
            ExamType examtype = GetCacheItem(rawKey);

            if (examtype == null)
            {
                examtype = RepositoryManager.ExamType_Repository.GetById(id);
                if (examtype != null)
                    AddCacheItem(rawKey,examtype);
            }

            return examtype;
        }

        public static List<ExamType> GetAll()
        {

            List<ExamType> list = null;

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamType_Repository.GetAll();
            }

            return list;
        }
    }
}

