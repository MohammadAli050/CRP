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
    public class ExamScheduleSeatPlanManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamScheduleSeatPlanCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamScheduleSeatPlan> GetCacheAsList(string rawKey)
        {
            List<ExamScheduleSeatPlan> list = (List<ExamScheduleSeatPlan>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamScheduleSeatPlan GetCacheItem(string rawKey)
        {
            ExamScheduleSeatPlan item = (ExamScheduleSeatPlan)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return item;
        }

        private static List<rExamSeatPlan> GetExamSeatPlanAsList(string rawKey)
        {
            List<rExamSeatPlan> list = (List<rExamSeatPlan>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
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

        public static int Insert(ExamScheduleSeatPlan examschedueseatplan)
        {
            int id = RepositoryManager.ExamScheduleSeatPlan_Repository.Insert(examschedueseatplan);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamScheduleSeatPlan examschedueseatplan)
        {
            bool isExecute = RepositoryManager.ExamScheduleSeatPlan_Repository.Update(examschedueseatplan);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamScheduleSeatPlan_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static bool DeleteByExamScheduleId(int acaCalId, int examSetId, int dayId, int timeSlotId)
        {
            bool isExecute = RepositoryManager.ExamScheduleSeatPlan_Repository.DeleteByExamScheduleId(acaCalId, examSetId, dayId, timeSlotId);
            InvalidateCache();
            return isExecute;
        }

        public static ExamScheduleSeatPlan GetById(int id)
        {
            string rawKey = "ExamScheduleSeatPlanByID" + id;
            ExamScheduleSeatPlan examschedueseatplan = GetCacheItem(rawKey);

            if (examschedueseatplan == null)
            {
                examschedueseatplan = RepositoryManager.ExamScheduleSeatPlan_Repository.GetById(id);
                if (examschedueseatplan != null)
                    AddCacheItem(rawKey,examschedueseatplan);
            }

            return examschedueseatplan;
        }

        public static List<ExamScheduleSeatPlan> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamScheduleSeatPlanGetAll";

            List<ExamScheduleSeatPlan> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamScheduleSeatPlan_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static int GenerateSeatPlan(int acaCalId, int examSetId, int dayId, int timeSlotId)
        {
            return RepositoryManager.ExamScheduleSeatPlan_Repository.GenerateSeatPlan(acaCalId, examSetId, dayId, timeSlotId);
        }

        public static List<rExamSeatPlan> GetExamSeatPlan(int acaCalId, string examScheduleSetId, int calenderUnitMasterId, int dayId, int timeSlotId)
        {
            //const string rawKey = "RptExamSeatPlan";

            List<rExamSeatPlan> list = RepositoryManager.ExamSeatPlan_Repository.GetExamSeatPlan(acaCalId, examScheduleSetId, calenderUnitMasterId, dayId, timeSlotId);
            /*
            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                if (list != null)
                    AddCacheItem(rawKey, list);
            }
            */
            return list;
        }

        public static List<ExamScheduleSeatPlan> GetAllByAcaCalExamSetDayTimeSlotRoom(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, int roomId)
        {
            return RepositoryManager.ExamScheduleSeatPlan_Repository.GetAllByAcaCalExamSetDayTimeSlotRoom(acaCalId, examScheduleSetId, dayId, timeSlotId, roomId);
        }

        public static List<rTopSheetPresent> GetAllByAcaCalExamSetDayTimeSlotCourseCode(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, string courseCode, string sectionId)
        {
            return RepositoryManager.ExamScheduleSeatPlan_Repository.GetAllByAcaCalExamSetDayTimeSlotCourseCode(acaCalId, examScheduleSetId, dayId, timeSlotId, courseCode, sectionId);
        }

        public static List<rTopSheetAbsent> GetAllByAcaCalExamSetDayTimeSlotCourseCodeAbsent(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, string courseCode, string sectionId)
        {
            return RepositoryManager.ExamScheduleSeatPlan_Repository.GetAllByAcaCalExamSetDayTimeSlotCourseCodeAbsent(acaCalId, examScheduleSetId, dayId, timeSlotId, courseCode, sectionId);
        }
    }
}

