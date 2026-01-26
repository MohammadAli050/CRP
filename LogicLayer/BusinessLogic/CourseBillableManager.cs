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
    public class CourseBillableManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "CourseBillableCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<CourseBillable> GetCacheAsList(string rawKey)
        {
            List<CourseBillable> list = (List<CourseBillable>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static CourseBillable GetCacheItem(string rawKey)
        {
            CourseBillable item = (CourseBillable)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(CourseBillable courseBillable)
        {
            int id = RepositoryManager.CourseBillable_Repository.Insert(courseBillable);
            InvalidateCache();
            return id;
        }

        public static bool Update(CourseBillable courseBillable)
        {
            bool isExecute = RepositoryManager.CourseBillable_Repository.Update(courseBillable);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.CourseBillable_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static CourseBillable GetById(int? id)
        {
            string rawKey = "CourseBillableById" + id;
            CourseBillable courseBillable = GetCacheItem(rawKey);

            if (courseBillable == null)
            {
                courseBillable = RepositoryManager.CourseBillable_Repository.GetById(id);
                if (courseBillable != null)
                    AddCacheItem(rawKey, courseBillable);
            }

            return courseBillable;
        }

        public static List<CourseBillable> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "CourseBillableGetAll";

            List<CourseBillable> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CourseBillable_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
