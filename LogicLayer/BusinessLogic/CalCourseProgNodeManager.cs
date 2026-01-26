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
    public class CalCourseProgNodeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "CalCourseProgNodeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<CalCourseProgNode> GetCacheAsList(string rawKey)
        {
            List<CalCourseProgNode> list = (List<CalCourseProgNode>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static CalCourseProgNode GetCacheItem(string rawKey)
        {
            CalCourseProgNode item = (CalCourseProgNode)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(CalCourseProgNode calCourseProgNode)
        {
            int id = RepositoryManager.CalCourseProgNode_Repository.Insert(calCourseProgNode);
            InvalidateCache();
            return id;
        }

        public static bool Update(CalCourseProgNode calCourseProgNode)
        {
            bool isExecute = RepositoryManager.CalCourseProgNode_Repository.Update(calCourseProgNode);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.CalCourseProgNode_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static CalCourseProgNode GetById(int? id)
        {
            string rawKey = "CalCourseProgNodeById" + id;
            CalCourseProgNode calCourseProgNode = GetCacheItem(rawKey);

            if (calCourseProgNode == null)
            {
                calCourseProgNode = RepositoryManager.CalCourseProgNode_Repository.GetById(id);
                if (calCourseProgNode != null)
                    AddCacheItem(rawKey, calCourseProgNode);
            }

            return calCourseProgNode;
        }

        public static List<CalCourseProgNode> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "CalCourseProgNodeGetAll";

            List<CalCourseProgNode> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CalCourseProgNode_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<CalCourseProgNode> GetByTreeCalenderDetailId(int treeCalenderDetailId)
        {
            List<CalCourseProgNode> list = RepositoryManager.CalCourseProgNode_Repository.GetByTreeCalenderDetailId(treeCalenderDetailId);
            return list;
        }

        public static CalCourseProgNode GetByTreeCalDetCourseIdVersionIdNodeCourseIdPriority(int treeMaterId, int calenderDistributionId, int courseId, int versionId, int nodeCourseId, int priority)
        {
            CalCourseProgNode obj = RepositoryManager.CalCourseProgNode_Repository.GetByTreeCalDetCourseIdVersionIdNodeCourseIdPriority(treeMaterId, calenderDistributionId, courseId, versionId, nodeCourseId, priority);
            return obj;
        }

        public static bool CheckCourseCalCourseProgNode(int treeMaterId, int calenderDistributionId, int courseId, int versionId, int nodeCourseId, int priority)
        {
            CalCourseProgNode obj = GetByTreeCalDetCourseIdVersionIdNodeCourseIdPriority(treeMaterId, calenderDistributionId, courseId, versionId, nodeCourseId, priority);
            if (obj == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static CalCourseProgNode GetByTreeCalDetNodeIdPriority(int treeMasterId, int calenderDistributionId, int nodeId, int priority)
        {
            CalCourseProgNode obj = RepositoryManager.CalCourseProgNode_Repository.GetByTreeCalDetNodeIdPriority(treeMasterId, calenderDistributionId, nodeId, priority);
            return obj;
        }

        public static bool CheckNodeCalCourseProgNode(int treeMasterId, int calenderDistributionId, int nodeId, int priority)
        {
            CalCourseProgNode obj = GetByTreeCalDetNodeIdPriority(treeMasterId, calenderDistributionId, nodeId, priority);
            if (obj == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public static CalCourseProgNode GetByProgramIdCourseIdVersionId(int programId, int courseId, int versionId) 
        //{
        //    CalCourseProgNode calCourseProgNode = RepositoryManager.CalCourseProgNode_Repository.GetByProgramIdCourseIdVersionId(programId, courseId, versionId);
        //    return calCourseProgNode;
        //}
    }
}
