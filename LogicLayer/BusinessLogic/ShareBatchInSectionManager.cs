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
    public class ShareBatchInSectionManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ShareBatchInSectionCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ShareBatchInSection> GetCacheAsList(string rawKey)
        {
            List<ShareBatchInSection> list = (List<ShareBatchInSection>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ShareBatchInSection GetCacheItem(string rawKey)
        {
            ShareBatchInSection item = (ShareBatchInSection)HttpRuntime.Cache[GetCacheKey(rawKey)];
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
        
        public static int Insert(ShareBatchInSection sharebatchinsection)
        {
            int id = RepositoryManager.ShareBatchInSection_Repository.Insert(sharebatchinsection);
            InvalidateCache();
            return id;
        }

        public static bool Update(ShareBatchInSection sharebatchinsection)
        {
            bool isExecute = RepositoryManager.ShareBatchInSection_Repository.Update(sharebatchinsection);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ShareBatchInSection_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ShareBatchInSection GetById(int id)
        {
            string rawKey = "ShareBatchInSectionByID" + id;
            ShareBatchInSection sharebatchinsection = GetCacheItem(rawKey);

            if (sharebatchinsection == null)
            {
                sharebatchinsection = RepositoryManager.ShareBatchInSection_Repository.GetById(id);
                if (sharebatchinsection != null)
                    AddCacheItem(rawKey,sharebatchinsection);
            }

            return sharebatchinsection;
        }

        public static List<ShareBatchInSection> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ShareBatchInSectionGetAll";

            List<ShareBatchInSection> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ShareBatchInSection_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static bool DeleteByAcademicCalenderSectionId(int id)
        {
            bool isExecute = RepositoryManager.ShareBatchInSection_Repository.DeleteByAcademicCalenderSectionId(id);
            InvalidateCache();
            return isExecute;
        }
    }
}

