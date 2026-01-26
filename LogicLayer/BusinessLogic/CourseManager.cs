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
    public class CourseManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "CourseCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Course> GetCacheAsList(string rawKey)
        {
            List<Course> list = (List<Course>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static List<rTreeDistribution> GetTreeDistributionAsList(string rawKey)
        {
            List<rTreeDistribution> list = (List<rTreeDistribution>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        private static List<rCourseRegistrationForm> GetCourseRegistrationFormAsList(string rawKey)
        {
            List<rCourseRegistrationForm> list = (List<rCourseRegistrationForm>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        private static List<rOfferedCourse> GetOfferedCourseAsList(string rawKey)
        {
            List<rOfferedCourse> list = (List<rOfferedCourse>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Course GetCacheItem(string rawKey)
        {
            Course item = (Course)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static List<Course> GetAllByProgram(int ProgramID)
        {

            List<Course> list = null;

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Course_Repository.GetAllByProgram(ProgramID);
            }

            return list;
        }

        public static List<Course> GetAll()
        {
            //string rawKey = "CourseGetAll";

            //if (list == null)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            //    list = RepositoryManager.Course_Repository.GetAll();
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            List<Course> list = RepositoryManager.Course_Repository.GetAll();
            return list;
        }

        public static List<Course> GetAllOfferedCourse()
        {
            List<Course> list = RepositoryManager.Course_Repository.GetAllOfferedCourse();
            return list;
        }

        public static Course GetByCourseIdVersionId(int CourseID, int VersionID)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "CourseByCourseIdVersionId" + CourseID + VersionID;
            Course obj = GetCacheItem(rawKey);

            if (obj == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                obj = RepositoryManager.Course_Repository.GetByCourseIdVersionId(CourseID, VersionID);
                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static List<NodeCourses> GetAllNodeCoursesByNodeId(int nodeId)
        {
            List<NodeCourses> collection = RepositoryManager.Course_Repository.GetAllNodeCoursesByNodeId(nodeId);
            return collection;
        }

        public static List<CourseListByTeacherDTO> GetCourseByTeacherId(int teacherId)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "GetCourseByTeacherId";

            List<CourseListByTeacherDTO> list;

            //if (list == null)
            //{
            // Item not found in cache - retrieve it and insert it into the cache
            list = RepositoryManager.Course_Repository.GetCourseByTeacherId(teacherId);
            if (list != null)
                AddCacheItem(rawKey, list);
            //}

            return list;
        }

        public static List<RptFlatCourse> GetAllFlatCourseByProgram(int programId)
        {

            List<RptFlatCourse> list = RepositoryManager.FlatCourseListByProgram_Repository.GetAllFlatCourseByProgram(programId);
            return list;
        }

        public static List<Course> GetAllCourseByProgramAndSessionFromStudentCourseHistoryTable(int programId, int sessionId)
        {

            string rawKey = "CourseGetAllByProgramAndSession" + programId + sessionId;

            List<Course> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.Course_Repository.GetAllByProgramAndSessionFromStudentCourseHistoryTable(programId, sessionId);
                if (list != null)
                {
                    AddCacheItem(rawKey, list);
                }
            }

            return list;
        }

        public static List<Course> GetAllCourseByProgramAndSessionBatchFromStudentCourseHistoryTable(int programId, int sessionId, int BatchId)
        { 
            string rawKey = "CourseGetAllByProgramAndSessionBatch" + programId + sessionId + BatchId;

            List<Course> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.Course_Repository.GetAllCourseByProgramAndSessionBatchFromStudentCourseHistoryTable(programId, sessionId, BatchId);
                if (list != null)
                {
                    AddCacheItem(rawKey, list);
                }
            }

            return list;
        }

        public static List<Course> GetAllCourseByProgramAndSessionBatchExamCenterFromStudentCourseHistoryTable(int programId, int sessionId, int BatchId,int ExamCenterId)
        {

            List<Course> list = null;

            if (list == null)
            {
                list = RepositoryManager.Course_Repository.GetAllCourseByProgramAndSessionBatchExamCenterFromStudentCourseHistoryTable(programId, sessionId, BatchId,ExamCenterId);           
            }

            return list;
        }
        

        public static List<Course> GetAllCourseByProgramBatchYearFromCourseHistory(int programId, int batchId, int yearId)
        {
            List<Course> list = RepositoryManager.Course_Repository.GetAllCourseByProgramBatchYearFromCourseHistory(programId, batchId, yearId);
            return list;
        }

        public static List<Course> GetAllByAcaCalIdProgramId(int acaCalId, int programId)
        {

            string rawKey = "CourseGetAllByAcaCalIdProgramId" + acaCalId + programId;

            List<Course> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.Course_Repository.GetAllByAcaCalIdProgramId(acaCalId, programId);
                if (list != null)
                {
                    AddCacheItem(rawKey, list);
                }
            }

            return list;
        }

        public static List<Course> GetAllByAcaCalIdProgramIdFromCourseHistory(int acaCalId, int programId)
        {

            string rawKey = "CourseGetAllByAcaCalIdProgramId" + acaCalId + programId;

            List<Course> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.Course_Repository.GetAllByAcaCalIdProgramIdFromCourseHistory(acaCalId, programId);
                if (list != null)
                {
                    AddCacheItem(rawKey, list);
                }
            }

            return list;
        }

        public static List<Course> GetAllByAcaCalIdStudentRoll(int acaCalId, string studentRoll)
        {

            return RepositoryManager.Course_Repository.GetAllByAcaCalIdStudentRoll(acaCalId, studentRoll);
        }

        public static List<rTreeDistribution> GetTreeDistributionByProgram(int programId, string treeCalendarMasterId)
        {

            string rawKey = "RptTreeDistributionByProgram" + programId + treeCalendarMasterId;

            List<rTreeDistribution> list = GetTreeDistributionAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.TreeDistribution_Repository.GetTreeDistributionByProgram(programId, treeCalendarMasterId);
                if (list != null)
                {
                    AddCacheItem(rawKey, list);
                }
            }

            return list;
        }

        public static List<rCourseRegistrationForm> GetCourseRegistrationForm(int programId, int acaCalId, string roll)
        {
            string rawKey = "CourseRegistrationForm" + programId + acaCalId + roll;

            List<rCourseRegistrationForm> list = GetCourseRegistrationFormAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.CourseRegistrationForm_Repository.GetCourseRegistrationForm(programId, acaCalId, roll);
                if (list != null)
                {
                    AddCacheItem(rawKey, list);
                }
            }

            return list;
        }

        public static List<rOfferedCourse> GetOfferedCourse(int programId, int acaCalId)
        {
            string rawKey = "RptOfferedCourseByProgram" + programId + acaCalId;

            List<rOfferedCourse> list = GetOfferedCourseAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.OfferedCourseByProgram_Repository.GetOfferedCourse(programId, acaCalId);
                if (list != null)
                {
                    AddCacheItem(rawKey, list);
                }
            }

            return list;
        }

        public static List<Course> GetOfferedCourseByProgramSession(int programId, int sessionId)
        {


            List<Course> list = RepositoryManager.OfferedCourseByProgram_Repository.GetOfferedCourseByProgramSession(programId, sessionId);

            return list;
        }

        public static List<rCourseWiseStudentList> GetCourseWiseStudentList(int programId, int acaCalId)
        {
            List<rCourseWiseStudentList> list = RepositoryManager.CourseWiseStudentList_Repository.GetCourseWiseStudentList(programId, acaCalId);
            return list;
        }

        public static List<rTopSheet> LoadTopSheet(int examScheduleSetId, int dayId, int timeSlotId)
        {
            List<rTopSheet> list = RepositoryManager.Course_Repository.LoadTopSheet(examScheduleSetId, dayId, timeSlotId);
            return list;
        }

        public static List<rTeacherList> LoadTeacherList(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, string courseId)
        {
            List<rTeacherList> list = RepositoryManager.Course_Repository.LoadTeacherList(acaCalId, examScheduleSetId, dayId, timeSlotId, courseId);
            return list;
        }

        public static List<rExamSection> LoadExamSection(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, string courseId, int teacherId)
        {
            List<rExamSection> list = RepositoryManager.Course_Repository.LoadExamSection(acaCalId, examScheduleSetId, dayId, timeSlotId, courseId, teacherId);
            return list;
        }

        public static List<Course> GetAllByFormalCode(string formalcode)
        {
            List<Course> list = RepositoryManager.Course_Repository.GetAllByFormalCode(formalcode);
            return list;
        }

        public static List<Course> GetAllByVersionCode(string versioncode)
        {
            List<Course> list = RepositoryManager.Course_Repository.GetAllByVersionCode(versioncode);
            return list;
        }

        public static List<Course> GetAllRetakeOrRegularOpenCourseByStudentIdCourseTypeId(int StudentId, int TypeId)
        {
            List<Course> list = RepositoryManager.Course_Repository.GetAllRetakeOrRegularOpenCourseByStudentIdCourseTypeId(StudentId, TypeId);
            return list;
        }

        public static int Insert(Course course)
        {
            int id = RepositoryManager.Course_Repository.Insert(course);
            return id;
        }

        public static bool Update(Course course)
        {
            bool result = RepositoryManager.Course_Repository.Update(course);
            return result;
        }

        public static bool Delete(int courseId, int versionId)
        {
            bool isExecute = RepositoryManager.Course_Repository.Delete(courseId, versionId);
            InvalidateCache();
            return isExecute;
        }
    }
}
