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
    public class StdAdmissionDiscountManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StdAdmissionDiscountCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StdAdmissionDiscount> GetCacheAsList(string rawKey)
        {
            List<StdAdmissionDiscount> list = (List<StdAdmissionDiscount>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StdAdmissionDiscount GetCacheItem(string rawKey)
        {
            StdAdmissionDiscount item = (StdAdmissionDiscount)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StdAdmissionDiscount stdAdmissionDiscount)
        {
            int id = RepositoryManager.StdAdmissionDiscount_Repository.Insert(stdAdmissionDiscount);
            InvalidateCache();
            return id;
        }

        public static bool Update(StdAdmissionDiscount stdAdmissionDiscount)
        {
            bool isExecute = RepositoryManager.StdAdmissionDiscount_Repository.Update(stdAdmissionDiscount);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StdAdmissionDiscount_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StdAdmissionDiscount GetById(int? id)
        {
            string rawKey = "StdAdmissionDiscountById" + id;
            StdAdmissionDiscount stdAdmissionDiscount = GetCacheItem(rawKey);

            if (stdAdmissionDiscount == null)
            {
                stdAdmissionDiscount = RepositoryManager.StdAdmissionDiscount_Repository.GetById(id);
                if (stdAdmissionDiscount != null)
                    AddCacheItem(rawKey, stdAdmissionDiscount);
            }

            return stdAdmissionDiscount;
        }

        public static List<StdAdmissionDiscount> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StdAdmissionDiscountGetAll";

            List<StdAdmissionDiscount> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StdAdmissionDiscount_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
