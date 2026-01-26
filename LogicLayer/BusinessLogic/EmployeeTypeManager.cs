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
    public class EmployeeTypeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "EmployeeTypeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<EmployeeType> GetCacheAsList(string rawKey)
        {
            List<EmployeeType> list = (List<EmployeeType>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static EmployeeType GetCacheItem(string rawKey)
        {
            EmployeeType item = (EmployeeType)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(EmployeeType employeetype)
        {
            int id = RepositoryManager.EmployeeType_Repository.Insert(employeetype);
            InvalidateCache();
            return id;
        }

        public static bool Update(EmployeeType employeetype)
        {
            bool isExecute = RepositoryManager.EmployeeType_Repository.Update(employeetype);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.EmployeeType_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static EmployeeType GetById(int? id)
        {
            string rawKey = "EmployeeTypeByID" + id;
            EmployeeType employeetype = GetCacheItem(rawKey);

            if (employeetype == null)
            {
                employeetype = RepositoryManager.EmployeeType_Repository.GetById(id);
                if (employeetype != null)
                    AddCacheItem(rawKey,employeetype);
            }

            return employeetype;
        }

        public static List<EmployeeType> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "EmployeeTypeGetAll";

            List<EmployeeType> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.EmployeeType_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

