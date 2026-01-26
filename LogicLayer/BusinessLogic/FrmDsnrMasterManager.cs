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
    public class FrmDsnrMasterManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "FrmDsnrMasterCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<FrmDsnrMaster> GetCacheAsList(string rawKey)
        {
            List<FrmDsnrMaster> list = (List<FrmDsnrMaster>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static FrmDsnrMaster GetCacheItem(string rawKey)
        {
            FrmDsnrMaster item = (FrmDsnrMaster)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(FrmDsnrMaster frmDsnrMaster)
        {
            int id = RepositoryManager.FrmDsnrMaster_Repository.Insert(frmDsnrMaster);
            InvalidateCache();
            return id;
        }

        public static bool Update(FrmDsnrMaster frmDsnrMaster)
        {
            bool isExecute = RepositoryManager.FrmDsnrMaster_Repository.Update(frmDsnrMaster);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.FrmDsnrMaster_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static FrmDsnrMaster GetById(int? id)
        {
            string rawKey = "FrmDsnrMasterById" + id;
            FrmDsnrMaster frmDsnrMaster = GetCacheItem(rawKey);

            if (frmDsnrMaster == null)
            {
                frmDsnrMaster = RepositoryManager.FrmDsnrMaster_Repository.GetById(id);
                if (frmDsnrMaster != null)
                    AddCacheItem(rawKey, frmDsnrMaster);
            }

            return frmDsnrMaster;
        }

        public static List<FrmDsnrMaster> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "FrmDsnrMasterGetAll";

            List<FrmDsnrMaster> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.FrmDsnrMaster_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
