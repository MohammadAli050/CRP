using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IUserRepository
    {
        int Insert(User user);
        bool Update(User user);
        bool Delete(int id);
        User GetById(int? id);
        List<User> GetAll();
        List<User> GetByPersonId(int PersonID);
        User GetByLogInId(String LogInID);
        List<User> GetByUserId(string userlogInID);
        int GenerateUserByProgramIdBatchId(int programId, int batchId);
        List<User> GetAllByProgramIdBatchId(int programId, int batchId);
        User GetAdminUserByLogInId(string LogInID);
    }
}
