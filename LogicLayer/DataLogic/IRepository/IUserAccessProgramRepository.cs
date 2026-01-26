using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IUserAccessProgramRepository
    {
        int Insert(UserAccessProgram useraccessprogram);
        bool Update(UserAccessProgram useraccessprogram);
        bool Delete(int ID);
        UserAccessProgram GetById(int? ID);
        UserAccessProgram GetByUserId(int? ID);
        List<UserAccessProgram> GetAll();
    }
}

