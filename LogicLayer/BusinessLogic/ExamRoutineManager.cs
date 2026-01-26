using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicLayer.BusinessLogic
{
    public class ExamRoutineManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamRoutineSectionCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamRoutine> GetCacheAsList(string rawKey)
        {
            List<ExamRoutine> list = (List<ExamRoutine>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamRoutine GetCacheItem(string rawKey)
        {
            ExamRoutine item = (ExamRoutine)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(ExamRoutine examRoutine)
        {
            int id = RepositoryManager.ExamRoutine_Repository.Insert(examRoutine);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamRoutine examRoutine)
        {
            bool isExecute = RepositoryManager.ExamRoutine_Repository.Update(examRoutine);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamRoutine_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamRoutine GetById(int id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "ExamRoutineById" + id;
            ExamRoutine examRoutine = GetCacheItem(rawKey);

            if (examRoutine == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                examRoutine = RepositoryManager.ExamRoutine_Repository.GetById(id);
                if (examRoutine != null)
                    AddCacheItem(rawKey, examRoutine);
            }

            return examRoutine;
        }

        public static List<ExamRoutine> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamRoutineGetAll";

            List<ExamRoutine> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamRoutine_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<rExamRoutine> GetExamRoutine(int acaCalId, int examScheduleSetId)
        {
            List<rExamRoutine> list = RepositoryManager.GetExamRoutine_Repository.GetExamRoutine(acaCalId, examScheduleSetId);
            return list;
        }

        public static List<InvigilationSchedule> GetInvigilationScheduleByAcaCalIdExamSetId(int acaCalId, int examScheduleSetId)
        {
            List<InvigilationSchedule> list = RepositoryManager.GetExamRoutine_Repository.GetInvigilationScheduleByAcaCalIdExamSetId(acaCalId, examScheduleSetId);
            return list;
        }

    }
}
