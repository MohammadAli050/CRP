using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICalenderUnitTypeRepository
    {
        int Insert(CalenderUnitType calenderUnitType);
        bool Update(CalenderUnitType calenderUnitType);
        bool Delete(int id);
        CalenderUnitType GetById(int? id);
        List<CalenderUnitType> GetAll();
    }
}
