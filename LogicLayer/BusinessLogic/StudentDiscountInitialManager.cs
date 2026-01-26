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
    public class StudentDiscountInitialManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentDiscountInitialCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentDiscountInitial> GetCacheAsList(string rawKey)
        {
            List<StudentDiscountInitial> list = (List<StudentDiscountInitial>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentDiscountInitial GetCacheItem(string rawKey)
        {
            StudentDiscountInitial item = (StudentDiscountInitial)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StudentDiscountInitial studentdiscountinitial)
        {
            int id = RepositoryManager.StudentDiscountInitial_Repository.Insert(studentdiscountinitial);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentDiscountInitial studentdiscountinitial)
        {
            bool isExecute = RepositoryManager.StudentDiscountInitial_Repository.Update(studentdiscountinitial);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentDiscountInitial_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentDiscountInitial GetById(int? id)
        {
            string rawKey = "StudentDiscountInitialByID" + id;
            StudentDiscountInitial studentdiscountinitial = GetCacheItem(rawKey);

            if (studentdiscountinitial == null)
            {
                studentdiscountinitial = RepositoryManager.StudentDiscountInitial_Repository.GetById(id);
                if (studentdiscountinitial != null)
                    AddCacheItem(rawKey, studentdiscountinitial);
            }

            return studentdiscountinitial;
        }

        public static List<StudentDiscountInitial> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StudentDiscountInitialGetAll";

            List<StudentDiscountInitial> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentDiscountInitial_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

