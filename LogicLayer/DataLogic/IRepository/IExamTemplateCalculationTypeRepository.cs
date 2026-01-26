using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamTemplateCalculationTypeRepository
    {
        int Insert(ExamTemplateCalculationType examtemplatecalculationtype);
        bool Update(ExamTemplateCalculationType examtemplatecalculationtype);
        bool Delete(int ExamCalculationTypeId);
        ExamTemplateCalculationType GetById(int? ExamCalculationTypeId);
        List<ExamTemplateCalculationType> GetAll();
    }
}

