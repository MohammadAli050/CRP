using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamMarkRepository
    {
        int Insert(ExamMark examMark);
        bool Update(ExamMark examMark);
        bool Delete(int id);
        ExamMark GetById(int id);
        List<ExamMark> GetAll();
        List<ExamMark> GetAllStudentByCourseSection(int courseId, int sectionId, int templateId);
        ExamMark GetStudentMarkByExamId(int courseHistoryId, int examId);
        List<ExamMarkDTO> GetAllStudentByAcaCalAcaCalSecExam(int acaCalId, int acaCalSecId, int examId);
        List<ExamMarkDTO> GetAllStudentByAcaCalAcaCalSec(int acaCalId, int acaCalSecId);
        int GetTotalUpdateNumberIsFinalSubmit(int acaCalId, int acaCalSecId);
        int GetTotalUpdateNumberIsTransfer(int acaCalId, int acaCalSecId);

        List<rExamResultCourseAndTeacherInfo> GetExamResultCourseAndTeacherInfo(int acaCalSecId, int acaCalId);

        List<rStudentGradePrevious> GetStudentGradeReportPrevious(string roll, int acaCalId);
        List<rStudentGradePreviousNew> GetStudentGradeReportPreviousNew(string roll, int acaCalId);

        List<rStudentGradeCurrent> GetStudentGradeReportCurrent(string roll, int acaCalId);
        List<rStudentGradeCurrent> GetStudentGradeReportCurrentNew(string roll, int acaCalId);
        List<ExamMarkApproveDTO> GetAllAcaCalIdProgramIdCourseIdVersionId(int acaCalId, int programId, int courseId, int versionId);
        string GetApprovedNumberByExamController(int acaCalSectionID);
        string GetApprovedNumberByExamControllerByAcaCalSecRoll(int acaCalSectionID, string roll);
        string GetShortSummaryReport(int acaCalId, int programId);
        int GetResubmitApprovedByExamController(string actionType, int acaCalSectionID);
        int GetPublishNumberByExamController(int acaCalId,int programId);
        List<rGradeOnly> GetGradeOnlyStudentResult(int acaCalSectionId);
        List<rGradeOnly> GetTotalMarksStudentResult(int acaCalSectionId);
        List<StudentGrade> GetGradeReportByRollSesmester(string roll);
        List<StudentGrade> GetGradeReportByRollSession(string roll, int SessionId);
        List<rCreditGPA> GetGradeReportCreditGPAByRoll(int ProgramId, int BatchId, string roll);
        List<StudentGrade> GetGradeReportByRollSemesterNo(string roll, int semesterId);
        
    }
}

