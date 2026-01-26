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
    public class FeeSetupManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "FeeSetupCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<FeeSetup> GetCacheAsList(string rawKey)
        {
            List<FeeSetup> list = (List<FeeSetup>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static FeeSetup GetCacheItem(string rawKey)
        {
            FeeSetup item = (FeeSetup)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(FeeSetup feeSetup)
        {
            int id = RepositoryManager.FeeSetup_Repository.Insert(feeSetup);
            InvalidateCache();
            return id;
        }

        public static bool Update(FeeSetup feeSetup)
        {
            bool isExecute = RepositoryManager.FeeSetup_Repository.Update(feeSetup);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.FeeSetup_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static FeeSetup GetById(int id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "FeeSetupById" + id;
            FeeSetup feeSetup = GetCacheItem(rawKey);

            if (feeSetup == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                feeSetup = RepositoryManager.FeeSetup_Repository.GetById(id);
                if (feeSetup != null)
                    AddCacheItem(rawKey, feeSetup);
            }

            return feeSetup;
        }

        public static List<FeeSetup> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "FeeSetupGetAll";

            List<FeeSetup> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.FeeSetup_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static FeeSetup GetByTypeDefinationAndSession(int typeDefinitionID, int sessionId)
        {
            FeeSetup feeSetup = RepositoryManager.FeeSetup_Repository.GetByTypeDefinationAndSession(typeDefinitionID, sessionId);
            return feeSetup;
        }

        public static FeeSetup GetByTypeDefinationSessionProgram(int typeDefinitionID, int sessionId, int? ProgramID)
        {
            FeeSetup feeSetup = RepositoryManager.FeeSetup_Repository.GetByTypeDefinationSessionProgram(typeDefinitionID, sessionId, ProgramID);
            return feeSetup;
        }

        public static List<FeeSetup> GetByProgramSession(int programId, int batchId)
        {
            List<FeeSetup> list = RepositoryManager.FeeSetup_Repository.GetByProgramSession(programId, batchId);
            return list;
        }

        public static List<rFeeSetup> GetFeeSetup(int programId, int batchId)
        {
            List<rFeeSetup> list = RepositoryManager.FeeSetup_Repository.GetFeeSetup(programId, batchId);
            return list;
        }
    }
}
