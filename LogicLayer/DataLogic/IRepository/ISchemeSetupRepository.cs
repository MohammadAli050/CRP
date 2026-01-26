using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ISchemeSetupRepository
    {
        int Insert(SchemeSetup schemesetup);
        bool Update(SchemeSetup schemesetup);
        bool Delete(int Id);
        SchemeSetup GetById(int Id);
        List<SchemeSetup> GetAll();
    }
}

