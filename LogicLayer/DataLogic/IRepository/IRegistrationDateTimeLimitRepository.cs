using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IRegistrationDateTimeLimitRepository
    {
        int Insert(RegistrationDateTimeLimit registrationDateTimeLimit);
        bool Update(RegistrationDateTimeLimit registrationDateTimeLimit);
        bool Delete(int id);
        RegistrationDateTimeLimit GetById(int? id);
        List<RegistrationDateTimeLimit> GetAll();
    }
}
