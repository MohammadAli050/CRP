using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamTypeRepository
    {
        int Insert(ExamType examtype);
        bool Update(ExamType examtype);
        bool Delete(int ExamTypeId);
        ExamType GetById(int? ExamTypeId);
        List<ExamType> GetAll();
    }
}

