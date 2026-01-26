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
    public class PrerequisiteMasterV2Manager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "PrerequisiteMasterV2Cache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<PrerequisiteMasterV2> GetCacheAsList(string rawKey)
        {
            List<PrerequisiteMasterV2> list = (List<PrerequisiteMasterV2>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static PrerequisiteMasterV2 GetCacheItem(string rawKey)
        {
            PrerequisiteMasterV2 item = (PrerequisiteMasterV2)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(PrerequisiteMasterV2 prerequisitemasterv2)
        {
            int id = RepositoryManager.PrerequisiteMasterV2_Repository.Insert(prerequisitemasterv2);
            InvalidateCache();
            return id;
        }

        public static bool Update(PrerequisiteMasterV2 prerequisitemasterv2)
        {
            bool isExecute = RepositoryManager.PrerequisiteMasterV2_Repository.Update(prerequisitemasterv2);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.PrerequisiteMasterV2_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static PrerequisiteMasterV2 GetById(int? id)
        {
            string rawKey = "PrerequisiteMasterV2ByID" + id;
            PrerequisiteMasterV2 prerequisitemasterv2 = GetCacheItem(rawKey);

            if (prerequisitemasterv2 == null)
            {
                prerequisitemasterv2 = RepositoryManager.PrerequisiteMasterV2_Repository.GetById(id);
                if (prerequisitemasterv2 != null)
                    AddCacheItem(rawKey,prerequisitemasterv2);
            }

            return prerequisitemasterv2;
        }

        public static List<PrerequisiteMasterV2> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "PrerequisiteMasterV2GetAll";

            List<PrerequisiteMasterV2> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.PrerequisiteMasterV2_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

