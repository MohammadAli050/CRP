using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class LogLoginLogout
    {
        public DateTime DateTime { get; set; }
        public string LoginID { get; set; }
        public string PasswordAttempts { get; set; }
        public string loginStatus { get; set; }
        public string Message { get; set; }
        public string LogInLogOut { get; set; }
    }
}

