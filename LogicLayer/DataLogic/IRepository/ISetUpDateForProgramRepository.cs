using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ISetUpDateForProgramRepository
    {
        int Insert(SetUpDateForProgram setupdateforprogram);
        bool Update(SetUpDateForProgram setupdateforprogram);
        bool Delete(int Id);
        SetUpDateForProgram GetById(int Id);
        List<SetUpDateForProgram> GetAll();
        List<SetUpDateForProgram> GetAll(int acaCalId, int programId, int typeId);
        SetUpDateForProgram GetActiveByProgram(int programId);
    }
}

