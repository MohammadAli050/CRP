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
   public class CourseWavTransfrManager
    {
        #region Cache

       public static readonly string[] MasterCacheKeyArray = { "CourseWavTransfrCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<CourseWavTransfr> GetCacheAsList(string rawKey)
        {
            List<CourseWavTransfr> list = (List<CourseWavTransfr>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static CourseWavTransfr GetCacheItem(string rawKey)
        {
            CourseWavTransfr item = (CourseWavTransfr)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(CourseWavTransfr courseWavTransfr)
        {
            int id = RepositoryManager.CourseWavTransfr_Repository.Insert(courseWavTransfr);
            InvalidateCache();
            return id;
        }

        public static bool Update(CourseWavTransfr courseWavTransfr)
        {
            bool isExecute = RepositoryManager.CourseWavTransfr_Repository.Update(courseWavTransfr);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.CourseWavTransfr_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static CourseWavTransfr GetById(int? id)
        {
            string rawKey = "CourseWavTransfrID" + id;
            CourseWavTransfr courseWavTransfr = GetCacheItem(rawKey);

            if (courseWavTransfr == null)
            {
                courseWavTransfr = RepositoryManager.CourseWavTransfr_Repository.GetById(id);
                if (courseWavTransfr != null)
                    AddCacheItem(rawKey, courseWavTransfr);
            }

            return courseWavTransfr;
        }

        public static List<CourseWavTransfr> GetByStudentId(int studentId)
        {
            List<CourseWavTransfr> list = RepositoryManager.CourseWavTransfr_Repository.GetByStudentId(studentId);
            return list;
        }

        public static List<CourseWavTransfr> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "CourseWavTransfrGetAll";

            List<CourseWavTransfr> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CourseWavTransfr_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<CourseWavTransfr> GetUniqueAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "CourseWavTransfrGetUniqueAll";

            List<CourseWavTransfr> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CourseWavTransfr_Repository.GetUniqueAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
