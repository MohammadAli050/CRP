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
   public class ActiveUserManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ActiveUserCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<UserActive> GetCacheAsList(string rawKey)
        {
            List<UserActive> list = (List<UserActive>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static UserActive GetCacheItem(string rawKey)
        {
            UserActive item = (UserActive)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static UserActive GetByLogInId(string LogInId)
        {
            string rawKey = "ActiveUserLogInId" + LogInId;
            UserActive activeUser = GetCacheItem(rawKey);

            if (activeUser == null)
            {
                activeUser = RepositoryManager.ActiveUser_Repository.GetByLogInId(LogInId);
                if (activeUser != null)
                    AddCacheItem(rawKey, activeUser);
            }

            return activeUser;
        }

        public static List<UserActive> GetAll(int ValueID, int AdmissionCalenderID, string LogInID)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();
            List<UserActive>  list = RepositoryManager.ActiveUser_Repository.GetAll(ValueID, AdmissionCalenderID, LogInID);
            //string rawKey = "ActiveUserGetAll" + ValueID.ToString() + AdmissionCalenderID.ToString() + LogInID; ;

            //List<UserActive> list = GetCacheAsList(rawKey);

            //if (list == null)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    list = RepositoryManager.ActiveUser_Repository.GetAll(ValueID, AdmissionCalenderID,LogInID);
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            return list;
        }

        public static UserActive GetById(int? id)
        {
            string rawKey = "User_ID" + id;
            UserActive userActive = GetCacheItem(rawKey);

            if (userActive == null)
            {
                userActive = RepositoryManager.ActiveUser_Repository.GetById(id);
                if (userActive != null)
                    AddCacheItem(rawKey, userActive);
            }

            return userActive;
        }

        public static bool Update(UserActive userActive)
        {
            bool isExecute = RepositoryManager.ActiveUser_Repository.Update(userActive);
            InvalidateCache();
            return isExecute;
        }
    }
}
