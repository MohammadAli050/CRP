using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonUtility
{
    public static class SessionManager
    {

        //
        //*******************************************************************
        //-----------------------Sample Session methods----------------------
        //*******************************************************************
        //
        //*-List-*
        //SessionSGD.SaveListToSession<PaymentInfo>(_paymentInfoList, "PaymentInfoList");
        //_paymentInfoList = new List<PaymentInfo>();
        //_paymentInfoList = SessionSGD.GetListFromSession<PaymentInfo>("PaymentInfoList");
        //
        //
        //*-Object-*
        //PaymentInfo pinfo = new PaymentInfo();
        //pinfo.Amount = 300;
        //SessionSGD.SaveObjToSession<PaymentInfo>(pinfo, "testM");
        //PaymentInfo obj = SessionSGD.GetObjFromSession<PaymentInfo>("testM");
        //
        //
        //*-String-*
        //SessionSGD.SaveObjToSession<string>("Test msg", "testM");
        //string str = SessionSGD.GetObjFromSession<string>("testM");
        //
        //*******************************************************************
        //-------------------------------------------------------------------
        //*******************************************************************
        //


        public static List<T> GetListFromSession<T>(string sessionName)
        {
            List<T> collection = new List<T>();

            collection = (List<T>)HttpContext.Current.Session[sessionName];

            return collection;
        }

        public static void SaveListToSession<T>(List<T> collection, string sessionName)
        {
            if (HttpContext.Current.Session[sessionName] != null)
            {
                HttpContext.Current.Session.Remove(sessionName);
            }

            HttpContext.Current.Session[sessionName] = collection;
        }

        public static T GetObjFromSession<T>(string sessionName)
        {
            T t;

            if (HttpContext.Current.Session[sessionName] == null)
            {
                return default(T);
            }
            else
            {
                t = (T)HttpContext.Current.Session[sessionName];
            }

            return (T)t;
        }

        public static void SaveObjToSession<T>(T obj, string sessionName)
        {
            if (HttpContext.Current.Session[sessionName] != null)
            {
                HttpContext.Current.Session.Remove(sessionName);
            }

            HttpContext.Current.Session[sessionName] = obj;
            
        }

        public static void DeletFromSession(string sessionName)
        {
            HttpContext.Current.Session.Remove(sessionName);
        }
    }
}
