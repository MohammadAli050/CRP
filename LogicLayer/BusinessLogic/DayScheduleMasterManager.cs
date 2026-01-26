using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.DAFactory;

namespace LogicLayer.BusinessLogic
{
    public class DayScheduleMasterManager
    {
        public static int Insert(DayScheduleMaster dayschedulemaster)
        {
            int id = RepositoryManager.DayScheduleMaster_Repository.Insert(dayschedulemaster);
            return id;
        }

        public static bool Update(DayScheduleMaster dayschedulemaster)
        {
            bool isExecute = RepositoryManager.DayScheduleMaster_Repository.Update(dayschedulemaster);
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.DayScheduleMaster_Repository.Delete(id);
            return isExecute;
        }

        public static DayScheduleMaster GetById(int? id)
        {
            DayScheduleMaster dayschedulemaster = RepositoryManager.DayScheduleMaster_Repository.GetById(id);
            return dayschedulemaster;
        }

        public static List<DayScheduleMaster> GetAll()
        {
            List<DayScheduleMaster> list = RepositoryManager.DayScheduleMaster_Repository.GetAll();

            return list;
        }

        public static List<DayScheduleMaster> GetAllByProgramSession(int ProgramId, int SessionId)
        {
            List<DayScheduleMaster> list = RepositoryManager.DayScheduleMaster_Repository.GetAllByProgramSession(ProgramId, SessionId);

            return list;
        }

        public static string GenerateDayScheduleMasterByProgramSession(int programId, int SessionId)
        {
            string result = RepositoryManager.DayScheduleMaster_Repository.GenerateDayScheduleMasterByProgramSession(programId, SessionId);

            return result;
        }

        
    }
}

