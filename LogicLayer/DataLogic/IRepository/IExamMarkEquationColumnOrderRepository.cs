using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamMarkEquationColumnOrderRepository
    {
        int Insert(ExamMarkEquationColumnOrder exammarkequationcolumnorder);
        bool Update(ExamMarkEquationColumnOrder exammarkequationcolumnorder);
        bool Delete(int Id);
        ExamMarkEquationColumnOrder GetById(int Id);
        List<ExamMarkEquationColumnOrder> GetAll();
        List<ExamMarkEquationColumnOrder> GetByTemplateItemId(int templateItemId);
        bool DeleteByTemplateItemSequenceId(int templateId, int columnSequence);
    }
}

