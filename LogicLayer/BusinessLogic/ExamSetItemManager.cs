using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.DataLogic;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicLayer.BusinessLogic
{
    public class ExamSetItemManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "MicroTestSetGroupCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamSetItem> GetCacheAsList(string rawKey)
        {
            List<ExamSetItem> list = (List<ExamSetItem>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamSetItem GetCacheItem(string rawKey)
        {
            ExamSetItem item = (ExamSetItem)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamSetItem examSetItemp)
        {
            int id = RepositoryManager.ExamSetItem_Repository.Insert(examSetItemp);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamSetItem examSetItemp)
        {
            bool isExecute = RepositoryManager.ExamSetItem_Repository.Update(examSetItemp);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamSetItem_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamSetItem GetById(int? id)
        {
            ExamSetItem examSetItem = RepositoryManager.ExamSetItem_Repository.GetById(id);
            return examSetItem;
        }

        public static List<ExamSetItem> GetAll()
        {
            List<ExamSetItem> list = RepositoryManager.ExamSetItem_Repository.GetAll();
            return list;
        }

        public static bool GetByExamExamSetId(int examSetId, int examId)
        {
            ExamSetItem examSetItemObj = RepositoryManager.ExamSetItem_Repository.GetByExamExamSetId(examSetId, examId);
            if (examSetItemObj == null) { return true; }
            else { return false; }
        }

        public static List<ExamSetItemDTO> GetAllExamSetItem()
        {
            List<ExamSetItemDTO> list = RepositoryManager.ExamSetItem_Repository.GetAllExamSetItem();
            return list;
        }
    }
}
