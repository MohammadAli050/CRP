using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamTypeNameRepository
    {
        int Insert(ExamTypeName examTypeName);
        bool Update(ExamTypeName examTypeName);
        bool Delete(int id);
        ExamTypeName GetById(int? id);
        List<ExamTypeName> GetAll();
    }
}
