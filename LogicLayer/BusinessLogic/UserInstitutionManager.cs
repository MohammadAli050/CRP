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
    public class UserInstitutionManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "UserInstitutionCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<UserInstitution> GetCacheAsList(string rawKey)
        {
            List<UserInstitution> list = (List<UserInstitution>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static UserInstitution GetCacheItem(string rawKey)
        {
            UserInstitution item = (UserInstitution)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(UserInstitution userinstitution)
        {
            int id = RepositoryManager.UserInstitution_Repository.Insert(userinstitution);
            InvalidateCache();
            return id;
        }

        public static bool Update(UserInstitution userinstitution)
        {
            bool isExecute = RepositoryManager.UserInstitution_Repository.Update(userinstitution);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.UserInstitution_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static UserInstitution GetById(int? id)
        {
            string rawKey = "UserInstitutionByID" + id;
            UserInstitution userinstitution = GetCacheItem(rawKey);

            if (userinstitution == null)
            {
                userinstitution = RepositoryManager.UserInstitution_Repository.GetById(id);
                if (userinstitution != null)
                    AddCacheItem(rawKey,userinstitution);
            }

            return userinstitution;
        }

        public static UserInstitution GetByInstituteIdUserId(int instituteId, int userId)
        {
            UserInstitution userinstitution = RepositoryManager.UserInstitution_Repository.GetByInstituteIdUserId(instituteId, userId);
            return userinstitution;
        }

        public static List<UserInstitution> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "UserInstitutionGetAll";

            List<UserInstitution> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.UserInstitution_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<UserInstitution> GetAllByUserId(int userId)
        {
            List<UserInstitution> list = RepositoryManager.UserInstitution_Repository.GetAllByUserId(userId);
            return list;
        }

    }
}

