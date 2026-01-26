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
    public class BuildingManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "BuildingCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Building> GetCacheAsList(string rawKey)
        {
            List<Building> list = (List<Building>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Building GetCacheItem(string rawKey)
        {
            Building item = (Building)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(Building building)
        {
            int id = RepositoryManager.Building_Repository.Insert(building);
            InvalidateCache();
            return id;
        }

        public static bool Update(Building building)
        {
            bool isExecute = RepositoryManager.Building_Repository.Update(building);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Building_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Building GetById(int id)
        {
            string rawKey = "BuildingByID" + id;
            Building building = GetCacheItem(rawKey);

            if (building == null)
            {
                building = RepositoryManager.Building_Repository.GetById(id);
                if (building != null)
                    AddCacheItem(rawKey,building);
            }

            return building;
        }

        public static List<Building> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "BuildingGetAll";

            List<Building> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Building_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

