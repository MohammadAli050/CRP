using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicLayer.BusinessLogic
{
   public class RegistrationDateTimeLimitManager
    {

        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "RegistrationDateTimeLimitCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<RegistrationDateTimeLimit> GetCacheAsList(string rawKey)
        {
            List<RegistrationDateTimeLimit> list = (List<RegistrationDateTimeLimit>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static RegistrationDateTimeLimit GetCacheItem(string rawKey)
        {
            RegistrationDateTimeLimit item = (RegistrationDateTimeLimit)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(RegistrationDateTimeLimit registrationDateTimeLimit)
        {
            int id = RepositoryManager.RegistrationDateTimeLimit_Repository.Insert(registrationDateTimeLimit);
            InvalidateCache();
            return id;
        }

        public static bool Update(RegistrationDateTimeLimit registrationDateTimeLimit)
        {
            bool isExecute = RepositoryManager.RegistrationDateTimeLimit_Repository.Update(registrationDateTimeLimit);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.RegistrationDateTimeLimit_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static RegistrationDateTimeLimit GetById(int? id)
        {
            string rawKey = "RegistrationDateTimeLimitID" + id;
            RegistrationDateTimeLimit registrationDateTimeLimit = GetCacheItem(rawKey);

            if (registrationDateTimeLimit == null)
            {
                registrationDateTimeLimit = RepositoryManager.RegistrationDateTimeLimit_Repository.GetById(id);
                if (registrationDateTimeLimit != null)
                    AddCacheItem(rawKey, registrationDateTimeLimit);
            }

            return registrationDateTimeLimit;
        }

        public static List<RegistrationDateTimeLimit> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "RegistrationDateTimeLimitGetAll";

            List<RegistrationDateTimeLimit> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RegistrationDateTimeLimit_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
