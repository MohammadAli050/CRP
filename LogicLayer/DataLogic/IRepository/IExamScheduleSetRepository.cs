using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamScheduleSetRepository
    {
        int Insert(ExamScheduleSet examscheduleset);
        bool Update(ExamScheduleSet examscheduleset);
        bool Delete(int Id);
        ExamScheduleSet GetById(int Id);
        ExamScheduleSet GetById(int id, string name);
        List<ExamScheduleSet> GetAll();
        List<ExamScheduleSet> GetAllByAcaCalId(int acaCalId);
    }
}

