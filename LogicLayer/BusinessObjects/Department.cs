using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Department
    {
        public int DeptID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<DateTime> OpeningDate { get; set; }
        public int SchoolID { get; set; }
        public string DetailedName { get; set; }
        public Nullable<DateTime> ClosingDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
