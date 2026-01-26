using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IAcademicCalenderExamSchedulerRepository
    {
        int Insert(AcademicCalenderExamScheduler academicCalenderExamScheduler);
        bool Update(AcademicCalenderExamScheduler academicCalenderExamScheduler);
        bool Delete(int id);
        AcademicCalenderExamScheduler GetById(int? id);
        List<AcademicCalenderExamScheduler> GetAll();
    }
}
