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
   public class GradeDetailsManager
    {

        #region Cache

       public static readonly string[] MasterCacheKeyArray = { "GradeDetailsCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<GradeDetails> GetCacheAsList(string rawKey)
        {
            List<GradeDetails> list = (List<GradeDetails>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static GradeDetails GetCacheItem(string rawKey)
        {
            GradeDetails item = (GradeDetails)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(GradeDetails gradeDetails)
        {
            int id = RepositoryManager.GradeDetails_Repository.Insert(gradeDetails);
            InvalidateCache();
            return id;
        }

        public static bool Update(GradeDetails gradeDetails)
        {
            bool isExecute = RepositoryManager.GradeDetails_Repository.Update(gradeDetails);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.GradeDetails_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static GradeDetails GetById(int id)
        {
            string rawKey = "GradeId" + id;
            GradeDetails gradeDetails = GetCacheItem(rawKey);

            if (gradeDetails == null)
            {
                gradeDetails = RepositoryManager.GradeDetails_Repository.GetById(id);
                if (gradeDetails != null)
                    AddCacheItem(rawKey, gradeDetails);
            }

            return gradeDetails;
        }

        public static GradeDetails GetByGrade(string grade)
        {
            string rawKey = "GradeDetailsGetByGrade" + grade;
            GradeDetails gradeDetails = GetCacheItem(rawKey);

            if (gradeDetails == null)
            {
                gradeDetails = RepositoryManager.GradeDetails_Repository.GetByGrade(grade);
                if (gradeDetails != null)
                    AddCacheItem(rawKey, gradeDetails);
            }

            return gradeDetails;
        }

        public static List<GradeDetails> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "GradeDetailsGetAll";

            List<GradeDetails> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.GradeDetails_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<GradeDetails> GetByGradeMasterId(int gradeMasterId)
        {
            List<GradeDetails> list = RepositoryManager.GradeDetails_Repository.GetByGradeMasterId(gradeMasterId);
            return list;
        }

        //added by mohammad ali

        public static GradeDetails GetByMarks(decimal mark)
        {
            GradeDetails gradeDetails = new GradeDetails();
            gradeDetails = RepositoryManager.GradeDetails_Repository.GetByMarks(mark);
            return gradeDetails;
        }
    }
}
