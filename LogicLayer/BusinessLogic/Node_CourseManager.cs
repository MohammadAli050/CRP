using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicLayer.BusinessLogic
{
   public class Node_CourseManager
    {
        #region Cache

       public static readonly string[] MasterCacheKeyArray = { "Node_CourseCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Node_Course> GetCacheAsList(string rawKey)
        {
            List<Node_Course> list = (List<Node_Course>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Node_Course GetCacheItem(string rawKey)
        {
            Node_Course item = (Node_Course)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(Node_Course node_Course)
        {
            int id = RepositoryManager.Node_Course_Repository.Insert(node_Course);
            InvalidateCache();
            return id;
        }

        public static bool Update(Node_Course node_Course)
        {
            bool isExecute = RepositoryManager.Node_Course_Repository.Update(node_Course);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Node_Course_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Node_Course GetById(int? id)
        {
            string rawKey = "Node_CourseID" + id;
            Node_Course node_Course = GetCacheItem(rawKey);

            if (node_Course == null)
            {
                node_Course = RepositoryManager.Node_Course_Repository.GetById(id);
                if (node_Course != null)
                    AddCacheItem(rawKey, node_Course);
            }

            return node_Course;
        }

        public static List<Node_Course> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

           // const string rawKey = "Node_CourseGetAll";

            List<Node_Course> list = RepositoryManager.Node_Course_Repository.GetAll();

            //if (list == null)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    list = RepositoryManager.Node_Course_Repository.GetAll();
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            return list;
        }

        public static List<NodeCoursesDTO> GetByNodeId(int nodeId)
        {
            List<NodeCoursesDTO> list = RepositoryManager.Node_Course_Repository.GetByNodeId(nodeId);
            return list;
        }
    }
}
