using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class UserInPerson
    {
       public int User_ID { get; set; }
       public int PersonID { get; set; }
       public int CreatedBy { get; set; }
       public DateTime CreatedDate { get; set; }
       public int ModifiedBy { get; set; }
       public DateTime ModifiedDate { get; set; }

       public User User
       {
           get
           {
              return UserManager.GetById(User_ID); 
           }
       }

       public Person Person
       {
           get 
           {
               return PersonManager.GetById(PersonID);
           }
       }



    }
}
