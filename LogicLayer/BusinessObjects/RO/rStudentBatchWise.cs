using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class rStudentBatchWise
    {
        public string StudentID { get; set; }
        public int BatchNO { get; set; }
        public string FullName { get; set; }       
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string PhotoPath { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
    }
}
