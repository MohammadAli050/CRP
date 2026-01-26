using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IRoleMenuRepository
    {
        int Insert(RoleMenu roleMenu);
        bool Update(RoleMenu roleMenu);
        bool Delete(int id);
        RoleMenu GetById(int? id);
        List<RoleMenu> GetAll();
    }
}
