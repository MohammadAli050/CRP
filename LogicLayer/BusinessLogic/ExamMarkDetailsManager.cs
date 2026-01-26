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
    public class ExamMarkDetailsManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "ExamMarkDetailsCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamMarkDetails> GetCacheAsList(string rawKey)
        {
            List<ExamMarkDetails> list = (List<ExamMarkDetails>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamMarkDetails GetCacheItem(string rawKey)
        {
            ExamMarkDetails item = (ExamMarkDetails)HttpRuntime.Cache[GetCacheKey(rawKey)];
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


        public static int Insert(ExamMarkDetails exammarkdetails)
        {
            int id = RepositoryManager.ExamMarkDetails_Repository.Insert(exammarkdetails);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamMarkDetails exammarkdetails)
        {
            bool isExecute = RepositoryManager.ExamMarkDetails_Repository.Update(exammarkdetails);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.ExamMarkDetails_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamMarkDetails GetById(int? id)
        {
            string rawKey = "ExamMarkDetailsByID" + id;
            ExamMarkDetails exammarkdetails = GetCacheItem(rawKey);

            if (exammarkdetails == null)
            {
                exammarkdetails = RepositoryManager.ExamMarkDetails_Repository.GetById(id);
                if (exammarkdetails != null)
                    AddCacheItem(rawKey,exammarkdetails);
            }

            return exammarkdetails;
        }

        public static List<ExamMarkDetails> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "ExamMarkDetailsGetAll";

            List<ExamMarkDetails> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.ExamMarkDetails_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<ExamMarkNewDTO> GetByExamMarkDtoByParameter(int programId, int sessionId, int courseId, int versionId, int acaCalSectionId, int examTemplateBasicItemId) 
        {
            List<ExamMarkNewDTO> list = RepositoryManager.ExamMarkDetails_Repository.GetByExamMarkDtoByParameter(programId, sessionId, courseId, versionId, acaCalSectionId, examTemplateBasicItemId);
            return list;
        }

        public static List<ExamMarkNewDTO> GetExamMarkForReport(int programId, int sessionId, int courseId, int versionId, int acaCalSectionId, int examTemplateBasicItemId)
        {
            List<ExamMarkNewDTO> list = RepositoryManager.ExamMarkDetails_Repository.GetExamMarkForReport(programId, sessionId, courseId, versionId, acaCalSectionId, examTemplateBasicItemId);
            return list;
        }
        public static List<ExamMarkNewDTO> GetConvertedExamMarkForReport(int programId, int sessionId, int courseId, int versionId, int acaCalSectionId, int examTemplateBasicItemId)
        {
            List<ExamMarkNewDTO> list = RepositoryManager.ExamMarkDetails_Repository.GetConvertedExamMarkForReport(programId, sessionId, courseId, versionId, acaCalSectionId, examTemplateBasicItemId);
            return list;
        }
        public static ExamMarkDetails GetByCourseHistoryIdExamTemplateItemId(int courseHistoryId, int examTemplateItemId)
        {
            ExamMarkDetails studentresult = RepositoryManager.ExamMarkDetails_Repository.GetByCourseHistoryIdExamTemplateItemId(courseHistoryId, examTemplateItemId);
            return studentresult;
        }

        public static List<ExamMarkDetails> GetExamMarkByCourseHistoryId(int courseHistoryId)
        {
            List<ExamMarkDetails> list = RepositoryManager.ExamMarkDetails_Repository.GetExamMarkByCourseHistoryId(courseHistoryId);
            return list;
        }
    }
}

