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
    public class ExamMarkReplicaManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamMarkReplicaCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamMarkReplica> GetCacheAsList(string rawKey)
        {
            List<ExamMarkReplica> list = (List<ExamMarkReplica>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamMarkReplica GetCacheItem(string rawKey)
        {
            ExamMarkReplica item = (ExamMarkReplica)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static ExamMarkReplica GetById(int id)
        {
            string rawKey = "ExamMarkReplicaByID" + id;
            ExamMarkReplica exammarkreplica = GetCacheItem(rawKey);

            if (exammarkreplica == null)
            {
                exammarkreplica = RepositoryManager.ExamMarkReplica_Repository.GetById(id);
                if (exammarkreplica != null)
                    AddCacheItem(rawKey,exammarkreplica);
            }

            return exammarkreplica;
        }

        public static List<ExamMarkReplica> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamMarkReplicaGetAll";

            List<ExamMarkReplica> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamMarkReplica_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static int InsertByAcaCalAcaCalSecRoll(int acaCalId, int acaCalSecId, string roll)
        {
            return RepositoryManager.ExamMarkReplica_Repository.InsertByAcaCalAcaCalSecRoll(acaCalId, acaCalSecId, roll);
        }
    }
}

