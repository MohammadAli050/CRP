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
    public class FAQManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "FAQCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<FAQ> GetCacheAsList(string rawKey)
        {
            List<FAQ> list = (List<FAQ>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static FAQ GetCacheItem(string rawKey)
        {
            FAQ item = (FAQ)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(FAQ faq)
        {
            int id = RepositoryManager.FAQ_Repository.Insert(faq);
            InvalidateCache();
            return id;
        }

        public static bool Update(FAQ faq)
        {
            bool isExecute = RepositoryManager.FAQ_Repository.Update(faq);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.FAQ_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static FAQ GetById(int? id)
        {
            string rawKey = "FAQByID" + id;
            FAQ faq = GetCacheItem(rawKey);

            if (faq == null)
            {
                faq = RepositoryManager.FAQ_Repository.GetById(id);
                if (faq != null)
                    AddCacheItem(rawKey,faq);
            }

            return faq;
        }

        public static List<FAQ> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "FAQGetAll";

            List<FAQ> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.FAQ_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

