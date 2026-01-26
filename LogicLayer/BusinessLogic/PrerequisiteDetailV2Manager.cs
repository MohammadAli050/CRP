using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.BusinessLogic
{
    public class PrerequisiteDetailV2Manager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "PrerequisiteDetailV2Cache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<PrerequisiteDetailV2> GetCacheAsList(string rawKey)
        {
            List<PrerequisiteDetailV2> list = (List<PrerequisiteDetailV2>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static PrerequisiteDetailV2 GetCacheItem(string rawKey)
        {
            PrerequisiteDetailV2 item = (PrerequisiteDetailV2)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(PrerequisiteDetailV2 prerequisitedetailv2)
        {
            int id = RepositoryManager.PrerequisiteDetailV2_Repository.Insert(prerequisitedetailv2);
            InvalidateCache();
            return id;
        }

        public static bool Update(PrerequisiteDetailV2 prerequisitedetailv2)
        {
            bool isExecute = RepositoryManager.PrerequisiteDetailV2_Repository.Update(prerequisitedetailv2);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.PrerequisiteDetailV2_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static PrerequisiteDetailV2 GetById(int? id)
        {
            string rawKey = "PrerequisiteDetailV2ByID" + id;
            PrerequisiteDetailV2 prerequisitedetailv2 = GetCacheItem(rawKey);

            if (prerequisitedetailv2 == null)
            {
                prerequisitedetailv2 = RepositoryManager.PrerequisiteDetailV2_Repository.GetById(id);
                if (prerequisitedetailv2 != null)
                    AddCacheItem(rawKey,prerequisitedetailv2);
            }

            return prerequisitedetailv2;
        }

        public static List<PrerequisiteDetailV2> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "PrerequisiteDetailV2GetAll";

            List<PrerequisiteDetailV2> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.PrerequisiteDetailV2_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<PreRequisiteSetDTO> GetAllPreRequisiteSetAndCourses(int programId, int courseId, int versionId, string versionCode)
        {
            List<PreRequisiteSetDTO> list = RepositoryManager.PrerequisiteDetailV2_Repository.GetAllPreRequisiteSetAndCourses( programId, courseId, versionId, versionCode);
            return list;
        }

        public static List<PreRequisiteSetDTO> GetAllPreRequisiteDetailCourses(int preRequisiteMasterId)
        {
            List<PreRequisiteSetDTO> list = RepositoryManager.PrerequisiteDetailV2_Repository.GetAllPreRequisiteDetailCourses(preRequisiteMasterId);
            return list;
        }

        public static List<rPreRequisiteCourse> GetAllPreRequisiteCoursesProgramWise(int programId)
        {
            List<rPreRequisiteCourse> list = RepositoryManager.PrerequisiteDetailV2_Repository.GetAllPreRequisiteCoursesProgramWise(programId);
            return list;
        }
        
    }
}

