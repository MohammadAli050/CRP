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
    public class TreeCalendarDetailManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "TreeCalendarDetailCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<TreeCalendarDetail> GetCacheAsList(string rawKey)
        {
            List<TreeCalendarDetail> list = (List<TreeCalendarDetail>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static TreeCalendarDetail GetCacheItem(string rawKey)
        {
            TreeCalendarDetail item = (TreeCalendarDetail)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(TreeCalendarDetail treeCalendarDetail)
        {
            int id = RepositoryManager.TreeCalendarDetail_Repository.Insert(treeCalendarDetail);
            InvalidateCache();
            return id;
        }

        public static bool Update(TreeCalendarDetail treeCalendarDetail)
        {
            bool isExecute = RepositoryManager.TreeCalendarDetail_Repository.Update(treeCalendarDetail);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.TreeCalendarDetail_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static TreeCalendarDetail GetById(int? id)
        {
            string rawKey = "TreeCalendarDetailById" + id;
            TreeCalendarDetail treeCalendarDetail = GetCacheItem(rawKey);

            if (treeCalendarDetail == null)
            {
                treeCalendarDetail = RepositoryManager.TreeCalendarDetail_Repository.GetById(id);
                if (treeCalendarDetail != null)
                    AddCacheItem(rawKey, treeCalendarDetail);
            }

            return treeCalendarDetail;
        }

        public static List<TreeCalendarDetail> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "TreeCalendarDetailGetAll";

            List<TreeCalendarDetail> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.TreeCalendarDetail_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<TreeCalendarDetail> GetByTreeCalenderMasterId(int treeCalenderMasterId)
        {
            List<TreeCalendarDetail> list = RepositoryManager.TreeCalendarDetail_Repository.GetByTreeCalenderMasterId(treeCalenderMasterId);
            return list;
        }
    }
}
