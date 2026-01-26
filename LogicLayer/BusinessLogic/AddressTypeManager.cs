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
    public class AddressTypeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "AddressTypeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<AddressType> GetCacheAsList(string rawKey)
        {
            List<AddressType> list = (List<AddressType>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static AddressType GetCacheItem(string rawKey)
        {
            AddressType item = (AddressType)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(AddressType addresstype)
        {
            int id = RepositoryManager.AddressType_Repository.Insert(addresstype);
            InvalidateCache();
            return id;
        }

        public static bool Update(AddressType addresstype)
        {
            bool isExecute = RepositoryManager.AddressType_Repository.Update(addresstype);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.AddressType_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static AddressType GetById(int id)
        {
            string rawKey = "AddressTypeByID" + id;
            AddressType addresstype = GetCacheItem(rawKey);

            if (addresstype == null)
            {
                addresstype = RepositoryManager.AddressType_Repository.GetById(id);
                if (addresstype != null)
                    AddCacheItem(rawKey,addresstype);
            }

            return addresstype;
        }

        public static List<AddressType> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "AddressTypeGetAll";

            List<AddressType> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.AddressType_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

