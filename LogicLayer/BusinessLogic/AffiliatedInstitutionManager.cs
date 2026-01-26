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
    public class AffiliatedInstitutionManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "AffiliatedInstitutionCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<AffiliatedInstitution> GetCacheAsList(string rawKey)
        {
            List<AffiliatedInstitution> list = (List<AffiliatedInstitution>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static AffiliatedInstitution GetCacheItem(string rawKey)
        {
            AffiliatedInstitution item = (AffiliatedInstitution)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(AffiliatedInstitution affiliatedinstitution)
        {
            int id = RepositoryManager.AffiliatedInstitution_Repository.Insert(affiliatedinstitution);
            InvalidateCache();
            return id;
        }

        public static bool Update(AffiliatedInstitution affiliatedinstitution)
        {
            bool isExecute = RepositoryManager.AffiliatedInstitution_Repository.Update(affiliatedinstitution);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.AffiliatedInstitution_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static AffiliatedInstitution GetById(int? id)
        {
            string rawKey = "AffiliatedInstitutionByID" + id;
            AffiliatedInstitution affiliatedinstitution = GetCacheItem(rawKey);

            if (affiliatedinstitution == null)
            {
                affiliatedinstitution = RepositoryManager.AffiliatedInstitution_Repository.GetById(id);
                if (affiliatedinstitution != null)
                    AddCacheItem(rawKey,affiliatedinstitution);
            }

            return affiliatedinstitution;
        }

        public static List<AffiliatedInstitution> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "AffiliatedInstitutionGetAll";

            List<AffiliatedInstitution> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.AffiliatedInstitution_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}

