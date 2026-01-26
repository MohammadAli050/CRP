using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IUserMenuRepository
    {
        int Insert(UserMenu userMenu);
        bool Update(UserMenu userMenu);
        bool Delete(int Id);
        UserMenu GetById(int Id);
        List<UserMenu> GetAll();
        List<UserMenu> GetAll(int UserId);
    }
}
