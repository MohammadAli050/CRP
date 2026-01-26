using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IBuildingRepository
    {
        int Insert(Building building);
        bool Update(Building building);
        bool Delete(int BuildingId);
        Building GetById(int BuildingId);
        List<Building> GetAll();
    }
}

