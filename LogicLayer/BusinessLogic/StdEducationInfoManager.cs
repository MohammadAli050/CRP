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
   public class StdEducationInfoManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StdEducationInfoCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StdEducationInfo> GetCacheAsList(string rawKey)
        {
            List<StdEducationInfo> list = (List<StdEducationInfo>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StdEducationInfo GetCacheItem(string rawKey)
        {
            StdEducationInfo item = (StdEducationInfo)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StdEducationInfo stdEducationInfo)
        {
            int id = RepositoryManager.StdEducationInfo_Repository.Insert(stdEducationInfo);
            InvalidateCache();
            return id;
        }

        public static bool Update(StdEducationInfo stdEducationInfo)
        {
            bool isExecute = RepositoryManager.StdEducationInfo_Repository.Update(stdEducationInfo);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StdEducationInfo_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StdEducationInfo GetById(int? id)
        {
            string rawKey = "StdEducationInfoID" + id;
            StdEducationInfo stdEducationInfo = GetCacheItem(rawKey);

            if (stdEducationInfo == null)
            {
                stdEducationInfo = RepositoryManager.StdEducationInfo_Repository.GetById(id);
                if (stdEducationInfo != null)
                    AddCacheItem(rawKey, stdEducationInfo);
            }

            return stdEducationInfo;
        }

        public static List<StdEducationInfo> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StdEducationInfoGetAll";

            List<StdEducationInfo> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StdEducationInfo_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
