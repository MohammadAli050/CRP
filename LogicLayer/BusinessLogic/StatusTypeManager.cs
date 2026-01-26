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
   public class StatusTypeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StatusTypeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StatusType> GetCacheAsList(string rawKey)
        {
            List<StatusType> list = (List<StatusType>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StatusType GetCacheItem(string rawKey)
        {
            StatusType item = (StatusType)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StatusType statusType)
        {
            int id = RepositoryManager.StatusType_Repository.Insert(statusType);
            InvalidateCache();
            return id;
        }

        public static bool Update(StatusType statusType)
        {
            bool isExecute = RepositoryManager.StatusType_Repository.Update(statusType);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StatusType_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StatusType GetById(int? id)
        {
            string rawKey = "StatusTypeID" + id;
            StatusType statusType = GetCacheItem(rawKey);

            if (statusType == null)
            {
                statusType = RepositoryManager.StatusType_Repository.GetById(id);
                if (statusType != null)
                    AddCacheItem(rawKey, statusType);
            }

            return statusType;
        }

        public static List<StatusType> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StatusTypeGetAll";

            List<StatusType> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StatusType_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
