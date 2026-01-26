using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class TreeCalendarMasterManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "TreeCalendarMasterCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<TreeCalendarMaster> GetCacheAsList(string rawKey)
        {
            List<TreeCalendarMaster> list = (List<TreeCalendarMaster>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static TreeCalendarMaster GetCacheItem(string rawKey)
        {
            TreeCalendarMaster item = (TreeCalendarMaster)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(TreeCalendarMaster obj)
        {
            int id = RepositoryManager.TreeCalendarMaster_Repository.Insert(obj);
            InvalidateCache();
            return id;
        }

        public static bool Update(TreeCalendarMaster obj)
        {
            bool isExecute = RepositoryManager.TreeCalendarMaster_Repository.Update(obj);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.TreeCalendarMaster_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static TreeCalendarMaster GetById(int? id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "TreeCalendarMasterID" + id;
            TreeCalendarMaster obj = GetCacheItem(rawKey);

            if (obj == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                obj = RepositoryManager.TreeCalendarMaster_Repository.GetById(id);
                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static List<TreeCalendarMaster> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "TreeCalendarMasterGetAll";

            List<TreeCalendarMaster> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.TreeCalendarMaster_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<TreeCalendarMaster> GetAllByTreeMasterID(int treeMasterID)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "TreeCalendarMasterGetAll" + treeMasterID;

            List<TreeCalendarMaster> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.TreeCalendarMaster_Repository.GetAllByTreeMasterID(treeMasterID);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static TreeCalendarMaster GetByTreeCalenderNameTreeMasterId(string treeCalenderName, int treeMasterId)
        {
            TreeCalendarMaster treeCalendarMaster = RepositoryManager.TreeCalendarMaster_Repository.GetByTreeCalenderNameTreeMasterId(treeCalenderName, treeMasterId);
            return treeCalendarMaster;
        }
    }
}
