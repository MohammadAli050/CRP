using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IUserObjectControlRepository
    {
        int Insert(UserObjectControl userObjectControl);
        bool Update(UserObjectControl userObjectControl);
        bool Delete(int Id);
        UserObjectControl GetById(int Id);
        List<UserObjectControl> GetAll();
        List<UserObjectControl> GetAll(int UserId);
    }
}
