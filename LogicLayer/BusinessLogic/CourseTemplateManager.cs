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
    public class CourseTemplateManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "CourseTemplateCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<CourseTemplate> GetCacheAsList(string rawKey)
        {
            List<CourseTemplate> list = (List<CourseTemplate>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static CourseTemplate GetCacheItem(string rawKey)
        {
            CourseTemplate item = (CourseTemplate)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


       

        public static CourseTemplate GetByCourseId(int courseId)
        {
            string rawKey = "CourseTemplateByCourseId" + courseId;
            CourseTemplate coursetemplate = GetCacheItem(rawKey);

            if (coursetemplate == null)
            {
                coursetemplate = RepositoryManager.CourseTemplate_Repository.GetByCourseId(courseId);
                if (coursetemplate != null)
                    AddCacheItem(rawKey, coursetemplate);
            }

            return coursetemplate;
        }
    }
}

