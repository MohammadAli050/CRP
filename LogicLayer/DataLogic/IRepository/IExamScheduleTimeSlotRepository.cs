using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamScheduleTimeSlotRepository
    {
        int Insert(ExamScheduleTimeSlot examscheduletimeslot);
        bool Update(ExamScheduleTimeSlot examscheduletimeslot);
        bool Delete(int Id);
        ExamScheduleTimeSlot GetById(int Id);
        List<ExamScheduleTimeSlot> GetAll();
        List<ExamScheduleTimeSlot> GetAllByExamSet(int examScheduleSetId);
    }
}

