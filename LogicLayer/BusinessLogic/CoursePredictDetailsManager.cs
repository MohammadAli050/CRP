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
    public class CoursePredictDetailsManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "CoursePredictDetailsCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<CoursePredictDetails> GetCacheAsList(string rawKey)
        {
            List<CoursePredictDetails> list = (List<CoursePredictDetails>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static CoursePredictDetails GetCacheItem(string rawKey)
        {
            CoursePredictDetails item = (CoursePredictDetails)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(CoursePredictDetails coursepredictdetails)
        {
            int id = RepositoryManager.CoursePredictDetails_Repository.Insert(coursepredictdetails);
            InvalidateCache();
            return id;
        }

        public static bool Update(CoursePredictDetails coursepredictdetails)
        {
            bool isExecute = RepositoryManager.CoursePredictDetails_Repository.Update(coursepredictdetails);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.CoursePredictDetails_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }
         
        public static CoursePredictDetails GetById(int id)
        {
            string rawKey = "CoursePredictDetailsByID" + id;
            CoursePredictDetails coursepredictdetails = GetCacheItem(rawKey);

            if (coursepredictdetails == null)
            {
                coursepredictdetails = RepositoryManager.CoursePredictDetails_Repository.GetById(id);
                if (coursepredictdetails != null)
                    AddCacheItem(rawKey,coursepredictdetails);
            }

            return coursepredictdetails;
        }

        public static List<CoursePredictDetails> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "CoursePredictDetailsGetAll";

            List<CoursePredictDetails> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CoursePredictDetails_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<CoursePredictDetails> GetAll(int acaCalId, int programId)
        {
            string rawKey = "CoursePredictDetailsGetAll" + acaCalId + programId;

            List<CoursePredictDetails> list = GetCacheAsList(rawKey);

            if (list == null || list.Count() == 0)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CoursePredictDetails_Repository.GetAll(acaCalId, programId);
                if (list != null && list.Count() != 0)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}