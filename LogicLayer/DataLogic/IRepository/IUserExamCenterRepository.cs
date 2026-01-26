using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IUserExamCenterRepository
    {
        int Insert(UserExamCenter userexamcenter);
        bool Update(UserExamCenter userexamcenter);
        bool Delete(int Id);
        UserExamCenter GetById(int? Id);
        List<UserExamCenter> GetAll();
        UserExamCenter GetByExamCenterIdUserId(int examCenterId, int userId);
        List<UserExamCenter> GetAllByUserId(int userId);
    }
}

