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
    public class AcademicCalenderScheduleManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "AcademicCalenderScheduleCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<AcademicCalenderSchedule> GetCacheAsList(string rawKey)
        {
            List<AcademicCalenderSchedule> list = (List<AcademicCalenderSchedule>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static AcademicCalenderSchedule GetCacheItem(string rawKey)
        {
            AcademicCalenderSchedule item = (AcademicCalenderSchedule)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(AcademicCalenderSchedule acacalshedule)
        {
            int id = RepositoryManager.AcademicCalenderSchedule_Repository.Insert(acacalshedule);
            InvalidateCache();
            return id;
        }

        public static bool Update(AcademicCalenderSchedule acacalshedule)
        {
            bool isExecute = RepositoryManager.AcademicCalenderSchedule_Repository.Update(acacalshedule);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.AcademicCalenderSchedule_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static AcademicCalenderSchedule GetById(int? id)
        {
            string rawKey = "AcademicCalenderScheduleByID" + id;
            AcademicCalenderSchedule academiccalenderschedule = GetCacheItem(rawKey);

            if (academiccalenderschedule == null)
            {
                academiccalenderschedule = RepositoryManager.AcademicCalenderSchedule_Repository.GetById(id);
                if (academiccalenderschedule != null)
                    AddCacheItem(rawKey,academiccalenderschedule);
            }

            return academiccalenderschedule;
        }

        public static List<AcademicCalenderSchedule> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "AcademicCalenderScheduleGetAll";

            List<AcademicCalenderSchedule> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.AcademicCalenderSchedule_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

