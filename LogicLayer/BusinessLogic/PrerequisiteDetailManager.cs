using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.BusinessLogic
{
    public class PrerequisiteDetailManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "PrerequisiteDetailCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<PrerequisiteDetail> GetCacheAsList(string rawKey)
        {
            List<PrerequisiteDetail> list = (List<PrerequisiteDetail>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static PrerequisiteDetail GetCacheItem(string rawKey)
        {
            PrerequisiteDetail item = (PrerequisiteDetail)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(PrerequisiteDetail prerequisiteDetail)
        {
            int id = RepositoryManager.PrerequisiteDetail_Repository.Insert(prerequisiteDetail);
            InvalidateCache();
            return id;
        }

        public static bool Update(PrerequisiteDetail prerequisiteDetail)
        {
            bool isExecute = RepositoryManager.PrerequisiteDetail_Repository.Update(prerequisiteDetail);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.PrerequisiteDetail_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static PrerequisiteDetail GetById(int? id)
        {
            string rawKey = "PrerequisiteDetailById" + id;
            PrerequisiteDetail prerequisiteDetail = GetCacheItem(rawKey);

            if (prerequisiteDetail == null)
            {
                prerequisiteDetail = RepositoryManager.PrerequisiteDetail_Repository.GetById(id);
                if (prerequisiteDetail != null)
                    AddCacheItem(rawKey, prerequisiteDetail);
            }

            return prerequisiteDetail;
        }


        public static List<PrerequisiteDetail> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            // const string rawKey = "PrerequisiteDetailGetAll";

            List<PrerequisiteDetail> list = RepositoryManager.PrerequisiteDetail_Repository.GetAll();

            //if (list == null)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    list = RepositoryManager.PrerequisiteDetail_Repository.GetAll();
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            return list;
        }

        internal static List<PrerequisiteDetail> GetByPrerequisiteMasterID(int prerequisiteMasterID)
        {
            List<PrerequisiteDetail> list = RepositoryManager.PrerequisiteDetail_Repository.GetAll();
            list = list.Where(p => p.PrerequisiteMasterID == prerequisiteMasterID).ToList();
            return list;
        }

        public static List<PrerequisiteDetail> GetAllByProgramId(int programId)
        {
            List<PrerequisiteDetail> list = RepositoryManager.PrerequisiteDetail_Repository.GetAllByProgramId(programId);

            return list;
        }
         
    }
}
