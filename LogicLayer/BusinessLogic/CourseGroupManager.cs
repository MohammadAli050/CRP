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
    public class CourseGroupManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "CourseGroupCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<CourseGroup> GetCacheAsList(string rawKey)
        {
            List<CourseGroup> list = (List<CourseGroup>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static CourseGroup GetCacheItem(string rawKey)
        {
            CourseGroup item = (CourseGroup)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(CourseGroup coursegroup)
        {
            int id = RepositoryManager.CourseGroup_Repository.Insert(coursegroup);
            InvalidateCache();
            return id;
        }

        public static bool Update(CourseGroup coursegroup)
        {
            bool isExecute = RepositoryManager.CourseGroup_Repository.Update(coursegroup);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.CourseGroup_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static CourseGroup GetById(int? id)
        {
            string rawKey = "CourseGroupByID" + id;
            CourseGroup coursegroup = GetCacheItem(rawKey);

            if (coursegroup == null)
            {
                coursegroup = RepositoryManager.CourseGroup_Repository.GetById(id);
                if (coursegroup != null)
                    AddCacheItem(rawKey,coursegroup);
            }

            return coursegroup;
        }

        public static List<CourseGroup> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "CourseGroupGetAll";

            List<CourseGroup> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CourseGroup_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

