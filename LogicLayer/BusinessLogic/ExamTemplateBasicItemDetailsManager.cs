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
    public class ExamTemplateBasicItemDetailsManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamTemplateBasicItemDetailsCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamTemplateBasicItemDetails> GetCacheAsList(string rawKey)
        {
            List<ExamTemplateBasicItemDetails> list = (List<ExamTemplateBasicItemDetails>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamTemplateBasicItemDetails GetCacheItem(string rawKey)
        {
            ExamTemplateBasicItemDetails item = (ExamTemplateBasicItemDetails)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamTemplateBasicItemDetails examtemplatebasicitemdetails)
        {
            int id = RepositoryManager.ExamTemplateBasicItemDetails_Repository.Insert(examtemplatebasicitemdetails);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamTemplateBasicItemDetails examtemplatebasicitemdetails)
        {
            bool isExecute = RepositoryManager.ExamTemplateBasicItemDetails_Repository.Update(examtemplatebasicitemdetails);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamTemplateBasicItemDetails_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamTemplateBasicItemDetails GetById(int? id)
        {
            string rawKey = "ExamTemplateBasicItemDetailsByID" + id;
            ExamTemplateBasicItemDetails examtemplatebasicitemdetails = GetCacheItem(rawKey);

            if (examtemplatebasicitemdetails == null)
            {
                examtemplatebasicitemdetails = RepositoryManager.ExamTemplateBasicItemDetails_Repository.GetById(id);
                if (examtemplatebasicitemdetails != null)
                    AddCacheItem(rawKey,examtemplatebasicitemdetails);
            }

            return examtemplatebasicitemdetails;
        }

        public static List<ExamTemplateBasicItemDetails> GetAll()
        {
            List<ExamTemplateBasicItemDetails> list = RepositoryManager.ExamTemplateBasicItemDetails_Repository.GetAll();
            return list;
        }

        public static List<ExamTemplateBasicItemDetails> GetByExamTemplateMasterId(int examTemplateMasterId)
        {
            List<ExamTemplateBasicItemDetails> list = RepositoryManager.ExamTemplateBasicItemDetails_Repository.GetByExamTemplateMasterId(examTemplateMasterId);
            return list;
        }
    }
}

