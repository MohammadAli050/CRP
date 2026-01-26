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
    public class FrmDsnrDetailManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "FrmDsnrDetailCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<FrmDsnrDetail> GetCacheAsList(string rawKey)
        {
            List<FrmDsnrDetail> list = (List<FrmDsnrDetail>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static FrmDsnrDetail GetCacheItem(string rawKey)
        {
            FrmDsnrDetail item = (FrmDsnrDetail)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        
        public static int Insert(FrmDsnrDetail frmDsnrDetail)
        {
            int id = RepositoryManager.FrmDsnrDetail_Repository.Insert(frmDsnrDetail);
            InvalidateCache();
            return id;
        }

        public static bool Update(FrmDsnrDetail frmDsnrDetail)
        {
            bool isExecute = RepositoryManager.FrmDsnrDetail_Repository.Update(frmDsnrDetail);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.FrmDsnrDetail_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static FrmDsnrDetail GetById(int? id)
        {
            string rawKey = "FrmDsnrDetailById" + id;
            FrmDsnrDetail frmDsnrDetail = GetCacheItem(rawKey);

            if (frmDsnrDetail == null)
            {
                frmDsnrDetail = RepositoryManager.FrmDsnrDetail_Repository.GetById(id);
                if (frmDsnrDetail != null)
                    AddCacheItem(rawKey, frmDsnrDetail);
            }

            return frmDsnrDetail;
        }

        public static List<FrmDsnrDetail> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "FrmDsnrDetailGetAll";

            List<FrmDsnrDetail> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.FrmDsnrDetail_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
