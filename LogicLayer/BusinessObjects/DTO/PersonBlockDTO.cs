using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class PersonBlockDTO
    {
        public int StudentID { get; set; }
        public string Roll { get; set; }
        public string Name { get; set; }
        public int programId { get; set; }
        public int BatchNO { get; set; }
        public bool IsActive { get; set; }
        public decimal CGPA { get; set; }
        public decimal Dues { get; set; }

        public bool IsAdmitCardBlock { get; set; }
        public bool IsRegistrationBlock { get; set; }
        public bool IsMasterBlock { get; set; }
        public bool IsResultBlock { get; set; }

        public bool IsProbationBlock { get; set; }
        public bool IsBlock2 { get; set; }
        public bool IsBlock3 { get; set; }      
        public string Remarks { get; set; }
    }
}
