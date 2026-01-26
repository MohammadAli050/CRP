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
    public class StudentDiscountMasterManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentDiscountMasterCache" };
        const double CacheDuration = 1.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentDiscountMaster> GetCacheAsList(string rawKey)
        {
            List<StudentDiscountMaster> list = (List<StudentDiscountMaster>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentDiscountMaster GetCacheItem(string rawKey)
        {
            StudentDiscountMaster item = (StudentDiscountMaster)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StudentDiscountMaster studentdiscountmaster)
        {
            int id = RepositoryManager.StudentDiscountMaster_Repository.Insert(studentdiscountmaster);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentDiscountMaster studentdiscountmaster)
        {
            bool isExecute = RepositoryManager.StudentDiscountMaster_Repository.Update(studentdiscountmaster);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentDiscountMaster_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentDiscountMaster GetById(int id)
        {
            string rawKey = "StudentDiscountMasterByID" + id;
            StudentDiscountMaster studentdiscountmaster = GetCacheItem(rawKey);

            if (studentdiscountmaster == null)
            {
                studentdiscountmaster = RepositoryManager.StudentDiscountMaster_Repository.GetById(id);
                if (studentdiscountmaster != null)
                    AddCacheItem(rawKey,studentdiscountmaster);
            }

            return studentdiscountmaster;
        }

        public static List<StudentDiscountMaster> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StudentDiscountMasterGetAll";

            List<StudentDiscountMaster> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentDiscountMaster_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static StudentDiscountMaster GetByStudentID(int StudentID)
        {
            string rawKey = "StudentDiscountMasterByStudentID" + StudentID;
            StudentDiscountMaster studentdiscountmaster = GetCacheItem(rawKey);

            if (studentdiscountmaster == null)
            {
                studentdiscountmaster = RepositoryManager.StudentDiscountMaster_Repository.GetByStudentID(StudentID);
                if (studentdiscountmaster != null)
                    AddCacheItem(rawKey, studentdiscountmaster);
            }

            return studentdiscountmaster;
        }


        public static List<StudentDiscountMaster> GetByAcaCalIDProgramID(int AcaCalID, int ProgramID)
        {
            string rawKey = "StudentDiscountMasterByAcaCalIDProgramID" + AcaCalID+ProgramID;
            List<StudentDiscountMaster> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentDiscountMaster_Repository.GetByAcaCalIDProgramID(AcaCalID,ProgramID);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

    }
}

