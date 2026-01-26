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
    public class ShareProgramInSectionManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ShareProgramInSectionCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ShareProgramInSection> GetCacheAsList(string rawKey)
        {
            List<ShareProgramInSection> list = (List<ShareProgramInSection>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ShareProgramInSection GetCacheItem(string rawKey)
        {
            ShareProgramInSection item = (ShareProgramInSection)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ShareProgramInSection shareprograminsection)
        {
            int id = RepositoryManager.ShareProgramInSection_Repository.Insert(shareprograminsection);
            InvalidateCache();
            return id;
        }

        public static bool Update(ShareProgramInSection shareprograminsection)
        {
            bool isExecute = RepositoryManager.ShareProgramInSection_Repository.Update(shareprograminsection);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int AcademicCalenderSectionId, int ProgramId)
        {
            bool isExecute = RepositoryManager.ShareProgramInSection_Repository.Delete(AcademicCalenderSectionId,ProgramId);
            InvalidateCache();
            return isExecute;
        }

        public static ShareProgramInSection GetById(int AcademicCalenderSectionId, int ProgramId)
        {
            string rawKey = "ShareProgramInSectionByID" + AcademicCalenderSectionId.ToString() + ProgramId.ToString();
            ShareProgramInSection shareprograminsection = GetCacheItem(rawKey);

            if (shareprograminsection == null)
            {
                shareprograminsection = RepositoryManager.ShareProgramInSection_Repository.GetById(AcademicCalenderSectionId,ProgramId);
                if (shareprograminsection != null)
                    AddCacheItem(rawKey,shareprograminsection);
            }

            return shareprograminsection;
        }

        public static List<ShareProgramInSection> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ShareProgramInSectionGetAll";

            List<ShareProgramInSection> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ShareProgramInSection_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static bool DeleteByAcademicCalenderSectionId(int academicCalenderSectionId)
        {
            bool isExecute = RepositoryManager.ShareProgramInSection_Repository.DeleteByAcademicCalenderSectionId(academicCalenderSectionId);
            InvalidateCache();
            return isExecute;
        }
    }
}

