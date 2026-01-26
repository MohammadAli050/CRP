using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IUsrPermsnRepository
    {
        int Insert(UsrPermsn usrPermsn);
        bool Update(UsrPermsn usrPermsn);
        bool Delete(int id);
        UsrPermsn GetById(int? id);
        List<UsrPermsn> GetAll();
    }
}
