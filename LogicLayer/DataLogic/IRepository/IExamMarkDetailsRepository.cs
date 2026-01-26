using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamMarkDetailsRepository
    {
        int Insert(ExamMarkDetails exammarkdetails);
        bool Update(ExamMarkDetails exammarkdetails);
        bool Delete(int ExamMarkDetailId);
        ExamMarkDetails GetById(int? ExamMarkDetailId);
        List<ExamMarkDetails> GetAll();
        List<ExamMarkNewDTO> GetByExamMarkDtoByParameter(int programId, int sessionId, int courseId, int versionId, int acaCalSectionId, int examTemplateBasicItemId);
        List<ExamMarkNewDTO> GetExamMarkForReport(int programId, int sessionId, int courseId, int versionId, int acaCalSectionId, int examTemplateBasicItemId);
        List<ExamMarkNewDTO> GetConvertedExamMarkForReport(int programId, int sessionId, int courseId, int versionId, int acaCalSectionId, int examTemplateBasicItemId);
        ExamMarkDetails GetByCourseHistoryIdExamTemplateItemId(int courseHistoryId, int examTemplateItemId);
        List<ExamMarkDetails> GetExamMarkByCourseHistoryId(int courseHistoryId);
    }
}

