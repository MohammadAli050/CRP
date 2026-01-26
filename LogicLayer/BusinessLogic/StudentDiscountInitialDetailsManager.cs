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
    public class StudentDiscountInitialDetailsManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentDiscountInitialDetailsCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentDiscountInitialDetails> GetCacheAsList(string rawKey)
        {
            List<StudentDiscountInitialDetails> list = (List<StudentDiscountInitialDetails>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentDiscountInitialDetails GetCacheItem(string rawKey)
        {
            StudentDiscountInitialDetails item = (StudentDiscountInitialDetails)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StudentDiscountInitialDetails studentdiscountinitialdetails)
        {
            int id = RepositoryManager.StudentDiscountInitialDetails_Repository.Insert(studentdiscountinitialdetails);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentDiscountInitialDetails studentdiscountinitialdetails)
        {
            bool isExecute = RepositoryManager.StudentDiscountInitialDetails_Repository.Update(studentdiscountinitialdetails);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentDiscountInitialDetails_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentDiscountInitialDetails GetById(int id)
        {
            string rawKey = "StudentDiscountInitialDetailsByID" + id;
            StudentDiscountInitialDetails studentdiscountinitialdetails = GetCacheItem(rawKey);

            if (studentdiscountinitialdetails == null)
            {
                studentdiscountinitialdetails = RepositoryManager.StudentDiscountInitialDetails_Repository.GetById(id);
                if (studentdiscountinitialdetails != null)
                    AddCacheItem(rawKey, studentdiscountinitialdetails);
            }

            return studentdiscountinitialdetails;
        }

        public static List<StudentDiscountInitialDetails> GetAll()
        {
            const string rawKey = "StudentDiscountInitialDetailsGetAll";

            List<StudentDiscountInitialDetails> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentDiscountInitialDetails_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<StudentDiscountInitialDetails> GetByStudentDiscountId(int StudentDiscountId)
        {
            List<StudentDiscountInitialDetails> list = RepositoryManager.StudentDiscountInitialDetails_Repository.GetByStudentDiscountId(StudentDiscountId);

            return list;
        }

        public static int CountBy(int StudentDiscountId, int TypeDefinitionId)
        {
            List<StudentDiscountInitialDetails> list = GetByStudentDiscountId(StudentDiscountId);

            int count = list.Where(l => l.TypeDefinitionId == TypeDefinitionId).ToList().Count;

            return count;
        }

        public static StudentDiscountInitialDetails GetBy(int StudentDiscountId, int TypeDefinitionId)
        {
            List<StudentDiscountInitialDetails> list = GetByStudentDiscountId(StudentDiscountId);

            StudentDiscountInitialDetails obj = list.Where(l => l.TypeDefinitionId == TypeDefinitionId).SingleOrDefault();
            return obj;
        }

        public static List<StudentDiscountInitialDetailsDTO> GetAllDiscountInitialByProgramBatchRoll(int programId, int acaCalId, string roll)
        {
            List<StudentDiscountInitialDetailsDTO> list = RepositoryManager.StudentDiscountInitialDetails_Repository.GetByStudentDiscountId(programId, acaCalId, roll);

            return list;
        }
    }
}

