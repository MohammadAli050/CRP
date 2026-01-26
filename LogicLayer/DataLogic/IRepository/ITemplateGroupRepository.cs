using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ITemplateGroupRepository
    {
        int Insert(ExamTemplateItem examTemplateItem);
        bool Update(ExamTemplateItem examTemplateItem);
        bool Delete(int TemplateGroupId);
        ExamTemplateItem GetById(int examTemplateItemId);
        List<ExamTemplateItem> GetAll();
        List<ExamTemplateItemDTO> GetAllTemplateItem();
        ExamTemplateItem GetByTemplateExamSetId(int templateId, int examSetId);
        ExamTemplateItem GetByTemplateExamSetExamId(int templateId, int examSetId, int examId);
        decimal GetByTemplateExamSetExamId(int templateId);
        List<ExamTemplateItemDTO> GetAllItemByTemplateId(int templateId);
        List<ExamTemplateItem> GetAllByTemplateId(int templateId);
    }
}

