using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.WAO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentCourseHistoryRepository
    {
        int Insert(StudentCourseHistory studentCourseHistory);
        bool Update(StudentCourseHistory studentCourseHistory);
        bool Delete(int id);
        decimal GetCompletedCredit(string Roll);
        decimal GetAttemptedCredit(string Roll);
        StudentCourseHistory GetById(int? id);
        List<StudentCourseHistory> GetAll();
        List<StudentCourseHistory> GetAllByAcaCalSectionId(int id);
        List<StudentCourseHistory> GetAllByStudentId(int StudentID);
        List<StudentCourseHistory> GetAll(int studentId, int courseId, int versionId);
        StudentCourseHistory GetBy(int studentId, int courseId, int versionId, bool considerGPA);
        StudentCourseHistory GetBy(int studentId, int courseId, int versionId, int acaCalSecId);
        List<StudentCourseHistory> GetAllByAcaCalId(int acaCalId);
        List<StudentCourseHistoryDTO> GetAllByAcaCalIdCourseId(int acaCalId, int CourseId);
        List<StudentCourseHistory> GetAllByStudentIdAcaCalId(int studentID, int acaCalId);
        List<StudentCourseHistory> GetByStudentIdAcaCalId(int AcaCalId, int StudentId);
        List<StudentCourseHistory> GetDistinctCourseHistoryByStudentIdAcaCalId(int StudentId, int AcaCalId);

        bool UpdateSectionBy(int SectionId, int Id);

        List<StudentCourseHistory> GetAllRegisteredStudentByProgramSessionCourse(int programId, int sessionId, int courseId, int versionId);
        List<StudentCourseHistory> GetAllRegisteredStudentByProgramSessionBatchCourse(int programId, int sessionId, int batchId, int courseId, int versionId);
        List<StudentCourseHistory> GetAllByProgramSession(int programId, int sessionId);
        List<CourseHistryForDegreeValidation> GetCourseHistoryByStudentIdForDegreeValidation(string Roll);

        List<StudentCourseHistory> GetAllByProgramSessionBatch(int programId, int sessionId, int batchId, int rangeId);

        List<GradeSheetInfo> GetAllRegisteredStudentForGradeSheetByProgramSessionBatchCourseExamCenter(int programId, int sessionId, int batchId, int courseId, int versionId, int ExamCeterId, int institutionId);
        #region Web Api

        List<StudentCourseHistoryWAO> GetStudentCourseHistoryWAOByStudentRoll(string roll);

        #endregion

    }
}
