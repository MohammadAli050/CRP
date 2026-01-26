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
    public class AddressManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "AddressCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Address> GetCacheAsList(string rawKey)
        {
            List<Address> list = (List<Address>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static List<AddressByRoll> GetCacheAsListAdd(string rawKey)
        {
            List<AddressByRoll> list = (List<AddressByRoll>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Address GetCacheItem(string rawKey)
        {
            Address item = (Address)HttpRuntime.Cache[GetCacheKey(rawKey)];
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
        
        public static int Insert(Address address)
        {
            int id = RepositoryManager.Address_Repository.Insert(address);
            InvalidateCache();
            return id;
        }

        public static bool Update(Address address)
        {
            bool isExecute = RepositoryManager.Address_Repository.Update(address);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Address_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Address GetById(int id)
        {            
            string rawKey = "AddressById" + id;
            Address address = GetCacheItem(rawKey);

            if (address == null)
            {   
                address = RepositoryManager.Address_Repository.GetById(id);
                if (address != null)
                    AddCacheItem(rawKey, address);
            }

            return address;
        }

        public static List<Address> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "AddressGetAll";

            List<Address> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Address_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<AddressByRoll> GetAddressByRoll(string roll)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "AddressGetByRoll";

            List<AddressByRoll> list = GetCacheAsListAdd(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Address_Repository.GetAddressByRoll(roll);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<Address> GetAddressByPersonId(int personId)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "AddressGetByPersonId" + personId;

            List<Address> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Address_Repository.GetAddressByPersonId(personId);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
