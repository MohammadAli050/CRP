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
    public class UsrPermsnManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "UsrPermsnCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<UsrPermsn> GetCacheAsList(string rawKey)
        {
            List<UsrPermsn> list = (List<UsrPermsn>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static UsrPermsn GetCacheItem(string rawKey)
        {
            UsrPermsn item = (UsrPermsn)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(UsrPermsn usrPermsn)
        {
            int id = RepositoryManager.UsrPermsn_Repository.Insert(usrPermsn);
            InvalidateCache();
            return id;
        }

        public static bool Update(UsrPermsn usrPermsn)
        {
            bool isExecute = RepositoryManager.UsrPermsn_Repository.Update(usrPermsn);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.UsrPermsn_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static UsrPermsn GetById(int? id)
        {
            string rawKey = "UsrPermsnById" + id;
            UsrPermsn usrPermsn = GetCacheItem(rawKey);

            if (usrPermsn == null)
            {
                usrPermsn = RepositoryManager.UsrPermsn_Repository.GetById(id);
                if (usrPermsn != null)
                    AddCacheItem(rawKey, usrPermsn);
            }

            return usrPermsn;
        }

        public static List<UsrPermsn> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "UsrPermsnGetAll";

            List<UsrPermsn> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.UsrPermsn_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
