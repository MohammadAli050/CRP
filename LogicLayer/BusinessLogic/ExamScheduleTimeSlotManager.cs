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
    public class ExamScheduleTimeSlotManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamScheduleTimeSlotCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamScheduleTimeSlot> GetCacheAsList(string rawKey)
        {
            List<ExamScheduleTimeSlot> list = (List<ExamScheduleTimeSlot>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamScheduleTimeSlot GetCacheItem(string rawKey)
        {
            ExamScheduleTimeSlot item = (ExamScheduleTimeSlot)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamScheduleTimeSlot examscheduletimeslot)
        {
            int id = RepositoryManager.ExamScheduleTimeSlot_Repository.Insert(examscheduletimeslot);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamScheduleTimeSlot examscheduletimeslot)
        {
            bool isExecute = RepositoryManager.ExamScheduleTimeSlot_Repository.Update(examscheduletimeslot);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamScheduleTimeSlot_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamScheduleTimeSlot GetById(int id)
        {
            string rawKey = "ExamScheduleTimeSlotByID" + id;
            ExamScheduleTimeSlot examscheduletimeslot = GetCacheItem(rawKey);

            if (examscheduletimeslot == null)
            {
                examscheduletimeslot = RepositoryManager.ExamScheduleTimeSlot_Repository.GetById(id);
                if (examscheduletimeslot != null)
                    AddCacheItem(rawKey,examscheduletimeslot);
            }

            return examscheduletimeslot;
        }

        public static List<ExamScheduleTimeSlot> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamScheduleTimeSlotGetAll";

            List<ExamScheduleTimeSlot> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamScheduleTimeSlot_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<ExamScheduleTimeSlot> GetAllByExamSet(int examScheduleSetId)
        {
            //string rawKey = "StudentGetAllByAcaCalId" + examScheduleSetId;

            //List<ExamScheduleTimeSlot> list = GetCacheAsList(rawKey);

            //if (list == null || list.Count() == 0)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    list = RepositoryManager.ExamScheduleTimeSlot_Repository.GetAllByExamSet(examScheduleSetId);
            //    if (list != null && list.Count() != 0)
            //        AddCacheItem(rawKey, list);
            //}

            //return list;

            return RepositoryManager.ExamScheduleTimeSlot_Repository.GetAllByExamSet(examScheduleSetId);
        }
    }
}

