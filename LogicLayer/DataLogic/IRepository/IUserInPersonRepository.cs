using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
   public interface IUserInPersonRepository
    {
       int Insert(UserInPerson userInPerson);
       bool Update(UserInPerson userInPerson);
       bool Delete(int id);
       UserInPerson GetById(int id);
       List<UserInPerson> GetAll();
       List<UserInPerson> GetByPersonId(int id);
    }
}
