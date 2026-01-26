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
    public class OfferedCourseManager
    {
        #region Cache

        //    public static readonly string[] MasterCacheKeyArray = { "OfferedCourseCache" };
        //    const double CacheDuration = 5;

        //    public static string GetCacheKey(string cacheKey)
        //    {
        //        return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        //    }

        //    public static List<OfferedCourse> GetCacheAsList(string rawKey)
        //    {
        //        List<OfferedCourse> list = (List<OfferedCourse>)HttpRuntime.Cache[GetCacheKey(rawKey)];
        //        return list;
        //    }

        //    public static OfferedCourse GetCacheItem(string rawKey)
        //    {
        //        OfferedCourse item = (OfferedCourse)HttpRuntime.Cache[GetCacheKey(rawKey)];
        //        return item;
        //    }

        //    public static void AddCacheItem(string rawKey, object value)
        //    {
        //        System.Web.Caching.Cache DataCache = HttpRuntime.Cache;

        //        // Make sure MasterCacheKeyArray[0] is in the cache - if not, add it
        //        if (DataCache[MasterCacheKeyArray[0]] == null)
        //            DataCache[MasterCacheKeyArray[0]] = DateTime.Now;

        //        // Add a CacheDependency
        //        System.Web.Caching.CacheDependency dependency = new System.Web.Caching.CacheDependency(null, MasterCacheKeyArray);
        //        DataCache.Insert(GetCacheKey(rawKey), value, dependency, DateTime.Now.AddSeconds(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        //    }

        //    public static void InvalidateCache()
        //    {
        //        // Remove the cache dependency
        //        HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        //    }

        #endregion

        public static int Insert(OfferedCourse offeredCourse)
        {
            int id = RepositoryManager.OfferedCourse_Repository.Insert(offeredCourse);
            // InvalidateCache();
            return id;
        }

        public static int InsertList(List<OfferedCourse> offeredCourseList)
        {
            int id = RepositoryManager.OfferedCourse_Repository.InsertList(offeredCourseList);
            // InvalidateCache();
            return id;
        }

        public static bool Update(OfferedCourse offeredCourse)
        {
            bool isExecute = RepositoryManager.OfferedCourse_Repository.Update(offeredCourse);
            // InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.OfferedCourse_Repository.Delete(id);
            // InvalidateCache();
            return isExecute;
        }

        public static bool DeleteByProgIdAndAcaCalId(int programId, int acaCalId)
        {
            bool isExecute = RepositoryManager.OfferedCourse_Repository.DeleteByProgIdAndAcaCalId(programId, acaCalId);
            // InvalidateCache();
            return isExecute;
        }

        public static bool DeleteByProgramAndAcaCalAndtreeRoot(int programId, int acaCalId, int treeRoot)
        {
            bool isExecute = RepositoryManager.OfferedCourse_Repository.DeleteByProgramAndAcaCalAndTreeRoot(programId, acaCalId, treeRoot);

            return isExecute;
        }

        public static OfferedCourse GetById(int id)
        {
            // return RepositoryAdmission.Program_Repository.GetById(id);

            //string rawKey = "OfferedCourseById" + id;
            //OfferedCourse offeredCourse = GetCacheItem(rawKey);

            //if (offeredCourse == null)
            //{
            //    // Item not found in cache - retrieve it and insert it into the cache
            OfferedCourse offeredCourse = RepositoryManager.OfferedCourse_Repository.GetById(id);
            //    if (offeredCourse != null)
            //        AddCacheItem(rawKey, offeredCourse);
            //}

            return offeredCourse;
        }

        public static List<OfferedCourse> GetAll()
        {
            //const string rawKey = "OfferedCourseGetAll";

            //List<OfferedCourse> list = GetCacheAsList(rawKey);

            //if (list == null)
            //{
            List<OfferedCourse> list = RepositoryManager.OfferedCourse_Repository.GetAll();
            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            return list;
        }

        public static List<OfferedCourse> GetAllByProgramId(int programId)
        {
            // string rawKey = "OfferedCourseGetAllBy" + programId;

            //List<OfferedCourse> list = GetCacheAsList(rawKey);

            //if (list == null)
            //{
            List<OfferedCourse> list = GetAll();
            list = list.Where(oc => oc.ProgramID == programId).ToList();

            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            return list;
        }

        public static List<OfferedCourse> GetAllByProgramIdAcaCalId(int programId, int acaCalId)
        {
            //string rawKey = "OfferedCourseGetAllBy" + programId + acaCalId;

            //List<OfferedCourse> list = GetCacheAsList(rawKey);

            //if (list == null)
            //{
            List<OfferedCourse> list = GetAll();
            list = list.Where(oc => oc.ProgramID == programId && oc.AcademicCalenderID == acaCalId).ToList();

            //    if (list != null)
            //        AddCacheItem(rawKey, list);
            //}

            return list;
        }

        public static List<OfferedCourse> GetAllByProgramIdAcaCalIdTreeRootID(int programId, int acaCalId, int treeRootID)
        {
            List<OfferedCourse> list = GetAll();
            list = list.Where(oc => oc.ProgramID == programId && oc.AcademicCalenderID == acaCalId && oc.TreeRootID == treeRootID).ToList();
            return list;
        }

        public static bool UpdateList(List<OfferedCourse> offeredCourseList)
        {
            bool isExecute = RepositoryManager.OfferedCourse_Repository.UpdateList(offeredCourseList);
            // InvalidateCache();
            return isExecute;
        }

        public static bool ActiveInactiveList(List<OfferedCourse> offeredCourseList)
        {
            bool isExecute = RepositoryManager.OfferedCourse_Repository.ActiveInactiveList(offeredCourseList);
            // InvalidateCache();
            return isExecute;
        }

        public static List<OfferedCourse> GetAllByProgramAcacalTreeroot(int programId, int acaCalId, int treeRoot)
        {
            List<OfferedCourse> list = RepositoryManager.OfferedCourse_Repository.GetAllByProgramAcacalTreeroot(programId, acaCalId, treeRoot);
            return list;
        }

        public static List<OfferedCourseDTO> GetAllDtoObjByProgramAcacalTreeroot(int programId, int acaCalId, int treeRoot)
        {
            List<OfferedCourseDTO> list = RepositoryManager.OfferedCourse_Repository.GetAllDtoObjByProgramAcacalTreeroot(programId, acaCalId, treeRoot);
            return list;
        }

        public static OfferedCourse GetBy(int ProgramID, int AcademicCalenderID, int TreeMasterID, int CourseID, int VersionID)
        {
            OfferedCourse offeredCourse = RepositoryManager.OfferedCourse_Repository.GetBy(ProgramID, AcademicCalenderID, TreeMasterID, CourseID, VersionID);
            return offeredCourse;
        }

        public static List<WorkSheetCourseHistoryDTO> GetStudentByProgramCourseVersionSection(int acaCalId, int programId, int courseId, int versionId, int sectionId)
        {
            return RepositoryManager.OfferedCourse_Repository.GetStudentByProgramCourseVersionSection(acaCalId, programId, courseId, versionId, sectionId);
        }
    }
}
