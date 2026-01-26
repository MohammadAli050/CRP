using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICalenderUnitDistributionRepository
    {
        int Insert(CalenderUnitDistribution calenderUnitDistribution);
        bool Update(CalenderUnitDistribution calenderUnitDistribution);
        bool Delete(int id);
        CalenderUnitDistribution GetById(int? id);
        CalenderUnitDistribution GetByCourseId(int? id);
        List<CalenderUnitDistribution> GetAll();
    }
}
