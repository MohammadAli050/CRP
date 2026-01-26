using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamScheduleSeatPlanRepository
    {
        int Insert(ExamScheduleSeatPlan examschedueseatplan);
        bool Update(ExamScheduleSeatPlan examschedueseatplan);
        bool Delete(int Id);
        bool DeleteByExamScheduleId(int acaCalId, int examSetId, int dayId, int timeSlotId);
        ExamScheduleSeatPlan GetById(int Id);
        List<ExamScheduleSeatPlan> GetAll();
        int GenerateSeatPlan(int acaCalId, int examSetId, int dayId, int timeSlotId);

        List<rExamSeatPlan> GetExamSeatPlan(int acaCalId, string examScheduleSetId, int calenderUnitMasterId, int dayId, int timeSlotId);
        List<ExamScheduleSeatPlan> GetAllByAcaCalExamSetDayTimeSlotRoom(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, int roomId);
        List<rTopSheetPresent> GetAllByAcaCalExamSetDayTimeSlotCourseCode(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, string courseCode, string sectionId);

        List<rTopSheetAbsent> GetAllByAcaCalExamSetDayTimeSlotCourseCodeAbsent(int acaCalId, int examScheduleSetId, int dayId, int timeSlotId, string courseCode, string sectionId);
    }
}

