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
    public class ProgramTypeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ProgramTypeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ProgramType> GetCacheAsList(string rawKey)
        {
            List<ProgramType> list = (List<ProgramType>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ProgramType GetCacheItem(string rawKey)
        {
            ProgramType item = (ProgramType)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(ProgramType obj)
        {
            int id = RepositoryManager.ProgramType_Repository.Insert(obj);
            InvalidateCache();
            return id;
        }

        public static bool Update(ProgramType obj)
        {
            bool isExecute = RepositoryManager.ProgramType_Repository.Update(obj);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ProgramType_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ProgramType GetById(int? id)
        {
            // return RepositoryAdmission.ProgramType_Repository.GetById(id);

            string rawKey = "ProgramTypeById" + id;
            ProgramType obj = GetCacheItem(rawKey);

            if (obj == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                obj = RepositoryManager.ProgramType_Repository.GetById(id);
                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static List<ProgramType> GetAll()
        {
            // return RepositoryAdmission.ProgramType_Repository.GetAll();

            const string rawKey = "ProgramTypeGetAll";

            List<ProgramType> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ProgramType_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
