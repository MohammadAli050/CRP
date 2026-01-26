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
    public class TreeMasterManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "TreeMasterCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<TreeMaster> GetCacheAsList(string rawKey)
        {
            List<TreeMaster> list = (List<TreeMaster>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static TreeMaster GetCacheItem(string rawKey)
        {
            TreeMaster item = (TreeMaster)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return item;
        }

        public static void AddCacheItem(string rawKey, object value)
        {
            System.Web.Caching.Cache DataCache = HttpRuntime.Cache;

            if (DataCache[MasterCacheKeyArray[0]] == null)
                DataCache[MasterCacheKeyArray[0]] = DateTime.Now;
           
            System.Web.Caching.CacheDependency dependency = new System.Web.Caching.CacheDependency(null, MasterCacheKeyArray);
            DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddMinutes(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        }



        public static void InvalidateCache()
        {           
            HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        }

        #endregion

        public static int Insert(TreeMaster obj)
        {
            int id = RepositoryManager.TreeMaster_Repository.Insert(obj);
            InvalidateCache();
            return id;
        }

        public static bool Update(TreeMaster obj)
        {
            bool isExecute = RepositoryManager.TreeMaster_Repository.Update(obj);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.TreeMaster_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static TreeMaster GetById(int? id)
        {

            string rawKey = "TreeMasterById" + id;
            TreeMaster obj = GetCacheItem(rawKey);

            if (obj == null)
            {               
                obj = RepositoryManager.TreeMaster_Repository.GetById(id);
                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static List<TreeMaster> GetAll()
        {
            List<TreeMaster> list = null;

            if (list == null)
            {               
                list = RepositoryManager.TreeMaster_Repository.GetAll();

            }

            return list;
        }

        public static List<TreeMaster> GetAllProgramID(int programID)
        {           
            string rawKey = "TreeMasterGetAllByProgramID" + programID;

            List<TreeMaster> list = GetCacheAsList(rawKey);

            if (list == null)
            {               
                list = RepositoryManager.TreeMaster_Repository.GetAllByProgramID(programID);
                if (list != null && list.Count > 0)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
