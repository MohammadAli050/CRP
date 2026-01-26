using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
   public interface  ITimeSlotPlanRepository
    {
       int Insert(TimeSlotPlanNew timeSlotPlanNew);
       bool Update(TimeSlotPlanNew timeSlotPlanNew);
        bool Delete(int id);
        TimeSlotPlanNew GetById(int id);
        List<TimeSlotPlanNew> GetAll();
    }
}
