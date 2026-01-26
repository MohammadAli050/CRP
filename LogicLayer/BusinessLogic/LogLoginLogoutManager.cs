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
    public class LogLoginLogoutManager
    {
        public static int Insert(   DateTime dateTime,
                                    string LoginID,
                                    string LogInLogOut,
                                    string loginStatus,
                                    string Message,
                                    string PasswordAttempts)
        {
            LogLoginLogout logloginlogout = new LogLoginLogout();

            logloginlogout.DateTime=dateTime;
            logloginlogout.LoginID=LoginID;
            logloginlogout.LogInLogOut=LogInLogOut;
            logloginlogout.loginStatus=loginStatus;
            logloginlogout.Message=Message;
            logloginlogout.PasswordAttempts = PasswordAttempts;
            

            int id = RepositoryManager.LogLoginLogout_Repository.Insert(logloginlogout);
            
            return id;
        }       

        public static List<LogLoginLogout> GetAll()
        {
            List<LogLoginLogout> list =  RepositoryManager.LogLoginLogout_Repository.GetAll();
                
            return list;
        }

        public static List<LogLoginLogout> GetByDateRange(DateTime fromDate, DateTime toDate)
        {
            List<LogLoginLogout> list = RepositoryManager.LogLoginLogout_Repository.GetByDateRange(fromDate, toDate);

            return list;
        }
    }
}

