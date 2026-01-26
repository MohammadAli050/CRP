using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class PersonBlock
    {
        public int PersonBlockId { get; set; }
        public int PersonId { get; set; }
        public DateTime? StartDateAndTime { get; set; }
        public DateTime EndDateAndTime { get; set; }
        public string Remarks { get; set; }       

        public bool IsAdmitCardBlock { get; set; }
        public bool IsRegistrationBlock { get; set; }
        public bool IsMasterBlock { get; set; }
        public bool IsResultBlock { get; set; }

        public bool IsProbationBlock { get; set; }
        public bool IsBlock2 { get; set; }
        public bool IsBlock3 { get; set; }
        
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

