using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.DataLogic.DAFactory;
using System.Text.RegularExpressions;
using LogicLayer.BusinessObjects.RO;
using LogicLayer.BusinessObjects.DTO;


namespace LogicLayer.BusinessLogic
{
    public class StudentManager
    {
        #region Cache

        public static readonly string[] MasterCacheKeyArray = { "StudentCache" };
        const double CacheDuration = 5.0;

        public static string GetCacheKey(string cacheKey)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }

        public static List<Student> GetCacheAsList(string rawKey)
        {
            List<Student> list = (List<Student>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static List<RunningStudent> GetRunningStudentCacheAsList(string rawKey)
        {
            List<RunningStudent> list = (List<RunningStudent>)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return list;
        }

        public static Student GetCacheItem(string rawKey)
        {
            Student item = (Student)HttpRuntime.Cache[GetCacheKey(rawKey)];
            return item;
        }

        public static LoadStudentByIdDTO GetCacheItemEdit(string rawKey)
        {
            LoadStudentByIdDTO item = (LoadStudentByIdDTO)HttpRuntime.Cache[GetCacheKey(rawKey)];
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

        public static int Insert(Student obj)
        {
            int id = RepositoryManager.Student_Repository.Insert(obj);
            InvalidateCache();
            return id;
        }

        public static bool Update(Student obj)
        {
            bool isExecute = RepositoryManager.Student_Repository.Update(obj);
            InvalidateCache();
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.Student_Repository.Delete(id);
            InvalidateCache();
            return isExecute;
        }

        public static Student GetById(int? id)
        {
            // return RepositoryAdmission.Student_Repository.GetById(id);

            string rawKey = "StudentById" + id;
            Student obj = GetCacheItem(rawKey);

            if (obj == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                obj = RepositoryManager.Student_Repository.GetById(id);
                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static Student GetBypersonID(int PersonID)
        {
            // return RepositoryAdmission.Student_Repository.GetById(id);

            string rawKey = "StudentByPersonID" + PersonID;
            Student obj = GetCacheItem(rawKey);

            if (obj == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                obj = RepositoryManager.Student_Repository.GetByPersonID(PersonID);
                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static Student GetByRoll(string roll)
        {
            string rawKey = "StudentByRoll" + roll;
            Student obj = GetCacheItem(rawKey);

            if (obj == null)
            {
                obj = RepositoryManager.Student_Repository.GetByRoll(roll);
                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static Student GetByRollOrSerialNo(string roll, int serialNo)
        {
            Student std = RepositoryManager.Student_Repository.GetByRollOrSerialNo(roll, serialNo);
            return std;
        }

        public static LoadStudentByIdDTO GetByRollEdit(string roll)
        {
            string rawKey = "StudentByRollEdit" + roll;
            LoadStudentByIdDTO obj = GetCacheItemEdit(rawKey);

            if (obj == null)
            {
                obj = RepositoryManager.Student_Repository.GetByRollEdit(roll);
                if (obj != null)
                    AddCacheItem(rawKey, obj);
            }

            return obj;
        }

        public static List<Student> GetAll()
        {
            // return RepositoryAdmission.Student_Repository.GetAll();

            const string rawKey = "StudentGetAll";

            List<Student> list = GetCacheAsList(rawKey);

            if (list == null)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Student_Repository.GetAll();
                if (list != null && list.Count() != 0)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<Student> GetAllByNameOrID(string searchKey)
        {
            List<Student> list = RepositoryManager.Student_Repository.GetAllByNameOrID(searchKey);
            return list;
        }

        public static List<Student> GetAllByProgramIdBatchId(int programID, int batchID)
        {
            // return RepositoryAdmission.Student_Repository.GetAll();

            //string rawKey = "StudentGetAll" + programID + batchID;

            List<Student> list = new List<Student>(); //GetCacheAsList(rawKey);

            //if (list == null || list.Count() == 0)
            //{
            // Item not found in cache - retrieve it and insert it into the cache
            list = RepositoryManager.Student_Repository.GetAllByProgramIdBatchId(programID, batchID);
            //    if (list != null && list.Count() != 0)
            //        AddCacheItem(rawKey, list);
            //}

            return list;
        }

        public static List<Student> GetAllFromRegWorksheetByProgramAndBatch(int programId, int acaCalId)
        {
            //string rawKey = "StudentGetAllFromRegWorksheetByProgramAndBatch" + programId + acaCalId;

            List<Student> list = new List<Student>(); //GetCacheAsList(rawKey);

            //if (list == null || list.Count() == 0)
            //{
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Student_Repository.GetAllFromRegWorksheetByProgramAndBatch(programId, acaCalId);
            //    if (list != null && list.Count() != 0)
            //        AddCacheItem(rawKey, list);
            //}

            return list;
        }

        public static List<Student> GetFromRegWorksheetByStudentRoll(string roll)
        {
            List<Student> list = RepositoryManager.Student_Repository.GetFromRegWorksheetByStudentRoll(roll);
            return list;
        }

        public static List<Student> GetAllByProgramOrBatchOrRollRange(int programId, int acaCalId, string roll)
        {
            int result;
            string rollFrom = string.Empty;
            string rollTo = string.Empty;

            if (!string.IsNullOrEmpty(roll))
            {
                string rolwithOutspace = roll.Replace(" ", "");
                string[] programBatch = rolwithOutspace.Split('-');


                if (programBatch.Count() > 1)
                {
                    string[] rolSl = Regex.Split(programBatch[1], "to");

                    rollFrom = programBatch[0] + rolSl[0];
                    rollTo = Int32.TryParse(rolSl[1], out  result) ? programBatch[0] + rolSl[1] : rollFrom;

                    if (rollFrom.Contains("_"))
                    {
                        rollFrom = string.Empty;
                    }
                    if (rollTo.Contains("_"))
                    {
                        rollTo = string.Empty;
                    }
                }
                else
                {
                    rollFrom = programBatch[0].Substring(0, 9);

                    rollTo = programBatch[0].Length == 12 ? programBatch[0].Substring(0, 6) + programBatch[0].Substring(9, 3) : rollFrom;
                }
            }

            List<Student> list = RepositoryManager.Student_Repository.GetAllByProgramOrBatchOrRoll(programId, acaCalId, rollFrom, rollTo);
            return list;
        }

        public static List<Student> GetAllByProgramOrBatchOrRoll(int programId, int batchId, string roll)
        {
            List<Student> list = RepositoryManager.Student_Repository.GetAllByProgramOrBatchOrRoll(programId, batchId, roll);
            return list;
        }

        public static List<rStudentBatchWise> GetStudentProgramOrBatch(int programId, int batchId)
        {
            List<rStudentBatchWise> list = RepositoryManager.Student_Repository.GetStudentProgramOrBatch(programId, batchId);
            return list;
        }

        public static List<rStudentBatchWise> GetStudentYearOrSemesterOrCalenderUnitOrProgramOrBatch(string year, int semesterId, int calenderUnitTypeId, int programId, int batchId)
        {
            List<rStudentBatchWise> list = RepositoryManager.Student_Repository.GetStudentYearOrSemesterOrCalenderUnitOrProgramOrBatch(year, semesterId, calenderUnitTypeId, programId, batchId);
            return list;
        }
        public static List<Student> GetAllByBatchProgram(string batch, string program)
        {
            string rawKey = "StudentGetAllByBatchProgram" + batch + program;

            List<Student> list = GetCacheAsList(rawKey);

            if (list == null || list.Count() == 0)
            {
                // Item not found in cache - retrieve it and insert it into the cache
                list = RepositoryManager.Student_Repository.GetAllByBatchProgram(batch, program);
                if (list != null && list.Count() != 0)
                    AddCacheItem(rawKey, list);
            }

            return list;
        }

        public static List<Student> StudentGetAllActiveInactiveWithRegistrationStatus(int programId, int acaCalBatchId, int acaCalSessionId, string roll)
        {
            List<Student> list = RepositoryManager.Student_Repository.StudentGetAllActiveInactiveWithRegistrationStatus(programId, acaCalBatchId, acaCalSessionId, roll);
            return list;
        }

        public static List<Student> GetAllRegisteredStudentBySession(int acaCalSessionId)
        {
            List<Student> list = RepositoryManager.Student_Repository.GetAllRegisteredStudentBySession(acaCalSessionId);
            return list;
        }

        #region Report Method
        public static List<rStudentMajorDefine> GetAllStudentByMajorDefine(int programId, int batchId, string roll)
        {
            List<rStudentMajorDefine> list = RepositoryManager.Student_Repository.GetAllStudentByMajorDefine(programId, batchId, roll);
            return list;
        }
        #endregion

        public static List<StudentBlockCountByProgramDTO> GetAllBlockStudentByProgram()
        {
            List<StudentBlockCountByProgramDTO> list = RepositoryManager.Student_Repository.GetAllBlockStudentByProgram();
            return list;
        }

        public static bool DeleteAllBlockStudentByProgram(int programId)
        {
            bool isExecute = RepositoryManager.Student_Repository.DeleteAllBlockStudentByProgram(programId);
            return isExecute;
        }

        public static List<StudentBlockCountByProgramDTO> GetAllInActiveStudentByProgram()
        {
            List<StudentBlockCountByProgramDTO> list = RepositoryManager.Student_Repository.GetAllInActiveStudentByProgram();
            return list;
        }

        public static bool UpdateInActiveToActiveByProgram(int programId)
        {
            bool isExecute = RepositoryManager.Student_Repository.UpdateInActiveToActiveByProgram(programId);
            return isExecute;
        }

        public static List<StudentProbationDTO> GetAllByProbationStatus(int programId, int acaCalBatchId, int? minProb, int? maxProb)
        {
            List<StudentProbationDTO> list = RepositoryManager.Student_Repository.GetAllByProbationStatus(programId, acaCalBatchId, minProb, maxProb);
            return list;
        }

        public static List<rStudentTranscript> GetStudentTrancriptById(string studentId)
        {
            List<rStudentTranscript> list = RepositoryManager.Student_Repository.GetStudentTrancriptById(studentId);
            return list;
        }
        public static List<rStudentTranscriptNew> GetStudentTrancriptByIdNew(string studentId)
        {
            List<rStudentTranscriptNew> list = RepositoryManager.Student_Repository.GetStudentTrancriptByIdNew(studentId);
            return list;
        }
        public static rStudentTranscriptGeneralInfo GetStudentTrancriptGeneralInfoById(string studentId)
        {
            rStudentTranscriptGeneralInfo studentGeneInfo = RepositoryManager.Student_Repository.GetStudentTrancriptGeneralInfoById(studentId);
            return studentGeneInfo;
        }
        public static List<rStudentGradeDetail> GetAllGrade(string studentId)
        {

            List<rStudentGradeDetail> list = RepositoryManager.StudentGradeDetail_Repository.GetAllGrade(studentId);
            return list;
        }

        public static List<rCourseListByProgram> GetAllByProgram(int programId)
        {

            List<rCourseListByProgram> list = RepositoryManager.CourseListByProgram_Repository.GetAllByProgram(programId);
            return list;
        }


        public static List<Student> GetAllRegisteredStudentByProgramSessionCourse(int programId, int sessionId, int courseId, int versionId)
        {
            List<Student> list = RepositoryManager.Student_Repository.GetAllRegisteredStudentByProgramSessionCourse(programId, sessionId, courseId, versionId);
            return list;
        }

        public static List<RunningStudent> GetRunningStudentByProgramIdAcaCalId(int programId, int acaCalId, int batchId)
        {
            List<RunningStudent> list = RepositoryManager.RunningStudent_Repository.GetRunningStudentByProgramIdAcaCalId(programId, acaCalId, batchId);
            return list;
        }

        public static List<RunningStudent> GetRunningStudentByProgramIdAcaCalIdNew(int programId, int acaCalId, int batchId)
        {
            List<RunningStudent> list = RepositoryManager.RunningStudent_Repository.GetRunningStudentByProgramIdAcaCalIdNew(programId, acaCalId, batchId);
            return list;
        }

        public static List<rStudentCourse> GetRunningStudentCourseByProgramIdAcaCalId(int programId, int acaCalId, int batchId)
        {
            List<rStudentCourse> list = RepositoryManager.RunningStudent_Repository.GetRunningStudentCourseByProgramIdAcaCalId(programId, acaCalId, batchId);
            return list;
        }

        public static List<StudentDiscountInitialDTO> GetAllDiscountInitialByProgramBatchRoll(int programId, int batchId, string roll)
        {
            List<StudentDiscountInitialDTO> list = RepositoryManager.Student_Repository.GetAllDiscountInitialByProgramBatchRoll(programId, batchId, roll);
            return list;
        }

        public static List<RunningStudent> GetStudentListByProgramAndBatch(int programId, int batchId)
        {
            List<RunningStudent> list = RepositoryManager.Student_Repository.GetStudentListByProgramAndBatch(programId, batchId);
            return list;
        }


        public static List<StudentDTO> GetAllDTOByProgramOrBatchOrRoll(int programId, int acaCalId, string roll)
        {
            List<StudentDTO> list = RepositoryManager.Student_Repository.GetAllDTOByProgramOrBatchOrRoll(programId, acaCalId, roll);
            return list;
        }

        public static List<StudentDTO> GetAllDTOHasInitialDiscountGetByProgramOrBatchOrRoll(int programId, int batchId, string roll)
        {
            List<StudentDTO> list = RepositoryManager.Student_Repository.GetAllDTOHasInitialDiscountGetByProgramOrBatchOrRoll(programId, batchId, roll);
            return list;
        }
        public static List<StudentDTO> GetAllDTOHasInitialDiscountGetByProgramOrBatchOrRoll(int programId, int batchId, string roll, int sessionId, int resultSessionId)
        {
            List<StudentDTO> list = RepositoryManager.Student_Repository.GetAllDTOHasInitialDiscountGetByProgramOrBatchOrRoll(programId, batchId, roll, sessionId, resultSessionId);
            return list;
        }

        public static List<StudentDTO> GetAllDTOForSiblingByProgramOrBatchOrRoll(int programId, int acaCalId, int relationDiscountType, string roll)
        {
            List<StudentDTO> list = RepositoryManager.Student_Repository.GetAllDTOForSiblingByProgramOrBatchOrRoll(programId, acaCalId, relationDiscountType, roll);
            return list;
        }

        public static List<StudentDTO> GetDTOAllBySiblingGroupId(int groupId)
        {
            List<StudentDTO> list = RepositoryManager.Student_Repository.GetDTOAllBySiblingGroupId(groupId);
            return list;
        }

        public static List<rStudentTranscript> GetStudentTrancriptByIdRunning(string studentId)
        {
            List<rStudentTranscript> list = RepositoryManager.Student_Repository.GetStudentTrancriptByIdRunning(studentId);
            return list;
        }

        public static List<rStudentTranscript> GetConsolidatedCrAssessment(string studentId)
        {
            List<rStudentTranscript> list = RepositoryManager.Student_Repository.GetConsolidatedCrAssessment(studentId);
            return list;
        }

        public static List<rStudentGPACGPA> GetStudentGPACGPAByRoll(string studentId)
        {
            List<rStudentGPACGPA> list = RepositoryManager.Student_Repository.GetStudentGPACGPAByRoll(studentId);
            return list;
        }

        public static List<rStudentByProgramAndBatch> GetStudentByProgramIdBatchId(int programId, int batchId)
        {
            List<rStudentByProgramAndBatch> list = RepositoryManager.Student_Repository.GetStudentByProgramIdBatchId(programId, batchId);
            return list;
        }

        public static List<rStudentByProgramAndBatch> GetCompleteStudentByProgramIdBatchId(int programId, int batchId, int acaCalId)
        {
            List<rStudentByProgramAndBatch> list = RepositoryManager.Student_Repository.GetCompleteStudentByProgramIdBatchId(programId, batchId, acaCalId);
            return list;
        }

        public static List<StudentDTO> GetAllDTOByProgramBatchResultSessionRoll(int programId, int acaCalId, int sessionId, int resultSessionId, string roll)
        {
            List<StudentDTO> list = RepositoryManager.Student_Repository.GetAllDTOByProgramBatchResultSessionRoll(programId, acaCalId, sessionId, resultSessionId, roll);
            return list;
        }

        public static List<rStudentResultProgramBatch> GetStudentGPACGPAByProgramBatchSession(int programId, int BatchId, int sessionId, int SemesterNo)
        {
            List<rStudentResultProgramBatch> list = RepositoryManager.Student_Repository.GetStudentGPACGPAByProgramBatchSession(programId, BatchId, sessionId, SemesterNo);
            return list;
        }

        public static List<rStudentRollSheet> GetStudentRollSheetByProgramBatchSession(int programId, int BatchId, int SessoinId, int ExamCenterId, int InstitutionId)
        {
            List<rStudentRollSheet> list = RepositoryManager.Student_Repository.GetStudentRollSheetByProgramBatchSession(programId, BatchId, SessoinId, ExamCenterId, InstitutionId);
            return list;
        }

        public static List<StudentCountProgramBatchWise> GetStudentCountProgramBatchWiseByAcaCalIdProgramId(int ProgramId, int AcaCalId)
        {
            List<StudentCountProgramBatchWise> list = RepositoryManager.Student_Repository.GetStudentCountProgramBatchWiseByAcaCalIdProgramId(ProgramId, AcaCalId);
            return list;
        }

        public static List<rStudentTabulationSheet> GetStudentTabulationSheetByProgramBatchSession(int programId, int BatchId, int SessoinId)
        {
            List<rStudentTabulationSheet> list = RepositoryManager.Student_Repository.GetStudentTabulationSheetByProgramBatchSession(programId, BatchId, SessoinId);
            return list;
        }

        public static List<StudentBillCourseCountDTO> GetStudentForBillPosting(int sessionId, int programId, int batchId)
        {
            List<StudentBillCourseCountDTO> list = RepositoryManager.Student_Repository.GetStudentForBillPosting(sessionId, programId, batchId);
            return list;
        }

        public static List<rStudentGradeCertificateInfo> GetStudentGradeCertificateInfoByRoll(string Roll, int sessionId, int semesterId)
        {
            List<rStudentGradeCertificateInfo> list = RepositoryManager.Student_Repository.GetStudentGradeCertificateInfoByRoll(Roll, sessionId, semesterId);
            return list;
        }

        public static List<StudentRollOnly> GetStudentListRollByProgramBatchSession(int sessionId, int programId, int batchId)
        {
            List<StudentRollOnly> list = RepositoryManager.Student_Repository.GetStudentListRollByProgramBatchSession(sessionId, programId, batchId);
            return list;
        }

        public static List<StudentRollOnly> GetStudentListRollByProgramBatchSessionInstitution(int sessionId, int programId, int batchId, int institutionId)
        {
            List<StudentRollOnly> list = RepositoryManager.Student_Repository.GetStudentListRollByProgramBatchSessionInstitution(sessionId, programId, batchId, institutionId);
            return list;
        }

        public static List<StudentIdCardInfo> GetStudentIdCardInfoByRoll(string Roll)
        {
            List<StudentIdCardInfo> list = RepositoryManager.Student_Repository.GetStudentIdCardInfoByRoll(Roll);
            return list;
        }

        public static List<StudentRollOnly> GetStudentListRollByProgramBatchSemester(int semesterId, int programId, int batchId)
        {
            List<StudentRollOnly> list = RepositoryManager.Student_Repository.GetStudentListRollByProgramBatchSemester(semesterId, programId, batchId);
            return list;
        }

        public static List<StudentRollOnly> GetStudentListRollByProgramBatchSemesterInstitution(int semesterId, int programId, int batchId, int institutionId)
        {
            List<StudentRollOnly> list = RepositoryManager.Student_Repository.GetStudentListRollByProgramBatchSemesterInstitution(semesterId, programId, batchId, institutionId);
            return list;
        }


        public static List<StudentInfoCourseAssign> GetAllRegisteredStudentByProgramYearBatchCourseCompCredit(int ProgramId, int BatchId, int YearId, int CompCr, int courseId, int versionId)
        {
            List<StudentInfoCourseAssign> list = RepositoryManager.Student_Repository.GetAllRegisteredStudentByProgramYearBatchCourseCompCredit(ProgramId, BatchId, YearId, CompCr, courseId, versionId);
            return list;
        }

        public static rStudentGeneralInfo GetStudentGeneralInfoById(int studentId)
        {
            rStudentGeneralInfo studentGeneInfo = RepositoryManager.Student_Repository.GetStudentGeneralInfoById(studentId);
            return studentGeneInfo;
        }


        public static List<rStudentTranscriptResult> GetTranscriptResultByRoll(string Roll)
        {
            List<rStudentTranscriptResult> list = RepositoryManager.Student_Repository.GetTranscriptResultByRoll(Roll);
            return list;
        }
    }
}
