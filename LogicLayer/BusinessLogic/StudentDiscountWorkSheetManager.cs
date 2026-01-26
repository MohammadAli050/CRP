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
    public class StudentDiscountWorkSheetManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentDiscountWorkSheetCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentDiscountWorkSheet> GetCacheAsList(string rawKey)
        {
            List<StudentDiscountWorkSheet> list = (List<StudentDiscountWorkSheet>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentDiscountWorkSheet GetCacheItem(string rawKey)
        {
            StudentDiscountWorkSheet item = (StudentDiscountWorkSheet)HttpRuntime.Cache[GetCacheKey(rawKey)];
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



        public static int Insert(StudentDiscountWorkSheet studentDiscountWorkSheet)
        {
            int id = RepositoryManager.StudentDiscountWorkSheet_Repository.Insert(studentDiscountWorkSheet);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentDiscountWorkSheet studentDiscountWorkSheet)
        {
            bool isExecute = RepositoryManager.StudentDiscountWorkSheet_Repository.Update(studentDiscountWorkSheet);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentDiscountWorkSheet_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentDiscountWorkSheet GetById(int? id)
        {
            string rawKey = "StudentDiscountWorkSheetById" + id;
            StudentDiscountWorkSheet studentDiscountWorkSheet = GetCacheItem(rawKey);

            if (studentDiscountWorkSheet == null)
            {
                studentDiscountWorkSheet = RepositoryManager.StudentDiscountWorkSheet_Repository.GetById(id);
                if (studentDiscountWorkSheet != null)
                    AddCacheItem(rawKey, studentDiscountWorkSheet);
            }

            return studentDiscountWorkSheet;
        }

        public static List<StudentDiscountWorkSheet> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StudentDiscountWorkSheetGetAll";

            List<StudentDiscountWorkSheet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentDiscountWorkSheet_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
