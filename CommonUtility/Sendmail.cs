using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{
    public class Sendmail
    {
        public static bool sendEmail(String senderName, String toAddr, String ccAddr, String subject, String body)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("edusoftconsultants", "esc1112012#");

            MailAddress from = new MailAddress("edusoftconsultants@gmail.com", senderName);

            MailAddress to = new MailAddress(toAddr);

            MailMessage message = new MailMessage(from, to);

            if (ccAddr.Trim() != "")
            {
                string[] strArray = ccAddr.Trim().Split(new char[] { ';' });

                for (int i = 0; i < strArray.Length; i++)
                {
                    message.CC.Add(strArray[i].Trim());
                }
            }

            message.Subject = subject;

            message.IsBodyHtml = true;

            message.Body = body;

            try
            {
                client.Send(message);

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
