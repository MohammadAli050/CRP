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
    public class PrerequisiteMasterManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "PrerequisiteMasterCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<PrerequisiteMaster> GetCacheAsList(string rawKey)
        {
            List<PrerequisiteMaster> list = (List<PrerequisiteMaster>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static PrerequisiteMaster GetCacheItem(string rawKey)
        {
            PrerequisiteMaster item = (PrerequisiteMaster)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(PrerequisiteMaster prerequisiteMaster)
        {
            int id = RepositoryManager.PrerequisiteMaster_Repository.Insert(prerequisiteMaster);
            InvalidateCache();
            return id;
        }

        public static bool Update(PrerequisiteMaster prerequisiteMaster)
        {
            bool isExecute = RepositoryManager.PrerequisiteMaster_Repository.Update(prerequisiteMaster);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.PrerequisiteMaster_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static PrerequisiteMaster GetById(int? id)
        {
            string rawKey = "PrerequisiteMasterById" + id;
            PrerequisiteMaster prerequisiteMaster = GetCacheItem(rawKey);

            if (prerequisiteMaster == null)
            {
                prerequisiteMaster = RepositoryManager.PrerequisiteMaster_Repository.GetById(id);
                if (prerequisiteMaster != null)
                    AddCacheItem(rawKey, prerequisiteMaster);
            }

            return prerequisiteMaster;
        }

        public static List<PrerequisiteMaster> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "PrerequisiteMasterGetAll";

            List<PrerequisiteMaster> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.PrerequisiteMaster_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
