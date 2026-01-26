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
    public class VoucherManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "VoucherCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Voucher> GetCacheAsList(string rawKey)
        {
            List<Voucher> list = (List<Voucher>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Voucher GetCacheItem(string rawKey)
        {
            Voucher item = (Voucher)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(Voucher voucher)
        {
            int id = RepositoryManager.Voucher_Repository.Insert(voucher);
            InvalidateCache();
            return id;
        }

        public static bool Update(Voucher voucher)
        {
            bool isExecute = RepositoryManager.Voucher_Repository.Update(voucher);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Voucher_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Voucher GetById(int id)
        {
            string rawKey = "VoucherByID" + id;
            Voucher voucher = GetCacheItem(rawKey);

            if (voucher == null)
            {
                voucher = RepositoryManager.Voucher_Repository.GetById(id);
                if (voucher != null)
                    AddCacheItem(rawKey,voucher);
            }

            return voucher;
        }

        public static List<Voucher> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "VoucherGetAll";

            List<Voucher> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Voucher_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        internal static int Insert(List<Voucher> voucherList)
        {
            int count = RepositoryManager.Voucher_Repository.Insert(voucherList);
            InvalidateCache();
            return count;
        }
    }
}

