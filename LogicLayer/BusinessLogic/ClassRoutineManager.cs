using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.DataLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class ClassRoutineManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ClassRoutineCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ClassRoutine> GetCacheAsList(string rawKey)
        {
            List<ClassRoutine> list = (List<ClassRoutine>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        //private static List<rClassRoutineByProgram> GetCacheClassRoutine(string rawKey)
        //{
        //    List<rClassRoutineByProgram> list = (List<rClassRoutineByProgram>)HttpRuntime.Cache[GetCacheKey(rawKey)];
        //    return list;
        //}

        public static ClassRoutine GetCacheItem(string rawKey)
        {
            ClassRoutine item = (ClassRoutine)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ClassRoutine classRoutine)
        {
            int id = RepositoryManager.ClassRoutine_Repository.Insert(classRoutine);
            InvalidateCache();
            return id;
        }

        public static bool Update(ClassRoutine classRoutine)
        {
            bool isExecute = RepositoryManager.ClassRoutine_Repository.Update(classRoutine);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ClassRoutine_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ClassRoutine GetById(int? id)
        {
            string rawKey = "ClassRoutineById" + id;
            ClassRoutine classRoutine = GetCacheItem(rawKey);

            if (classRoutine == null)
            {
                classRoutine = RepositoryManager.ClassRoutine_Repository.GetById(id);
                if (classRoutine != null)
                    AddCacheItem(rawKey, classRoutine);
            }

            return classRoutine;
        }

        public static List<ClassRoutine> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ClassRoutineGetAll";

            List<ClassRoutine> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ClassRoutine_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<rClassRoutineByProgram> GetClassRoutineByProgramAndAcaCalId(int programID, int acaCalID)
        {
            List<rClassRoutineByProgram> list = RepositoryManager.ClassRoutine_RepositoryByProgram.GetClassRoutineByProgramAndAcaCalId(programID, acaCalID);
            return list;
        }

        public static List<rClassScheduleForFaculty> GetClassScheduleForFaculty(int facultyId, int programId, int sessionId)
        {
            List<rClassScheduleForFaculty> list = RepositoryManager.ClassRoutine_Repository.GetClassScheduleForFaculty(facultyId, programId, sessionId);
            return list;
        }
    }
}

