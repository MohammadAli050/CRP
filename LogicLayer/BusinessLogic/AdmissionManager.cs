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
    public class AdmissionManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "AdmissionCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Admission> GetCacheAsList(string rawKey)
        {
            List<Admission> list = (List<Admission>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Admission GetCacheItem(string rawKey)
        {
            Admission item = (Admission)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(Admission admission)
        {
            int id = RepositoryManager.Admission_Repository.Insert(admission);
            InvalidateCache();
            return id;
        }

        public static bool Update(Admission admission)
        {
            bool isExecute = RepositoryManager.Admission_Repository.Update(admission);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Admission_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Admission GetById(int? id)
        {
            string rawKey = "AdmissionById" + id;
            Admission admission = GetCacheItem(rawKey);

            if (admission == null)
            {
                admission = RepositoryManager.Admission_Repository.GetById(id);
                if (admission != null)
                    AddCacheItem(rawKey, admission);
            }

            return admission;
        }

        public static Admission GetByStudentID(int studentID)
        {
            string rawKey = "AdmissionByStudentId" + studentID;
            Admission admission = GetCacheItem(rawKey);

            if (admission == null)
            {
                admission = RepositoryManager.Admission_Repository.GetByStudentId(studentID);
                if (admission != null)
                    AddCacheItem(rawKey, admission);
            }

            return admission;
        }
        

        public static List<Admission> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "AdmissionGetAll";

            List<Admission> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Admission_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
