using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicLayer.BusinessLogic
{
    public class CriteriaTypeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "CriteriaTypeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<CriteriaType> GetCacheAsList(string rawKey)
        {
            List<CriteriaType> list = (List<CriteriaType>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static CriteriaType GetCacheItem(string rawKey)
        {
            CriteriaType item = (CriteriaType)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(CriteriaType criteriatype)
        {
            int id = RepositoryManager.CriteriaType_Repository.Insert(criteriatype);
            InvalidateCache();
            return id;
        }

        public static bool Update(CriteriaType criteriatype)
        {
            bool isExecute = RepositoryManager.CriteriaType_Repository.Update(criteriatype);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.CriteriaType_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static CriteriaType GetById(int? id)
        {
            string rawKey = "CriteriaTypeByID" + id;
            CriteriaType criteriatype = GetCacheItem(rawKey);

            if (criteriatype == null)
            {
                criteriatype = RepositoryManager.CriteriaType_Repository.GetById(id);
                if (criteriatype != null)
                    AddCacheItem(rawKey, criteriatype);
            }

            return criteriatype;
        }

        public static List<CriteriaType> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "CriteriaTypeGetAll";

            List<CriteriaType> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.CriteriaType_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
