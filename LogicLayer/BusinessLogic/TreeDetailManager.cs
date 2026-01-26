using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class TreeDetailManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "TreeDetailCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<TreeDetail> GetCacheAsList(string rawKey)
        {
            List<TreeDetail> list = (List<TreeDetail>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static TreeDetail GetCacheItem(string rawKey)
        {
            TreeDetail item = (TreeDetail)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(TreeDetail obj)
        {
            int id = RepositoryManager.TreeDetail_Repository.Insert(obj);
            InvalidateCache();
            return id;
        }

        public static bool Update(TreeDetail obj)
        {
            bool isExecute = RepositoryManager.TreeDetail_Repository.Update(obj);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.TreeDetail_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static TreeDetail GetById(int? id)
        {
            // return RepositoryAdmission.TreeDetail_Repository.GetById(id);

            string rawKey = "TreeDetailById" + id;
            TreeDetail obj = GetCacheItem(rawKey);

            if (obj == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                obj = RepositoryManager.TreeDetail_Repository.GetById(id);
                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static List<TreeDetail> GetAll()
        {
            // return RepositoryAdmission.TreeDetail_Repository.GetAll();

            const string rawKey = "TreeDetailGetAll";

            List<TreeDetail> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.TreeDetail_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<TreeDetail> GetByTreeMasterIdParentNodeId(int treeMasterId, int parentNodeId)
        {
            List<TreeDetail> list = RepositoryManager.TreeDetail_Repository.GetByTreeMasterIdParentNodeId(treeMasterId, parentNodeId);
            return list;
        }
    }
}
