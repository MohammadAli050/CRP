using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class StudentFeedback
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Message { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Roll { get; set; }
        public string FullName { get; set; }
        public string RegistrationNo { get; set; }
        public string Phone { get; set; }
        
    }
}

