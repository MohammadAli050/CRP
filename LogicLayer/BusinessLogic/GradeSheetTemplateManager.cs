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
    public class GradeSheetTemplateManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "GradeSheetTemplateCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<GradeSheetTemplate> GetCacheAsList(string rawKey)
        {
            List<GradeSheetTemplate> list = (List<GradeSheetTemplate>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static GradeSheetTemplate GetCacheItem(string rawKey)
        {
            GradeSheetTemplate item = (GradeSheetTemplate)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(GradeSheetTemplate gradeSheetTemplate)
        {
            int id = RepositoryManager.GradeSheetTemplate_Repository.Insert(gradeSheetTemplate);
            InvalidateCache();
            return id;
        }

        public static bool Update(GradeSheetTemplate gradeSheetTemplate)
        {
            bool isExecute = RepositoryManager.GradeSheetTemplate_Repository.Update(gradeSheetTemplate);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.GradeSheetTemplate_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static GradeSheetTemplate GetById(int? id)
        {
            string rawKey = "GradeSheetTemplateById" + id;
            GradeSheetTemplate gradeSheetTemplate = GetCacheItem(rawKey);

            if (gradeSheetTemplate == null)
            {
                gradeSheetTemplate = RepositoryManager.GradeSheetTemplate_Repository.GetById(id);
                if (gradeSheetTemplate != null)
                    AddCacheItem(rawKey, gradeSheetTemplate);
            }

            return gradeSheetTemplate;
        }

        public static List<GradeSheetTemplate> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "GradeSheetTemplateGetAll";

            List<GradeSheetTemplate> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.GradeSheetTemplate_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
