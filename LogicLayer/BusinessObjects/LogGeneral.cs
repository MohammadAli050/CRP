using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class LogGeneral
    {
        public string AcademicCalenderCode { get; set; }
        public string AcademicCalenderName { get; set; }
        public DateTime DateTime { get; set; }
        public string UserLoginId { get; set; }
        public string PageId { get; set; }
        public string PageName { get; set; }
        public string PageUrl { get; set; }
        public string EventName { get; set; }
        public string MessageType { get; set; }
        public string Message { get; set; }
        public string StudentRoll { get; set; }
        public string CourseFormalCode { get; set; }
        public string CourseVersionCode { get; set; }
    }
}

