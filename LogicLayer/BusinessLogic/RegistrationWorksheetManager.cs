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
    public class RegistrationWorksheetManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "RegistrationWorksheetCache" };
        const double CacheDuration = 2.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<RegistrationWorksheet> GetCacheAsList(string rawKey)
        {
            List<RegistrationWorksheet> list = (List<RegistrationWorksheet>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static List<RegistrationWorksheet> GetCacheAsListSpecific(string rawKey)
        {
            List<RegistrationWorksheet> list = (List<RegistrationWorksheet>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static RegistrationWorksheet GetCacheItem(string rawKey)
        {
            RegistrationWorksheet item = (RegistrationWorksheet)HttpRuntime.Cache[GetCacheKey(rawKey)];
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
            DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddSeconds(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public static void InvalidateCache()
        {
            // Remove the cache dependency
            HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        }

        #endregion

        public static int Insert(RegistrationWorksheet registrationWorksheet)
        {
            int id = RepositoryManager.RegistrationWorksheet_Repository.Insert(registrationWorksheet);
            InvalidateCache();
            return id;
        }

        public static bool Update(RegistrationWorksheet registrationWorksheet)
        {
            bool isExecute = RepositoryManager.RegistrationWorksheet_Repository.Update(registrationWorksheet);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.RegistrationWorksheet_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static RegistrationWorksheet GetById(int id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            string rawKey = "RegistrationWorksheetById" + id;
            RegistrationWorksheet registrationWorksheet = GetCacheItem(rawKey);

            if (registrationWorksheet == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                registrationWorksheet = RepositoryManager.RegistrationWorksheet_Repository.GetById(id);
                if (registrationWorksheet != null)
                    AddCacheItem(rawKey, registrationWorksheet);
            }

            return registrationWorksheet;
        }

        public static List<RegistrationWorksheet> GetAll()
        {

            const string rawKey = "RegistrationWorksheetGetAll";

            List<RegistrationWorksheet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.RegistrationWorksheet_Repository.GetAll();
                list = list.OrderBy(l => l.Priority).ToList();
                if (list != null && list.Count() != 0)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<RegistrationWorksheet> GetAllOpenCourseByStudentID(int studentId)
        {
            List<RegistrationWorksheet> list = RepositoryManager.RegistrationWorksheet_Repository.GetAllOpenCourseByStudentID(studentId);
            list = list.OrderBy(l => l.Priority).ToList();

            return list;
        }

        public static List<RegistrationWorksheet> GetAllAutoAssignCourseByStudentID(int studentId)
        {
            string rawKey = "RegistrationWorksheetAutoAssignCourseByStudentID" + studentId;

            List<RegistrationWorksheet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RegistrationWorksheet_Repository.GetAllAutoAssignCourseByStudentID(studentId);
                list = list.OrderBy(l => l.Priority).ToList();
                if (list != null && list.Count() != 0)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<RegistrationWorksheet> GetByStudentID(int studentId)
        {
            string rawKey = "RegistrationWorksheetByStudentID" + studentId;

            List<RegistrationWorksheet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.RegistrationWorksheet_Repository.GetByStudentID(studentId);
                list = list.OrderBy(l => l.Priority).ToList();
                if (list != null && list.Count() != 0)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<RegistrationWorksheet> GetByProgramId(int programId)
        {
            string rawKey = "RegistrationWorksheetByProgramId" + programId;

            List<RegistrationWorksheet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.RegistrationWorksheet_Repository.GetByProgramId(programId);
                list = list.OrderBy(l => l.Priority).ToList();

                if (list != null && list.Count() != 0)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static bool RegisterCourse(int id, int createdBy, int courseStatusID)
        {
            return RepositoryManager.RegistrationWorksheet_Repository.RegisterCourse(id, createdBy, courseStatusID);
        }

        public static List<RegistrationWorksheet> GetAllByStdProgCal(int studentId, int programId, int batchId)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "RegistrationWorksheetGetAllByStdProgCal" + studentId + programId + batchId;

            List<RegistrationWorksheet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RegistrationWorksheet_Repository.GetAllByStdProgCal(studentId, programId, batchId);
                list = list.OrderBy(l => l.Priority).ToList();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<RegistrationWorksheet> GetAllByProgCal(int programId, int batchId)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "RegistrationWorksheetGetAllByProgCal" + programId + batchId;

            List<RegistrationWorksheet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RegistrationWorksheet_Repository.GetAllByProgCal(programId, batchId);
                list = list.OrderBy(l => l.Priority).ToList();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<RegistrationWorksheet> GetReqByProgCal(int programId, int batchId)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "RegistrationWorksheetGetReqByProgCal" + programId + batchId;

            List<RegistrationWorksheet> list = GetCacheAsListSpecific(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RegistrationWorksheet_Repository.GetReqByProgCal(programId, batchId);
                list = list.OrderBy(l => l.Priority).ToList();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<RegistrationWorksheet> GetPreRegByProgCal(int programId, int batchId)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "RegistrationWorksheetGetPreRegByProgCal" + programId + batchId;

            List<RegistrationWorksheet> list = GetCacheAsListSpecific(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RegistrationWorksheet_Repository.GetPreRegByProgCal(programId, batchId);
                list = list.OrderBy(l => l.Priority).ToList();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<RegistrationWorksheet> GetPreAdByProgCal(int programId, int batchId)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "RegistrationWorksheetGetPreAdByProgCal" + programId + batchId;

            List<RegistrationWorksheet> list = GetCacheAsListSpecific(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RegistrationWorksheet_Repository.GetPreAdByProgCal(programId, batchId);
                list = list.OrderBy(l => l.Priority).ToList();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<RegistrationWorksheet> GetPreRegByCourse(int courseId, int versionId)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "RegistrationWorksheetGetPreRegByCourse" + courseId + versionId;

            List<RegistrationWorksheet> list = GetCacheAsListSpecific(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RegistrationWorksheet_Repository.GetPreRegByCourse(courseId, versionId);
                list = list.OrderBy(l => l.Priority).ToList();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<RegistrationWorksheet> GetReqByCourse(int courseId, int versionId)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "RegistrationWorksheetGetPreReqByCourse" + courseId + versionId;

            List<RegistrationWorksheet> list = GetCacheAsListSpecific(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RegistrationWorksheet_Repository.GetReqByCourse(courseId, versionId);
                list = list.OrderBy(l => l.Priority).ToList();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<RegistrationWorksheet> GetAllByAcaProgCourse(int acaCalId, int programId, string courseList)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "RegistrationWorksheetGetAllByAcaProgCourse" + acaCalId + programId + courseList;

            List<RegistrationWorksheet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RegistrationWorksheet_Repository.GetAllByAcaProgCourse(acaCalId, programId, courseList);
                if (list != null)
                    list = list.OrderBy(l => l.Priority).ToList();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        #region Generate RegistrationWorksheet
        public static bool RegistrationWorksheetGeneratePerStudent(RegistrationWorksheetParam rwParam, int semesterNumber)
        {
            bool isExecute = RepositoryManager.RegistrationWorksheet_Repository.RegistrationWorksheetGeneratePerStudent(rwParam, semesterNumber);
            InvalidateCache();
            HttpRuntime.Cache.Remove("StudentCache");

            return isExecute;
        }

        public static bool RegistrationWorksheetAutoOpen(RegistrationWorksheetParam registrationWorksheetAutoOpen)
        {
            bool isExecute = RepositoryManager.RegistrationWorksheet_Repository.RegistrationWorksheetAutoOpen(registrationWorksheetAutoOpen);
            InvalidateCache();
            HttpRuntime.Cache.Remove("StudentCache");

            return isExecute;
        }

        public static bool RegistrationWorksheetAutoPreRegistration(RegistrationWorksheetParam registrationWorksheetAutoPreReg)
        {
            bool isExecute = RepositoryManager.RegistrationWorksheet_Repository.RegistrationWorksheetAutoPreRegistration(registrationWorksheetAutoPreReg);
            InvalidateCache();
            HttpRuntime.Cache.Remove("StudentCache");

            return isExecute;
        }

        public static bool RegistrationWorksheetAutoMandatory(RegistrationWorksheetParam registrationWorksheetAutoMandatory)
        {
            bool isExecute = RepositoryManager.RegistrationWorksheet_Repository.RegistrationWorksheetAutoMandatory(registrationWorksheetAutoMandatory);
            InvalidateCache();
            HttpRuntime.Cache.Remove("StudentCache");
            return isExecute;
        }
        #endregion

        public static bool UpdateForAssignCourseNew(RegistrationWorksheet registrationWorksheet)
        {

            bool isExecute = RepositoryManager.RegistrationWorksheet_Repository.UpdateForAssignCourseNew(registrationWorksheet);
            InvalidateCache();
            return isExecute;
        }

        public static bool UpdateForAssignCourseRetake(RegistrationWorksheet registrationWorksheet)
        {
            bool isExecute = RepositoryManager.RegistrationWorksheet_Repository.UpdateForAssignCourseRetake(registrationWorksheet);
            InvalidateCache();
            return isExecute;
        }

        public static RegistrationWorksheet GetByStudentIdCourseIdVersionId(int studentId, int courseId, int versionId)
        {
            RegistrationWorksheet registrationWorksheet = RepositoryManager.RegistrationWorksheet_Repository.GetByStudentIdCourseIdVersionId(studentId, courseId, versionId);
             
            return registrationWorksheet;
        }

        public static bool CourseRegistrationForStudent(int studentId)
        {
            bool isExecute = RepositoryManager.RegistrationWorksheet_Repository.CourseRegistrationForStudent(studentId);
            InvalidateCache();
            return isExecute;
        }

        public static bool RegistrationWorksheetGeneratePerStudentBBA(RegistrationWorksheetParam rwParam)
        {
            bool isExecute = RepositoryManager.RegistrationWorksheet_Repository.RegistrationWorksheetGeneratePerStudentBBA(rwParam);
            InvalidateCache();
            HttpRuntime.Cache.Remove("StudentCache");

            return isExecute;
        }

        public static List<RegistrationWorksheet> GetRegistrationSessionDataByStudentID(int studentId)
        {
            string rawKey = "RegistrationWorksheetRegistrationSessionDataByStudentID" + studentId;

            List<RegistrationWorksheet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                list = RepositoryManager.RegistrationWorksheet_Repository.GetRegistrationSessionDataByStudentID(studentId);
                if (list != null)
                    list = list.OrderBy(l => l.Priority).ToList();
                if (list != null && list.Count() != 0)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        //public List<RegistrationWorksheet> GetAll(int acaCalId, int programId)
        public static List<RegistrationWorksheet> GetAll(int acaCalId, int programId)
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            string rawKey = "RegistrationWorksheetGetAllByAcaCalProgramId" + acaCalId + programId;

            List<RegistrationWorksheet> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.RegistrationWorksheet_Repository.GetAll(acaCalId, programId);
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static int CountTakenCourseInRW(int ProgramID, int CourseID, int VersionID, int TreeMasterID)
        {
            int i = RepositoryManager.RegistrationWorksheet_Repository.CountTakenCourseInRW(ProgramID, CourseID, VersionID, TreeMasterID);
            return i;
        }

        public static int CountOpenCourseInRW(int ProgramID, int CourseID, int VersionID, int TreeMasterID)
        {
            int i = RepositoryManager.RegistrationWorksheet_Repository.CountOpenCourseInRW(ProgramID, CourseID, VersionID, TreeMasterID);
            return i;
        }

        public static int CountMandatoryCourseInRW(int ProgramID, int CourseID, int VersionID, int TreeMasterID)
        {
            int i = RepositoryManager.RegistrationWorksheet_Repository.CountMandatoryCourseInRW(ProgramID, CourseID, VersionID, TreeMasterID);
            return i;
        }
        
        public static List<RegistrationWorksheet> GetAllOpenCourseWhichSectionIsMatchInStudentBatchByStudentID(int studentId)
        {
            List<RegistrationWorksheet> list = RepositoryManager.RegistrationWorksheet_Repository.GetAllOpenCourseWhichSectionIsMatchInStudentBatchByStudentID(studentId);
           
            return list;
        }

        public static bool UpdateForSectionTake(RegistrationWorksheet registrationWorksheet)
        {
            bool isExecute = RepositoryManager.RegistrationWorksheet_Repository.UpdateForSectionTake(registrationWorksheet);
            InvalidateCache();
            return isExecute;
        }

        public static bool UpdateForSectionRemove(RegistrationWorksheet registrationWorksheet)
        {
            bool isExecute = RepositoryManager.RegistrationWorksheet_Repository.UpdateForSectionRemove(registrationWorksheet);
            InvalidateCache();
            return isExecute;
        }

        public static List<RegistrationWorksheet> GetAllStudentByProgramSession(int programId, int sessionId)
        {
            List<RegistrationWorksheet> list = RepositoryManager.RegistrationWorksheet_Repository.GetAllStudentByProgramSession(programId, sessionId);
            return list;
        }
        
        public static List<RegistrationWorksheet> GetAllStudentByProgramSessionBatch(int programId, int sessionId, int batchId, int courseId, int versionId)
        {
            List<RegistrationWorksheet> list = RepositoryManager.RegistrationWorksheet_Repository.GetAllStudentByProgramSessionCourse(programId, sessionId, batchId, courseId, versionId);
            return list;
        }

        public static List<StudentBulkRegistration> GetAllStudentWithAllCourseSectionByProgramSessionBatch(int programId, int sessionId, int batchId)
        {
            List<StudentBulkRegistration> list = RepositoryManager.RegistrationWorksheet_Repository.GetAllStudentWithAllCourseSectionByProgramSessionBatch(programId, sessionId, batchId);
            return list;
        }

        public static List<RegistrationWorksheet> GetAllCourseByProgramSessionBatchStudentId(int StudentId,int sessionId)
        {
            List<RegistrationWorksheet> list = RepositoryManager.RegistrationWorksheet_Repository.GetAllCourseByProgramSessionBatchStudentId(StudentId, sessionId);
            return list;
        }

        public static List<RegistrationWorksheet> GetAllForwardCoursesByStudentIDAcaCalId(int studentId, int AcaCalId)
        {
            List<RegistrationWorksheet> list = RepositoryManager.RegistrationWorksheet_Repository.GetAllForwardCoursesByStudentIDAcaCalId(studentId, AcaCalId);

            return list;
        }

        public static bool IsForwardOrRegistrationDoneForStudent(int StudentId,int AcaCalId,int IsRetake)
        {
            bool isExecute = RepositoryManager.RegistrationWorksheet_Repository.IsForwardOrRegistrationDoneForStudent(StudentId,AcaCalId,IsRetake); 
            return isExecute;
        }

    }
}
