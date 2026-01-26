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
    public class CourseStatusManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "CourseStatusCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<CourseStatus> GetCacheAsList(string rawKey)
        {
            List<CourseStatus> list = (List<CourseStatus>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static CourseStatus GetCacheItem(string rawKey)
        {
            CourseStatus item = (CourseStatus)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(CourseStatus courseStatus)
        {
            int id = RepositoryManager.CourseStatus_Repository.Insert(courseStatus);
            InvalidateCache();
            return id;
        }

        public static bool Update(CourseStatus courseStatus)
        {
            bool isExecute = RepositoryManager.CourseStatus_Repository.Update(courseStatus);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.CourseStatus_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static CourseStatus GetById(int? id)
        {
            string rawKey = "CourseStatusById" + id;
            CourseStatus courseStatus = GetCacheItem(rawKey);

            if (courseStatus == null)
            {
                courseStatus = RepositoryManager.CourseStatus_Repository.GetById(id);
                if (courseStatus != null)
                    AddCacheItem(rawKey, courseStatus);
            }

            return courseStatus;
        }

        public static CourseStatus GetByCode(string code)
        {
            string rawKey = "CourseStatusByCode" + code;
            CourseStatus courseStatus = GetCacheItem(rawKey);

            if (courseStatus == null)
            {
                courseStatus = RepositoryManager.CourseStatus_Repository.GetByCode(code);
                if (courseStatus != null)
                    AddCacheItem(rawKey, courseStatus);
            }

            return courseStatus;
        }

        public static List<CourseStatus> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "CourseStatusGetAll";

            List<CourseStatus> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CourseStatus_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

      
    }
}
