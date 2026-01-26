using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamTemplateRepository
    {
        int Insert(ExamTemplate examTemplate);
        bool Update(ExamTemplate examTemplate);
        bool Delete(int examTemplateId);
        ExamTemplate GetById(int? examTemplateId);
        List<ExamTemplate> GetAll();
        ExamTemplate GetExamTemplateByName(string examTemplateName);
    }
}

