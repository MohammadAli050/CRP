using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class ReportManager
    {
        public static List<rClassRoutine> GetByAcaCalIDAndProgramID(int AcaCalID, int ProgramID)
        {
            List<rClassRoutine> list = RepositoryManager.Report_Repository.GetByAcaCalIDAndProgramID(AcaCalID, ProgramID);
            return list;
        }

        public static List<rStudentCGPAList> GetStudentCGPAListByStudentID(string studentId)
        {
            List<rStudentCGPAList> list = RepositoryManager.Report_Repository.GetStudentCGPAListByStudentID(studentId);
            return list;
        }

        public static List<rStudentResultHistory> GetStudentResultHistoryListByStudentID(string studentId)
        {
            List<rStudentResultHistory> list = RepositoryManager.Report_Repository.GetStudentResultHistoryListByStudentID(studentId);
            return list;
        }

        public static List<rStudentClassRoutine> GetStudentClassRoutineByStudentIDAndAcaCalID(string studentId, int AcaCalID)
        {
            List<rStudentClassRoutine> list = RepositoryManager.Report_Repository.GetStudentClassRoutineByStudentIDAndAcaCalID(studentId, AcaCalID);
            return list;
        }

        public static List<rStudentRoadmap> GetStudentRoadmapByStudentID(string studentId)
        {
            List<rStudentRoadmap> list = RepositoryManager.Report_Repository.GetStudentRoadmapByStudentID(studentId);
            return list;
        }
        public static List<rAttendance> GetAttendanceListByAcaCalID(int AcaCalID)
        {
            List<rAttendance> list = RepositoryManager.Report_Repository.GetAttendanceListByAcaCalID(AcaCalID);
            return list;
        }

        public static List<rAttendanceClassTeacher> GetAttendanceCourseTeacherByAcaCalID(int AcaCalID)
        {
            List<rAttendanceClassTeacher> list = RepositoryManager.Report_Repository.GetAttendanceCourseTeacherByAcaCalID(AcaCalID);
            return list;
        }

        public static List<rOfferedCourseCount> GetOfferedCourseCountByProgramID(int programID)
        {
            List<rOfferedCourseCount> list = RepositoryManager.Report_Repository.GetOfferedCourseCountByProgramID(programID);
            return list;
        }

        public static List<RptAdmitCard> GetAdmitCard(int programId, int acaCalId, int batchId, string roll, int institutionId, int examCenterId)
        {
            List<RptAdmitCard> list = RepositoryManager.AdmitCard_Repository.GetAdmitCard(programId, acaCalId, batchId, roll, institutionId, examCenterId);
            return list;
        }

        public static List<rStudentClassExamSum> GetStudentRegSummary(string roll, int acaCalId)
        {
            List<rStudentClassExamSum> list = RepositoryManager.StudentRegSummary_Repository.GetStudentRegSummary(roll, acaCalId);
            return list;
        }

        public static List<rClassRoutineConflict> GetClassRoutineConflict(int programId, int acaCalId)
        {
            List<rClassRoutineConflict> list = RepositoryManager.Report_Repository.GetClassRoutineConflict(programId, acaCalId);
            return list;
        }

        public static List<rExamRoutineConflict> GetExamRoutineConflict(int programId, int acaCalId)
        {
            List<rExamRoutineConflict> list = RepositoryManager.Report_Repository.GetExamRoutineConflict(programId, acaCalId);
            return list;
        }

        public static List<rClassRoutineConflictPair> GetClassRoutineConflictPair(int programId, int acaCalId)
        {
            List<rClassRoutineConflictPair> list = RepositoryManager.Report_Repository.GetClassRoutineConflictPair(programId, acaCalId);
            return list;
        }

        public static List<rExamRoutineConflictPair> GetExamRoutineConflictPair(int programId, int acaCalId)
        {
            List<rExamRoutineConflictPair> list = RepositoryManager.Report_Repository.GetExamRoutineConflictPair(programId, acaCalId);
            return list;
        }

        public static List<rOfferedCourseClassRoutine> GetOfferedCourseClassRoutine(string roll, int acaCalId)
        {
            List<rOfferedCourseClassRoutine> list = RepositoryManager.Report_Repository.GetOfferedCourseClassRoutine(roll, acaCalId);
            return list;
        }

        public static List<rTabulationSheet> GetTabulationSheet(int programId, int acaCalId, int batchId, string roll, int majorId)
        {
            List<rTabulationSheet> list = RepositoryManager.Report_Repository.GetTabulationSheet(programId, acaCalId, batchId, roll, majorId);
            return list;
        }

        public static List<rDeptWiseRegisteredStudent> GetDeptWiseRegisteredStudent(int year)
        {
            List<rDeptWiseRegisteredStudent> list = RepositoryManager.Report_Repository.GetDeptWiseRegisteredStudent(year);
            return list;
        }

        public static List<rProgramWiseRegistrationCount> GetProgramWiseRegistrationCount(int acaCalId)
        {
            List<rProgramWiseRegistrationCount> list = RepositoryManager.Report_Repository.GetProgramWiseRegistrationCount(acaCalId);
            return list;
        }

        public static List<rRegisteredStudentList> GetRegisteredStudentList(int acaCalId, int programId, int batchId)
        {
            List<rRegisteredStudentList> list = RepositoryManager.Report_Repository.GetRegisteredStudentList(acaCalId, programId, batchId);
            return list;
        }

        public static List<rDegreeCompletion> GetDegreeCompletionListByProgramIDAndSessionRange(int programId, int fromSessionId, int toSessionId, int semesterType)
        {
            List<rDegreeCompletion> list = RepositoryManager.Report_Repository.GetDegreeCompletionListByProgramIDAndSessionRange(programId, fromSessionId, toSessionId, semesterType);
            return list;
        }

        public static List<rFinalMeritList> GetMeritListByProgramIDAndSessionRange(int programId, int fromSessionId, int toSessionId)
        {
            List<rFinalMeritList> list = RepositoryManager.Report_Repository.GetMeritListByProgramIDAndSessionRange(programId, fromSessionId, toSessionId);
            return list;
        }
        public static List<rFinalMeritList> GetConsolidatedCreditAssessmentByProgramIDAndSessionRange(int programId, int fromSessionId, int toSessionId, decimal credit)
        {
            List<rFinalMeritList> list = RepositoryManager.Report_Repository.GetConsolidatedCreditAssessmentByProgramIDAndSessionRange(programId, fromSessionId, toSessionId, credit);
            return list;
        }
        public static List<rDegreeCompletedStudent> GetDegreeCompletedStudentCountSessionRange()
        {
            List<rDegreeCompletedStudent> list = RepositoryManager.Report_Repository.GetDegreeCompletedStudentCountSessionRange();
            return list;
        }

        public static List<rCreditDistribution> GetAllCreditDstributionList()
        {
            List<rCreditDistribution> list = RepositoryManager.Report_Repository.GetAllCreditDstributionList();
            return list;
        }

        public static List<rAdmittedStudentCount> GetAdmittedStudentCount()
        {
            List<rAdmittedStudentCount> list = RepositoryManager.Report_Repository.GetAdmittedStudentCount();
            return list;
        }

        public static List<rStudentClassExamSum> GetStudentCourseRegSummaryNew(int StudentId, int acaCalId, int userId, int Retake)
        {
            List<rStudentClassExamSum> list = RepositoryManager.StudentRegSummary_Repository.GetStudentCourseRegSummaryNew(StudentId, acaCalId, userId, Retake);
            return list;
        }

    }
}
