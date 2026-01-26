using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStdAcademicCalenderRepository
    {
        int Insert(StdAcademicCalender std_AcademicCalender);
        bool Update(StdAcademicCalender std_AcademicCalender);
        bool Delete(int id);
        StdAcademicCalender GetById(int? id);
        List<StdAcademicCalender> GetAll();
    }
}
