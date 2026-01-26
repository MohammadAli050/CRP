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
    public class PersonByUserTypeAndUserCodeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "PersonByUserTypeAndUserCodeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<PersonByUserTypeAndUserCode> GetCacheAsList(string rawKey)
        {
            List<PersonByUserTypeAndUserCode> list = (List<PersonByUserTypeAndUserCode>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static PersonByUserTypeAndUserCode GetCacheItem(string rawKey)
        {
            PersonByUserTypeAndUserCode item = (PersonByUserTypeAndUserCode)HttpRuntime.Cache[GetCacheKey(rawKey)];
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
        
        public static List<PersonByUserTypeAndUserCode> GetAllPersonByUserTypeAndUserCode(int userType, string userCode)
        {
            // return RepositoryAdmission.Program_Repository.GetAll(); 

            string rawKey = "PersonGetAllByUserTypeAndUserCode" + userType + userCode;

            List<PersonByUserTypeAndUserCode> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.PersonByUserTypeAndUserCode_Repository.GetAllPersonByUserTypeAndUserCode(userType, userCode);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
