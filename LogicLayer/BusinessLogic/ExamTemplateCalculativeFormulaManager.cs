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
    public class ExamTemplateCalculativeFormulaManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamTemplateCalculativeFormulaCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamTemplateCalculativeFormula> GetCacheAsList(string rawKey)
        {
            List<ExamTemplateCalculativeFormula> list = (List<ExamTemplateCalculativeFormula>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamTemplateCalculativeFormula GetCacheItem(string rawKey)
        {
            ExamTemplateCalculativeFormula item = (ExamTemplateCalculativeFormula)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamTemplateCalculativeFormula examtemplatecalculativeformula)
        {
            int id = RepositoryManager.ExamTemplateCalculativeFormula_Repository.Insert(examtemplatecalculativeformula);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamTemplateCalculativeFormula examtemplatecalculativeformula)
        {
            bool isExecute = RepositoryManager.ExamTemplateCalculativeFormula_Repository.Update(examtemplatecalculativeformula);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamTemplateCalculativeFormula_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamTemplateCalculativeFormula GetById(int? id)
        {
            string rawKey = "ExamTemplateCalculativeFormulaByID" + id;
            ExamTemplateCalculativeFormula examtemplatecalculativeformula = GetCacheItem(rawKey);

            if (examtemplatecalculativeformula == null)
            {
                examtemplatecalculativeformula = RepositoryManager.ExamTemplateCalculativeFormula_Repository.GetById(id);
                if (examtemplatecalculativeformula != null)
                    AddCacheItem(rawKey,examtemplatecalculativeformula);
            }

            return examtemplatecalculativeformula;
        }

        public static List<ExamTemplateCalculativeFormula> GetAll()
        {
            List<ExamTemplateCalculativeFormula> list =  RepositoryManager.ExamTemplateCalculativeFormula_Repository.GetAll();
            return list;
        }


        public static List<ExamTemplateCalculativeFormula> GetByExamTemplateMasterId(int examTemplateMasterId)
        {
            List<ExamTemplateCalculativeFormula> list = RepositoryManager.ExamTemplateCalculativeFormula_Repository.GetByExamTemplateMasterId(examTemplateMasterId);
            return list;
        }
    }
}

