using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IRoomInformationRepository
    {
        RoomInformation GetById(int? id);
        List<RoomInformation> GetAll();
        List<RoomInformation> GetAllByBuildingIdCustom(int buildingId, int acaCalId, int examScheduleSetId, int dayId, int timeSlotId);

        List<RoomList> LoadRoom(int examScheduleSetId, int dayId, int timeSlotId);

        int Insert(RoomInformation roominformation);
        bool Update(RoomInformation roominformation);
        bool Delete(int id);
    }
}
