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
    public class SetUpDateForProgramManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "SetUpDateForProgramCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<SetUpDateForProgram> GetCacheAsList(string rawKey)
        {
            List<SetUpDateForProgram> list = (List<SetUpDateForProgram>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static SetUpDateForProgram GetCacheItem(string rawKey)
        {
            SetUpDateForProgram item = (SetUpDateForProgram)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(SetUpDateForProgram setupdateforprogram)
        {
            int id = RepositoryManager.SetUpDateForProgram_Repository.Insert(setupdateforprogram);
            InvalidateCache();
            return id;
        }

        public static bool Update(SetUpDateForProgram setupdateforprogram)
        {
            bool isExecute = RepositoryManager.SetUpDateForProgram_Repository.Update(setupdateforprogram);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.SetUpDateForProgram_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static SetUpDateForProgram GetById(int id)
        {
            string rawKey = "SetUpDateForProgramByID" + id;
            SetUpDateForProgram setupdateforprogram = GetCacheItem(rawKey);

            if (setupdateforprogram == null)
            {
                setupdateforprogram = RepositoryManager.SetUpDateForProgram_Repository.GetById(id);
                if (setupdateforprogram != null)
                    AddCacheItem(rawKey,setupdateforprogram);
            }

            return setupdateforprogram;
        }

        public static List<SetUpDateForProgram> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "SetUpDateForProgramGetAll";

            List<SetUpDateForProgram> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.SetUpDateForProgram_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<SetUpDateForProgram> GetAll(int acaCalId, int programId, int typeId)
        {
            return RepositoryManager.SetUpDateForProgram_Repository.GetAll(acaCalId, programId, typeId);
        }

        public static SetUpDateForProgram GetByActiveByProgram(int programId)
        {
            return RepositoryManager.SetUpDateForProgram_Repository.GetActiveByProgram(programId);
        }
    }
}

