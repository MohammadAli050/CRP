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
    public class NodeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "NodeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Node> GetCacheAsList(string rawKey)
        {
            List<Node> list = (List<Node>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Node GetCacheItem(string rawKey)
        {
            Node item = (Node)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(Node obj)
        {
            int id = RepositoryManager.Node_Repository.Insert(obj);
            InvalidateCache();
            return id;
        }

        public static bool Update(Node obj)
        {
            bool isExecute = RepositoryManager.Node_Repository.Update(obj);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Node_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Node GetById(int? id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "NodeById" + id;
            Node obj = GetCacheItem(rawKey);

            if (obj == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                obj = RepositoryManager.Node_Repository.GetById(id);
                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static List<Node> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "NodeGetAll";

            List<Node> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Node_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<Node> GetAllMajorNode()
        {
            List<Node> list = RepositoryManager.Node_Repository.GetAll();

            if (list != null)
                list = list.Where(l => l.IsMajor == true).ToList();

            return list;
        }

        public static List<Node> GetAllMajorNodeByBatchId(int batchId)
        {
            List<Node> list = RepositoryManager.Node_Repository.GetAllMajorNodeByBatchId(batchId);

            if (list != null)
                list = list.Where(l => l.IsMajor == true).ToList();

            return list;
        }

        public static List<Node> GetNodeByTreeMasterId(int treeMasterId)
        {
            List<Node> list = RepositoryManager.Node_Repository.GetNodeByTreeMasterId(treeMasterId);
            return list;
        }
    }
}
