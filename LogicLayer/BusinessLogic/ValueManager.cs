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
    public class ValueManager
    {

        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ValueCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Value> GetCacheAsList(string rawKey)
        {
            List<Value> list = (List<Value>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Value GetCacheItem(string rawKey)
        {
            Value item = (Value)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(Value value)
        {
            int id = RepositoryManager.Value_Repository.Insert(value);
            InvalidateCache();
            return id;
        }

        public static bool Update(Value value)
        {
            bool isExecute = RepositoryManager.Value_Repository.Update(value);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Value_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Value GetById(int id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "ValueById" + id;
            Value value = GetCacheItem(rawKey);

            if (value == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                value = RepositoryManager.Value_Repository.GetById(id);
                if (value != null)
                    AddCacheItem(rawKey, value);
            }

            return value;
        }

        public static List<Value> GetByValueSetId(int valueSet)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "ValueByValueSetId" + valueSet;
            List<Value> valueList = GetCacheAsList(rawKey);

            if (valueList == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                valueList = RepositoryManager.Value_Repository.GetByValueSetId(valueSet);
                if (valueList != null)
                    AddCacheItem(rawKey, valueList);
            }

            return valueList;
        }

        public static List<Value> GetAll()
        {


            const string rawKey = "ValueGetAll";

            List<Value> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Value_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

