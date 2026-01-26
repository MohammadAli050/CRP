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
    public class TeacherCourseSectionManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "TeacherCourseSectionCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<TeacherCourseSection> GetCacheAsList(string rawKey)
        {
            List<TeacherCourseSection> list = (List<TeacherCourseSection>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static TeacherCourseSection GetCacheItem(string rawKey)
        {
            TeacherCourseSection item = (TeacherCourseSection)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(TeacherCourseSection teachercoursesection)
        {
            int id = RepositoryManager.TeacherCourseSection_Repository.Insert(teachercoursesection);
            InvalidateCache();
            return id;
        }

        public static bool Update(TeacherCourseSection teachercoursesection)
        {
            bool isExecute = RepositoryManager.TeacherCourseSection_Repository.Update(teachercoursesection);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.TeacherCourseSection_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static TeacherCourseSection GetById(int? id)
        {
            string rawKey = "TeacherCourseSectionByID" + id;
            TeacherCourseSection teachercoursesection = GetCacheItem(rawKey);

            if (teachercoursesection == null)
            {
                teachercoursesection = RepositoryManager.TeacherCourseSection_Repository.GetById(id);
                if (teachercoursesection != null)
                    AddCacheItem(rawKey,teachercoursesection);
            }

            return teachercoursesection;
        }

        public static List<TeacherCourseSection> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "TeacherCourseSectionGetAll";

            List<TeacherCourseSection> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.TeacherCourseSection_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

