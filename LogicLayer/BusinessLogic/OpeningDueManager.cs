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
    public class OpeningDueManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "OpeningDueCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<OpeningDue> GetCacheAsList(string rawKey)
        {
            List<OpeningDue> list = (List<OpeningDue>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static OpeningDue GetCacheItem(string rawKey)
        {
            OpeningDue item = (OpeningDue)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(OpeningDue openingdue)
        {
            int id = RepositoryManager.OpeningDue_Repository.Insert(openingdue);
            InvalidateCache();
            return id;
        }

        public static bool Update(OpeningDue openingdue)
        {
            bool isExecute = RepositoryManager.OpeningDue_Repository.Update(openingdue);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.OpeningDue_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static OpeningDue GetById(int? id)
        {
            string rawKey = "OpeningDueByID" + id;
            OpeningDue openingdue = GetCacheItem(rawKey);

            if (openingdue == null)
            {
                openingdue = RepositoryManager.OpeningDue_Repository.GetById(id);
                if (openingdue != null)
                    AddCacheItem(rawKey,openingdue);
            }

            return openingdue;
        }

        public static List<OpeningDue> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "OpeningDueGetAll";

            List<OpeningDue> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.OpeningDue_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        
        public static OpeningDue GetByStudentId(int studentID)
        {
            OpeningDue openingdue = RepositoryManager.OpeningDue_Repository.GetByStudentId(studentID);
            return openingdue;
        }
    }
}

