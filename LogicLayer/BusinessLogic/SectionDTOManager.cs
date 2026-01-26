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
    public class SectionDTOManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "SectionEntityCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<SectionDTO> GetCacheAsList(string rawKey)
        {
            List<SectionDTO> list = (List<SectionDTO>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static SectionDTO GetCacheItem(string rawKey)
        {
            SectionDTO item = (SectionDTO)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static List<SectionDTO> GetAllBy(int acaCalId,  int programId, int courseId, int versionId)
        {

           // const string rawKey = "SectionEntityGetAllBy";

           // List<SectionDTO> list = GetCacheAsList(rawKey);

            //if (list == null)
            //{
                // Item not found in cache - retrieve it and insert it into the cache
            List<SectionDTO> list = RepositoryManager.SectionEntity_Repository.GetAllBy(acaCalId,    programId,  courseId,  versionId);
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            return list;
        }

    }
}
