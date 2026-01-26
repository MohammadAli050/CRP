using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IProgramRepository
    {
        int Insert(Program program);
        bool Update(Program program);
        bool Delete(int id);
        Program GetById(int? id);
        List<Program> GetAll();
        List<Program> GetByAcaCalSectionID(int AcaCalSectionID);
    }
}
