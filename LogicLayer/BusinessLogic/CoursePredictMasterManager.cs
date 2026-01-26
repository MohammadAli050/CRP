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
    public class CoursePredictMasterManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "CoursePredictMasterCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<CoursePredictMaster> GetCacheAsList(string rawKey)
        {
            List<CoursePredictMaster> list = (List<CoursePredictMaster>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static CoursePredictMaster GetCacheItem(string rawKey)
        {
            CoursePredictMaster item = (CoursePredictMaster)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(CoursePredictMaster coursepredictmaster)
        {
            int id = RepositoryManager.CoursePredictMaster_Repository.Insert(coursepredictmaster);
            InvalidateCache();
            return id;
        }

        public static bool Update(CoursePredictMaster coursepredictmaster)
        {
            bool isExecute = RepositoryManager.CoursePredictMaster_Repository.Update(coursepredictmaster);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.CoursePredictMaster_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static CoursePredictMaster GetById(int id)
        {
            string rawKey = "CoursePredictMasterByID" + id;
            CoursePredictMaster coursepredictmaster = GetCacheItem(rawKey);

            if (coursepredictmaster == null)
            {
                coursepredictmaster = RepositoryManager.CoursePredictMaster_Repository.GetById(id);
                if (coursepredictmaster != null)
                    AddCacheItem(rawKey,coursepredictmaster);
            }

            return coursepredictmaster;
        }

        public static List<CoursePredictMaster> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "CoursePredictMasterGetAll";

            List<CoursePredictMaster> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CoursePredictMaster_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static bool InsertByAcaCalProgram(int acaCalId, int programId)
        {
            bool isExecute = RepositoryManager.CoursePredictMaster_Repository.InsertByAcaCalProgram(acaCalId, programId);
            InvalidateCache();
            return isExecute;
        }

        public static List<CoursePredictMaster> GetAll(int acaCalId, int programId)
        {
            string rawKey = "CoursePredictMasterGetAll" + acaCalId + programId;

            List<CoursePredictMaster> list = GetCacheAsList(rawKey);

            if (list == null || list.Count() == 0)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CoursePredictMaster_Repository.GetAll(acaCalId, programId);
                if (list != null && list.Count() != 0)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

