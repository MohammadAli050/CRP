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
    public class VNodeSetMasterManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "VNodeSetMasterCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<VNodeSetMaster> GetCacheAsList(string rawKey)
        {
            List<VNodeSetMaster> list = (List<VNodeSetMaster>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static VNodeSetMaster GetCacheItem(string rawKey)
        {
            VNodeSetMaster item = (VNodeSetMaster)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(VNodeSetMaster vNodeSetMaster)
        {
            int id = RepositoryManager.VNodeSetMaster_Repository.Insert(vNodeSetMaster);
            InvalidateCache();
            return id;
        }

        public static bool Update(VNodeSetMaster vNodeSetMaster)
        {
            bool isExecute = RepositoryManager.VNodeSetMaster_Repository.Update(vNodeSetMaster);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.VNodeSetMaster_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static VNodeSetMaster GetById(int? id)
        {
            string rawKey = "VNodeSetMasterById" + id;
            VNodeSetMaster vNodeSetMaster = GetCacheItem(rawKey);

            if (vNodeSetMaster == null)
            {
                vNodeSetMaster = RepositoryManager.VNodeSetMaster_Repository.GetById(id);
                if (vNodeSetMaster != null)
                    AddCacheItem(rawKey, vNodeSetMaster);
            }

            return vNodeSetMaster;
        }

        public static List<VNodeSetMaster> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "VNodeSetMasterGetAll";

            List<VNodeSetMaster> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.VNodeSetMaster_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<VNodeSetMaster> GetByNodeId(int nodeId)
        {
            List<VNodeSetMaster> list = RepositoryManager.VNodeSetMaster_Repository.GetByNodeId(nodeId);
            return list;
        }
    }
}
