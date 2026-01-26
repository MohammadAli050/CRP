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
   public class StdAcademicCalenderManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "Std_AcademicCalenderCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StdAcademicCalender> GetCacheAsList(string rawKey)
        {
            List<StdAcademicCalender> list = (List<StdAcademicCalender>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StdAcademicCalender GetCacheItem(string rawKey)
        {
            StdAcademicCalender item = (StdAcademicCalender)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StdAcademicCalender std_AcademicCalender)
        {
            int id = RepositoryManager.Std_AcademicCalender_Repository.Insert(std_AcademicCalender);
            InvalidateCache();
            return id;
        }

        public static bool Update(StdAcademicCalender std_AcademicCalender)
        {
            bool isExecute = RepositoryManager.Std_AcademicCalender_Repository.Update(std_AcademicCalender);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Std_AcademicCalender_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StdAcademicCalender GetById(int? id)
        {
            string rawKey = "StdAcademicCalenderID" + id;
            StdAcademicCalender std_AcademicCalender = GetCacheItem(rawKey);

            if (std_AcademicCalender == null)
            {
                std_AcademicCalender = RepositoryManager.Std_AcademicCalender_Repository.GetById(id);
                if (std_AcademicCalender != null)
                    AddCacheItem(rawKey, std_AcademicCalender);
            }

            return std_AcademicCalender;
        }

        public static List<StdAcademicCalender> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "Std_AcademicCalenderGetAll";

            List<StdAcademicCalender> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Std_AcademicCalender_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
