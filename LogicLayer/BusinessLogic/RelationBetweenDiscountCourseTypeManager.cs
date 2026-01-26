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
    public class RelationBetweenDiscountCourseTypeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "RelationBetweenDiscountCourseTypeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<RelationBetweenDiscountCourseType> GetCacheAsList(string rawKey)
        {
            List<RelationBetweenDiscountCourseType> list = (List<RelationBetweenDiscountCourseType>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static RelationBetweenDiscountCourseType GetCacheItem(string rawKey)
        {
            RelationBetweenDiscountCourseType item = (RelationBetweenDiscountCourseType)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(RelationBetweenDiscountCourseType relationBetweenDiscountCourseType)
        {
            int id = RepositoryManager.RelationBetweenDiscountCourseType_Repository.Insert(relationBetweenDiscountCourseType);
            InvalidateCache();
            return id;
        }

        public static bool Update(RelationBetweenDiscountCourseType relationBetweenDiscountCourseType)
        {
            bool isExecute = RepositoryManager.RelationBetweenDiscountCourseType_Repository.Update(relationBetweenDiscountCourseType);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.RelationBetweenDiscountCourseType_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static RelationBetweenDiscountCourseType GetById(int? id)
        {
            string rawKey = "RelationBetweenDiscountCourseTypeById" + id;
            RelationBetweenDiscountCourseType relationBetweenDiscountCourseType = GetCacheItem(rawKey);

            if (relationBetweenDiscountCourseType == null)
            {
                relationBetweenDiscountCourseType = RepositoryManager.RelationBetweenDiscountCourseType_Repository.GetById(id);
                if (relationBetweenDiscountCourseType != null)
                    AddCacheItem(rawKey, relationBetweenDiscountCourseType);
            }

            return relationBetweenDiscountCourseType;
        }
        
        public static List<RelationBetweenDiscountCourseType> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "RelationBetweenDiscountCourseType_RepositoryGetAll";

            List<RelationBetweenDiscountCourseType> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RelationBetweenDiscountCourseType_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
