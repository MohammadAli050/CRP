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
   public class CourseACUSpanMasManager
    {
        #region Cache

       public static readonly string[] MasterCacheKeyArray = { "CourseACUSpanMasCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<CourseACUSpanMas> GetCacheAsList(string rawKey)
        {
            List<CourseACUSpanMas> list = (List<CourseACUSpanMas>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static CourseACUSpanMas GetCacheItem(string rawKey)
        {
            CourseACUSpanMas item = (CourseACUSpanMas)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

       public static int Insert(CourseACUSpanMas obj)
        {
            int id = RepositoryManager.CourseACUSpanMas_Repository.Insert(obj);
            InvalidateCache();
            return id;
        }

       public static bool Update(CourseACUSpanMas obj)
        {
            bool isExecute = RepositoryManager.CourseACUSpanMas_Repository.Update(obj);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.CourseACUSpanMas_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static CourseACUSpanMas GetById(int? id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "CourseACUSpanMasID" + id;
            CourseACUSpanMas obj = GetCacheItem(rawKey);

            if (obj == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                obj = RepositoryManager.CourseACUSpanMas_Repository.GetById(id);
                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static List<CourseACUSpanMas> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "CourseACUSpanMasGetAll";

            List<CourseACUSpanMas> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CourseACUSpanMas_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static CourseACUSpanMas GetByCourseIDVersionID(int CourseID, int VersionID)
        {
            List<CourseACUSpanMas> list = GetAll();
            CourseACUSpanMas CourseACUSpanMas = new CourseACUSpanMas();

            if (list != null)
            {
                CourseACUSpanMas = list.Where(x => x.CourseID == CourseID && x.VersionID == VersionID).ToList().SingleOrDefault();
            }

            return CourseACUSpanMas;
        }
    }
}
