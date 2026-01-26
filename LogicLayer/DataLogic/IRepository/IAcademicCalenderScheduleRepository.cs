using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IAcademicCalenderScheduleRepository
    {
        int Insert(AcademicCalenderSchedule academiccalenderschedule);
        bool Update(AcademicCalenderSchedule academiccalenderschedule);
        bool Delete(int AcademicCalenderScheduleId);
        AcademicCalenderSchedule GetById(int? AcademicCalenderScheduleId);
        List<AcademicCalenderSchedule> GetAll();
    }
}

