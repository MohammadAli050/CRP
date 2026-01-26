using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.WAO;

namespace LogicLayer.BusinessLogic
{
    public class StudentCourseHistoryManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentCourseHistoryCache" };
        const double CacheDuration = 1.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentCourseHistory> GetCacheAsList(string rawKey)
        {
            List<StudentCourseHistory> list = (List<StudentCourseHistory>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentCourseHistory GetCacheItem(string rawKey)
        {
            StudentCourseHistory item = (StudentCourseHistory)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(StudentCourseHistory studentCourseHistory)
        {
            int id = RepositoryManager.StudentCourseHistory_Repository.Insert(studentCourseHistory);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentCourseHistory studentCourseHistory)
        {
            bool isExecute = RepositoryManager.StudentCourseHistory_Repository.Update(studentCourseHistory);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentCourseHistory_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentCourseHistory GetById(int id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "StudentCourseHistoryById" + id;
            StudentCourseHistory studentCourseHistory = GetCacheItem(rawKey);

            if (studentCourseHistory == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                studentCourseHistory = RepositoryManager.StudentCourseHistory_Repository.GetById(id);
                if (studentCourseHistory != null)
                    AddCacheItem(rawKey, studentCourseHistory);
            }

            return studentCourseHistory;
        }

        public static List<StudentCourseHistory> GetAll()
        {
            const string rawKey = "StudentCourseHistoryGetAll";

            List<StudentCourseHistory> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.StudentCourseHistory_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<StudentCourseHistory> GetAllByAcaCalSectionId(int id)
        {
            string rawKey = "StudentCourseHistoryGetAllByAcaCalSectionId" + id;

            List<StudentCourseHistory> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.StudentCourseHistory_Repository.GetAllByAcaCalSectionId(id);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<StudentCourseHistory> GetAllByAcaCalId(int acaCalId)
        {
            //string rawKey = "StudentCourseHistoryGetAllByAcaCalId" + acaCalId;

            //List<StudentCourseHistory> list = GetCacheAsList(rawKey);

            List<StudentCourseHistory> list = new List<StudentCourseHistory>();
            //if (list == null)
            //{
                list = RepositoryManager.StudentCourseHistory_Repository.GetAllByAcaCalId(acaCalId);
                //if (list != null)
                //    AddCacheItem(rawKey, list);
            //}

            return list;
        }

        public static List<StudentCourseHistoryDTO> GetAllByAcaCalIdCourseId(int acaCalId, int CourseId)
        {
            List<StudentCourseHistoryDTO> list = RepositoryManager.StudentCourseHistory_Repository.GetAllByAcaCalIdCourseId(acaCalId, CourseId);
            return list;
        }

        public static List<StudentCourseHistory> GetAllByStudentId(int StudentID)
        {
           // string rawKey = "StudentCourseHistoryGetAllByStudentId" + StudentID;

            List<StudentCourseHistory> list = RepositoryManager.StudentCourseHistory_Repository.GetAllByStudentId(StudentID);

            //if (list == null)
            //{
            //    list = RepositoryManager.StudentCourseHistory_Repository.GetAllByStudentId(StudentID);
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            return list;
        }

        public static List<StudentCourseHistory> GetAllMultiSpanCourseByStudentID(int studentID)
        {
            string rawKey = "StudentCourseHistoryGetAllMultiSpanCourseByStudentID" + studentID;

            List<StudentCourseHistory> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = GetAllByStudentId(studentID);

                if (list != null)
                {
                    list = list.Where(l => l.IsMultipleACUSpan == true).ToList();

                    if (list != null && list.Count > 0)
                        AddCacheItem(rawKey, list);
                }
            }

            return list;
        }

        public static List<StudentCourseHistory> GetAll(int studentId, int courseId, int versionId)
        {
            string rawKey = "StudentCourseHistoryGetAll" + studentId + courseId + versionId;

            List<StudentCourseHistory> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.StudentCourseHistory_Repository.GetAll(studentId, courseId, versionId);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static StudentCourseHistory GetBy(int studentId, int courseId, int versionId, bool considerGPA)
        {
            string rawKey = "StudentCourseHistoryGetBy" + studentId + courseId + versionId + considerGPA;
            StudentCourseHistory studentCourseHistory = GetCacheItem(rawKey);

            if (studentCourseHistory == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                studentCourseHistory = RepositoryManager.StudentCourseHistory_Repository.GetBy(studentId, courseId, versionId, considerGPA);
                if (studentCourseHistory != null)
                    AddCacheItem(rawKey, studentCourseHistory);
            }

            return studentCourseHistory;
        }

        public static StudentCourseHistory GetBy(int studentId, int courseId, int versionId, int acaCalSecId)
        {
            string rawKey = "StudentCourseHistoryGetBy" + studentId + courseId + versionId + acaCalSecId;
            StudentCourseHistory studentCourseHistory = GetCacheItem(rawKey);

            if (studentCourseHistory == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                studentCourseHistory = RepositoryManager.StudentCourseHistory_Repository.GetBy(studentId, courseId, versionId, acaCalSecId);
                if (studentCourseHistory != null)
                    AddCacheItem(rawKey, studentCourseHistory);
            }

            return studentCourseHistory;
        }

        public static List<StudentCourseHistory> GetAllByStudentIdAcaCalId(int studentID, int acaCalId)
        {
            //string rawKey = "StudentCourseHistoryGetAllByStudentIdAcaCalId" + studentID + acaCalId;

            //List<StudentCourseHistory> list = GetCacheAsList(rawKey);

            //if (list == null)
            //{
            //    list = RepositoryManager.StudentCourseHistory_Repository.GetAllByStudentIdAcaCalId(studentID, acaCalId);
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            //return list;
            List<StudentCourseHistory> list = RepositoryManager.StudentCourseHistory_Repository.GetAllByStudentIdAcaCalId(studentID, acaCalId);
            return list;
        }

        public static List<StudentCourseHistory> GetDistinctCourseHistoryByStudentIdAcaCalId(int StudentId, int AcaCalId)
        {
            List<StudentCourseHistory> list = RepositoryManager.StudentCourseHistory_Repository.GetDistinctCourseHistoryByStudentIdAcaCalId(StudentId, AcaCalId);
            return list;
        }

        public static bool UpdateSectionBy(int SectionId, int  Id )
        {
            bool isExecute = RepositoryManager.StudentCourseHistory_Repository.UpdateSectionBy(  SectionId,   Id );
            InvalidateCache();
            return isExecute;
        }

        public static List<StudentCourseHistory> GetAllRegisteredStudentByProgramSessionCourse(int programId, int sessionId, int courseId, int versionId)
        {
            List<StudentCourseHistory> list = RepositoryManager.StudentCourseHistory_Repository.GetAllRegisteredStudentByProgramSessionCourse(programId, sessionId, courseId, versionId);
            return list;
        }

        public static List<StudentCourseHistory> GetAllRegisteredStudentByProgramSessionBatchCourse(int programId, int sessionId, int batchId, int courseId, int versionId)
        {
            List<StudentCourseHistory> list = RepositoryManager.StudentCourseHistory_Repository.GetAllRegisteredStudentByProgramSessionBatchCourse(programId, sessionId, batchId, courseId, versionId);
            return list;
        }

        public static List<StudentCourseHistory> GetAllByProgramSession(int programId, int sessionId)
        {
            List<StudentCourseHistory> list = RepositoryManager.StudentCourseHistory_Repository.GetAllByProgramSession(programId, sessionId);
            return list;
        }

        public static decimal GetCompletedCreditByRoll(string Roll)
        {
            decimal completedCredit = RepositoryManager.StudentCourseHistory_Repository.GetCompletedCredit(Roll);
            return completedCredit;
        }

        public static decimal GetAttemptedCreditByRoll(string Roll)
        {
            decimal attemptedCredit = RepositoryManager.StudentCourseHistory_Repository.GetAttemptedCredit(Roll);
            return attemptedCredit;
        }

        public static List<CourseHistryForDegreeValidation> GetCourseHistoryByStudentIdForDegreeValidation(string Roll)
        {
            List<CourseHistryForDegreeValidation> list = RepositoryManager.StudentCourseHistory_Repository.GetCourseHistoryByStudentIdForDegreeValidation(Roll);
            return list;
        }

        public static List<StudentCourseHistory> GetAllByProgramSessionBatch(int programId, int sessionId, int batchId, int rangeId)
        {
            List<StudentCourseHistory> list = RepositoryManager.StudentCourseHistory_Repository.GetAllByProgramSessionBatch(programId, sessionId, batchId,rangeId);
            return list;
        }

        public static List<GradeSheetInfo> GetAllRegisteredStudentForGradeSheetByProgramSessionBatchCourseExamCenter(int programId, int sessionId, int batchId, int courseId, int versionId, int ExamCeterId, int institutionId)
        {
            List<GradeSheetInfo> list = RepositoryManager.StudentCourseHistory_Repository.GetAllRegisteredStudentForGradeSheetByProgramSessionBatchCourseExamCenter(programId, sessionId, batchId, courseId, versionId, ExamCeterId, institutionId);
            return list;
        }


        #region Web Api

        public static List<StudentCourseHistoryWAO> GetStudentCourseHistoryWAOByStudentRoll(string roll)
        {
            List<StudentCourseHistoryWAO> list = RepositoryManager.StudentCourseHistory_Repository.GetStudentCourseHistoryWAOByStudentRoll(roll);
            return list;
        }

        #endregion
    }
}
