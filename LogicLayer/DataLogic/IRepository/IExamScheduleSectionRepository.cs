using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamScheduleSectionRepository
    {
        int Insert(ExamScheduleSection examschedulesection);
        bool Update(ExamScheduleSection examschedulesection);
        bool Delete(int Id);
        bool DeleteByExamSchedule(int examScheduleId);
        ExamScheduleSection GetById(int Id);
        List<ExamScheduleSection> GetAll();
        List<ExamScheduleSection> GetAllByExamSchedule(int examScheduleId);
    }
}

