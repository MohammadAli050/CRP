using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamRoutineRepository
    {
        int Insert(ExamRoutine examRoutine);
        bool Update(ExamRoutine examRoutine);
        bool Delete(int id);
        ExamRoutine GetById(int id);
        List<ExamRoutine> GetAll();

        List<rExamRoutine> GetExamRoutine(int acaCalId, int examScheduleSetId);
        List<InvigilationSchedule> GetInvigilationScheduleByAcaCalIdExamSetId(int acaCalId, int examScheduleSetId);
    }
}
