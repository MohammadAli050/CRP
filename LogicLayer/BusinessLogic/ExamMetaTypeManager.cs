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
    public class ExamMetaTypeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamMetaTypeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamMetaType> GetCacheAsList(string rawKey)
        {
            List<ExamMetaType> list = (List<ExamMetaType>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamMetaType GetCacheItem(string rawKey)
        {
            ExamMetaType item = (ExamMetaType)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamMetaType exammetatype)
        {
            int id = RepositoryManager.ExamMetaType_Repository.Insert(exammetatype);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamMetaType exammetatype)
        {
            bool isExecute = RepositoryManager.ExamMetaType_Repository.Update(exammetatype);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamMetaType_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamMetaType GetById(int? id)
        {
            string rawKey = "ExamMetaTypeByID" + id;
            ExamMetaType exammetatype = GetCacheItem(rawKey);

            if (exammetatype == null)
            {
                exammetatype = RepositoryManager.ExamMetaType_Repository.GetById(id);
                if (exammetatype != null)
                    AddCacheItem(rawKey,exammetatype);
            }

            return exammetatype;
        }

        public static List<ExamMetaType> GetAll()
        {

            List<ExamMetaType> list = null;

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamMetaType_Repository.GetAll();
            }

            return list;
        }
    }
}

