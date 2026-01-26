using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicLayer.BusinessLogic
{
   public class EquiCourseManager
    {
        #region Cache

       public static readonly string[] MasterCacheKeyArray = { "EquiCourseCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<EquiCourse> GetCacheAsList(string rawKey)
        {
            List<EquiCourse> list = (List<EquiCourse>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static EquiCourse GetCacheItem(string rawKey)
        {
            EquiCourse item = (EquiCourse)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(EquiCourse obj)
        {
            int id = RepositoryManager.EquiCourse_Repository.Insert(obj);
            InvalidateCache();
            return id;
        }

        public static bool Update(EquiCourse obj)
        {
            bool isExecute = RepositoryManager.EquiCourse_Repository.Update(obj);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.EquiCourse_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static EquiCourse GetById(int? id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "EquivalentID" + id;
            EquiCourse obj = GetCacheItem(rawKey);

            if (obj == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                obj = RepositoryManager.EquiCourse_Repository.GetById(id);
                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static List<EquiCourse> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

           // const string rawKey = "EquiCourseGetAll";

            List<EquiCourse> list = RepositoryManager.EquiCourse_Repository.GetAll();

            //if (list == null)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    list = RepositoryManager.EquiCourse_Repository.GetAll();
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            return list;
        }

        public static List<rEquivalentCourse> GetAllEquivalentCourseByProgramId(int programId) 
        {
            List<rEquivalentCourse> list = RepositoryManager.EquiCourse_Repository.GetAllEquivalentCourseByProgramId(programId);
            return list;
        }
    }
}
