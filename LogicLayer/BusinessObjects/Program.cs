using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Program
    {
        public int ProgramID { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public decimal TotalCredit { get; set; }
        public int DeptID { get; set; }
        public string DetailName { get; set; }
        public string DegreeName { get; set; }
        public int Duration { get; set; }
        public int ProgramTypeID { get; set; }
        public int CalenderUnitMasterID { get; set; }
        public int FacultyId { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int InstituteId { get; set; }
        public string NameWithCode { get { return "[" + Code + "] " + ShortName; } }
        public string NameAndCode { get { return Code + " - " + ShortName; } }
    }
}
