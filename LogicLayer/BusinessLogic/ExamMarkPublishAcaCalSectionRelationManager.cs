using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using LogicLayer.BusinessObjects.DTO;

namespace LogicLayer.BusinessLogic
{
    public class ExamMarkPublishAcaCalSectionRelationManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamMarkPublishAcaCalSectionRelationCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamMarkPublishAcaCalSectionRelation> GetCacheAsList(string rawKey)
        {
            List<ExamMarkPublishAcaCalSectionRelation> list = (List<ExamMarkPublishAcaCalSectionRelation>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamMarkPublishAcaCalSectionRelation GetCacheItem(string rawKey)
        {
            ExamMarkPublishAcaCalSectionRelation item = (ExamMarkPublishAcaCalSectionRelation)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamMarkPublishAcaCalSectionRelation exammarkpublishacacalsectionrelation)
        {
            int id = RepositoryManager.ExamMarkPublishAcaCalSectionRelation_Repository.Insert(exammarkpublishacacalsectionrelation);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamMarkPublishAcaCalSectionRelation exammarkpublishacacalsectionrelation)
        {
            bool isExecute = RepositoryManager.ExamMarkPublishAcaCalSectionRelation_Repository.Update(exammarkpublishacacalsectionrelation);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamMarkPublishAcaCalSectionRelation_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamMarkPublishAcaCalSectionRelation GetById(int? id)
        {
            string rawKey = "ExamMarkPublishAcaCalSectionRelationByID" + id;
            ExamMarkPublishAcaCalSectionRelation exammarkpublishacacalsectionrelation = GetCacheItem(rawKey);

            if (exammarkpublishacacalsectionrelation == null)
            {
                exammarkpublishacacalsectionrelation = RepositoryManager.ExamMarkPublishAcaCalSectionRelation_Repository.GetById(id);
                if (exammarkpublishacacalsectionrelation != null)
                    AddCacheItem(rawKey,exammarkpublishacacalsectionrelation);
            }

            return exammarkpublishacacalsectionrelation;
        }

        public static ExamMarkPublishAcaCalSectionRelation GetByAcacalSecId(int id)
        {
            string rawKey = "ExamMarkPublishAcaCalSectionRelationByAcaCalID" + id;
            ExamMarkPublishAcaCalSectionRelation exammarkpublishacacalsectionrelation = GetCacheItem(rawKey);

            if (exammarkpublishacacalsectionrelation == null)
            {
                exammarkpublishacacalsectionrelation = RepositoryManager.ExamMarkPublishAcaCalSectionRelation_Repository.GetByAcacalSecId(id);
                if (exammarkpublishacacalsectionrelation != null)
                    AddCacheItem(rawKey, exammarkpublishacacalsectionrelation);
            }

            return exammarkpublishacacalsectionrelation;
        }

        public static List<ExamMarkPublishAcaCalSectionRelation> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamMarkPublishAcaCalSectionRelationGetAll";

            List<ExamMarkPublishAcaCalSectionRelation> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamMarkPublishAcaCalSectionRelation_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }


        public static List<AcacalSectionResultPublishDTO> GetByProgramIdAcaCalId(int programId, int acaCalId)
        {
            List<AcacalSectionResultPublishDTO> list = RepositoryManager.ExamMarkPublishAcaCalSectionRelation_Repository.GetByProgramIdAcaCalId(programId, acaCalId);
            return list;
        }
    }
}

