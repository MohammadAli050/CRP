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
    public class ExamMarkEquationColumnOrderManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamMarkEquationColumnOrderCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamMarkEquationColumnOrder> GetCacheAsList(string rawKey)
        {
            List<ExamMarkEquationColumnOrder> list = (List<ExamMarkEquationColumnOrder>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamMarkEquationColumnOrder GetCacheItem(string rawKey)
        {
            ExamMarkEquationColumnOrder item = (ExamMarkEquationColumnOrder)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamMarkEquationColumnOrder exammarkequationcolumnorder)
        {
            int id = RepositoryManager.ExamMarkEquationColumnOrder_Repository.Insert(exammarkequationcolumnorder);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamMarkEquationColumnOrder exammarkequationcolumnorder)
        {
            bool isExecute = RepositoryManager.ExamMarkEquationColumnOrder_Repository.Update(exammarkequationcolumnorder);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamMarkEquationColumnOrder_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamMarkEquationColumnOrder GetById(int id)
        {
            string rawKey = "ExamMarkEquationColumnOrderByID" + id;
            ExamMarkEquationColumnOrder exammarkequationcolumnorder = GetCacheItem(rawKey);

            if (exammarkequationcolumnorder == null)
            {
                exammarkequationcolumnorder = RepositoryManager.ExamMarkEquationColumnOrder_Repository.GetById(id);
                if (exammarkequationcolumnorder != null)
                    AddCacheItem(rawKey,exammarkequationcolumnorder);
            }

            return exammarkequationcolumnorder;
        }

        public static List<ExamMarkEquationColumnOrder> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamMarkEquationColumnOrderGetAll";

            List<ExamMarkEquationColumnOrder> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamMarkEquationColumnOrder_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<ExamMarkEquationColumnOrder> GetByTemplateItemId(int templateItemId) 
        {
            List<ExamMarkEquationColumnOrder> list = RepositoryManager.ExamMarkEquationColumnOrder_Repository.GetByTemplateItemId(templateItemId);
            return list;
        }

        public static bool DeleteByTemplateItemSequenceId(int templateItemId, int columnSequence) 
        {
            bool isExecute = RepositoryManager.ExamMarkEquationColumnOrder_Repository.DeleteByTemplateItemSequenceId(templateItemId, columnSequence);
            InvalidateCache();
            return isExecute;
        }
    }
}

