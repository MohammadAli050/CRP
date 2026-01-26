using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamSetRepository
    {
        int Insert(ExamSet examSet);
        bool Update(ExamSet examSet);
        bool Delete(int examSetId);
        ExamSet GetById(int? examSetId);
        List<ExamSet> GetAll();

        ExamSet GetExamSetByName(string examSetName);
    }
}
