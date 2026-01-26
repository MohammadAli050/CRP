using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IAcademicCalenderRepository
    {
        int Insert(AcademicCalender academicCalender);
        bool Update(AcademicCalender academicCalender);
        bool Delete(int id);
        AcademicCalender GetById(int? id);
        List<AcademicCalender> GetAll();
        List<AcademicCalender> GetAll(int calenderUnitMasterID);
        AcademicCalender GetActiveRegistrationCalenderByProgramId(int programId);
        List<AcademicCalender> GetCustom();
        List<rAcaCalSessionListByRoll> GetAcaCalSessionListByRoll(string roll);
        List<rAcaCalSessionListByProgram> GetAcaCalSessionListCompleted(string roll);
        AcademicCalender GetIsActiveRegistrationByProgramId(int programId);
        List<rYear> GetAllYear();
        AcademicCalender GetIsCurrentRegistrationByProgramId(int programId);
        List<Semester> SemesterListByProgramIdBatchId(int programId, int BatchId);
        List<AcademicCalender> AcaCalSessionByProgramIdBatchId(int programId, int BatchId);
        List<AcademicCalender> GetAllByProgram(int programId);


    }
}
