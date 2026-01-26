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
    public class ExamScheduleRoomInfoManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamScheduleRoomInfoCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamScheduleRoomInfo> GetCacheAsList(string rawKey)
        {
            List<ExamScheduleRoomInfo> list = (List<ExamScheduleRoomInfo>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamScheduleRoomInfo GetCacheItem(string rawKey)
        {
            ExamScheduleRoomInfo item = (ExamScheduleRoomInfo)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamScheduleRoomInfo examscheduleroominfo)
        {
            int id = RepositoryManager.ExamScheduleRoomInfo_Repository.Insert(examscheduleroominfo);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamScheduleRoomInfo examscheduleroominfo)
        {
            bool isExecute = RepositoryManager.ExamScheduleRoomInfo_Repository.Update(examscheduleroominfo);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamScheduleRoomInfo_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamScheduleRoomInfo GetById(int id)
        {
            string rawKey = "ExamScheduleRoomInfoByID" + id;
            ExamScheduleRoomInfo examscheduleroominfo = GetCacheItem(rawKey);

            if (examscheduleroominfo == null)
            {
                examscheduleroominfo = RepositoryManager.ExamScheduleRoomInfo_Repository.GetById(id);
                if (examscheduleroominfo != null)
                    AddCacheItem(rawKey,examscheduleroominfo);
            }

            return examscheduleroominfo;
        }

        public static List<ExamScheduleRoomInfo> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamScheduleRoomInfoGetAll";

            List<ExamScheduleRoomInfo> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamScheduleRoomInfo_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<ExamScheduleRoomInfo> GetAllByAcaCalExamSetDayTimeSlot(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId)
        {
            return RepositoryManager.ExamScheduleRoomInfo_Repository.GetAllByAcaCalExamSetDayTimeSlot(acaCalId, examScheduleSetId, dayId, timeSlotId);
        }

        //public static bool DeleteByAcaCalExamSetDayTimeSlotRoomInfo(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, int roomInfoId)
        //{
        //    bool isExecute = RepositoryManager.ExamScheduleRoomInfo_Repository.DeleteByAcaCalExamSetDayTimeSlotRoomInfo(acaCalId, examScheduleSetId, dayId, timeSlotId, roomInfoId);
        //    InvalidateCache();
        //    return isExecute;
        //}
    }
}