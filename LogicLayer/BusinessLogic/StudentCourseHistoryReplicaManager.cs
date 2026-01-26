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
    public class StudentCourseHistoryReplicaManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentCourseHistoryReplicaCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentCourseHistoryReplica> GetCacheAsList(string rawKey)
        {
            List<StudentCourseHistoryReplica> list = (List<StudentCourseHistoryReplica>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentCourseHistoryReplica GetCacheItem(string rawKey)
        {
            StudentCourseHistoryReplica item = (StudentCourseHistoryReplica)HttpRuntime.Cache[GetCacheKey(rawKey)];
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
        
        public static int Insert(StudentCourseHistoryReplica studentcoursehistoryreplica)
        {
            int id = RepositoryManager.StudentCourseHistoryReplica_Repository.Insert(studentcoursehistoryreplica);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentCourseHistoryReplica studentcoursehistoryreplica)
        {
            bool isExecute = RepositoryManager.StudentCourseHistoryReplica_Repository.Update(studentcoursehistoryreplica);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentCourseHistoryReplica_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentCourseHistoryReplica GetById(int? id)
        {
            string rawKey = "StudentCourseHistoryReplicaByID" + id;
            StudentCourseHistoryReplica studentcoursehistoryreplica = GetCacheItem(rawKey);

            if (studentcoursehistoryreplica == null)
            {
                studentcoursehistoryreplica = RepositoryManager.StudentCourseHistoryReplica_Repository.GetById(id);
                if (studentcoursehistoryreplica != null)
                    AddCacheItem(rawKey,studentcoursehistoryreplica);
            }

            return studentcoursehistoryreplica;
        }

        public static List<StudentCourseHistoryReplica> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StudentCourseHistoryReplicaGetAll";

            List<StudentCourseHistoryReplica> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentCourseHistoryReplica_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<StudentCourseHistoryReplica> GetAllByCourseHistoryID(int id)
        {
            List<StudentCourseHistoryReplica> list = RepositoryManager.StudentCourseHistoryReplica_Repository.GetAllByCourseHistoryID(id);

            return list;
        }
    }
}

