using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicLayer.BusinessLogic
{
    public class PreregistrationCountDTOManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "PreregistrationCountDTOCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<PreregistrationCountDTO> GetCacheAsList(string rawKey)
        {
            List<PreregistrationCountDTO> list = (List<PreregistrationCountDTO>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static PreregistrationCountDTO GetCacheItem(string rawKey)
        {
            PreregistrationCountDTO item = (PreregistrationCountDTO)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static List<PreregistrationCountDTO> GetAllByProgAcaCal(int ProgramID, int AcademicCalenderID)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "PreregistrationCountDTOGetAllByProgAcaCal" + ProgramID;

            List<PreregistrationCountDTO> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.PreregistrationCountDTO_Repository.GetAllByProgAcaCal(ProgramID, AcademicCalenderID);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
