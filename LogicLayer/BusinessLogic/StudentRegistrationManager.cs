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
    public class StudentRegistrationManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentRegistrationCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentRegistration> GetCacheAsList(string rawKey)
        {
            List<StudentRegistration> list = (List<StudentRegistration>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentRegistration GetCacheItem(string rawKey)
        {
            StudentRegistration item = (StudentRegistration)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StudentRegistration studentregistration)
        {
            int id = RepositoryManager.StudentRegistration_Repository.Insert(studentregistration);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentRegistration studentregistration)
        {
            bool isExecute = RepositoryManager.StudentRegistration_Repository.Update(studentregistration);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentRegistration_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentRegistration GetById(int? id)
        {
            string rawKey = "StudentRegistrationByID" + id;
            StudentRegistration studentregistration = GetCacheItem(rawKey);

            if (studentregistration == null)
            {
                studentregistration = RepositoryManager.StudentRegistration_Repository.GetById(id);
                if (studentregistration != null)
                    AddCacheItem(rawKey,studentregistration);
            }

            return studentregistration;
        }

        public static StudentRegistration GetByLoginId(string loginId)
        {
            StudentRegistration studentregistration = RepositoryManager.StudentRegistration_Repository.GetByLoginId(loginId);
         
            return studentregistration;
        }

        public static StudentRegistration GetByRegistrationNo(string RegNo)
        {
            string rawKey = "StudentRegistrationByID" + RegNo;
            StudentRegistration studentregistration = GetCacheItem(rawKey);

            if (studentregistration == null)
            {
                studentregistration = RepositoryManager.StudentRegistration_Repository.GetByRegistrationNo(RegNo);
                if (studentregistration != null)
                    AddCacheItem(rawKey, studentregistration);
            }

            return studentregistration;
        }

        public static StudentRegistration GetByStudentId(int studentId)
        {
            StudentRegistration studentregistration = RepositoryManager.StudentRegistration_Repository.GetByStudentId(studentId);
            return studentregistration;
        }

        public static List<StudentRegistration> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StudentRegistrationGetAll";

            List<StudentRegistration> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentRegistration_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<StudentRegistration> GetAllByProgramBatchStudent(int programId, int batchId, string roll)
        {
            // return RepositoryAdmission.Student_Repository.GetAll();

            //string rawKey = "StudentRegistrationGetAllByProgramBatchStudent" + programId + batchId + roll;

            List<StudentRegistration> list = null;

            if (list == null || list.Count() == 0)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentRegistration_Repository.GetAllByProgramBatchStudent(programId, batchId, roll);
                //if (list != null && list.Count() != 0)
                //    AddCacheItem(rawKey, list);
            }

            return list;
        } 
         

    }
}

