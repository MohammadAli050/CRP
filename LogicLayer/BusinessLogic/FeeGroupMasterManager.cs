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
    public class FeeGroupMasterManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "FeeGroupMasterCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<FeeGroupMaster> GetCacheAsList(string rawKey)
        {
            List<FeeGroupMaster> list = (List<FeeGroupMaster>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static FeeGroupMaster GetCacheItem(string rawKey)
        {
            FeeGroupMaster item = (FeeGroupMaster)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(FeeGroupMaster feegroupmaster)
        {
            int id = RepositoryManager.FeeGroupMaster_Repository.Insert(feegroupmaster);
            InvalidateCache();
            return id;
        }

        public static bool Update(FeeGroupMaster feegroupmaster)
        {
            bool isExecute = RepositoryManager.FeeGroupMaster_Repository.Update(feegroupmaster);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.FeeGroupMaster_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static FeeGroupMaster GetById(int? id)
        {
            string rawKey = "FeeGroupMasterByID" + id;
            FeeGroupMaster feegroupmaster = GetCacheItem(rawKey);

            if (feegroupmaster == null)
            {
                feegroupmaster = RepositoryManager.FeeGroupMaster_Repository.GetById(id);
                if (feegroupmaster != null)
                    AddCacheItem(rawKey,feegroupmaster);
            }

            return feegroupmaster;
        }

        public static List<FeeGroupMaster> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "FeeGroupMasterGetAll";

            List<FeeGroupMaster> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.FeeGroupMaster_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

