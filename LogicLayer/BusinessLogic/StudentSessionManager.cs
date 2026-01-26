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
    public class StudentSessionManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentSessionCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentSession> GetCacheAsList(string rawKey)
        {
            List<StudentSession> list = (List<StudentSession>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentSession GetCacheItem(string rawKey)
        {
            StudentSession item = (StudentSession)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StudentSession studentsession)
        {
            int id = RepositoryManager.StudentSession_Repository.Insert(studentsession);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentSession studentsession)
        {
            bool isExecute = RepositoryManager.StudentSession_Repository.Update(studentsession);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentSession_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentSession GetById(int? id)
        {
            string rawKey = "StudentSessionByID" + id;
            StudentSession studentsession = GetCacheItem(rawKey);

            if (studentsession == null)
            {
                studentsession = RepositoryManager.StudentSession_Repository.GetById(id);
                if (studentsession != null)
                    AddCacheItem(rawKey,studentsession);
            }

            return studentsession;
        }

        public static List<StudentSession> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StudentSessionGetAll";

            List<StudentSession> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentSession_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

