using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class User
    {
        public int User_ID { get; set; }
        public string LogInID { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public Nullable<DateTime> RoleExistStartDate { get; set; }
        public Nullable<DateTime> RoleExistEndDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Person Person
        {
            get
            {
                Person person = null;
                person = PersonManager.GetByUserId(User_ID);
                return person;
            }
        }
        public Role Role
        {
            get
            {
                Role role = null;
                role = RoleManager.GetById(RoleID);
                return role;
            }
        }

        public string RegistrationNo { get; set; }

    }
}
