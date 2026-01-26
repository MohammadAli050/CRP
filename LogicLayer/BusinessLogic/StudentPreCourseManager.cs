using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class StudentPreCourseManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentPreCourseCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentPreCourse> GetCacheAsList(string rawKey)
        {
            List<StudentPreCourse> list = (List<StudentPreCourse>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentPreCourse GetCacheItem(string rawKey)
        {
            StudentPreCourse item = (StudentPreCourse)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(StudentPreCourse obj)
        {
            int id = RepositoryManager.StudentPreCourse_Repository.Insert(obj);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentPreCourse obj)
        {
            bool isExecute = RepositoryManager.StudentPreCourse_Repository.Update(obj);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentPreCourse_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentPreCourse GetById(int id)
        {
            // return RepositoryAdmission.TreeMaster_Repository.GetById(id);

            string rawKey = "StudentPreCourseById" + id;
            StudentPreCourse obj = GetCacheItem(rawKey);

            if (obj == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                obj = RepositoryManager.StudentPreCourse_Repository.GetById(id);
                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static List<StudentPreCourse> GetByStudentId(int studentId)
        {
           
            List<StudentPreCourse> collection = null;

            string rawKey = "StudentPreCourseByStudentId" + studentId;
            collection = GetCacheAsList(rawKey);

            if (collection == null)
            {
                collection = GetAll();

                if (collection != null)
                {
                    collection = collection.Where(s => s.StudentId == studentId).ToList();
                }

                if (collection != null)
                    AddCacheItem(rawKey, collection);
            }

            return collection;
        }

        public static List<StudentPreCourse> GetAll()
        {
            // return RepositoryAdmission.TreeMaster_Repository.GetAll();

            const string rawKey = "StudentPreCourseAll";

            List<StudentPreCourse> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentPreCourse_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<StudentPreCourse> GetAllByParameter(int action, string batchCode, string programCode, string preMandatoryCourse, int preCourseId, int preVersionId, int mainCourseId, int mainVersionId)
        {
            if (action == 0)
                return RepositoryManager.StudentPreCourse_Repository.GetAllByParameter(action, batchCode, programCode, preMandatoryCourse, preCourseId, preVersionId, mainCourseId, mainVersionId);

            string rawKey = "GetAllByParameter" + action + batchCode + programCode + preMandatoryCourse + preCourseId + preVersionId + mainCourseId + mainVersionId;

            List<StudentPreCourse> list = GetCacheAsList(rawKey);

            if (list == null || list.Count == 0)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentPreCourse_Repository.GetAllByParameter(action, batchCode, programCode, preMandatoryCourse, preCourseId, preVersionId, mainCourseId, mainVersionId);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
