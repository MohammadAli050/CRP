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
    public class LogSMSManager
    {


        public static int Insert(DateTime date,string sender, string recipient, string message,bool status)
        {

            LogSMS obj = new LogSMS();

            obj.Date = date;
            obj.Message = message;
            obj.Sender =sender ;
            obj.Receipient = recipient;
            obj.Status = status;

            int id = RepositoryManager.LogSMS_Repository.Insert(obj);

            return id;
        }


        public static List<LogSMS> GetAll()
        {

            List<LogSMS> list = RepositoryManager.LogSMS_Repository.GetAll();

            return list;
        }

        public static List<LogSMS> GetByDateRange(DateTime fromDate, DateTime toDate)
        {

            List<LogSMS> list = RepositoryManager.LogSMS_Repository.GetByDateRange(fromDate, toDate);

            return list;
        }
    }
}

