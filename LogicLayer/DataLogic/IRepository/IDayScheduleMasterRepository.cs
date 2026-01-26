using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IDayScheduleMasterRepository
    {
        int Insert(DayScheduleMaster dayschedulemaster);
        bool Update(DayScheduleMaster dayschedulemaster);
        bool Delete(int Id);
        DayScheduleMaster GetById(int? Id);
        List<DayScheduleMaster> GetAll();
        List<DayScheduleMaster> GetAllByProgramSession(int ProgramId, int SessionId);
        string GenerateDayScheduleMasterByProgramSession(int programId, int SessionId);
    }
}

