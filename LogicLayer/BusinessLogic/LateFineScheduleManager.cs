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
    public class LateFineScheduleManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "LateFineScheduleCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<LateFineSchedule> GetCacheAsList(string rawKey)
        {
            List<LateFineSchedule> list = (List<LateFineSchedule>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static LateFineSchedule GetCacheItem(string rawKey)
        {
            LateFineSchedule item = (LateFineSchedule)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(LateFineSchedule latefineschedule)
        {
            int id = RepositoryManager.LateFineSchedule_Repository.Insert(latefineschedule);
            InvalidateCache();
            return id;
        }

        public static bool Update(LateFineSchedule latefineschedule)
        {
            bool isExecute = RepositoryManager.LateFineSchedule_Repository.Update(latefineschedule);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.LateFineSchedule_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static LateFineSchedule GetById(int? id)
        {
            string rawKey = "LateFineScheduleByID" + id;
            LateFineSchedule latefineschedule = GetCacheItem(rawKey);

            if (latefineschedule == null)
            {
                latefineschedule = RepositoryManager.LateFineSchedule_Repository.GetById(id);
                if (latefineschedule != null)
                    AddCacheItem(rawKey,latefineschedule);
            }

            return latefineschedule;
        }

        public static List<LateFineSchedule> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "LateFineScheduleGetAll";

            List<LateFineSchedule> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.LateFineSchedule_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

