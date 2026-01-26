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
    public class StudentDiscountCurrentDetailsManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentDiscountCurrentDetailsCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentDiscountCurrentDetails> GetCacheAsList(string rawKey)
        {
            List<StudentDiscountCurrentDetails> list = (List<StudentDiscountCurrentDetails>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentDiscountCurrentDetails GetCacheItem(string rawKey)
        {
            StudentDiscountCurrentDetails item = (StudentDiscountCurrentDetails)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StudentDiscountCurrentDetails studentdiscountcurrentdetails)
        {
            int id = RepositoryManager.StudentDiscountCurrentDetails_Repository.Insert(studentdiscountcurrentdetails);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentDiscountCurrentDetails studentdiscountcurrentdetails)
        {
            bool isExecute = RepositoryManager.StudentDiscountCurrentDetails_Repository.Update(studentdiscountcurrentdetails);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentDiscountCurrentDetails_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentDiscountCurrentDetails GetById(int id)
        {
            string rawKey = "StudentDiscountCurrentDetailsByID" + id;
            StudentDiscountCurrentDetails studentdiscountcurrentdetails = GetCacheItem(rawKey);

            if (studentdiscountcurrentdetails == null)
            {
                studentdiscountcurrentdetails = RepositoryManager.StudentDiscountCurrentDetails_Repository.GetById(id);
                if (studentdiscountcurrentdetails != null)
                    AddCacheItem(rawKey, studentdiscountcurrentdetails);
            }

            return studentdiscountcurrentdetails;
        }

        public static List<StudentDiscountCurrentDetails> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StudentDiscountCurrentDetailsGetAll";

            List<StudentDiscountCurrentDetails> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentDiscountCurrentDetails_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<StudentDiscountCurrentDetails> GetByStudentDiscountId(int StudentDiscountId)
        {
            List<StudentDiscountCurrentDetails> list = RepositoryManager.StudentDiscountCurrentDetails_Repository.GetByStudentDiscountId(StudentDiscountId);

            return list;
        }

        public static StudentDiscountCurrentDetails GetBy(int StudentDiscountId, int TypeDefinitionId)
        {
            List<StudentDiscountCurrentDetails> list = GetByStudentDiscountId(StudentDiscountId);
            StudentDiscountCurrentDetails obj = null;
            if (list != null)
            {
                obj = list.Where(l => l.TypeDefinitionId == TypeDefinitionId).SingleOrDefault();
            }
            return obj;
        }

        public static List<StudentDiscountCurrentDetails> GetByStudentDiscountAndAcaCalSession(int StudentDiscountId, int AcaCalSessionId)
        {
            //List<StudentDiscountCurrentDetails> list = GetByStudentDiscountId(StudentDiscountId);

            //if (list != null)
            //{
            //    list = list.Where(l => l.AcaCalSession == AcaCalSessionId).ToList();
            //}

            List<StudentDiscountCurrentDetails> list = RepositoryManager.StudentDiscountCurrentDetails_Repository.GetByStudentDiscountAndAcaCalSession(StudentDiscountId, AcaCalSessionId);
            return list;
        }

        public static Boolean GenetareCurrentDiscount(int acaCalBatch, int acaCalSession, int program)
        {
            bool boo = RepositoryManager.StudentDiscountCurrentDetails_Repository.GenetareCurrentDiscount(acaCalBatch, acaCalSession, program);
            return boo;
        }


        public static bool DiscountTransferFromInitialToCurrentPerStudent(int student, int batchId, int sessionId, int programId)
        {
            bool boo = RepositoryManager.StudentDiscountCurrentDetails_Repository.DiscountTransferFromInitialToCurrentPerStudent(student,  batchId,  sessionId,  programId);
            return boo;
        }

        public static List<StudentDiscountCurrentDetailsDTO> GetAllDiscountCurrentByProgramBatchRoll(int programId, int acaCalBatchId, int acaCalSessionId, string roll)
        {
            List<StudentDiscountCurrentDetailsDTO> list = RepositoryManager.StudentDiscountCurrentDetails_Repository.GetAllDiscountCurrentByProgramBatchRoll(programId, acaCalBatchId, acaCalSessionId, roll);
            return list;
        }

        public static StudentDiscountCurrentDetails GetBy(int StudentDiscountId, int typeDefinitionId, int acaCalSessionId)
        {
            StudentDiscountCurrentDetails obj = RepositoryManager.StudentDiscountCurrentDetails_Repository.GetBy(StudentDiscountId, typeDefinitionId, acaCalSessionId);
            return obj;
        }

        public static bool DiscountPostingWaiver(int studentId, int batchId, int programId, int sessionId, int discountTypeId)
        {
            bool boo = RepositoryManager.StudentDiscountCurrentDetails_Repository.DiscountPostingWaiver(studentId, batchId, programId, sessionId, discountTypeId);
            return boo;
        }

        public static bool Delete(int studentId, int sessionId)
        {
            bool isExecute = RepositoryManager.StudentDiscountCurrentDetails_Repository.Delete(studentId,  sessionId);
            InvalidateCache();
            return isExecute;
        }
    }
}

