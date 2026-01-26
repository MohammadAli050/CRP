using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamScheduleRepository
    {
        int Insert(ExamSchedule examschedule);
        bool Update(ExamSchedule examschedule);
        bool Delete(int Id);
        ExamSchedule GetById(int Id);
        List<ExamSchedule> GetAll();
        List<ExamSchedule> GetAllByAcaCalExamSet(int acaCalId, int examSet);
        List<ConflictStudentDTO> GetAllByAcaCalExamSetDaySlot(int acaCalId, int examSetId, int dayId, int timeSlotId);
        ExamSchedule GetByParameters(int acaCalId, int examSetId, int dayId, int timeSlotId, int courseId, int versionId);
        string GetTotalStudentMaleFemale(int examScheduleId);
        List<ExamSchedule> GetAllByAcaCalExamSetDayTimeSlot(int acaCalId, int examSet, int dayId, int timeSlotId);
        string GetTotalMaleFemale(int acaCalId, int dayId, int timeSlotId);
        List<rExamAttendanceSheet> GetExamAttendaceListByRoom(int acaCalId, int examSetId, int dayId, int timeSlotId, int roomId);
        List<rExamSeatPlanByRoom> GetExamSeatPlanByRoom(int acaCalId, int examSetId, int dayId, int timeSlotId, int roomId);
        List<ConflictStudentDTO> GetAllStudentRollbyExamScheduleGender(int examScheduleId, string gender);
        List<ConflictStudentDTO> GetAllStudentRollbyExamSchedule(int examScheduleId);
    }
}