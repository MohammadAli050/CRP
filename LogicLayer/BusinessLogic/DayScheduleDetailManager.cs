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
    public class DayScheduleDetailManager
    { 
        public static int Insert(DayScheduleDetail dayscheduledetail)
        {
            int id = RepositoryManager.DayScheduleDetail_Repository.Insert(dayscheduledetail); 
            return id;
        }

        public static bool Update(DayScheduleDetail dayscheduledetail)
        {
            bool isExecute = RepositoryManager.DayScheduleDetail_Repository.Update(dayscheduledetail); 
            return isExecute;
        }

        public static bool Delete(int id)
        {
            bool isExecute = RepositoryManager.DayScheduleDetail_Repository.Delete(id); 
            return isExecute;
        }

        public static DayScheduleDetail GetById(int? id)
        { 
            DayScheduleDetail dayscheduledetail   = RepositoryManager.DayScheduleDetail_Repository.GetById(id); 

            return dayscheduledetail;
        }

        public static List<DayScheduleDetail> GetAll()
        {
            List<DayScheduleDetail> list = RepositoryManager.DayScheduleDetail_Repository.GetAll();
            return list;
        }

        public static List<DayScheduleDetail> GetAllByDayScheduleMasterId(int DayScheduleMasterId)
        {
            List<DayScheduleDetail> list = RepositoryManager.DayScheduleDetail_Repository.GetAllByDayScheduleMasterId(DayScheduleMasterId);
            return list;
        }

        public static List<rDayScheduleDetails> GetDayScheduleDetailReportByProgramIdSessionId(int ProgramId, int SessionId)
        {
            List<rDayScheduleDetails> list = RepositoryManager.DayScheduleDetail_Repository.GetDayScheduleDetailReportByProgramIdSessionId(ProgramId, SessionId);
            return list;
        }

        

    }
}

