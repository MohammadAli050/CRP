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
    public class ExamTemplateManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "TestTemplateCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamTemplate> GetCacheAsList(string rawKey)
        {
            List<ExamTemplate> list = (List<ExamTemplate>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamTemplate GetCacheItem(string rawKey)
        {
            ExamTemplate item = (ExamTemplate)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamTemplate examTemplate)
        {
            int id = RepositoryManager.ExamTemplate_Repository.Insert(examTemplate);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamTemplate examTemplate)
        {
            bool isExecute = RepositoryManager.ExamTemplate_Repository.Update(examTemplate);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamTemplate_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamTemplate GetById(int? id)
        {
            string rawKey = "TestTemplateByID" + id;
            ExamTemplate testtemplate = GetCacheItem(rawKey);

            if (testtemplate == null)
            {
                testtemplate = RepositoryManager.ExamTemplate_Repository.GetById(id);
                if (testtemplate != null)
                    AddCacheItem(rawKey,testtemplate);
            }

            return testtemplate;
        }

        public static List<ExamTemplate> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "TestTemplateGetAll";

            List<ExamTemplate> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamTemplate_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static bool GetExamTemplateByName(string examTemplateName) 
        {
            ExamTemplate examTemplate = RepositoryManager.ExamTemplate_Repository.GetExamTemplateByName(examTemplateName);
            if (examTemplate == null) { return true; }
            else { return false; }
        }
    }
}

