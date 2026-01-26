using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStdCrsBillWorksheetRepository
    {
        int Insert(StdCrsBillWorksheet stdCrsBillWorksheet);
        bool Update(StdCrsBillWorksheet stdCrsBillWorksheet);
        bool Delete(int id);
        StdCrsBillWorksheet GetById(int? id);
        List<StdCrsBillWorksheet> GetAll();
    }
}
