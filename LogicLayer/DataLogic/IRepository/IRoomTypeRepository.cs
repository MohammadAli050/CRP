using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IRoomTypeRepository
    {
        int Insert(RoomType roomType);
        bool Update(RoomType roomType);
        bool Delete(int id);
        RoomType GetById(int? id);
        List<RoomType> GetAll();
    }
}
