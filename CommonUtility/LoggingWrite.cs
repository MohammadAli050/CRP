using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace CommonUtility
{
    public static class LogWriter
    {    
        public static void Write(string message)
        {
            LogManager.GetLogger("TextLogger").Error(Environment.NewLine + 
                                                     "LOGINID : "+  SessionManager.GetObjFromSession<string>(ConstantValue.Session_LoginID) +
                                                     Environment.NewLine + Environment.NewLine + 
                                                     "MESSAGE : " + message);
        }

        public static void Write(string message, string ex)
        {
            LogManager.GetLogger("TextLogger").Error(Environment.NewLine +
                                                     "LOGINID : " + SessionManager.GetObjFromSession<string>(ConstantValue.Session_LoginID) +
                                                     Environment.NewLine + Environment.NewLine + 
                                                     "MESSAGE : " + message +
                                                     Environment.NewLine + Environment.NewLine + 
                                                     "EXCEPTION : " + ex);
        }

        public static void Email(string message)
        {
            LogManager.GetLogger("EmailLogger").Error(Environment.NewLine +
                                                      "LOGINID : " + SessionManager.GetObjFromSession<string>(ConstantValue.Session_LoginID) +
                                                      Environment.NewLine + Environment.NewLine + 
                                                      "MESSAGE : " + message);           
        }

        public static void Email(string message, string ex)
        {
            LogManager.GetLogger("EmailLogger").Error(Environment.NewLine +
                                                      "LOGINID : " + SessionManager.GetObjFromSession<string>(ConstantValue.Session_LoginID) +
                                                      Environment.NewLine + Environment.NewLine + 
                                                      "MESSAGE : " + message +
                                                      Environment.NewLine + Environment.NewLine +
                                                      "EXCEPTION : " + ex);

        }
    }
}
