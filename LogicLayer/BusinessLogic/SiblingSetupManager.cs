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
    public class SiblingSetupManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "SiblingSetupCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<SiblingSetup> GetCacheAsList(string rawKey)
        {
            List<SiblingSetup> list = (List<SiblingSetup>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static SiblingSetup GetCacheItem(string rawKey)
        {
            SiblingSetup item = (SiblingSetup)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        
        
        public static int Insert(SiblingSetup siblingSetup)
        {
            int id = RepositoryManager.SiblingSetup_Repository.Insert(siblingSetup);
            InvalidateCache();
            return id;
        }

        public static bool Update(SiblingSetup siblingSetup)
        {
            bool isExecute = RepositoryManager.SiblingSetup_Repository.Update(siblingSetup);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.SiblingSetup_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static SiblingSetup GetById(int? id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "SiblingSetupById" + id;
            SiblingSetup siblingSetup = GetCacheItem(rawKey);

            if (siblingSetup == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                siblingSetup = RepositoryManager.SiblingSetup_Repository.GetById(id);
                if (siblingSetup != null)
                    AddCacheItem(rawKey, siblingSetup);
            }

            return siblingSetup;
        }

        public static List<SiblingSetup> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "SiblingSetupGetAll";

            List<SiblingSetup> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.SiblingSetup_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static SiblingSetup GetByApplicantId(int applicantId)
        {
            SiblingSetup siblingSetup = RepositoryManager.SiblingSetup_Repository.GetByApplicantId(applicantId);
            return siblingSetup;
        }

        public static bool DeleteByApplicantIdGroupId(int applicant, int groupId)
        {
            bool isExecute = RepositoryManager.SiblingSetup_Repository.DeleteByApplicantIdGroupId(applicant, groupId);
            InvalidateCache();
            return isExecute;
        }
    }
}
