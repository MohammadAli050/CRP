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
    public class BatchManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "BatchCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Batch> GetCacheAsList(string rawKey)
        {
            List<Batch> list = (List<Batch>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Batch GetCacheItem(string rawKey)
        {
            Batch item = (Batch)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(Batch batch)
        {
            int id = RepositoryManager.Batch_Repository.Insert(batch);
            InvalidateCache();
            return id;
        }

        public static bool Update(Batch batch)
        {
            bool isExecute = RepositoryManager.Batch_Repository.Update(batch);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Batch_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Batch GetById(int id)
        {
            Batch batch = null;

            if (batch == null)
            {
                batch = RepositoryManager.Batch_Repository.GetById(id);
            }

            return batch;
        }

        public static List<Batch> GetAll()
        {

            List<Batch> list = null;

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Batch_Repository.GetAll();
            }

            return list;
        }

        internal static List<Batch> GetByAcaCalSectionID(int AcaCalSectionID)
        {
            List<Batch> list = RepositoryManager.Batch_Repository.GetByAcaCalSectionID(AcaCalSectionID);
            return list;
        }

        public static List<Batch> GetAllByProgram(int programId)
        {
            List<Batch> list = RepositoryManager.Batch_Repository.GetAllByProgram(programId);
            return list;
        }

        public static List<rBatchListByProgram> GetBatchListByProgram(int programId)
        {
            List<rBatchListByProgram> list = RepositoryManager.Batch_Repository.GetBatchListByProgram(programId);
            return list;
        }
    }
}

