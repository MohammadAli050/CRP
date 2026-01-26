using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IPrerequisiteMasterRepository
    {
        int Insert(PrerequisiteMaster prerequisiteMaster);
        bool Update(PrerequisiteMaster prerequisiteMaster);
        bool Delete(int id);
        PrerequisiteMaster GetById(int? id);
        List<PrerequisiteMaster> GetAll();
    }
}
