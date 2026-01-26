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
    public class GradeWiseRetakeDiscountManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "GradeWiseRetakeDiscountCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<GradeWiseRetakeDiscount> GetCacheAsList(string rawKey)
        {
            List<GradeWiseRetakeDiscount> list = (List<GradeWiseRetakeDiscount>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static GradeWiseRetakeDiscount GetCacheItem(string rawKey)
        {
            GradeWiseRetakeDiscount item = (GradeWiseRetakeDiscount)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(GradeWiseRetakeDiscount gradewiseretakediscount)
        {
            int id = RepositoryManager.GradeWiseRetakeDiscount_Repository.Insert(gradewiseretakediscount);
            InvalidateCache();
            return id;
        }

        public static bool Update(GradeWiseRetakeDiscount gradewiseretakediscount)
        {
            bool isExecute = RepositoryManager.GradeWiseRetakeDiscount_Repository.Update(gradewiseretakediscount);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.GradeWiseRetakeDiscount_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static GradeWiseRetakeDiscount GetById(int id)
        {
            string rawKey = "GradeWiseRetakeDiscountByID" + id;
            GradeWiseRetakeDiscount gradewiseretakediscount = GetCacheItem(rawKey);

            if (gradewiseretakediscount == null)
            {
                gradewiseretakediscount = RepositoryManager.GradeWiseRetakeDiscount_Repository.GetById(id);
                if (gradewiseretakediscount != null)
                    AddCacheItem(rawKey,gradewiseretakediscount);
            }

            return gradewiseretakediscount;
        }

        public static List<GradeWiseRetakeDiscount> GetAll()
        {
            const string rawKey = "GradeWiseRetakeDiscountGetAll";

            List<GradeWiseRetakeDiscount> list = GetCacheAsList(rawKey);

            if (list == null)
            {  
                list = RepositoryManager.GradeWiseRetakeDiscount_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<GradeWiseRetakeDiscount> GetAllBy(int? programId, int sessionId)
        {
            List<GradeWiseRetakeDiscount> list = RepositoryManager.GradeWiseRetakeDiscount_Repository.GetAllBy(programId,  sessionId);
            return list;
        }
    }
}

