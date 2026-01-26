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
    public class ProbationManager
    {
        #region Cache
        public static readonly string[] MasterCacheKeyArray = { "ProbationCache" };
        const double CacheDuration = 1.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<rProbationStudent> GetCacheAsList(string rawKey)
        {
            List<rProbationStudent> list = (List<rProbationStudent>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static rProbationStudent GetCacheItem(string rawKey)
        {
            rProbationStudent item = (rProbationStudent)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static List<rProbationStudent> GetAllByProgramOrder(int progamId, string orderType)
        {
            return RepositoryManager.Probation_Repository.GetAllByProgramOrder(progamId, orderType);

            //string rawKey = "ProbationGetAllByProgramOrder" + progamId + orderType;

            //List<rProbationStudent> list = GetCacheAsList(rawKey);

            //if (list == null)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    list = RepositoryManager.Probation_Repository.GetAllByProgramOrder(progamId, orderType);
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            //return list;
        }

        public static List<rProbationStudent> GetAll(int FromAcaCalId, int ToAcaCalId, decimal FromRange, decimal ToRange, int ProgamId, int FromSemester, int ToSemester)
        {
            return RepositoryManager.Probation_Repository.GetAll(FromAcaCalId,ToAcaCalId,FromRange,ToRange,ProgamId,FromSemester,ToSemester);

            //string rawKey = "ProbationGetAllByAcaIdProgId" + FromAcaCalId + ToAcaCalId + FromRange + ToRange + ProgamId+FromSemester+ToSemester;

            //List<rProbationStudent> list = GetCacheAsList(rawKey);

            //if (list == null)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    list = RepositoryManager.Probation_Repository.GetAll(FromAcaCalId,ToAcaCalId,FromRange,ToRange,ProgamId,FromSemester,ToSemester);
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            //return list;
        }
    }
}
