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
   public class CourseWavTransfrDetailManager
    {
        #region Cache

       public static readonly string[] MasterCacheKeyArray = { "CourseWavTransfrDetailCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<CourseWavTransfrDetail> GetCacheAsList(string rawKey)
        {
            List<CourseWavTransfrDetail> list = (List<CourseWavTransfrDetail>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static CourseWavTransfrDetail GetCacheItem(string rawKey)
        {
            CourseWavTransfrDetail item = (CourseWavTransfrDetail)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(CourseWavTransfrDetail courseWavTransfrDetail)
        {
            int id = RepositoryManager.CourseWavTransfrDetail_Repository.Insert(courseWavTransfrDetail);
            InvalidateCache();
            return id;
        }

        public static bool Update(CourseWavTransfrDetail courseWavTransfrDetail)
        {
            bool isExecute = RepositoryManager.CourseWavTransfrDetail_Repository.Update(courseWavTransfrDetail);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.CourseWavTransfrDetail_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static CourseWavTransfrDetail GetById(int? id)
        {
            string rawKey = "CourseWavTransfrDetailID" + id;
            CourseWavTransfrDetail courseWavTransfrDetailID = GetCacheItem(rawKey);

            if (courseWavTransfrDetailID == null)
            {
                courseWavTransfrDetailID = RepositoryManager.CourseWavTransfrDetail_Repository.GetById(id);
                if (courseWavTransfrDetailID != null)
                    AddCacheItem(rawKey, courseWavTransfrDetailID);
            }

            return courseWavTransfrDetailID;
        }

        public static List<CourseWavTransfrDetail> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "CourseWavTransfrDetailID";

            List<CourseWavTransfrDetail> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CourseWavTransfrDetail_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

    }
}
