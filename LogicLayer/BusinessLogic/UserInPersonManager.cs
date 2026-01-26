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
   public class UserInPersonManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "UserInPersonCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<UserInPerson> GetCacheAsList(string rawKey)
        {
            List<UserInPerson> list = (List<UserInPerson>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static UserInPerson GetCacheItem(string rawKey)
        {
            UserInPerson item = (UserInPerson)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(UserInPerson userInPerson)
        {
            int id = RepositoryManager.UserInPerson_Repository.Insert(userInPerson);
            InvalidateCache();
            return id;
        }

        public static bool Update(UserInPerson userInPerson)
        {
            bool isExecute = RepositoryManager.UserInPerson_Repository.Update(userInPerson);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.UserInPerson_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static UserInPerson GetById(int id)
        {
            string rawKey = "User_ID" + "PersonID" + id;
     
            UserInPerson userInPerson = GetCacheItem(rawKey);

            if (userInPerson == null)
            {
                userInPerson = RepositoryManager.UserInPerson_Repository.GetById(id);
                if (userInPerson != null)
                    AddCacheItem(rawKey, userInPerson);
            }

            return userInPerson;
        }

        public static List<UserInPerson> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "UserInPersonGetAll";

            List<UserInPerson> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.UserInPerson_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<UserInPerson> GetByPersonId(int id)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "UserInPersonGetByPersonId" + id;

            List<UserInPerson> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.UserInPerson_Repository.GetByPersonId(id);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
