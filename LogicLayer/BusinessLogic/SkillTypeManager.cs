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
   public class SkillTypeManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "SkillTypeCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<SkillType> GetCacheAsList(string rawKey)
        {
            List<SkillType> list = (List<SkillType>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static SkillType GetCacheItem(string rawKey)
        {
            SkillType item = (SkillType)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(SkillType skillType)
        {
            int id = RepositoryManager.SkillType_Repository.Insert(skillType);
            InvalidateCache();
            return id;
        }

        public static bool Update(SkillType skillType)
        {
            bool isExecute = RepositoryManager.SkillType_Repository.Update(skillType);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.SkillType_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static SkillType GetById(int? id)
        {
            string rawKey = "SkillTypeID" + id;
            SkillType skillType = GetCacheItem(rawKey);

            if (skillType == null)
            {
                skillType = RepositoryManager.SkillType_Repository.GetById(id);
                if (skillType != null)
                    AddCacheItem(rawKey, skillType);
            }

            return skillType;
        }

        public static List<SkillType> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "SkillTypeGetAll";

            List<SkillType> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.SkillType_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
