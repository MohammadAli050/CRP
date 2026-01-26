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
    public class RegistrationDateTimeLimitInBatchManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "RegistrationDateTimeLimitInBatchCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<RegistrationDateTimeLimitInBatch> GetCacheAsList(string rawKey)
        {
            List<RegistrationDateTimeLimitInBatch> list = (List<RegistrationDateTimeLimitInBatch>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static RegistrationDateTimeLimitInBatch GetCacheItem(string rawKey)
        {
            RegistrationDateTimeLimitInBatch item = (RegistrationDateTimeLimitInBatch)HttpRuntime.Cache[GetCacheKey(rawKey)];
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



        public static int Insert(RegistrationDateTimeLimitInBatch registrationDateTimeLimitInBatch)
        {
            int id = RepositoryManager.RegistrationDateTimeLimitInBatch_Repository.Insert(registrationDateTimeLimitInBatch);
            InvalidateCache();
            return id;
        }

        public static bool Update(RegistrationDateTimeLimitInBatch registrationDateTimeLimitInBatch)
        {
            bool isExecute = RepositoryManager.RegistrationDateTimeLimitInBatch_Repository.Update(registrationDateTimeLimitInBatch);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.RegistrationDateTimeLimitInBatch_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static RegistrationDateTimeLimitInBatch GetById(int? id)
        {
            string rawKey = "RegistrationDateTimeLimitInBatchById" + id;
            RegistrationDateTimeLimitInBatch registrationDateTimeLimitInBatch = GetCacheItem(rawKey);

            if (registrationDateTimeLimitInBatch == null)
            {
                registrationDateTimeLimitInBatch = RepositoryManager.RegistrationDateTimeLimitInBatch_Repository.GetById(id);
                if (registrationDateTimeLimitInBatch != null)
                    AddCacheItem(rawKey, registrationDateTimeLimitInBatch);
            }

            return registrationDateTimeLimitInBatch;
        }


        public static List<RegistrationDateTimeLimitInBatch> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "RegistrationDateTimeLimitInBatchGetAll";

            List<RegistrationDateTimeLimitInBatch> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RegistrationDateTimeLimitInBatch_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
