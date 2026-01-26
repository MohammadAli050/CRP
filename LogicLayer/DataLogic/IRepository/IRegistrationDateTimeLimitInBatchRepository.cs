using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IRegistrationDateTimeLimitInBatchRepository
    {
        int Insert(RegistrationDateTimeLimitInBatch registrationDateTimeLimitInBatch);
        bool Update(RegistrationDateTimeLimitInBatch registrationDateTimeLimitInBatch);
        bool Delete(int id);
        RegistrationDateTimeLimitInBatch GetById(int? id);
        List<RegistrationDateTimeLimitInBatch> GetAll();
    }
}
