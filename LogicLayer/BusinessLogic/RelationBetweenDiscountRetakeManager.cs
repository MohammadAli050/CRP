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
    public class RelationBetweenDiscountRetakeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "RelationBetweenDiscountRetakeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<RelationBetweenDiscountRetake> GetCacheAsList(string rawKey)
        {
            List<RelationBetweenDiscountRetake> list = (List<RelationBetweenDiscountRetake>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static RelationBetweenDiscountRetake GetCacheItem(string rawKey)
        {
            RelationBetweenDiscountRetake item = (RelationBetweenDiscountRetake)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(RelationBetweenDiscountRetake relationBetweenDiscountRetake)
        {
            int id = RepositoryManager.RelationBetweenDiscountRetake_Repository.Insert(relationBetweenDiscountRetake);
            InvalidateCache();
            return id;
        }

        public static bool Update(RelationBetweenDiscountRetake relationBetweenDiscountRetake)
        {
            bool isExecute = RepositoryManager.RelationBetweenDiscountRetake_Repository.Update(relationBetweenDiscountRetake);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.RelationBetweenDiscountRetake_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static RelationBetweenDiscountRetake GetById(int? id)
        {
            string rawKey = "RelationBetweenDiscountRetakeById" + id;
            RelationBetweenDiscountRetake relationBetweenDiscountRetake = GetCacheItem(rawKey);

            if (relationBetweenDiscountRetake == null)
            {
                relationBetweenDiscountRetake = RepositoryManager.RelationBetweenDiscountRetake_Repository.GetById(id);
                if (relationBetweenDiscountRetake != null)
                    AddCacheItem(rawKey, relationBetweenDiscountRetake);
            }

            return relationBetweenDiscountRetake;
        }

        public static List<RelationBetweenDiscountRetake> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "RelationBetweenDiscountRetakeGetAll";

            List<RelationBetweenDiscountRetake> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RelationBetweenDiscountRetake_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

    }
}
