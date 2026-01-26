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
    public class AccountHeadsManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "AccountHeadsCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<AccountHeads> GetCacheAsList(string rawKey)
        {
            List<AccountHeads> list = (List<AccountHeads>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static AccountHeads GetCacheItem(string rawKey)
        {
            AccountHeads item = (AccountHeads)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(AccountHeads accountHeads)
        {
            int id = RepositoryManager.AccountHeads_Repository.Insert(accountHeads);
            InvalidateCache();
            return id;
        }

        public static bool Update(AccountHeads accountHeads)
        {
            bool isExecute = RepositoryManager.AccountHeads_Repository.Update(accountHeads);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.AccountHeads_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static AccountHeads GetById(int? id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "AccountHeadsById" + id;
            AccountHeads accountHeads = GetCacheItem(rawKey);

            if (accountHeads == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                accountHeads = RepositoryManager.AccountHeads_Repository.GetById(id);
                if (accountHeads != null)
                    AddCacheItem(rawKey, accountHeads);
            }

            return accountHeads;
        }

        public static List<AccountHeads> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "AccountHeadsGetAll";

            List<AccountHeads> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.AccountHeads_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
