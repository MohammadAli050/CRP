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
    public class DeptRegSetUpManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "DeptRegSetUpCache" };
        const double CacheDuration = 15.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<DeptRegSetUp> GetCacheAsList(string rawKey)
        {
            List<DeptRegSetUp> list = (List<DeptRegSetUp>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static DeptRegSetUp GetCacheItem(string rawKey)
        {
            DeptRegSetUp item = (DeptRegSetUp)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(DeptRegSetUp deptRegSetUp)
        {
            int id = RepositoryManager.DeptRegSetUp_Repository.Insert(deptRegSetUp);
            InvalidateCache();
            return id;
        }

        public static bool Update(DeptRegSetUp deptRegSetUp)
        {
            bool isExecute = RepositoryManager.DeptRegSetUp_Repository.Update(deptRegSetUp);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.DeptRegSetUp_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static DeptRegSetUp GetById(int? id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "DeptRegSetUpById" + id;
            DeptRegSetUp deptRegSetUp = GetCacheItem(rawKey);

            if (deptRegSetUp == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                deptRegSetUp = RepositoryManager.DeptRegSetUp_Repository.GetById(id);
                if (deptRegSetUp != null)
                    AddCacheItem(rawKey, deptRegSetUp);
            }

            return deptRegSetUp;
        }

        public static List<DeptRegSetUp> GetAll()
        {
            const string rawKey = "DeptRegSetUpGetAll";

            List<DeptRegSetUp> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.DeptRegSetUp_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static  DeptRegSetUp GetByProgramId(int? programId)
        {
            string rawKey = "DeptRegSetUpGetAllByProgramId" + programId;

             DeptRegSetUp obj = GetCacheItem(rawKey);

             if (obj == null)
            {
               List< DeptRegSetUp> list = GetAll();

                if (list != null)
                    list = list.Where(l => l.ProgramID == programId).ToList();

                if (list != null && list.Count() != 0)
                    obj = list[0];//.Single();

                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }
    }
}
