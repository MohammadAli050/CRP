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
    public class FeeGroupDetailManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "FeeGroupDetailCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<FeeGroupDetail> GetCacheAsList(string rawKey)
        {
            List<FeeGroupDetail> list = (List<FeeGroupDetail>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static FeeGroupDetail GetCacheItem(string rawKey)
        {
            FeeGroupDetail item = (FeeGroupDetail)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(FeeGroupDetail feegroupdetail)
        {
            int id = RepositoryManager.FeeGroupDetail_Repository.Insert(feegroupdetail);
            InvalidateCache();
            return id;
        }

        public static bool Update(FeeGroupDetail feegroupdetail)
        {
            bool isExecute = RepositoryManager.FeeGroupDetail_Repository.Update(feegroupdetail);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.FeeGroupDetail_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static FeeGroupDetail GetById(int? id)
        {
            string rawKey = "FeeGroupDetailByID" + id;
            FeeGroupDetail feegroupdetail = GetCacheItem(rawKey);

            if (feegroupdetail == null)
            {
                feegroupdetail = RepositoryManager.FeeGroupDetail_Repository.GetById(id);
                if (feegroupdetail != null)
                    AddCacheItem(rawKey,feegroupdetail);
            }

            return feegroupdetail;
        }

        public static List<FeeGroupDetail> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "FeeGroupDetailGetAll";

            List<FeeGroupDetail> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.FeeGroupDetail_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }


        public static List<FeeGroupDetail> GetByFeeGroupMasterId(int feeGroupMasterId)
        {
            List<FeeGroupDetail> list = RepositoryManager.FeeGroupDetail_Repository.GetByFeeGroupMasterId(feeGroupMasterId);
            return list;
        }
    }
}

