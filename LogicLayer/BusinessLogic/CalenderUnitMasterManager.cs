using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class CalenderUnitMasterManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "CalenderUnitMasterCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<CalenderUnitMaster> GetCacheAsList(string rawKey)
        {
            List<CalenderUnitMaster> list = (List<CalenderUnitMaster>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static CalenderUnitMaster GetCacheItem(string rawKey)
        {
            CalenderUnitMaster item = (CalenderUnitMaster)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(CalenderUnitMaster obj)
        {
            int id = RepositoryManager.CalenderUnitMaster_Repository.Insert(obj);
            InvalidateCache();
            return id;
        }

        public static bool Update(CalenderUnitMaster obj)
        {
            bool isExecute = RepositoryManager.CalenderUnitMaster_Repository.Update(obj);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.CalenderUnitMaster_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static CalenderUnitMaster GetById(int? id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "CalenderUnitMasterById" + id;
            CalenderUnitMaster obj = GetCacheItem(rawKey);

            if (obj == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                obj = RepositoryManager.CalenderUnitMaster_Repository.GetById(id);
                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static List<CalenderUnitMaster> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "CalenderUnitMasterGetAll";

            List<CalenderUnitMaster> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CalenderUnitMaster_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
