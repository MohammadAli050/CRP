using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using LogicLayer.BusinessObjects.DTO;

namespace LogicLayer.BusinessLogic
{
    public class EquiCourseDetailsManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "EquiCourseDetailsCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<EquiCourseDetails> GetCacheAsList(string rawKey)
        {
            List<EquiCourseDetails> list = (List<EquiCourseDetails>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static EquiCourseDetails GetCacheItem(string rawKey)
        {
            EquiCourseDetails item = (EquiCourseDetails)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(EquiCourseDetails equicoursedetails)
        {
            int id = RepositoryManager.EquiCourseDetails_Repository.Insert(equicoursedetails);
            InvalidateCache();
            return id;
        }

        public static bool Update(EquiCourseDetails equicoursedetails)
        {
            bool isExecute = RepositoryManager.EquiCourseDetails_Repository.Update(equicoursedetails);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.EquiCourseDetails_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static EquiCourseDetails GetById(int? id)
        {
            string rawKey = "EquiCourseDetailsByID" + id;
            EquiCourseDetails equicoursedetails = GetCacheItem(rawKey);

            if (equicoursedetails == null)
            {
                equicoursedetails = RepositoryManager.EquiCourseDetails_Repository.GetById(id);
                if (equicoursedetails != null)
                    AddCacheItem(rawKey,equicoursedetails);
            }

            return equicoursedetails;
        }

        public static List<EquiCourseDetails> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "EquiCourseDetailsGetAll";

            List<EquiCourseDetails> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.EquiCourseDetails_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<EquivalentCourseDTO> GetAllEquivalentCourse()
        {
            List<EquivalentCourseDTO> list = RepositoryManager.EquiCourseDetails_Repository.GetAllEquivalentCourse();
            return list;
        }

        public static List<EquivalentCourseDTO> GetAllEquivalentCourseByMasterId(int equivalentCoursemasterId) 
        {
            List<EquivalentCourseDTO> list = RepositoryManager.EquiCourseDetails_Repository.GetAllEquivalentCourseByMasterId(equivalentCoursemasterId);
            return list;
        }

        public static List<EquivalentCourseDTO> GetAllEquivalentCourseByParameters(int programId, int courseId, int versionId, string vesionCode)
        {
            List<EquivalentCourseDTO> list = RepositoryManager.EquiCourseDetails_Repository.GetAllEquivalentCourseByParameters(programId,  courseId,  versionId,  vesionCode);
            return list;
        }
        public static List<EquivalentCourseDTO> GetAllEquivalentCourseForRpt()
        {
            List<EquivalentCourseDTO> list = RepositoryManager.EquiCourseDetails_Repository.GetAllEquivalentCourseForRpt();
            return list;
        }
    }
}

