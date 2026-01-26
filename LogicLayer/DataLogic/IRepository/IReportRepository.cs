using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IReportRepository
    {
        List<rClassRoutine> GetByAcaCalIDAndProgramID(int AcaCalID, int ProgramID);

        List<rStudentCGPAList> GetStudentCGPAListByStudentID(string studentId);

        List<rStudentResultHistory> GetStudentResultHistoryListByStudentID(string studentId);

        List<rStudentClassRoutine> GetStudentClassRoutineByStudentIDAndAcaCalID(string studentId, int AcaCalID);

        List<rStudentRoadmap> GetStudentRoadmapByStudentID(string studentId);

        List<rAttendance> GetAttendanceListByAcaCalID(int AcaCalID);

        List<rAttendanceClassTeacher> GetAttendanceCourseTeacherByAcaCalID(int AcaCalID);

        List<rOfferedCourseCount> GetOfferedCourseCountByProgramID(int programID);

        List<RptAdmitCard> GetAdmitCard(int programId, int acaCalId, int batchId, string roll,int institutionId,int examCenterId);

        List<rStudentClassExamSum> GetStudentRegSummary(string roll, int acaCalId);

        List<rClassRoutineConflict> GetClassRoutineConflict(int programId, int acaCalId);

        List<rExamRoutineConflict> GetExamRoutineConflict(int programId, int acaCalId);

        List<rClassRoutineConflictPair> GetClassRoutineConflictPair(int programId, int acaCalId);

        List<rExamRoutineConflictPair> GetExamRoutineConflictPair(int programId, int acaCalId);

        List<rOfferedCourseClassRoutine> GetOfferedCourseClassRoutine(string roll, int acaCalId);

        List<rTabulationSheet> GetTabulationSheet(int programId, int acaCalId, int batchId, string roll, int majorId);

        List<rDeptWiseRegisteredStudent> GetDeptWiseRegisteredStudent(int year);

        List<rProgramWiseRegistrationCount> GetProgramWiseRegistrationCount(int acaCalId);

        List<rRegisteredStudentList> GetRegisteredStudentList(int acaCalId, int programId, int batchId);

        List<rDegreeCompletion> GetDegreeCompletionListByProgramIDAndSessionRange(int programId, int fromSessionId, int toSessionId, int semesterType);

        List<rFinalMeritList> GetMeritListByProgramIDAndSessionRange(int programId, int fromSessionId, int toSessionId);

        List<rFinalMeritList> GetConsolidatedCreditAssessmentByProgramIDAndSessionRange(int programId, int fromSessionId, int toSessionId, decimal credit);

        List<rDegreeCompletedStudent> GetDegreeCompletedStudentCountSessionRange();
        List<rAdmittedStudentCount> GetAdmittedStudentCount();

        List<rCreditDistribution> GetAllCreditDstributionList();

        List<rStudentClassExamSum> GetStudentCourseRegSummaryNew(int StudentId, int acaCalId, int userId, int Retake);
    }


}
