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
   public class GradeSheetManager
    {

        #region Cache

       public static readonly string[] MasterCacheKeyArray = { "GradeSheetCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<GradeSheet> GetCacheAsList(string rawKey)
        {
            List<GradeSheet> list = (List<GradeSheet>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static GradeSheet GetCacheItem(string rawKey)
        {
            GradeSheet item = (GradeSheet)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(GradeSheet gradeSheet)
        {
            int id = RepositoryManager.GradeSheet_Repository.Insert(gradeSheet);
            InvalidateCache();
            return id;
        }

        public static bool Update(GradeSheet gradeSheet)
        {
            bool isExecute = RepositoryManager.GradeSheet_Repository.Update(gradeSheet);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.GradeSheet_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static GradeSheet GetById(int? id)
        {
            string rawKey = "GradeSheetId" + id;
            GradeSheet gradeSheet = GetCacheItem(rawKey);

            if (gradeSheet == null)
            {
                gradeSheet = RepositoryManager.GradeSheet_Repository.GetById(id);
                if (gradeSheet != null)
                    AddCacheItem(rawKey, gradeSheet);
            }

            return gradeSheet;
        }

        public static List<GradeSheet> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "GradeSheetGetAll";

            List<GradeSheet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.GradeSheet_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<GradeSheet> GetAllByAcaCalSectionId(int id)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "GradeSheetGetAllByAcaCalSectionId" + id + DateTime.Now;

            List<GradeSheet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.GradeSheet_Repository.GetAllByAcaCalSectionId(id);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

    }
}
