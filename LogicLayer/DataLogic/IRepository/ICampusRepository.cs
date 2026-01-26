using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICampusRepository
    {
        int Insert(Campus campus);
        bool Update(Campus campus);
        bool Delete(int CampusId);
        Campus GetById(int CampusId);
        List<Campus> GetAll();
    }
}

