using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IProgramTypeRepository
    {
        int Insert(ProgramType programType);
        bool Update(ProgramType programType);
        bool Delete(int id);
        ProgramType GetById(int? id);
        List<ProgramType> GetAll();
    }
}
