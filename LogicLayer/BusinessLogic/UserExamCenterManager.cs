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
    public class UserExamCenterManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "UserExamCenterCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<UserExamCenter> GetCacheAsList(string rawKey)
        {
            List<UserExamCenter> list = (List<UserExamCenter>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static UserExamCenter GetCacheItem(string rawKey)
        {
            UserExamCenter item = (UserExamCenter)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(UserExamCenter userexamcenter)
        {
            int id = RepositoryManager.UserExamCenter_Repository.Insert(userexamcenter);
            InvalidateCache();
            return id;
        }

        public static bool Update(UserExamCenter userexamcenter)
        {
            bool isExecute = RepositoryManager.UserExamCenter_Repository.Update(userexamcenter);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.UserExamCenter_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static UserExamCenter GetById(int? id)
        {
            string rawKey = "UserExamCenterByID" + id;
            UserExamCenter userexamcenter = GetCacheItem(rawKey);

            if (userexamcenter == null)
            {
                userexamcenter = RepositoryManager.UserExamCenter_Repository.GetById(id);
                if (userexamcenter != null)
                    AddCacheItem(rawKey,userexamcenter);
            }

            return userexamcenter;
        }

        public static List<UserExamCenter> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "UserExamCenterGetAll";

            List<UserExamCenter> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.UserExamCenter_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static UserExamCenter GetByExamCenterIdUserId(int examCenterId, int userId)
        {
            UserExamCenter userExamCenter = RepositoryManager.UserExamCenter_Repository.GetByExamCenterIdUserId(examCenterId, userId);
            return userExamCenter;
        }

        public static List<UserExamCenter> GetAllByUserId(int userId)
        {
            List<UserExamCenter> list = RepositoryManager.UserExamCenter_Repository.GetAllByUserId(userId);
            return list;
        }
    }
}

