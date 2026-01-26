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
    public class VNodeSetManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "VNodeSetCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<VNodeSet> GetCacheAsList(string rawKey)
        {
            List<VNodeSet> list = (List<VNodeSet>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static VNodeSet GetCacheItem(string rawKey)
        {
            VNodeSet item = (VNodeSet)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(VNodeSet vNodeSet)
        {
            int id = RepositoryManager.VNodeSet_Repository.Insert(vNodeSet);
            InvalidateCache();
            return id;
        }

        public static bool Update(VNodeSet vNodeSet)
        {
            bool isExecute = RepositoryManager.VNodeSet_Repository.Update(vNodeSet);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.VNodeSet_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static VNodeSet GetById(int? id)
        {
            string rawKey = "VNodeSetById" + id;
            VNodeSet vNodeSet = GetCacheItem(rawKey);

            if (vNodeSet == null)
            {
                vNodeSet = RepositoryManager.VNodeSet_Repository.GetById(id);
                if (vNodeSet != null)
                    AddCacheItem(rawKey, vNodeSet);
            }

            return vNodeSet;
        }

        public static List<VNodeSet> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "VNodeSetGetAll";

            List<VNodeSet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.VNodeSet_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<VNodeSet> GetbyNodeId(int nodeId)
        {
            List<VNodeSet> list = RepositoryManager.VNodeSet_Repository.GetbyNodeId(nodeId);

            return list;
        }

        public static List<VNodeSet> GetbyVNodeSetMasterId(int vNodeSetMasterId)
        {
            List<VNodeSet> list = RepositoryManager.VNodeSet_Repository.GetbyVNodeSetMasterId(vNodeSetMasterId);

            return list;
        }
    }
}
