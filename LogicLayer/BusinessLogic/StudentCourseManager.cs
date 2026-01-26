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
   public class StudentCourseManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "Student_CourseCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentCourse> GetCacheAsList(string rawKey)
        {
            List<StudentCourse> list = (List<StudentCourse>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentCourse GetCacheItem(string rawKey)
        {
            StudentCourse item = (StudentCourse)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(StudentCourse student_Course)
        {
            int id = RepositoryManager.Student_Course_Repository.Insert(student_Course);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentCourse student_Course)
        {
            bool isExecute = RepositoryManager.Student_Course_Repository.Update(student_Course);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Student_Course_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentCourse GetById(int? id)
        {
            string rawKey = "Student_CourseID" + id;
            StudentCourse student_Course = GetCacheItem(rawKey);

            if (student_Course == null)
            {
                student_Course = RepositoryManager.Student_Course_Repository.GetById(id);
                if (student_Course != null)
                    AddCacheItem(rawKey, student_Course);
            }

            return student_Course;
        }

        public static List<StudentCourse> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "Student_CourseGetAll";

            List<StudentCourse> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Student_Course_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
