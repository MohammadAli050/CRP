using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS.Module
{
    public class DateTimeCheckingMethod
    {
        public static bool CheckDateTimeRange(int SessionId, int ProgramId, int ActivityTypeId)
        {
            bool isTrue = true;

            try
            {

                List<SetUpDateForProgram> setUpDateForProgramList = SetUpDateForProgramManager.GetAll(SessionId, ProgramId, ActivityTypeId);
                if (setUpDateForProgramList != null && setUpDateForProgramList.Count > 0)
                {
                    SetUpDateForProgram setUpDateForProgram = setUpDateForProgramList[0];

                    if (setUpDateForProgram.IsActive == true && TimeCheck(setUpDateForProgram.StartDate, (DateTime)setUpDateForProgram.StartTime, setUpDateForProgram.EndDate, (DateTime)setUpDateForProgram.EndTime))
                    {
                        isTrue = false;
                    }
                    else
                    {
                        isTrue = true;
                    }
                }

                return isTrue;
            }
            catch (Exception ex)
            {
                return true;
            }
        }


        private static bool TimeCheck(DateTime StartDate, DateTime StartTime, DateTime EndDate, DateTime EndTime)
        {
            DateTime actualStartDate = Convert.ToDateTime(StartDate.Date + StartTime.TimeOfDay);
            DateTime actualEndDate = Convert.ToDateTime(EndDate.Date + EndTime.TimeOfDay);
            if (actualStartDate <= DateTime.Now && DateTime.Now <= actualEndDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}