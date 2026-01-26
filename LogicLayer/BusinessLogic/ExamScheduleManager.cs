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
    public class ExamScheduleManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamScheduleCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamSchedule> GetCacheAsList(string rawKey)
        {
            List<ExamSchedule> list = (List<ExamSchedule>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamSchedule GetCacheItem(string rawKey)
        {
            ExamSchedule item = (ExamSchedule)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamSchedule examschedule)
        {
            int id = RepositoryManager.ExamSchedule_Repository.Insert(examschedule);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamSchedule examschedule)
        {
            bool isExecute = RepositoryManager.ExamSchedule_Repository.Update(examschedule);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamSchedule_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamSchedule GetById(int id)
        {
            string rawKey = "ExamScheduleByID" + id;
            ExamSchedule examschedule = GetCacheItem(rawKey);

            if (examschedule == null)
            {
                examschedule = RepositoryManager.ExamSchedule_Repository.GetById(id);
                if (examschedule != null)
                    AddCacheItem(rawKey,examschedule);
            }

            return examschedule;
        }

        public static List<ExamSchedule> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamScheduleGetAll";

            List<ExamSchedule> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamSchedule_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<ExamSchedule> GetAllByAcaCalExamSet(int acaCalId, int examSet)
        {
            //string rawKey = "StudentGetAllByAcaCalExamSet" + acaCalId + examSet;

            //List<ExamSchedule> list = GetCacheAsList(rawKey);

            //if (list == null || list.Count() == 0)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    list = RepositoryManager.ExamSchedule_Repository.GetAllByAcaCalExamSet(acaCalId, examSet);
            //    if (list != null && list.Count() != 0)
            //        AddCacheItem(rawKey, list);
            //}

            //return list;
            return RepositoryManager.ExamSchedule_Repository.GetAllByAcaCalExamSet(acaCalId, examSet);
        }

        public static List<ConflictStudentDTO> GetAllByAcaCalExamSetDaySlot(int acaCalId, int examSetId, int dayId, int timeSlotId)
        {
            return RepositoryManager.ExamSchedule_Repository.GetAllByAcaCalExamSetDaySlot(acaCalId, examSetId, dayId, timeSlotId);
        }

        public static ExamSchedule GetByParameters(int acaCalId, int examSetId, int dayId, int timeSlotId, int courseId, int versionId)
        {
            string rawKey = "ExamScheduleByParameters" + acaCalId + examSetId + dayId + timeSlotId + courseId + versionId;
            ExamSchedule examschedule = GetCacheItem(rawKey);

            if (examschedule == null)
            {
                examschedule = RepositoryManager.ExamSchedule_Repository.GetByParameters(acaCalId, examSetId, dayId, timeSlotId, courseId, versionId);
                if (examschedule != null)
                    AddCacheItem(rawKey, examschedule);
            }

            return examschedule;
        }

        public static string GetTotalStudentMaleFemale(int examScheduleId)
        {
            return RepositoryManager.ExamSchedule_Repository.GetTotalStudentMaleFemale(examScheduleId);
        }

        public static string GetTotalMaleFemale(int acaCalId, int dayId, int timeSlotId)
        {
            return RepositoryManager.ExamSchedule_Repository.GetTotalMaleFemale(acaCalId, dayId, timeSlotId);
        }

        public static List<ExamSchedule> GetAllByAcaCalExamSetDayTimeSlot(int acaCalId, int examSet, int dayId, int timeSlotId)
        {
            string rawKey = "StudentGetAllByAcaCalExamSetDayTimeSlot" + acaCalId + examSet + dayId + timeSlotId;

            List<ExamSchedule> list = GetCacheAsList(rawKey);

            if (list == null || list.Count() == 0)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamSchedule_Repository.GetAllByAcaCalExamSetDayTimeSlot(acaCalId, examSet, dayId, timeSlotId);
                if (list != null && list.Count() != 0)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<rExamAttendanceSheet> GetExamAttendaceListByRoom(int acaCalId, int examSetId, int dayId, int timeSlotId, int roomId)
        {
            List<rExamAttendanceSheet> list = null;
            list = RepositoryManager.ExamSchedule_Repository.GetExamAttendaceListByRoom(acaCalId, examSetId, dayId, timeSlotId, roomId);

            return list;
        }

        public static List<rExamSeatPlanByRoom> GetExamSeatPlanByRoom(int acaCalId, int examSetId, int dayId, int timeSlotId, int roomId)
        {

            List<rExamSeatPlanByRoom> list = null;
            list = RepositoryManager.ExamSchedule_Repository.GetExamSeatPlanByRoom(acaCalId, examSetId, dayId, timeSlotId, roomId);

            return list;
        }

        public static List<ConflictStudentDTO> GetAllStudentRollbyExamScheduleGender(int examScheduleId, string gender)
        {
            return RepositoryManager.ExamSchedule_Repository.GetAllStudentRollbyExamScheduleGender(examScheduleId, gender);
        }

        public static List<ConflictStudentDTO> GetAllStudentRollbyExamSchedule(int examScheduleId)
        {
            return RepositoryManager.ExamSchedule_Repository.GetAllStudentRollbyExamSchedule(examScheduleId);
        }

        
    }
}

