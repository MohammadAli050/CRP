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
   public class CourseWavTransfrMasterManager
    {
        #region Cache

       public static readonly string[] MasterCacheKeyArray = { "CourseWavTransfrMasterCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<CourseWavTransfrMaster> GetCacheAsList(string rawKey)
        {
            List<CourseWavTransfrMaster> list = (List<CourseWavTransfrMaster>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static CourseWavTransfrMaster GetCacheItem(string rawKey)
        {
            CourseWavTransfrMaster item = (CourseWavTransfrMaster)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(CourseWavTransfrMaster courseWavTransfrMaster)
        {
            int id = RepositoryManager.CourseWavTransfrMaster_Repository.Insert(courseWavTransfrMaster);
            InvalidateCache();
            return id;
        }

        public static bool Update(CourseWavTransfrMaster CourseWavTransfrMaster)
        {
            bool isExecute = RepositoryManager.CourseWavTransfrMaster_Repository.Update(CourseWavTransfrMaster);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.CourseWavTransfrMaster_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static CourseWavTransfrMaster GetById(int? id)
        {
            string rawKey = "CourseWavTransfrMasterID" + id;
            CourseWavTransfrMaster courseWavTransfrMaster = GetCacheItem(rawKey);

            if (courseWavTransfrMaster == null)
            {
                courseWavTransfrMaster = RepositoryManager.CourseWavTransfrMaster_Repository.GetById(id);
                if (courseWavTransfrMaster != null)
                    AddCacheItem(rawKey, courseWavTransfrMaster);
            }

            return courseWavTransfrMaster;
        }

        public static List<CourseWavTransfrMaster> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "CourseWavTransfrMasterGetAll";

            List<CourseWavTransfrMaster> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CourseWavTransfrMaster_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
