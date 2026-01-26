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
    public class ExamScheduleDayManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamScheduleDayCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamScheduleDay> GetCacheAsList(string rawKey)
        {
            List<ExamScheduleDay> list = (List<ExamScheduleDay>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamScheduleDay GetCacheItem(string rawKey)
        {
            ExamScheduleDay item = (ExamScheduleDay)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamScheduleDay examscheduleday)
        {
            int id = RepositoryManager.ExamScheduleDay_Repository.Insert(examscheduleday);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamScheduleDay examscheduleday)
        {
            bool isExecute = RepositoryManager.ExamScheduleDay_Repository.Update(examscheduleday);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamScheduleDay_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamScheduleDay GetById(int id)
        {
            string rawKey = "ExamScheduleDayByID" + id;
            ExamScheduleDay examscheduleday = GetCacheItem(rawKey);

            if (examscheduleday == null)
            {
                examscheduleday = RepositoryManager.ExamScheduleDay_Repository.GetById(id);
                if (examscheduleday != null)
                    AddCacheItem(rawKey,examscheduleday);
            }

            return examscheduleday;
        }

        public static List<ExamScheduleDay> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamScheduleDayGetAll";

            List<ExamScheduleDay> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamScheduleDay_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<ExamScheduleDay> GetAllByExamSet(int examScheduleSetId)
        {
            //string rawKey = "ExamScheduleDayGetAllByAcaCalId" + examScheduleSetId;

            //List<ExamScheduleDay> list = GetCacheAsList(rawKey);

            //if (list == null || list.Count() == 0)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    list = RepositoryManager.ExamScheduleDay_Repository.GetAllByExamSet(examScheduleSetId);
            //    if (list != null && list.Count() != 0)
            //        AddCacheItem(rawKey, list);
            //}

            //return list;
            return RepositoryManager.ExamScheduleDay_Repository.GetAllByExamSet(examScheduleSetId);
        }
    }
}

