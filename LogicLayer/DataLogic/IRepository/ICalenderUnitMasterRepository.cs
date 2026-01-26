using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICalenderUnitMasterRepository
    {
        int Insert(CalenderUnitMaster calenderUnitMaster);
        bool Update(CalenderUnitMaster calenderUnitMaster);
        bool Delete(int id);
        CalenderUnitMaster GetById(int? id);
        List<CalenderUnitMaster> GetAll();
    }
}
