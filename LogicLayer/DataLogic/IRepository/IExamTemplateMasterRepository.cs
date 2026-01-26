using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamTemplateMasterRepository
    {
        int Insert(ExamTemplateMaster examtemplatemaster);
        bool Update(ExamTemplateMaster examtemplatemaster);
        bool Delete(int ExamTemplateMasterId);
        ExamTemplateMaster GetById(int? ExamTemplateMasterId);
        List<ExamTemplateMaster> GetAll();
        ExamTemplateMaster GetExamTemplateMasterByName(string examTemplateMasterName);
        List<ExamTemplateBasicCalculativeItemDTO> ExamTemplateItemGetByAcaCalSectionId(int acaCalSectionId);
        List<ExamMarkColumnWiseDTO> GetStudentExamMarkColumnWise(int courseId, int versionId, int acaCalId, int acaCalSectionId);
        List<ExamMarkColumnWiseDTO> GetStudentExamMarkColumnWiseByStudentId(int courseId, int versionId, int acaCalId, int acaCalSectionId, int courseHistoryId);
    }
}

