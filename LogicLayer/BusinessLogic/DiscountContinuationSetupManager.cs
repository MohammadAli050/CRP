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
    public class DiscountContinuationSetupManager
    {
        //#region Cache

        //public static readonly string[] MasterCacheKeyArray = { "DiscountContinuationSetupCache" };
        //const double CacheDuration = 1.0;

        //public static string GetCacheKey(string cacheKey)
        //{
        //    return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        //}

        //public static List<DiscountContinuationSetup> GetCacheAsList(string rawKey)
        //{
        //    List<DiscountContinuationSetup> list = (List<DiscountContinuationSetup>)HttpRuntime.Cache[GetCacheKey(rawKey)];
        //    return list;
        //}

        //public static DiscountContinuationSetup GetCacheItem(string rawKey)
        //{
        //    DiscountContinuationSetup item = (DiscountContinuationSetup)HttpRuntime.Cache[GetCacheKey(rawKey)];
        //    return item;
        //}

        //public static void AddCacheItem(string rawKey, object value)
        //{
        //    System.Web.Caching.Cache DataCache = HttpRuntime.Cache;

        //    // Make sure MasterCacheKeyArray[0] is in the cache - if not, add it
        //    if (DataCache[MasterCacheKeyArray[0]] == null)
        //        DataCache[MasterCacheKeyArray[0]] = DateTime.Now;

        //    // Add a CacheDependency
        //    System.Web.Caching.CacheDependency dependency = new System.Web.Caching.CacheDependency(null, MasterCacheKeyArray);
        //    DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddSeconds(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        //}


        //public static void InvalidateCache()
        //{
        //    // Remove the cache dependency
        //    HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        //}

        //#endregion


        public static int Insert(DiscountContinuationSetup discountContinuationSetup)
        {
            int id = RepositoryManager.DiscountContinuationSetup_Repository.Insert(discountContinuationSetup);
            //InvalidateCache();
            return id;
        }

        public static bool Update(DiscountContinuationSetup discountContinuationSetup)
        {
            bool isExecute = RepositoryManager.DiscountContinuationSetup_Repository.Update(discountContinuationSetup);
            //InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.DiscountContinuationSetup_Repository.Delete(id);
            //InvalidateCache();
            return isExecute;
        }

        public static DiscountContinuationSetup GetById(int id)
        {
            //string rawKey = "DiscountContinuationSetupById" + id;
            //DiscountContinuationSetup discountContinuationSetup = GetCacheItem(rawKey);

            //if (discountContinuationSetup == null)
            //{
            //    discountContinuationSetup = RepositoryManager.DiscountContinuationSetup_Repository.GetById(id);
            //    if (discountContinuationSetup != null)
            //        AddCacheItem(rawKey, discountContinuationSetup);
            //}

            //return discountContinuationSetup;
            return RepositoryManager.DiscountContinuationSetup_Repository.GetById(id);
        }

        public static List<DiscountContinuationSetup> GetAll()
        {
            return RepositoryManager.DiscountContinuationSetup_Repository.GetAll();

            //const string rawKey = "DiscountContinuationSetupGetAll";

            //List<DiscountContinuationSetup> list = GetCacheAsList(rawKey);

            //if (list == null)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    list = RepositoryManager.DiscountContinuationSetup_Repository.GetAll();
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            //return list;
        }

        public static List<DiscountContinuationSetup> GetAll(int batchId, int programId)
        {
            //string rawKey = "DiscountContinuationSetupGetAll" + batchId + programId;

            //List<DiscountContinuationSetup> list = GetCacheAsList(rawKey);

            //if (list == null)
            //{
            //    list = RepositoryManager.DiscountContinuationSetup_Repository.GetAll(batchId, programId);
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            //return list;

            return RepositoryManager.DiscountContinuationSetup_Repository.GetAll(batchId, programId);
        }
    }
}
