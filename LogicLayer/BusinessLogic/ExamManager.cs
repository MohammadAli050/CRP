using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.DataLogic;
using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicLayer.BusinessLogic
{
    public class ExamManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "MicroTestCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Exam> GetCacheAsList(string rawKey)
        {
            List<Exam> list = (List<Exam>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Exam GetCacheItem(string rawKey)
        {
            Exam item = (Exam)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(Exam exam)
        {
            int id = RepositoryManager.Exam_Repository.Insert(exam);
            InvalidateCache();
            return id;
        }

        public static bool Update(Exam exam)
        {
            bool isExecute = RepositoryManager.Exam_Repository.Update(exam);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Exam_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Exam GetById(int id)
        {
            string rawKey = "MicroTestByID" + id;
            Exam exam = GetCacheItem(rawKey);

            if (exam == null)
            {
                exam = RepositoryManager.Exam_Repository.GetById(id);
                if (exam != null)
                    AddCacheItem(rawKey, exam);
            }

            return exam;
        }

        public static List<Exam> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "MicroTestGetAll";

            List<Exam> list = RepositoryManager.Exam_Repository.GetAll();

            //if (list == null)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    list = 
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            return list;
        }

        public static List<Exam> GetAllExam(int examSetId) 
        {
            const string rawKey = "GetMicroTestByTestSetId";

            List<Exam> list = RepositoryManager.Exam_Repository.GetAllExam(examSetId);
            return list;
        }

        public static List<Exam> GetExamByCouseTemplateId(int courseId, int templateId)
        {
            const string rawKey = "GetTestByCouseTemplateId";

            List<Exam> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Exam_Repository.GetExamByCouseTemplateId(courseId, templateId);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<ExamDTO> GetExamByCourseId(int courseId)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "GetMicroTestByCourseId";

            List<ExamDTO> list = RepositoryManager.Exam_Repository.GetExamByCourseId(courseId);

            //if (list == null)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    list = 
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            return list;
        }

        public static List<ExamDTO> GetCacheAsMicroTestList(string rawKey)
        {
            List<ExamDTO> list = (List<ExamDTO>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static bool GetExamByName(string examName) 
        {
            string rawKey = "MicroTestByName" + examName;
            Exam examObj = RepositoryManager.Exam_Repository.GetExamByName(examName);

            if (examObj == null) { return true; }
            else { return false; }
        }

        public static List<Exam> GetAllByExamTemplateId(int examTemplateId)
        {
            string rawKey = "ExamGetAllByExamTemplateId" + examTemplateId;

            List<Exam> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.Exam_Repository.GetAllByExamTemplateId(examTemplateId);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }


        public static List<rSemesterResultSummary> GetSemesterResultSummary(int sessionId)
        {
            List<rSemesterResultSummary> list = RepositoryManager.Exam_Repository.GetSemesterResultSummary(sessionId);
            return list;
        }

        public static List<rStudentExamRoutine> GetStudentExamRoutine(string roll, int acaCalId)
        {
            List<rStudentExamRoutine> list = RepositoryManager.Exam_Repository.GetStudentExamRoutine(roll, acaCalId);
            return list;
        }

        public static List<rOfferedCourseExamRoutine> GetOfferedCourseExamRoutineForStudent(string roll, int acaCalId)
        {
            List<rOfferedCourseExamRoutine> list = RepositoryManager.Exam_Repository.GetOfferedCourseExamRoutineForStudent(roll, acaCalId);
            return list;
        }

        public static List<TabulationSheetMajor> GetMajor(int programId)
        {
            List<TabulationSheetMajor> list = RepositoryManager.Exam_Repository.GetMajor(programId);
            return list;
        }

        public static List<StudentResultPublishCourseHistoryDTO> GetStudentForResultPublish(int programId, int sessionId, int courseId, int versionId, int acaCalSectionId) 
        {
            List<StudentResultPublishCourseHistoryDTO> list = RepositoryManager.Exam_Repository.GetStudentForResultPublish(programId, sessionId, courseId, versionId, acaCalSectionId);
            return list;
        }
    }
}
