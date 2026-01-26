using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ILateFineScheduleRepository
    {
        int Insert(LateFineSchedule latefineschedule);
        bool Update(LateFineSchedule latefineschedule);
        bool Delete(int LateFineScheduleId);
        LateFineSchedule GetById(int? LateFineScheduleId);
        List<LateFineSchedule> GetAll();
    }
}

