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
    public class LogGeneralManager
    {


        public static int Insert(   DateTime DateTime, 
                                    string AcademicCalenderCode,
                                    string AcademicCalenderName,
                                    string UserLoginId, 
                                    string CourseFormalCode,
                                    string CourseVersionCode, 
                                    string EventName,
                                    string Message,
                                    string MessageType, 
                                    string PageId, 
                                    string PageName,
                                    string PageUrl, 
                                    string StudentRoll)
        {

            LogGeneral obj = new LogGeneral();

            obj.DateTime = DateTime;
            obj.AcademicCalenderCode = AcademicCalenderCode;
            obj.AcademicCalenderName = AcademicCalenderName;
            obj.CourseFormalCode = CourseFormalCode;
            obj.CourseVersionCode = CourseVersionCode;
            obj.EventName = EventName;
            obj.Message = Message;
            obj.MessageType = MessageType;
            obj.PageId = PageId;
            obj.PageName = PageName;
            obj.PageUrl = PageUrl;
            obj.StudentRoll = StudentRoll;
            obj.UserLoginId = UserLoginId;

            int id = RepositoryManager.LogGeneral_Repository.Insert(obj);

            return id;
        }


        public static List<LogGeneral> GetAll()
        {

            List<LogGeneral> list = RepositoryManager.LogGeneral_Repository.GetAll();

            return list;
        }

        public static List<LogGeneral> GetByDateRange(DateTime fromDate,DateTime toDate)
        {

            List<LogGeneral> list = RepositoryManager.LogGeneral_Repository.GetByDateRange(fromDate,toDate);

            return list;
        }
    }
}

