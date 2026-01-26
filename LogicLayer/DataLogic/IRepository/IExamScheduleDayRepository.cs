using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamScheduleDayRepository
    {
        int Insert(ExamScheduleDay examscheduleday);
        bool Update(ExamScheduleDay examscheduleday);
        bool Delete(int Id);
        ExamScheduleDay GetById(int Id);
        List<ExamScheduleDay> GetAll();
        List<ExamScheduleDay> GetAllByExamSet(int examScheduleSetId);
    }
}

