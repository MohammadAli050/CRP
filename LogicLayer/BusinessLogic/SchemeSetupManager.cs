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
    public class SchemeSetupManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "SchemeSetupCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<SchemeSetup> GetCacheAsList(string rawKey)
        {
            List<SchemeSetup> list = (List<SchemeSetup>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static SchemeSetup GetCacheItem(string rawKey)
        {
            SchemeSetup item = (SchemeSetup)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(SchemeSetup schemesetup)
        {
            int id = RepositoryManager.SchemeSetup_Repository.Insert(schemesetup);
            InvalidateCache();
            return id;
        }

        public static bool Update(SchemeSetup schemesetup)
        {
            bool isExecute = RepositoryManager.SchemeSetup_Repository.Update(schemesetup);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.SchemeSetup_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static SchemeSetup GetById(int id)
        {
            string rawKey = "SchemeSetupByID" + id;
            SchemeSetup schemesetup = GetCacheItem(rawKey);

            if (schemesetup == null)
            {
                schemesetup = RepositoryManager.SchemeSetup_Repository.GetById(id);
                if (schemesetup != null)
                    AddCacheItem(rawKey,schemesetup);
            }

            return schemesetup;
        }

        public static List<SchemeSetup> GetAll()
        {
            return RepositoryManager.SchemeSetup_Repository.GetAll();

            //const string rawKey = "SchemeSetupGetAll";

            //List<SchemeSetup> list = GetCacheAsList(rawKey);

            //if (list == null)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    list = RepositoryManager.SchemeSetup_Repository.GetAll();
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            //return list;
        }
    }
}