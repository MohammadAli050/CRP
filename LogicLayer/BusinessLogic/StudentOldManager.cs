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
   public class StudentOldManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "Student_OldCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentOld> GetCacheAsList(string rawKey)
        {
            List<StudentOld> list = (List<StudentOld>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentOld GetCacheItem(string rawKey)
        {
            StudentOld item = (StudentOld)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StudentOld student_Old)
        {
            int id = RepositoryManager.Student_Old_Repository.Insert(student_Old);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentOld student_Old)
        {
            bool isExecute = RepositoryManager.Student_Old_Repository.Update(student_Old);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Student_Old_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentOld GetById(int? id)
        {
            string rawKey = "StudentID" + id;
            StudentOld student_Old = GetCacheItem(rawKey);

            if (student_Old == null)
            {
                student_Old = RepositoryManager.Student_Old_Repository.GetById(id);
                if (student_Old != null)
                    AddCacheItem(rawKey, student_Old);
            }

            return student_Old;
        }

        public static List<StudentOld> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "Student_OldGetAll";

            List<StudentOld> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Student_Old_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
