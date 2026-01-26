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
    public class EvaluationFormManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "EvaluationFormCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<EvaluationForm> GetCacheAsList(string rawKey)
        {
            List<EvaluationForm> list = (List<EvaluationForm>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static EvaluationForm GetCacheItem(string rawKey)
        {
            EvaluationForm item = (EvaluationForm)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(EvaluationForm evaluationform)
        {
            int id = RepositoryManager.EvaluationForm_Repository.Insert(evaluationform);
            InvalidateCache();
            return id;
        }

        public static bool Update(EvaluationForm evaluationform)
        {
            bool isExecute = RepositoryManager.EvaluationForm_Repository.Update(evaluationform);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.EvaluationForm_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static EvaluationForm GetById(int id)
        {
            string rawKey = "EvaluationFormByID" + id;
            EvaluationForm evaluationform = GetCacheItem(rawKey);

            if (evaluationform == null)
            {
                evaluationform = RepositoryManager.EvaluationForm_Repository.GetById(id);
                if (evaluationform != null)
                    AddCacheItem(rawKey, evaluationform);
            }

            return evaluationform;
        }

        public static List<EvaluationForm> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "EvaluationFormGetAll";

            List<EvaluationForm> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.EvaluationForm_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static EvaluationForm GetBy(int personId, int acaCalSecId)
        {
            string rawKey = "EvaluationFormBy" + personId + acaCalSecId;
            EvaluationForm evaluationform = GetCacheItem(rawKey);

            if (evaluationform == null)
            {
                evaluationform = RepositoryManager.EvaluationForm_Repository.GetBy(personId, acaCalSecId);
                if (evaluationform != null)
                    AddCacheItem(rawKey, evaluationform);
            }

            return evaluationform;
        }

        public static List<EvaluationForm> GetAll(int acaCalSecId)
        {
            string rawKey = "EvaluationFormGetAll" + acaCalSecId;

            List<EvaluationForm> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.EvaluationForm_Repository.GetAll(acaCalSecId);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<EvaluationForm> GetAllByAcaCalId(int acaCalId)
        {
            string rawKey = "EvaluationFormGetAllByAcaCalId" + acaCalId;

            List<EvaluationForm> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.EvaluationForm_Repository.GetAllByAcaCalId(acaCalId);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<EvaluationForm> GetAllByPersonId(int personId)
        {
            string rawKey = "EvaluationFormGetAllByPersonId" + personId;

            List<EvaluationForm> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.EvaluationForm_Repository.GetAllByPersonId(personId);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }
    }
}
