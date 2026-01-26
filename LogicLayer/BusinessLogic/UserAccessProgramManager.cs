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
    public class UserAccessProgramManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "UserAccessProgramCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<UserAccessProgram> GetCacheAsList(string rawKey)
        {
            List<UserAccessProgram> list = (List<UserAccessProgram>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static UserAccessProgram GetCacheItem(string rawKey)
        {
            UserAccessProgram item = (UserAccessProgram)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(UserAccessProgram useraccessprogram)
        {
            int id = RepositoryManager.UserAccessProgram_Repository.Insert(useraccessprogram);
            InvalidateCache();
            return id;
        }

        public static bool Update(UserAccessProgram useraccessprogram)
        {
            bool isExecute = RepositoryManager.UserAccessProgram_Repository.Update(useraccessprogram);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.UserAccessProgram_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static UserAccessProgram GetById(int? id)
        {
            string rawKey = "UserAccessProgramByID" + id;
            UserAccessProgram useraccessprogram = GetCacheItem(rawKey);

            if (useraccessprogram == null)
            {
                useraccessprogram = RepositoryManager.UserAccessProgram_Repository.GetById(id);
                if (useraccessprogram != null)
                    AddCacheItem(rawKey,useraccessprogram);
            }

            return useraccessprogram;
        }

        public static UserAccessProgram GetByUserId(int? id)
        {
            string rawKey = "UserAccessProgramByUserID" + id;
            UserAccessProgram useraccessprogram = GetCacheItem(rawKey);

            if (useraccessprogram == null)
            {
                useraccessprogram = RepositoryManager.UserAccessProgram_Repository.GetByUserId(id);
                if (useraccessprogram != null)
                    AddCacheItem(rawKey, useraccessprogram);
            }

            return useraccessprogram;
        }

        public static List<UserAccessProgram> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "UserAccessProgramGetAll";

            List<UserAccessProgram> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.UserAccessProgram_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

