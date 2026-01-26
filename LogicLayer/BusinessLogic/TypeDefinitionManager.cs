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
    public class TypeDefinitionManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "TypeDefinitionCache" };
        const double CacheDuration = 2.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<TypeDefinition> GetCacheAsList(string rawKey)
        {
            List<TypeDefinition> list = (List<TypeDefinition>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static TypeDefinition GetCacheItem(string rawKey)
        {
            TypeDefinition item = (TypeDefinition)HttpRuntime.Cache[GetCacheKey(rawKey)];
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
            DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddSeconds(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        }



        public static void InvalidateCache()
        {
            // Remove the cache dependency
            HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        }

        #endregion

        
        public static int Insert(TypeDefinition typeDefinition)
        {
            int id = RepositoryManager.TypeDefinition_Repository.Insert(typeDefinition);
            InvalidateCache();
            return id;
        }

        public static bool Update(TypeDefinition typeDefinition)
        {
            bool isExecute = RepositoryManager.TypeDefinition_Repository.Update(typeDefinition);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.TypeDefinition_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static TypeDefinition GetById(int id)
        {
            string rawKey = "TypeDefinitionById" + id;
            TypeDefinition typeDefinition = GetCacheItem(rawKey);

            if (typeDefinition == null)
            {
                typeDefinition = RepositoryManager.TypeDefinition_Repository.GetById(id);
                if (typeDefinition != null)
                    AddCacheItem(rawKey, typeDefinition);
            }

            return typeDefinition;
        }

        public static List<TypeDefinition> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "TypeDefinitionGetAll";

            List<TypeDefinition> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.TypeDefinition_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<TypeDefinition> GetAll(string type)
        {
            string rawKey = "EvaluationFormGetAll" + type;

            List<TypeDefinition> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.TypeDefinition_Repository.GetAll(type);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

       
    }
}
