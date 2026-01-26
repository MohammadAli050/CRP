using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamCenterRepository
    {
        int Insert(ExamCenter examcenter);
        bool Update(ExamCenter examcenter);
        bool Delete(int Id);
        ExamCenter GetById(int? Id);
        List<ExamCenter> GetAll();
    }
}

