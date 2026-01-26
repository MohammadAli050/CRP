using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IUserInstitutionRepository
    {
        int Insert(UserInstitution userinstitution);
        bool Update(UserInstitution userinstitution);
        bool Delete(int Id);
        UserInstitution GetById(int? Id);
        UserInstitution GetByInstituteIdUserId(int instituteId, int userId);
        List<UserInstitution> GetAll();
        List<UserInstitution> GetAllByUserId(int userId);
    }
}

