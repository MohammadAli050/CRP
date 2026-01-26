using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamMetaTypeRepository
    {
        int Insert(ExamMetaType exammetatype);
        bool Update(ExamMetaType exammetatype);
        bool Delete(int ExamMetaTypeId);
        ExamMetaType GetById(int? ExamMetaTypeId);
        List<ExamMetaType> GetAll();
    }
}

