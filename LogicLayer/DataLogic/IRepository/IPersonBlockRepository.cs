using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IPersonBlockRepository
    {
        int Insert(PersonBlock personblock);
        bool Update(PersonBlock personblock);
        bool Delete(int PersonBlockId);
        PersonBlock GetById(int PersonBlockId);
        List<PersonBlock> GetAll();
        PersonBlockDTO GetByRoll(string roll);
        PersonBlock GetByPersonId(int PersonID);

        bool DeleteByPerson(int personId);

        List<PersonBlockDTO> GetAllByProgramOrBatchOrRoll(int programId, int batchid, string roll, int dueUptoSession);

        List<PersonBlockDTO> GetAllByProgramOrBatchOrRoll(int programId, int batchid, string roll,int registrationSession,int dueUptoSession);
    }
}
