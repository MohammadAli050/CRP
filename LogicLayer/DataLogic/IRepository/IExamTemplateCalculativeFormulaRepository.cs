using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamTemplateCalculativeFormulaRepository
    {
        int Insert(ExamTemplateCalculativeFormula examtemplatecalculativeformula);
        bool Update(ExamTemplateCalculativeFormula examtemplatecalculativeformula);
        bool Delete(int Id);
        ExamTemplateCalculativeFormula GetById(int? Id);
        List<ExamTemplateCalculativeFormula> GetAll();
        List<ExamTemplateCalculativeFormula> GetByExamTemplateMasterId(int examTemplateMasterId);
    }
}

