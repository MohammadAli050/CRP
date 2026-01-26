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
   public class OperatorManager
    {
        #region Cache

       public static readonly string[] MasterCacheKeyArray = { "OperatorCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Operator> GetCacheAsList(string rawKey)
        {
            List<Operator> list = (List<Operator>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Operator GetCacheItem(string rawKey)
        {
            Operator item = (Operator)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(Operator objOperator)
        {
            int id = RepositoryManager.Operator_Repository.Insert(objOperator);
            InvalidateCache();
            return id;
        }

        public static bool Update(Operator objOperator)
        {
            bool isExecute = RepositoryManager.Operator_Repository.Update(objOperator);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Operator_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Operator GetById(int? id)
        {
            string rawKey = "OperatorID" + id;
            Operator objOperator = GetCacheItem(rawKey);

            if (objOperator == null)
            {
                objOperator = RepositoryManager.Operator_Repository.GetById(id);
                if (objOperator != null)
                    AddCacheItem(rawKey, objOperator);
            }

            return objOperator;
        }

        public static List<Operator> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "OperatorID";

            List<Operator> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Operator_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
