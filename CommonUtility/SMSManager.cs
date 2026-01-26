using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{
    public class SMSManager
    {
        private static string userName = "edusoft-co";
        private static string password = "wb4PEbDY";
        private static string sender = "MIU";

        public async static void Send(string phoneNo,string roll, string msg, Action<string[]> action)
        {
            string[] dt = new string[3];
            try
            {
                var client = new WebClient();
                phoneNo = phoneNo.Replace("+", "00");
                dt[0] = roll;
                dt[1] = msg;
                dt[2] = await client.DownloadStringTaskAsync("http://api.bulksms.icombd.com/api/v3/sendsms/plain?user=" + userName + "&password=" + password + "&sender=MIU" + "&SMSText=" + msg + "&GSM=" + phoneNo);
                action(dt);
            }
            catch
            {
                dt[0] = roll;
                dt[1] = msg;
                dt[2] = "";
                action(dt);
            }
        }

        public async static void GetBalance(Action<string> action)
        {
            string balance = "";
            try
            {
                var client = new WebClient();
                balance = await client.DownloadStringTaskAsync("http://api.bulksms.icombd.com/api/command?username=" + userName + "&password=" + password + "&cmd=CREDITS");
                action(balance);
            }
            catch
            {

                action(balance);
            }
        }

    }
}
