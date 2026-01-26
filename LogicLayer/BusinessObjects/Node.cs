using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Node
    {
        public int NodeID { get; set; }
        public string Name { get; set; }
        public bool IsLastLevel { get; set; }
        public decimal MinCredit { get; set; }
        public decimal MaxCredit { get; set; }
        public Nullable<int> MinCourses { get; set; }
        public Nullable<int> MaxCourses { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsVirtual { get; set; }
        public bool IsBundle { get; set; }
        public bool IsAssociated { get; set; }
        public Nullable<int> StartTrimesterID { get; set; }
        public Nullable<int> OperatorID { get; set; }
        public Nullable<int> OperandNodes { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public bool IsMajor { get; set; }
    }
}
