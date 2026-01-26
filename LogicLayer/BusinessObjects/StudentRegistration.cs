using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    public class StudentRegistration
    {
        public int Id {get; set; }
		public int StudentId {get; set; }
		public string RegistrationNo {get; set; }
		public int SessionId {get; set; }
		public string Attribute1 {get; set; }
		public string Attribute2 {get; set; }
		public string Attribute3 {get; set; }
		public int CreatedBy {get; set; }
		public DateTime CreatedDate{get; set; }
		public int ModifiedBy {get; set; }
		public DateTime ModifiedDate{get; set; }

        public String Roll
        {
            get
            {
                Student std = StudentManager.GetById(StudentId);
                if (std != null)
                {
                    return std.Roll;
                }
                else
                {
                    return "";
                }


            }
        }
    }
}

