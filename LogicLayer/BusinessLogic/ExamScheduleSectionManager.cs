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
    public class ExamScheduleSectionManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamScheduleSectionCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamScheduleSection> GetCacheAsList(string rawKey)
        {
            List<ExamScheduleSection> list = (List<ExamScheduleSection>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamScheduleSection GetCacheItem(string rawKey)
        {
            ExamScheduleSection item = (ExamScheduleSection)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamScheduleSection examschedulesection)
        {
            int id = RepositoryManager.ExamScheduleSection_Repository.Insert(examschedulesection);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamScheduleSection examschedulesection)
        {
            bool isExecute = RepositoryManager.ExamScheduleSection_Repository.Update(examschedulesection);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamScheduleSection_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static bool DeleteByExamSchedule(int examScheduleId)
        {
            bool isExecute = RepositoryManager.ExamScheduleSection_Repository.DeleteByExamSchedule(examScheduleId);
            InvalidateCache();
            return isExecute;
        }

        public static ExamScheduleSection GetById(int id)
        {
            string rawKey = "ExamScheduleSectionByID" + id;
            ExamScheduleSection examschedulesection = GetCacheItem(rawKey);

            if (examschedulesection == null)
            {
                examschedulesection = RepositoryManager.ExamScheduleSection_Repository.GetById(id);
                if (examschedulesection != null)
                    AddCacheItem(rawKey,examschedulesection);
            }

            return examschedulesection;
        }

        public static List<ExamScheduleSection> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamScheduleSectionGetAll";

            List<ExamScheduleSection> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamScheduleSection_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<ExamScheduleSection> GetAllByExamSchedule(int examScheduleId)
        {
            string rawKey = "ExamScheduleSectionGetAllByExamSchedule" + examScheduleId;

            List<ExamScheduleSection> list = GetCacheAsList(rawKey);

            if (list == null || list.Count() == 0)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamScheduleSection_Repository.GetAllByExamSchedule(examScheduleId);
                if (list != null && list.Count() != 0)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

