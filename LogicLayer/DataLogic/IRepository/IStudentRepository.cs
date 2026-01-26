using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.RO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentRepository
    {
        int Insert(Student student);
        bool Update(Student student);
        bool Delete(int id);
        Student GetById(int? id);
        List<Student> GetAll();

        List<Student> GetAllByNameOrID(string searchKey);
        
        List<Student> GetAllByProgramIdBatchId(int programID, int academicCalenderID);

        Student GetByRoll(string roll);

        Student GetByRollOrSerialNo(string roll,int serialNo);
        
        LoadStudentByIdDTO GetByRollEdit(string roll);

        Student GetByPersonID(int personID);

        List<Student> GetAllFromRegWorksheetByProgramAndBatch(int programId, int acaCalId);

        List<Student> GetFromRegWorksheetByStudentRoll(string roll);

        List<Student> GetAllByProgramOrBatchOrRoll(int programId, int batchId, string roll);

        List<Student> GetAllByBatchProgram(string batch, string program);

        List<Student> GetAllByProgramOrBatchOrRoll(int programId, int acaCalId, string rollFrom, string rollTo);

        List<Student> StudentGetAllActiveInactiveWithRegistrationStatus(int programId, int acaCalBatchId, int acaCalSessionId, string roll);

        List<Student> GetAllRegisteredStudentBySession(int acaCalSessionId);

        List<rStudentMajorDefine> GetAllStudentByMajorDefine(int programId, int batchId, string roll);

        List<StudentBlockCountByProgramDTO> GetAllBlockStudentByProgram();

        bool DeleteAllBlockStudentByProgram(int programId);

        List<StudentBlockCountByProgramDTO> GetAllInActiveStudentByProgram();
        
        bool UpdateInActiveToActiveByProgram(int programId);

        List<StudentProbationDTO> GetAllByProbationStatus(int programId, int acaCalBatchId, int? minProb, int? maxProb);

        List<rStudentBatchWise> GetStudentProgramOrBatch(int programId, int batchId);

        List<rStudentTranscript> GetStudentTrancriptById(string studentId);

        List<rStudentTranscriptNew> GetStudentTrancriptByIdNew(string studentId);

        rStudentTranscriptGeneralInfo GetStudentTrancriptGeneralInfoById(string studentId);

        List<Student> GetAllRegisteredStudentByProgramSessionCourse(int programId, int sessionId, int courseId, int versionId);

        List<StudentDiscountInitialDTO> GetAllDiscountInitialByProgramBatchRoll(int programId, int batchId, string roll);

        List<RunningStudent> GetStudentListByProgramAndBatch(int programId, int batchId);

        List<StudentDTO> GetAllDTOByProgramOrBatchOrRoll(int programId, int acaCalId, string roll);

        List<StudentDTO> GetAllDTOHasInitialDiscountGetByProgramOrBatchOrRoll(int programId, int batchId, string roll);

        List<StudentDTO> GetAllDTOForSiblingByProgramOrBatchOrRoll(int programId, int acaCalId,int relationDiscountType, string roll);

        List<StudentDTO> GetDTOAllBySiblingGroupId(int groupId);

        List<rStudentTranscript> GetStudentTrancriptByIdRunning(string studentId);

        List<rStudentTranscript> GetConsolidatedCrAssessment(string studentId);

        List<rStudentGPACGPA> GetStudentGPACGPAByRoll(string studentId);

        List<rStudentByProgramAndBatch> GetStudentByProgramIdBatchId(int programId, int batchId);

        List<RunningStudent> GetRunningStudentByProgramIdAcaCalId(int programId, int acaCalId, int batchId);
        
        List<RunningStudent> GetRunningStudentByProgramIdAcaCalIdNew(int programId, int acaCalId, int batchId);

        List<rStudentCourse> GetRunningStudentCourseByProgramIdAcaCalId(int programId, int acaCalId, int batchId);
        
        List<rStudentByProgramAndBatch> GetCompleteStudentByProgramIdBatchId(int programId, int batchId, int acaCalId);
        
        List<StudentDTO> GetAllDTOByProgramBatchResultSessionRoll(int programId, int acaCalId, int sessionId, int resultSessionId, string roll);
        
        List<StudentDTO> GetAllDTOHasInitialDiscountGetByProgramOrBatchOrRoll(int programId, int batchId, string roll, int sessionId, int resultSessionId);

        List<rStudentBatchWise> GetStudentYearOrSemesterOrCalenderUnitOrProgramOrBatch(string year, int semesterId, int calenderUnitTypeId, int programId, int batchId);

        List<rStudentResultProgramBatch> GetStudentGPACGPAByProgramBatchSession(int programId, int BatchId, int sessionId, int SemesterNo);

        List<rStudentRollSheet> GetStudentRollSheetByProgramBatchSession(int programId, int BatchId, int SessoinId, int ExamCenterId, int InstitutionId);

        List<StudentCountProgramBatchWise> GetStudentCountProgramBatchWiseByAcaCalIdProgramId(int ProgramId,int AcaCalId);

        List<rStudentTabulationSheet> GetStudentTabulationSheetByProgramBatchSession(int programId, int BatchId, int SessoinId);

        List<StudentBillCourseCountDTO> GetStudentForBillPosting(int sessionId, int programId, int batchId);

        List<rStudentGradeCertificateInfo> GetStudentGradeCertificateInfoByRoll(string Roll, int sessionId, int semesterId);

        List<StudentRollOnly> GetStudentListRollByProgramBatchSession(int sessionId, int programId, int batchId);

        List<StudentRollOnly> GetStudentListRollByProgramBatchSessionInstitution(int sessionId, int programId, int batchId, int institutionId);

        List<StudentIdCardInfo> GetStudentIdCardInfoByRoll(string Roll);

        List<StudentRollOnly> GetStudentListRollByProgramBatchSemester(int semesterId, int programId, int batchId);

        List<StudentRollOnly> GetStudentListRollByProgramBatchSemesterInstitution(int semesterId, int programId, int batchId, int institutionId);

        List<StudentInfoCourseAssign> GetAllRegisteredStudentByProgramYearBatchCourseCompCredit(int ProgramId, int BatchId, int YearId, int CompCr, int courseId, int versionId);

        rStudentGeneralInfo GetStudentGeneralInfoById(int studentId);

        List<rStudentTranscriptResult> GetTranscriptResultByRoll(string Roll);
    }
}
