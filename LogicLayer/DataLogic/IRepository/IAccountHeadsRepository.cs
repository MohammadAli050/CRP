using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IAccountHeadsRepository
    {
        int Insert(AccountHeads accountHeads);
        bool Update(AccountHeads accountHeads);
        bool Delete(int id);
        AccountHeads GetById(int? id);
        List<AccountHeads> GetAll();
    }
}
