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
   public class CourseACUSpanDtlManager
    {

        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "CourseACUSpanDtlCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<CourseACUSpanDtl> GetCacheAsList(string rawKey)
        {
            List<CourseACUSpanDtl> list = (List<CourseACUSpanDtl>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static CourseACUSpanDtl GetCacheItem(string rawKey)
        {
            CourseACUSpanDtl item = (CourseACUSpanDtl)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(CourseACUSpanDtl obj)
        {
            int id = RepositoryManager.CourseACUSpanDtl_Repository.Insert(obj);
            InvalidateCache();
            return id;
        }

        public static bool Update(CourseACUSpanDtl obj)
        {
            bool isExecute = RepositoryManager.CourseACUSpanDtl_Repository.Update(obj);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.CourseACUSpanDtl_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static CourseACUSpanDtl GetById(int? id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "CourseACUSpanDtlID" + id;
            CourseACUSpanDtl obj = GetCacheItem(rawKey);

            if (obj == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                obj = RepositoryManager.CourseACUSpanDtl_Repository.GetById(id);
                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static List<CourseACUSpanDtl> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "CourseACUSpanDtlGetAll";

            List<CourseACUSpanDtl> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CourseACUSpanDtl_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        internal static List<CourseACUSpanDtl> GetAllByCourseACUSpanMasID(int CourseACUSpanMasID)
        {
            string rawKey = "CourseACUSpanDtlGetAllByCourseACUSpanMasID" + CourseACUSpanMasID;

            List<CourseACUSpanDtl> list = GetCacheAsList(rawKey);

            if (list == null)
            {   
                list = GetAll();
                if (list != null)
                {
                    list = list.Where(x => x.CourseACUSpanMasID == CourseACUSpanMasID).ToList();

                    if (list != null)
                        AddCacheItem(rawKey, list);
                }
            }

            return list;
        }
    }
}
