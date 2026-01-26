using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicLayer.BusinessLogic
{
    public class ScholarshipListManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ScholarshipListCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ScholarshipList> GetCacheAsList(string rawKey)
        {
            List<ScholarshipList> list = (List<ScholarshipList>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ScholarshipList GetCacheItem(string rawKey)
        {
            ScholarshipList item = (ScholarshipList)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(ScholarshipList scholarshiplist)
        {
            int id = RepositoryManager.ScholarshipList_Repository.Insert(scholarshiplist);
            InvalidateCache();
            return id;
        }

        public static bool Update(ScholarshipList scholarshiplist)
        {
            bool isExecute = RepositoryManager.ScholarshipList_Repository.Update(scholarshiplist);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ScholarshipList_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ScholarshipList GetById(int id)
        {
            string rawKey = "ScholarshipListByID" + id;
            ScholarshipList scholarshiplist = GetCacheItem(rawKey);

            if (scholarshiplist == null)
            {
                scholarshiplist = RepositoryManager.ScholarshipList_Repository.GetById(id);
                if (scholarshiplist != null)
                    AddCacheItem(rawKey, scholarshiplist);
            }

            return scholarshiplist;
        }

        public static List<ScholarshipList> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ScholarshipListGetAll";

            List<ScholarshipList> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ScholarshipList_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<ScholarshipList> GetAll(int acaCalId, string programCode, string fromBatch, string toBatch)
        {
            List<ScholarshipList> list = RepositoryManager.ScholarshipList_Repository.GetAll(acaCalId, programCode, fromBatch, toBatch);
            
            //string rawKey = "ScholarshipListGetAllByAcaCalProgBatch" + acaCalId + programCode + fromBatch + toBatch;

            //List<ScholarshipList> list = GetCacheAsList(rawKey);

            //if (list == null)
            //{
            //    list = RepositoryManager.ScholarshipList_Repository.GetAll(acaCalId, programCode, fromBatch, toBatch);
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            return list;
        }

        public static List<ScholarshipList> GetAllByParameter(int acaCalId, string programCode, string fromBatch, string toBatch)
        {
            List<ScholarshipList> list = RepositoryManager.ScholarshipList_Repository.GetAllByParameter(acaCalId, programCode, fromBatch, toBatch);

            return list;
        }

        public static List<ScholarshipList> GetAllByAcaCalProg(int acaCalId, string programCode)
        {
            List<ScholarshipList> list = RepositoryManager.ScholarshipList_Repository.GetAllByAcaCalProg(acaCalId, programCode);

            return list;
        }

        public static List<StudentMeritListForScholarship> GetStudentMeritListForScholarship(int acaCalId, int programId, int batchId)
        {
            List<StudentMeritListForScholarship> list = RepositoryManager.ScholarshipList_Repository.GetStudentMeritListForScholarship(acaCalId, programId, batchId);
            return list;
        }

        public static List<StudentMeritListForScholarship> GetStudentMeritListForScholarship2(int acaCalId, int programId, int batchId, decimal registeredCredit)
        {
            List<StudentMeritListForScholarship> list = RepositoryManager.ScholarshipList_Repository.GetStudentMeritListForScholarship2(acaCalId, programId, batchId, registeredCredit);
            return list;
        }
    }
}
