using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using LogicLayer.BusinessObjects.DTO;

namespace LogicLayer.BusinessLogic
{
    public class TemplateGroupManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "TemplateGroupCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamTemplateItem> GetCacheAsList(string rawKey)
        {
            List<ExamTemplateItem> list = (List<ExamTemplateItem>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamTemplateItem GetCacheItem(string rawKey)
        {
            ExamTemplateItem item = (ExamTemplateItem)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamTemplateItem examTemplateItem)
        {
            int id = RepositoryManager.TemplateGroup_Repository.Insert(examTemplateItem);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamTemplateItem examTemplateItem)
        {
            bool isExecute = RepositoryManager.TemplateGroup_Repository.Update(examTemplateItem);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.TemplateGroup_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamTemplateItem GetById(int id)
        {
            string rawKey = "TemplateGroupByID" + id;
            ExamTemplateItem examTemplateItem = GetCacheItem(rawKey);

            if (examTemplateItem == null)
            {
                examTemplateItem = RepositoryManager.TemplateGroup_Repository.GetById(id);
                if (examTemplateItem != null)
                    AddCacheItem(rawKey, examTemplateItem);
            }

            return examTemplateItem;
        }

        public static List<ExamTemplateItem> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "TemplateGroupGetAll";

            List<ExamTemplateItem> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.TemplateGroup_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        #region Get All Template Group List
        public static List<ExamTemplateItemDTO> GetAllTemplateGroup()
        {
            List<ExamTemplateItemDTO> list = RepositoryManager.TemplateGroup_Repository.GetAllTemplateItem();
            return list;
        }

        //public static List<ExamTemplateItemDTO> GetAllTemplateGroupAsList(string rawKey)
        //{
        //    List<ExamTemplateItemDTO> list = (List<ExamTemplateItemDTO>)HttpRuntime.Cache[GetCacheKey(rawKey)];
        //    return list;
        //}
        #endregion Get All Template Group List

        public static bool GetByTemplateExamSetId(int templateId, int examSetId)
        {
            ExamTemplateItem examTemplateItem = RepositoryManager.TemplateGroup_Repository.GetByTemplateExamSetId(templateId, examSetId);
            if (examTemplateItem == null) { return true; }
            else { return false; }
        }

        public static bool GetByTemplateExamSetExamId(int templateId, int examSetId, int examId)
        {
            ExamTemplateItem examTemplateItem = RepositoryManager.TemplateGroup_Repository.GetByTemplateExamSetExamId(templateId, examSetId, examId);
            if (examTemplateItem == null) { return true; }
            else { return false; }
        }

        public static decimal GetByTemplateExamSetExamId(int templateId)
        {
            decimal examTemplateItem = RepositoryManager.TemplateGroup_Repository.GetByTemplateExamSetExamId(templateId);
            return examTemplateItem;
        }

        public static List<ExamTemplateItemDTO> GetAllItemByTemplateId(int templateId)
        {
            List<ExamTemplateItemDTO> list = RepositoryManager.TemplateGroup_Repository.GetAllItemByTemplateId(templateId);
            return list;
        }

        public static List<ExamTemplateItem> GetAllByTemplateId(int templateId)
        {
            List<ExamTemplateItem> list = RepositoryManager.TemplateGroup_Repository.GetAllByTemplateId(templateId);
            return list;
        }
    }
}

