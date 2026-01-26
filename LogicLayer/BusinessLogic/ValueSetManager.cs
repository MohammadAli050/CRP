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
    public class ValueSetManager
    {

        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ValueSetCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ValueSet> GetCacheAsList(string rawKey)
        {
            List<ValueSet> list = (List<ValueSet>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ValueSet GetCacheItem(string rawKey)
        {
            ValueSet item = (ValueSet)HttpRuntime.Cache[GetCacheKey(rawKey)];
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
        
        
        public static int Insert(ValueSet valueset)
        {
            int id = RepositoryManager.ValueSet_Repository.Insert(valueset);
            InvalidateCache();
            return id;
        }

        public static bool Update(ValueSet valueset)
        {
            bool isExecute = RepositoryManager.ValueSet_Repository.Update(valueset);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ValueSet_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ValueSet GetById(int? id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "ValueSetById" + id;
            ValueSet valueset = GetCacheItem(rawKey);

            if (valueset == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                valueset = RepositoryManager.ValueSet_Repository.GetById(id);
                if (valueset != null)
                    AddCacheItem(rawKey, valueset);
            }

            return valueset;
        }

        public static List<ValueSet> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ValueSetGetAll";

            List<ValueSet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ValueSet_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
