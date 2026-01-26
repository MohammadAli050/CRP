using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.BusinessLogic
{
    public class RegistrationInSemisterYearManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "RegistrationInSemisterYearManagerCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<rRegistrationInSemesterYear> GetCacheAsList(string rawKey)
        {
            List<rRegistrationInSemesterYear> list = (List<rRegistrationInSemesterYear>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
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

        public static List<rRegistrationInSemesterYear> GetAllBySemesterYear(int year, int trimesterAcaCalId, int semesterAcaCalId)
        {
          

            List<rRegistrationInSemesterYear> list =  RepositoryManager.RegistrationInSemesterYear_Repository.GetAllBySemesterYear(year, trimesterAcaCalId, semesterAcaCalId);
               
            return list;
        }

       
    }
}

