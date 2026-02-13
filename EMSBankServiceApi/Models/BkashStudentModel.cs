using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSBankServiceApi.Models
{
    public class BkashStudentModel
    {
        public string StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentMobile { get; set; }
        public string Amount { get; set; }
        public string ReferenceNo { get; set; }
        public string Token { get; set; }
        public bool IsPaid { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }

    }
}
