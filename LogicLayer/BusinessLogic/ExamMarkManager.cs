using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.RO;
using System.Data;

namespace LogicLayer.BusinessLogic
{
    public class ExamMarkManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentResultCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<ExamMark> GetCacheAsList(string rawKey)
        {
            List<ExamMark> list = (List<ExamMark>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamMark GetCacheItem(string rawKey)
        {
            ExamMark item = (ExamMark)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(ExamMark examMark)
        {
            int id = RepositoryManager.StudentResult_Repository.Insert(examMark);
            InvalidateCache();
            return id;
        }

        public static bool Update(ExamMark examMark)
        {
            bool isExecute = RepositoryManager.StudentResult_Repository.Update(examMark);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.StudentResult_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static ExamMark GetById(int id)
        {
            string rawKey = "StudentResultByID" + id;
            ExamMark studentresult = GetCacheItem(rawKey);

            if (studentresult == null)
            {
                studentresult = RepositoryManager.StudentResult_Repository.GetById(id);
                if (studentresult != null)
                    AddCacheItem(rawKey, studentresult);
            }

            return studentresult;
        }

        public static List<ExamMark> GetAll()
        {
            // return RepositoryAdmission.Program_Repository.GetAll();

            const string rawKey = "StudentResultGetAll";

            List<ExamMark> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.StudentResult_Repository.GetAll();
                if (list != null)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<ExamMark> GetAllStudentByCourseSection(int courseId, int sectionId, int templateId)
        {
            List<ExamMark> list = RepositoryManager.StudentResult_Repository.GetAllStudentByCourseSection(courseId, sectionId, templateId);
            return list;
        }

        public static List<ExamMarkDTO> GetCacheAsStudentList(string rawKey)
        {
            List<ExamMarkDTO> list = (List<ExamMarkDTO>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static ExamMark GetStudentMarkByExamId(int courseHistoryId, int examId)
        {
            //string rawKey = "GetStudentMarkByCourseSectionTest" + id;
            ExamMark studentresult = RepositoryManager.StudentResult_Repository.GetStudentMarkByExamId(courseHistoryId, examId);
            return studentresult;
        }

        public static List<ExamMarkDTO> GetAllStudentByAcaCalAcaCalSecExam(int acaCalId, int acaCalSecId, int examId)
        {
            return RepositoryManager.StudentResult_Repository.GetAllStudentByAcaCalAcaCalSecExam(acaCalId, acaCalSecId, examId);
        }

        public static List<ExamMarkDTO> GetAllStudentByAcaCalAcaCalSec(int acaCalId, int acaCalSecId)
        {
            return RepositoryManager.StudentResult_Repository.GetAllStudentByAcaCalAcaCalSec(acaCalId, acaCalSecId);
        }

        public static int GetTotalUpdateNumberIsFinalSubmit(int acaCalId, int acaCalSecId)
        {
            return RepositoryManager.StudentResult_Repository.GetTotalUpdateNumberIsFinalSubmit(acaCalId, acaCalSecId);
        }

        public static string GetApprovedNumberByExamController(int acaCalSectionID)
        {
            return RepositoryManager.StudentResult_Repository.GetApprovedNumberByExamController(acaCalSectionID);
        }

        public static string GetApprovedNumberByExamControllerByAcaCalSecRoll(int acaCalSectionID, string roll)
        {
            return RepositoryManager.StudentResult_Repository.GetApprovedNumberByExamControllerByAcaCalSecRoll(acaCalSectionID, roll);
        }

        public static string GetShortSummaryReport(int acaCalId, int programId)
        {
            return RepositoryManager.StudentResult_Repository.GetShortSummaryReport(acaCalId, programId);
        }

        public static int GetTotalUpdateNumberIsTransfer(int acaCalId, int acaCalSecId)
        {
            return RepositoryManager.StudentResult_Repository.GetTotalUpdateNumberIsTransfer(acaCalId, acaCalSecId);
        }

        public static List<rExamResultPrintTheory> GetExamResultPrintTheory(DataTable table, string column1, string column2, string column3, string totalColumn)
        {
            try
            {
                List<rExamResultPrintTheory> examResultPrintTheory = new List<rExamResultPrintTheory>();

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    //3
                    rExamResultPrintTheory temp = new rExamResultPrintTheory();

                    temp.Name = "";
                    temp.Roll = table.Rows[i]["Student Roll"].ToString();
                    temp.Marks1 = table.Rows[i][column1].ToString();
                    //if (temp.Marks1 == "-1") { temp.Marks1 = "ab"; } else { temp.Marks1 = table.Rows[i][column1].ToString(); }
                    temp.Marks2 = table.Rows[i][column2].ToString();
                    //if (temp.Marks2 == "-1") { temp.Marks2 = "ab"; } else { temp.Marks2 = table.Rows[i][column2].ToString(); }
                    temp.Marks3 = table.Rows[i][column3].ToString();
                    //if (temp.Marks3 == "-1") { temp.Marks3 = "ab"; } else { temp.Marks3 = table.Rows[i][column3].ToString(); }
                    temp.Total = table.Rows[i][totalColumn].ToString();
                    temp.Grade = table.Rows[i]["Grade"].ToString();
                    temp.Point = table.Rows[i]["Point"].ToString();

                    examResultPrintTheory.Add(temp);
                }

                return examResultPrintTheory;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public static List<rGradeOnly> GetGradeOnly(DataTable table, string totalColumn)
        {
            List<rGradeOnly> gradeOnly = new List<rGradeOnly>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                //Grade Only
                rGradeOnly temp = new rGradeOnly();

                temp.Name = "";
                temp.Roll = table.Rows[i]["Student Roll"].ToString();
                temp.Total = table.Rows[i][totalColumn].ToString();
                temp.Grade = table.Rows[i]["Grade"].ToString();
                temp.Point = table.Rows[i]["Point"].ToString();

                gradeOnly.Add(temp);
            }
            return gradeOnly;
        }

        public static List<rGradeOnly> GetTotalMarksStudentResult(DataTable table)
        {
            List<rGradeOnly> studentTotalMarkList = new List<rGradeOnly>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                rGradeOnly rGradeOnlyObj = new rGradeOnly();
                rGradeOnlyObj.Name = "";
                rGradeOnlyObj.Roll = table.Rows[i]["Student Roll"].ToString();
                rGradeOnlyObj.Total = table.Rows[i]["Total Marks(100)"].ToString();
                rGradeOnlyObj.Total = table.Rows[i]["Total(100)"].ToString();
                rGradeOnlyObj.Grade = table.Rows[i]["Grade"].ToString();
                rGradeOnlyObj.Point = table.Rows[i]["Point"].ToString();

                studentTotalMarkList.Add(rGradeOnlyObj);
            }
            return studentTotalMarkList;
        }

        public static List<rExamResultPrintLab> GetExamResultPrintLab(DataTable table, string column1, string column2, string column3, string column4, string totalColumn)
        {
            List<rExamResultPrintLab> examResultPrintLab = new List<rExamResultPrintLab>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                //4
                rExamResultPrintLab temp = new rExamResultPrintLab();

                temp.Name = "";
                temp.Roll = table.Rows[i]["Student Roll"].ToString();
                temp.Marks1 = table.Rows[i][column1].ToString();
                //if (temp.Marks1 == "-1") { temp.Marks1 = "ab"; } else { temp.Marks1 = table.Rows[i][column1].ToString(); }
                temp.Marks2 = table.Rows[i][column2].ToString();
                //if (temp.Marks2 == "-1") { temp.Marks2 = "ab"; } else { temp.Marks2 = table.Rows[i][column2].ToString(); }
                temp.Marks3 = table.Rows[i][column3].ToString();
                //if (temp.Marks3 == "-1") { temp.Marks3 = "ab"; } else { temp.Marks3 = table.Rows[i][column3].ToString(); }
                temp.Marks4 = table.Rows[i][column4].ToString();
                //if (temp.Marks4 == "-1") { temp.Marks4 = "ab"; } else { temp.Marks4 = table.Rows[i][column4].ToString(); }
                temp.Total = table.Rows[i][totalColumn].ToString();
                temp.Grade = table.Rows[i]["Grade"].ToString();
                temp.Point = table.Rows[i]["Point"].ToString();

                examResultPrintLab.Add(temp);
            }
            return examResultPrintLab;
        }

        public static List<rExamResultPrintSpecial> GetExamResultPrintLabSpecial(DataTable table, string column1, string column2, string column3, string column4, string column5, string column6, string column7, string totalColumn)
        {
            List<rExamResultPrintSpecial> examResultPrintSpecial = new List<rExamResultPrintSpecial>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                //Special
                rExamResultPrintSpecial temp = new rExamResultPrintSpecial();

                temp.Name = "";
                temp.Roll = table.Rows[i]["Student Roll"].ToString();
                temp.Marks1 = table.Rows[i][column1].ToString();
                temp.Marks2 = table.Rows[i][column2].ToString();
                temp.Marks3 = table.Rows[i][column3].ToString();
                temp.Marks4 = table.Rows[i][column4].ToString();
                temp.Marks5 = table.Rows[i][column5].ToString();
                temp.Marks6 = table.Rows[i][column6].ToString();
                temp.Marks7 = table.Rows[i][column7].ToString();
                temp.Total = table.Rows[i][totalColumn].ToString();
                temp.Grade = table.Rows[i]["Grade"].ToString();
                temp.Point = table.Rows[i]["Point"].ToString();

                examResultPrintSpecial.Add(temp);
            }
            return examResultPrintSpecial;
        }

        public static List<rExamResultPrintTheorySpecial> GetExamResultPrintTheorySpecial(DataTable table, string column1, string column2, string column3, string column4, string column5, string column6, string totalColumn)
        {
            List<rExamResultPrintTheorySpecial> examResultPrintTheorySpecialList = new List<rExamResultPrintTheorySpecial>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                //Special
                rExamResultPrintTheorySpecial temp = new rExamResultPrintTheorySpecial();

                temp.Name = "";
                temp.Roll = table.Rows[i]["Student Roll"].ToString();
                temp.Marks1 = table.Rows[i][column1].ToString();
                temp.Marks2 = table.Rows[i][column2].ToString();
                temp.Marks3 = table.Rows[i][column3].ToString();
                temp.Marks4 = table.Rows[i][column4].ToString();
                temp.Marks5 = table.Rows[i][column5].ToString();
                temp.Marks6 = table.Rows[i][column6].ToString();
                temp.Total = table.Rows[i][totalColumn].ToString();
                temp.Grade = table.Rows[i]["Grade"].ToString();
                temp.Point = table.Rows[i]["Point"].ToString();
                examResultPrintTheorySpecialList.Add(temp);
            }
            return examResultPrintTheorySpecialList;
        }

        public static List<rExamResultCourseAndTeacherInfo> GetExamResultCourseAndTeacherInfo(int acaCalSecId, int acaCalId)
        {
            List<rExamResultCourseAndTeacherInfo> list = RepositoryManager.StudentResult_Repository.GetExamResultCourseAndTeacherInfo(acaCalSecId, acaCalId);
            return list;
        }
        public static List<rStudentGradePrevious> GetStudentGradeReportPrevious(string roll, int acaCalId)
        {
            List<rStudentGradePrevious> list = RepositoryManager.StudentResult_Repository.GetStudentGradeReportPrevious(roll, acaCalId);
            return list;
        }

        public static List<rStudentGradePreviousNew> GetStudentGradeReportPreviousNew(string roll, int acaCalId)
        {
            List<rStudentGradePreviousNew> list = RepositoryManager.StudentResult_Repository.GetStudentGradeReportPreviousNew(roll, acaCalId);
            return list;
        }

        public static List<rStudentGradeCurrent> GetStudentGradeReportCurrent(string roll, int acaCalId)
        {
            List<rStudentGradeCurrent> list = RepositoryManager.StudentResult_Repository.GetStudentGradeReportCurrent(roll, acaCalId);
            return list;
        }
        public static List<rStudentGradeCurrent> GetStudentGradeReportCurrentNew(string roll, int acaCalId)
        {
            List<rStudentGradeCurrent> list = RepositoryManager.StudentResult_Repository.GetStudentGradeReportCurrentNew(roll, acaCalId);
            return list;
        }
        public static List<ExamMarkApproveDTO> GetAllAcaCalIdProgramIdCourseIdVersionId(int acaCalId, int programId, int courseId, int versionId)
        {
            return RepositoryManager.StudentResult_Repository.GetAllAcaCalIdProgramIdCourseIdVersionId(acaCalId, programId, courseId, versionId);
        }

        public static int GetResubmitApprovedByExamController(string actionType, int acaCalSectionID)
        {
            return RepositoryManager.StudentResult_Repository.GetResubmitApprovedByExamController(actionType, acaCalSectionID);
        }

        public static int GetPublishNumberByExamController(int acaCalId, int programId)
        {
            return RepositoryManager.StudentResult_Repository.GetPublishNumberByExamController(acaCalId, programId);
        }

        public static List<rGradeOnly> GetGradeOnlyStudentResult(int acaCalSectionId)
        {
            List<rGradeOnly> list = RepositoryManager.StudentResult_Repository.GetGradeOnlyStudentResult(acaCalSectionId);
            return list;
        }

        public static List<StudentGrade> GetGradeReportByRollSesmester(string roll)
        {
            List<StudentGrade> list = RepositoryManager.StudentResult_Repository.GetGradeReportByRollSesmester(roll);
            return list;
        }

        public static List<StudentGrade> GetGradeReportByRollSession(string roll,int SessionId)
        {
            List<StudentGrade> list = RepositoryManager.StudentResult_Repository.GetGradeReportByRollSession(roll,SessionId);
            return list;
        }

        public static List<rCreditGPA> GetGradeReportCreditGPAByRoll(int ProgramId, int BatchId, string roll)
        {
            List<rCreditGPA> list = RepositoryManager.StudentResult_Repository.GetGradeReportCreditGPAByRoll(ProgramId, BatchId, roll);
            return list;
        }

        public static List<StudentGrade> GetGradeReportByRollSemesterNo(string roll, int semesterId)
        {
            List<StudentGrade> list = RepositoryManager.StudentResult_Repository.GetGradeReportByRollSemesterNo(roll, semesterId);
            return list;
        }
    }
}

