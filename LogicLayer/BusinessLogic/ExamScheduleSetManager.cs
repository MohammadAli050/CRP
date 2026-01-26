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
    public class ExamScheduleSetManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamScheduleSetCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamScheduleSet> GetCacheAsList(string rawKey)
        {
            List<ExamScheduleSet> list = (List<ExamScheduleSet>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamScheduleSet GetCacheItem(string rawKey)
        {
            ExamScheduleSet item = (ExamScheduleSet)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamScheduleSet examscheduleset)
        {
            int id = RepositoryManager.ExamScheduleSet_Repository.Insert(examscheduleset);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamScheduleSet examscheduleset)
        {
            bool isExecute = RepositoryManager.ExamScheduleSet_Repository.Update(examscheduleset);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamScheduleSet_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamScheduleSet GetById(int id)
        {
            string rawKey = "ExamScheduleSetByID" + id;
            ExamScheduleSet examscheduleset = GetCacheItem(rawKey);

            if (examscheduleset == null)
            {
                examscheduleset = RepositoryManager.ExamScheduleSet_Repository.GetById(id);
                if (examscheduleset != null)
                    AddCacheItem(rawKey,examscheduleset);
            }

            return examscheduleset;
        }

        public static ExamScheduleSet GetById(int id, string name)
        {
            string rawKey = "ExamScheduleSetByID" + id + name;
            ExamScheduleSet examscheduleset = GetCacheItem(rawKey);

            if (examscheduleset == null)
            {
                examscheduleset = RepositoryManager.ExamScheduleSet_Repository.GetById(id, name);
                if (examscheduleset != null)
                    AddCacheItem(rawKey, examscheduleset);
            }

            return examscheduleset;
        }

        public static List<ExamScheduleSet> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamScheduleSetGetAll";

            List<ExamScheduleSet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamScheduleSet_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<ExamScheduleSet> GetAllByAcaCalId(int acaCalId)
        {
            //string rawKey = "StudentGetAllByAcaCalId" + acaCalId;

            //List<ExamScheduleSet> list = GetCacheAsList(rawKey);

            //if (list == null || list.Count() == 0)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    list = RepositoryManager.ExamScheduleSet_Repository.GetAllByAcaCalId(acaCalId);
            //    if (list != null && list.Count() != 0)
            //        AddCacheItem(rawKey, list);
            //}

            //return list;
            return RepositoryManager.ExamScheduleSet_Repository.GetAllByAcaCalId(acaCalId);
        }
    }
}

