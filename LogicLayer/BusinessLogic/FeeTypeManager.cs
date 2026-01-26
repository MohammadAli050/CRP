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
    public class FeeTypeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "FeeTypeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<FeeType> GetCacheAsList(string rawKey)
        {
            List<FeeType> list = (List<FeeType>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static FeeType GetCacheItem(string rawKey)
        {
            FeeType item = (FeeType)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(FeeType feetype)
        {
            int id = RepositoryManager.FeeType_Repository.Insert(feetype);
            InvalidateCache();
            return id;
        }

        public static bool Update(FeeType feetype)
        {
            bool isExecute = RepositoryManager.FeeType_Repository.Update(feetype);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.FeeType_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static FeeType GetById(int? id)
        {
            string rawKey = "FeeTypeByID" + id;
            FeeType feetype = GetCacheItem(rawKey);

            if (feetype == null)
            {
                feetype = RepositoryManager.FeeType_Repository.GetById(id);
                if (feetype != null)
                    AddCacheItem(rawKey,feetype);
            }

            return feetype;
        }

        public static List<FeeType> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "FeeTypeGetAll";

            List<FeeType> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.FeeType_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

