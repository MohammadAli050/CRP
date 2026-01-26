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
    public class StudentACUDetailManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentACUDetailCache" };
        const double CacheDuration = 30.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<StudentACUDetail> GetCacheAsList(string rawKey)
        {
            List<StudentACUDetail> list = (List<StudentACUDetail>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static StudentACUDetail GetCacheItem(string rawKey)
        {
            StudentACUDetail item = (StudentACUDetail)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return item;
        }

        public static void AddCacheItem(string rawKey, object value)
        {
            System.Web.Caching.Cache DataCache = HttpRuntime.Cache;
                       
            if (DataCache[MasterCacheKeyArray[0]] == null)
                DataCache[MasterCacheKeyArray[0]] = DateTime.Now;
                       
            System.Web.Caching.CacheDependency dependency = new System.Web.Caching.CacheDependency(null, MasterCacheKeyArray);
            DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddMinutes(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        }
        
        public static void InvalidateCache()
        {  
            HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        }

        #endregion


        public static int Insert(StudentACUDetail studentACUDetail)
        {
            int id = RepositoryManager.StudentACUDetail_Repository.Insert(studentACUDetail);
            InvalidateCache();
            return id;
        }

        public static bool Update(StudentACUDetail studentACUDetail)
        {
            bool isExecute = RepositoryManager.StudentACUDetail_Repository.Update(studentACUDetail);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentACUDetail_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static StudentACUDetail GetById(int id)
        {
            string rawKey = "StudentACUDetailById" + id;
            StudentACUDetail studentACUDetail = GetCacheItem(rawKey);

            if (studentACUDetail == null)
            { 
                studentACUDetail = RepositoryManager.StudentACUDetail_Repository.GetById(id);
                if (studentACUDetail != null)
                    AddCacheItem(rawKey, studentACUDetail);
            }

            return studentACUDetail;
        }

        public static List<StudentACUDetail> GetAll(int studentId)
        {

            List<StudentACUDetail> list = list = RepositoryManager.StudentACUDetail_Repository.GetAll(studentId);

            return list;
        }

        public static List<StudentACUDetail> GetAllByStudentId(int studentId)
        {

            List<StudentACUDetail> list = null;

            if (list == null)
            {
                list = GetAll(studentId);
                if (list != null && list.Count() != 0)
                    list = list.Where(s => s.StudentID==studentId).ToList();

            }

            return list;
        }

        public static StudentACUDetail GetCurrentCgpaByStudentId(int studentId)
        {
            string rawKey = "StudentACUDetailGetLastCgpaByStudentId" + studentId;

             StudentACUDetail  obj = GetCacheItem(rawKey);

             if (obj == null)
            {
                List<StudentACUDetail> list = GetAllByStudentId(studentId);
                if (list != null && list.Count() != 0)
                {
                    var maxAcaCalId = list.Max(l => l.StdAcademicCalenderID);
                    obj =  list.Where(s => s.StdAcademicCalenderID == maxAcaCalId).Single();  
                }

                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }
        
        public static StudentACUDetail GetByStudentIdAndBatchId(int studentId, int batchId)
        {
            string rawKey = "StudentACUDetailGetByStudentIdAndBatchId" + studentId + batchId;

            StudentACUDetail obj = GetCacheItem(rawKey);

            if (obj == null)
            {
                List<StudentACUDetail> list = GetAllByStudentId(studentId);
                if (list != null && list.Count() !=0)
                    list = list.Where(s => s.StdAcademicCalenderID == batchId).ToList();
                if (list != null && list.Count() != 0)
                    obj = list.Single();

                if (obj != null )
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static int UpdateByAcaCalRoll(int studentId, int acaCalId)
        {
            int id = RepositoryManager.StudentACUDetail_Repository.UpdateByAcaCalRoll(studentId, acaCalId);
            InvalidateCache();
            return id;
        }

        internal static StudentACUDetail GetAllByStudentAndAcaCal(int currentAcaCal, int studentID)
        {
            throw new NotImplementedException();
        }

        public static StudentACUDetail GetLatestCGPAByStudentId(int studentId)
        {
            string rawKey = "StudentACUDetailGetLatestCGPAByStudnetId" + studentId;
            StudentACUDetail studentACUDetail = GetCacheItem(rawKey);

            if (studentACUDetail == null)
            {
                studentACUDetail = RepositoryManager.StudentACUDetail_Repository.GetLatestCGPAByStudentId(studentId);
                if (studentACUDetail != null)
                    AddCacheItem(rawKey, studentACUDetail);
            }

            return studentACUDetail;
        }
        public static int Calculate_GPAandCGPAByRoll(string roll)
        {
            int result = RepositoryManager.StudentACUDetail_Repository.Calculate_GPAandCGPAByRoll(roll);
            InvalidateCache();
            return result;
        }

        public static int Calculate_GPAandCGPAByBatch(string batch)
        {
            int result = RepositoryManager.StudentACUDetail_Repository.Calculate_GPAandCGPAByBatch(batch);
            InvalidateCache();
            return result;
        }

        public static int Calculate_GPAandCGPA_Bulk()
        {
            int result = RepositoryManager.StudentACUDetail_Repository.Calculate_GPAandCGPA_Bulk();
            InvalidateCache();
            return result;
        }

        public static string Calculate_GpaCgpa(int acaCalId, int programId, int batchId, string studentId)
        {
            string result = RepositoryManager.StudentACUDetail_Repository.Calculate_GpaCgpa(acaCalId, programId, batchId, studentId);
            InvalidateCache();
            return result;
        }

        public static List<StudentACUDetail> GetAllByAcaCalProgramBatchStudent(int acaCalId, int programId, int batchId, string studentId)
        {
            return RepositoryManager.StudentACUDetail_Repository.GetAllByAcaCalProgramBatchStudent(acaCalId, programId, batchId, studentId);
        }
        public static List<StudentACUDetail> GetAllByAcaCalProgramBatchStudentForRemarks(int semesterNo, int programId, int batchId, string studentId)
        {
            return RepositoryManager.StudentACUDetail_Repository.GetAllByAcaCalProgramBatchStudentForRemarks(semesterNo, programId, batchId, studentId);
        }
        public static List<rStudentMeritList> GetMeritListByProgramSessionBatch(int programId, int acaCalId, int batchId) 
        {
            List<rStudentMeritList> studentMeritList = RepositoryManager.StudentACUDetail_Repository.GetMeritListByProgramSessionBatch(programId, acaCalId, batchId);
            return studentMeritList;
        }
    }
}
