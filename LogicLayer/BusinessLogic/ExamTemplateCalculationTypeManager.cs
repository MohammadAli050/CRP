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
    public class ExamTemplateCalculationTypeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamTemplateCalculationTypeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamTemplateCalculationType> GetCacheAsList(string rawKey)
        {
            List<ExamTemplateCalculationType> list = (List<ExamTemplateCalculationType>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamTemplateCalculationType GetCacheItem(string rawKey)
        {
            ExamTemplateCalculationType item = (ExamTemplateCalculationType)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamTemplateCalculationType examtemplatecalculationtype)
        {
            int id = RepositoryManager.ExamTemplateCalculationType_Repository.Insert(examtemplatecalculationtype);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamTemplateCalculationType examtemplatecalculationtype)
        {
            bool isExecute = RepositoryManager.ExamTemplateCalculationType_Repository.Update(examtemplatecalculationtype);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamTemplateCalculationType_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamTemplateCalculationType GetById(int? id)
        {
            string rawKey = "ExamTemplateCalculationTypeByID" + id;
            ExamTemplateCalculationType examtemplatecalculationtype = GetCacheItem(rawKey);

            if (examtemplatecalculationtype == null)
            {
                examtemplatecalculationtype = RepositoryManager.ExamTemplateCalculationType_Repository.GetById(id);
                if (examtemplatecalculationtype != null)
                    AddCacheItem(rawKey,examtemplatecalculationtype);
            }

            return examtemplatecalculationtype;
        }

        public static List<ExamTemplateCalculationType> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamTemplateCalculationTypeGetAll";

            List<ExamTemplateCalculationType> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamTemplateCalculationType_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

