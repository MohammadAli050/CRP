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
    public class ExamMarkMasterManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamMarkMasterCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamMarkMaster> GetCacheAsList(string rawKey)
        {
            List<ExamMarkMaster> list = (List<ExamMarkMaster>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamMarkMaster GetCacheItem(string rawKey)
        {
            ExamMarkMaster item = (ExamMarkMaster)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamMarkMaster exammarkmaster)
        {
            int id = RepositoryManager.ExamMarkMaster_Repository.Insert(exammarkmaster);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamMarkMaster exammarkmaster)
        {
            bool isExecute = RepositoryManager.ExamMarkMaster_Repository.Update(exammarkmaster);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamMarkMaster_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamMarkMaster GetById(int? id)
        {
            string rawKey = "ExamMarkMasterByID" + id;
            ExamMarkMaster exammarkmaster = GetCacheItem(rawKey);

            if (exammarkmaster == null)
            {
                exammarkmaster = RepositoryManager.ExamMarkMaster_Repository.GetById(id);
                if (exammarkmaster != null)
                    AddCacheItem(rawKey,exammarkmaster);
            }

            return exammarkmaster;
        }

        public static List<ExamMarkMaster> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamMarkMasterGetAll";

            List<ExamMarkMaster> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamMarkMaster_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static ExamMarkMaster GetByAcaCalIdAcaCalSectionIdExamTemplateItemId(int acaCalId, int acaCalsectionId, int examTemplateBasicItemId) 
        {
            ExamMarkMaster exammarkmaster = RepositoryManager.ExamMarkMaster_Repository.GetByAcaCalIdAcaCalSectionIdExamTemplateItemId(acaCalId, acaCalsectionId, examTemplateBasicItemId);
            return exammarkmaster;
        }
    }
}

