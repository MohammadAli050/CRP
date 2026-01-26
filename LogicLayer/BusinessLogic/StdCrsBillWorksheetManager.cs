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
    public class StdCrsBillWorksheetManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StdCrsBillWorksheetCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StdCrsBillWorksheet> GetCacheAsList(string rawKey)
        {
            List<StdCrsBillWorksheet> list = (List<StdCrsBillWorksheet>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StdCrsBillWorksheet GetCacheItem(string rawKey)
        {
            StdCrsBillWorksheet item = (StdCrsBillWorksheet)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StdCrsBillWorksheet stdCrsBillWorksheet)
        {
            int id = RepositoryManager.StdCrsBillWorksheet_Repository.Insert(stdCrsBillWorksheet);
            InvalidateCache();
            return id;
        }

        public static bool Update(StdCrsBillWorksheet stdCrsBillWorksheet)
        {
            bool isExecute = RepositoryManager.StdCrsBillWorksheet_Repository.Update(stdCrsBillWorksheet);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StdCrsBillWorksheet_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StdCrsBillWorksheet GetById(int? id)
        {
            string rawKey = "StdCrsBillWorksheetById" + id;
            StdCrsBillWorksheet stdCrsBillWorksheet = GetCacheItem(rawKey);

            if (stdCrsBillWorksheet == null)
            {
                stdCrsBillWorksheet = RepositoryManager.StdCrsBillWorksheet_Repository.GetById(id);
                if (stdCrsBillWorksheet != null)
                    AddCacheItem(rawKey, stdCrsBillWorksheet);
            }

            return stdCrsBillWorksheet;
        }

        public static List<StdCrsBillWorksheet> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StdCrsBillWorksheetGetAll";

            List<StdCrsBillWorksheet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StdCrsBillWorksheet_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
