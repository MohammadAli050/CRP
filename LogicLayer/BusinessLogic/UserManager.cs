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
    public class UserManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "UserCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<User> GetCacheAsList(string rawKey)
        {
            List<User> list = (List<User>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static User GetCacheItem(string rawKey)
        {
            User item = (User)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(User user)
        {
            int id = RepositoryManager.User_Repository.Insert(user);
            InvalidateCache();
            return id;
        }

        public static bool Update(User user)
        {
            bool isExecute = RepositoryManager.User_Repository.Update(user);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.User_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static User GetById(int? id)
        {
            string rawKey = "UserById" + id;
            User user = GetCacheItem(rawKey);

            if (user == null)
            {
                user = RepositoryManager.User_Repository.GetById(id);
                if (user != null)
                    AddCacheItem(rawKey, user);
            }

            return user;
        }

        public static User GetByLogInId(string LogInID)
        {
            //string rawKey = "UserByLogInId" + LogInID;

            //User user = GetCacheItem(rawKey);

            //if (user == null)
            //{
            //    user = RepositoryManager.User_Repository.GetByLogInId(LogInID);
            //    if (user != null)
            //        AddCacheItem(rawKey, user);
            //}
            User user = RepositoryManager.User_Repository.GetByLogInId(LogInID);
            return user;
        }

        public static User GetAdminUserByLogInId(string LogInID)
        {
            User user = RepositoryManager.User_Repository.GetAdminUserByLogInId(LogInID);
            return user;
        }

        public static List<User> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "UserGetAll";

            List<User> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.User_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        internal static List<User> GetByPersonId(int PersonID)
        {
            //string rawKey = "UserGetByPersonId" + PersonID;

            //List<User> list = GetCacheAsList(rawKey);

            //if (list == null)
            //{ 
            //    list = RepositoryManager.User_Repository.GetByPersonId(PersonID);
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}
            List<User> list = RepositoryManager.User_Repository.GetByPersonId(PersonID);
            return list;
        }

        public static List<User> GetByUserId(string userlogInID)
        {
            string rawKey = "UserByUserId" + userlogInID;

            List<User> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.User_Repository.GetByUserId(userlogInID);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static int GenerateUserByProgram(int programId, int batchId)
        {
            return RepositoryManager.User_Repository.GenerateUserByProgramIdBatchId(programId, batchId);
        }

        public static List<User> GetAllByProgramId(int programId, int batchId)
        {
            //string rawKey = "UserByAllProgramId" + programId;

            //List<User> list = GetCacheAsList(rawKey);

            //if (list == null)
            //{
            //    list = RepositoryManager.User_Repository.GetAllByProgramIdBatchId(programId, batchId);
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            //return list;

            return RepositoryManager.User_Repository.GetAllByProgramIdBatchId(programId, batchId);
        }


        public static bool ValidateUser(string code)
        {
            bool isValidate = RepositoryManager.Employee_Repository.ValidateEmployee(code);
            return isValidate;
        }

    }
}
