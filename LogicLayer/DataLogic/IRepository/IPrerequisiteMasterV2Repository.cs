using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IPrerequisiteMasterV2Repository
    {
        int Insert(PrerequisiteMasterV2 prerequisitemasterv2);
        bool Update(PrerequisiteMasterV2 prerequisitemasterv2);
        bool Delete(int PreRequisiteMasterId);
        PrerequisiteMasterV2 GetById(int? PreRequisiteMasterId);
        List<PrerequisiteMasterV2> GetAll();
    }
}

