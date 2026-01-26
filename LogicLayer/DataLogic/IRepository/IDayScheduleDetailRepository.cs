using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IDayScheduleDetailRepository
    {
        int Insert(DayScheduleDetail dayscheduledetail);
        bool Update(DayScheduleDetail dayscheduledetail);
        bool Delete(int Id);
        DayScheduleDetail GetById(int? Id);
        List<DayScheduleDetail> GetAll();
        List<DayScheduleDetail> GetAllByDayScheduleMasterId(int DayScheduleMasterId);
        List<rDayScheduleDetails> GetDayScheduleDetailReportByProgramIdSessionId(int ProgramId, int SessionId);
    }
}

