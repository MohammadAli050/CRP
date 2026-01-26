using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class LogSMS
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Sender { get; set; }
        public string Receipient { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
  }
}
