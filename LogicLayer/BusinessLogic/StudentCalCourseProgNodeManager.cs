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
    public class StudentCalCourseProgNodeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentCalCourseProgNodeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentCalCourseProgNode> GetCacheAsList(string rawKey)
        {
            List<StudentCalCourseProgNode> list = (List<StudentCalCourseProgNode>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentCalCourseProgNode GetCacheItem(string rawKey)
        {
            StudentCalCourseProgNode item = (StudentCalCourseProgNode)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StudentCalCourseProgNode studentCalCourseProgNode)
        {
            int id = RepositoryManager.StudentCalCourseProgNode_Repository.Insert(studentCalCourseProgNode);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentCalCourseProgNode studentCalCourseProgNode)
        {
            bool isExecute = RepositoryManager.StudentCalCourseProgNode_Repository.Update(studentCalCourseProgNode);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentCalCourseProgNode_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentCalCourseProgNode GetById(int? id)
        {
            string rawKey = "StudentCalCourseProgNodeById" + id;
            StudentCalCourseProgNode studentCalCourseProgNode = GetCacheItem(rawKey);

            if (studentCalCourseProgNode == null)
            {
                studentCalCourseProgNode = RepositoryManager.StudentCalCourseProgNode_Repository.GetById(id);
                if (studentCalCourseProgNode != null)
                    AddCacheItem(rawKey, studentCalCourseProgNode);
            }

            return studentCalCourseProgNode;
        }

        public static List<StudentCalCourseProgNode> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StudentCalCourseProgNodeGetAll";

            List<StudentCalCourseProgNode> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentCalCourseProgNode_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
