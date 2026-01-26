using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class AcademicCalenderManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "AcademicCalenderCache" };
        const double CacheDuration = 1.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<AcademicCalender> GetCacheAsList(string rawKey)
        {
            List<AcademicCalender> list = (List<AcademicCalender>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static AcademicCalender GetCacheItem(string rawKey)
        {
            AcademicCalender item = (AcademicCalender)HttpRuntime.Cache[GetCacheKey(rawKey)];
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
            DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddSeconds(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        }



        public static void InvalidateCache()
        {
            // Remove the cache dependency
            HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        }

        #endregion

        public static int Insert(AcademicCalender academicCalender)
        {
            int id = RepositoryManager.AcademicCalender_Repository.Insert(academicCalender);
            InvalidateCache();
            return id;
        }

        public static bool Update(AcademicCalender academicCalender)
        {
            bool isExecute = RepositoryManager.AcademicCalender_Repository.Update(academicCalender);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.AcademicCalender_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static AcademicCalender GetById(int? id)
        {
            string rawKey = "AcademicCalenderById" + id;
            AcademicCalender academicCalender = GetCacheItem(rawKey);

            if (academicCalender == null)
            {
                academicCalender = RepositoryManager.AcademicCalender_Repository.GetById(id);
                if (academicCalender != null)
                    AddCacheItem(rawKey, academicCalender);
            }

            return academicCalender;
        }

        public static AcademicCalender GetActiveRegistrationCalender()
        {
            string rawKey = "AcademicCalenderActiveRegistrationCalender";
            AcademicCalender academicCalender = GetCacheItem(rawKey);

            if (academicCalender == null)
            {
                List<AcademicCalender> list = GetAll();

                if (list != null)
                    academicCalender = list.Where(a => a.IsActiveRegistration == true).Single();

                if (academicCalender != null)
                    AddCacheItem(rawKey, academicCalender);
            }

            return academicCalender;
        }

        public static AcademicCalender GetActiveAdmissionCalender()
        {
            string rawKey = "AcademicCalenderActiveAdmissionCalender";
            AcademicCalender academicCalender = GetCacheItem(rawKey);

            if (academicCalender == null)
            {
                List<AcademicCalender> list = GetAll();

                if (list != null)
                    academicCalender = list.Where(a => a.IsActiveAdmission == true).Single();

                if (academicCalender != null)
                    AddCacheItem(rawKey, academicCalender);
            }

            return academicCalender;
        }

        public static AcademicCalender GetIsCurrent()
        {
            string rawKey = "AcademicCalenderGetIsCurrent";
            AcademicCalender academicCalender = GetCacheItem(rawKey);

            if (academicCalender == null)
            {
                List<AcademicCalender> list = GetAll();

                if (list != null)
                    academicCalender = list.Where(a => a.IsCurrent == true).FirstOrDefault();

                if (academicCalender != null)
                    AddCacheItem(rawKey, academicCalender);
            }

            return academicCalender;
        }

        public static AcademicCalender GetIsCurrent(int CalenderMasterID)
        {
            
            string rawKey = "AcademicCalenderGetIsCurrent";
            AcademicCalender academicCalender = GetCacheItem(rawKey);

            if (academicCalender == null)
            {
                List<AcademicCalender> list = GetAll(CalenderMasterID);
                if (list != null)
                    academicCalender = list.Where(a => a.IsCurrent == true).Single();

                if (academicCalender != null)
                    AddCacheItem(rawKey, academicCalender);
            }

            return academicCalender;
        }

        public static List<AcademicCalender> GetIsCurrentAllSession()
        {
            string rawKey = "AcademicCalenderGetIsCurrentAllSession";
            List<AcademicCalender> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = GetAll();

                if (list != null)
                    list = list.Where(a => a.IsCurrent == true).ToList();

                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static AcademicCalender GetIsActiveRegistration()
        {
            string rawKey = "AcademicCalenderIsActiveRegistration";
            AcademicCalender academicCalender = GetCacheItem(rawKey);

            if (academicCalender == null)
            {
                List<AcademicCalender> list = GetAll();

                if (list != null)
                    academicCalender = list.Where(a => a.IsActiveRegistration == true).Single();

                if (academicCalender != null)
                    AddCacheItem(rawKey, academicCalender);
            }

            return academicCalender;
        }

        public static List<AcademicCalender> GetIsActiveRegistrationAllSession()
        {
            string rawKey = "AcademicCalenderIsActiveRegistrationAllSession";
            List<AcademicCalender> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = GetAll();

                if (list != null)
                    list = list.Where(a => a.IsActiveRegistration == true).ToList();

                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<AcademicCalender> GetAll()
        {

            List<AcademicCalender> list = null;

            if (list == null)
            {
                list = RepositoryManager.AcademicCalender_Repository.GetAll();
            }

            return list;
        }

        public static List<AcademicCalender> GetCustom()
        {
            const string rawKey = "AcademicCalenderGetAll";

            List<AcademicCalender> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.AcademicCalender_Repository.GetCustom();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }


        public static List<AcademicCalender> GetAll(int calenderUnitMasterID)
        {
            List<AcademicCalender> list = null;

            if (list == null)
            {
                list = RepositoryManager.AcademicCalender_Repository.GetAll(calenderUnitMasterID);
            }

            return list;
        }

        public static AcademicCalender GetActiveRegistrationCalenderByProgramId(int programId)
        {
            string rawKey = "AcademicCalenderByProgramId" + programId;
            AcademicCalender academicCalender = GetCacheItem(rawKey);

            if (academicCalender == null)
            {
                academicCalender = RepositoryManager.AcademicCalender_Repository.GetActiveRegistrationCalenderByProgramId(programId);
                if (academicCalender != null)
                    AddCacheItem(rawKey, academicCalender);
            }

            return academicCalender;
        }

        public static List<rAcaCalSessionListByRoll> GetAcaCalSessionListByRoll(string roll)
        {
            const string rawKey = "RptAcaCalSessionListByRoll";

            List<rAcaCalSessionListByRoll> list = GetCacheAsAcaCalSessionListByRoll(rawKey);

            if (list == null)
            {
                list = RepositoryManager.AcademicCalender_Repository.GetAcaCalSessionListByRoll(roll);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        private static List<rAcaCalSessionListByRoll> GetCacheAsAcaCalSessionListByRoll(string rawKey)
        {
            List<rAcaCalSessionListByRoll> list = (List<rAcaCalSessionListByRoll>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static List<rAcaCalSessionListByProgram> GetAcaCalSessionListCompleted(string roll)
        {
            const string rawKey = "RptAcaCalSessionListCompleted";

            List<rAcaCalSessionListByProgram> list = GetCacheAsAcaCalSessionListCompleted(rawKey);

            if (list == null)
            {
                list = RepositoryManager.AcademicCalender_Repository.GetAcaCalSessionListCompleted(roll);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        private static List<rAcaCalSessionListByProgram> GetCacheAsAcaCalSessionListCompleted(string rawKey)
        {
            List<rAcaCalSessionListByProgram> list = (List<rAcaCalSessionListByProgram>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static AcademicCalender GetIsActiveRegistrationByProgramId(int programId)
        {
            AcademicCalender academicCalender = RepositoryManager.AcademicCalender_Repository.GetIsActiveRegistrationByProgramId(programId);

            return academicCalender;
        }

        public static List<rYear> GetAllYear()
        {
            List<rYear> list = RepositoryManager.AcademicCalender_Repository.GetAllYear();
            return list;
        }

        public static AcademicCalender GetIsCurrentRegistrationByProgramId(int programId)
        {
            AcademicCalender academicCalender = RepositoryManager.AcademicCalender_Repository.GetIsCurrentRegistrationByProgramId(programId);
            return academicCalender;
        }

        public static List<Semester> SemesterListByProgramIdBatchId(int programId, int BatchId)
        {
            List<Semester> list = RepositoryManager.AcademicCalender_Repository.SemesterListByProgramIdBatchId(programId, BatchId);
            return list;
        }

        public static List<AcademicCalender> AcaCalSessionByProgramIdBatchId(int programId, int BatchId)
        {
            List<AcademicCalender> list = RepositoryManager.AcademicCalender_Repository.AcaCalSessionByProgramIdBatchId(programId, BatchId);
            return list;
        }

        public static List<AcademicCalender> GetAllByProgram(int programId)
        {
            List<AcademicCalender> list = RepositoryManager.AcademicCalender_Repository.GetAllByProgram(programId);
            return list;
        }

        

    }
}
