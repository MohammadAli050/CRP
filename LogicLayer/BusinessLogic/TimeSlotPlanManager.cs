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
    public  class TimeSlotPlanManager
    {

        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "TimeSlotPlanNewCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<TimeSlotPlanNew> GetCacheAsList(string rawKey)
        {
            List<TimeSlotPlanNew> list = (List<TimeSlotPlanNew>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static TimeSlotPlanNew GetCacheItem(string rawKey)
        {
            TimeSlotPlanNew item = (TimeSlotPlanNew)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(TimeSlotPlanNew timeSlotPlan)
        {
            int id = RepositoryManager.TimeSlotPlan_Repository.Insert(timeSlotPlan);
            InvalidateCache();
            return id;
        }

        public static bool Update(TimeSlotPlanNew timeSlotPlan)
        {
            bool isExecute = RepositoryManager.TimeSlotPlan_Repository.Update(timeSlotPlan);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.TimeSlotPlan_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static TimeSlotPlanNew GetById(int id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "TimeSlotPlanNewById" + id;
            TimeSlotPlanNew timeSlotPlan = GetCacheItem(rawKey);

            if (timeSlotPlan == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                timeSlotPlan = RepositoryManager.TimeSlotPlan_Repository.GetById(id);
                if (timeSlotPlan != null)
                    AddCacheItem(rawKey, timeSlotPlan);
            }

            return timeSlotPlan;
        }

        public static List<TimeSlotPlanNew> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "TimeSlotPlanGetAll";

            List<TimeSlotPlanNew> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.TimeSlotPlan_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
