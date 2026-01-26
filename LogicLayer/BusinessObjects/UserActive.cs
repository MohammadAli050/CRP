using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class UserActive
    {
       public string LogInId { get; set; }
       public string FirstName { get; set; }
       public string Phone { get; set; }
       public Boolean IsActive { get; set; }
       public int User_ID { get; set; }
    }
}
