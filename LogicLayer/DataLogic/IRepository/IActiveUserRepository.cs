using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
   public interface IActiveUserRepository
    {
       UserActive GetByLogInId(string LogInId);
       List<UserActive> GetAll(int ValueID, int AdmissionCalenderID, string LogInID);
       UserActive GetById(int? id);
       bool Update(UserActive userActive);
    }
}
