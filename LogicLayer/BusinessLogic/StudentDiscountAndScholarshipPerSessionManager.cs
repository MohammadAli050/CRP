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
    public class StudentDiscountAndScholarshipPerSessionManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentDiscountAndScholarshipPerSessionCache" };
        const double CacheDuration = 2.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentDiscountAndScholarshipPerSession> GetCacheAsList(string rawKey)
        {
            List<StudentDiscountAndScholarshipPerSession> list = (List<StudentDiscountAndScholarshipPerSession>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentDiscountAndScholarshipPerSession GetCacheItem(string rawKey)
        {
            StudentDiscountAndScholarshipPerSession item = (StudentDiscountAndScholarshipPerSession)HttpRuntime.Cache[GetCacheKey(rawKey)];
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
            DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddSeconds(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public static void InvalidateCache()
        {
            // Remove the cache dependency
            HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        }

        #endregion


        public static int Insert(StudentDiscountAndScholarshipPerSession studentdiscountandscholarshippersession)
        {
            int id = RepositoryManager.StudentDiscountAndScholarshipPerSession_Repository.Insert(studentdiscountandscholarshippersession);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentDiscountAndScholarshipPerSession studentdiscountandscholarshippersession)
        {
            bool isExecute = RepositoryManager.StudentDiscountAndScholarshipPerSession_Repository.Update(studentdiscountandscholarshippersession);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentDiscountAndScholarshipPerSession_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentDiscountAndScholarshipPerSession GetById(int id)
        {
            string rawKey = "StudentDiscountAndScholarshipPerSessionByID" + id;
            StudentDiscountAndScholarshipPerSession studentdiscountandscholarshippersession = GetCacheItem(rawKey);

            if (studentdiscountandscholarshippersession == null)
            {
                studentdiscountandscholarshippersession = RepositoryManager.StudentDiscountAndScholarshipPerSession_Repository.GetById(id);
                if (studentdiscountandscholarshippersession != null)
                    AddCacheItem(rawKey, studentdiscountandscholarshippersession);
            }

            return studentdiscountandscholarshippersession;
        }

        public static List<StudentDiscountAndScholarshipPerSession> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StudentDiscountAndScholarshipPerSessionGetAll";

            List<StudentDiscountAndScholarshipPerSession> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentDiscountAndScholarshipPerSession_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        internal static bool Delete(int studentId, int sessionId, int tdId)
        {
            bool isExecute = RepositoryManager.StudentDiscountAndScholarshipPerSession_Repository.Delete(studentId, sessionId, tdId);
            InvalidateCache();
            return isExecute;
        }

        public static List<StudentDiscountAndScholarshipPerSessionCount> getCountByProgramBatch(int sessionId)
        {
            List<StudentDiscountAndScholarshipPerSessionCount> list = RepositoryManager.StudentDiscountAndScholarshipPerSession_Repository.getCountByProgramBatch(sessionId);
            return list;
        }

        public static List<StudentDiscountAndScholarshipPerSession> GetAllByAcaCalIDProgramID(int sessionId, int programId)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StudentDiscountAndScholarshipPerSessionGetByAcaCalIDProgramID";

            List<StudentDiscountAndScholarshipPerSession> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentDiscountAndScholarshipPerSession_Repository.GetAllBySessionIDProgramID(sessionId,programId);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

