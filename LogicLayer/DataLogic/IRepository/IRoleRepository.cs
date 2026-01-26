using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IRoleRepository
    {
        int Insert(Role role);
        bool Update(Role role);
        bool Delete(int id);
        Role GetById(int? id);
        List<Role> GetAll();
    }
}
