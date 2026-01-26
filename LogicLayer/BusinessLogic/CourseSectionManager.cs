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
    public class CourseSectionManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "CourseSectionCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<CourseSectionByCourseIdDTO> GetCacheAsList(string rawKey)
        {
            List<CourseSectionByCourseIdDTO> list = (List<CourseSectionByCourseIdDTO>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static CourseSectionByCourseIdDTO GetCacheItem(string rawKey)
        {
            CourseSectionByCourseIdDTO item = (CourseSectionByCourseIdDTO)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static List<CourseSectionByCourseIdDTO> GetSectionByCourseId(int courseId)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "GetCourseByTeacherId";

            List<CourseSectionByCourseIdDTO> list;

            //if (list == null)
            //{
            // Item not found in cache - retrieve it and insert it into the cache
            list = RepositoryManager.CourseSection_Repository.GetSectionByCourseId(courseId);
            if (list != null)
                AddCacheItem(rawKey, list);
            //}

            return list;
        }
    }
}

