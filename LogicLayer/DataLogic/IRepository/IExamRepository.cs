using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamRepository
    {
        int Insert(Exam exam);
        bool Update(Exam exam);
        bool Delete(int examId);
        Exam GetById(int examId);
        List<Exam> GetAll();
        List<Exam> GetAllExam(int examSetId);
        List<Exam> GetExamByCouseTemplateId(int courseId, int templateId);
        List<ExamDTO> GetExamByCourseId(int courseId);
        Exam GetExamByName(string examName);
        List<Exam> GetAllByExamTemplateId(int examTemplateId);
        List<rSemesterResultSummary> GetSemesterResultSummary(int sessionId);
        List<rStudentExamRoutine> GetStudentExamRoutine(string roll, int acaCalId);
        List<rOfferedCourseExamRoutine> GetOfferedCourseExamRoutineForStudent(string roll, int acaCalId);
        List<TabulationSheetMajor> GetMajor(int programId);
        List<StudentResultPublishCourseHistoryDTO> GetStudentForResultPublish(int programId, int sessionId, int courseId, int versionId, int acaCalSectionId);
    }
}
