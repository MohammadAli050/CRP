using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IMenuRepository
    {
        int Insert(Menu menu);
        bool Update(Menu menu);
        bool Delete(int id);
        Menu GetById(int? id);
        List<Menu> GetAll();
        List<Menu> GetByRoleId(int roleId);
    }
}
