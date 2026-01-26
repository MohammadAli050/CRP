using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamScheduleRoomInfoRepository
    {
        int Insert(ExamScheduleRoomInfo examscheduleroominfo);
        bool Update(ExamScheduleRoomInfo examscheduleroominfo);
        bool Delete(int Id);
        ExamScheduleRoomInfo GetById(int Id);
        List<ExamScheduleRoomInfo> GetAll();
        List<ExamScheduleRoomInfo> GetAllByAcaCalExamSetDayTimeSlot(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId);
        //bool DeleteByAcaCalExamSetDayTimeSlotRoomInfo(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, int roomInfoId);
    }
}

