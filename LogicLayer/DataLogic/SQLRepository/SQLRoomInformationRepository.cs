using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLRoomInformationRepository : IRoomInformationRepository
    {
        Database db = null;

        private string sqlInsert = "RoomInformationInsert";
        private string sqlUpdate = "RoomInformationUpdate";
        private string sqlDelete = "RoomInformationDeleteById";
        private string sqlGetById = "RoomInformationGetById";
        private string sqlGetAll = "RoomInformationGetAll";
        private string sqlGetAllByBuildingIdCustom = "RoomInformationGetAllByBuildingIdCustom";

        public int Insert(RoomInformation roominformation)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, roominformation, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "RoomInfoID");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }
            }
            catch (Exception ex)
            {
                id = 0;
            }

            return id;
        }

        public bool Update(RoomInformation roominformation)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, roominformation, isInsert);

                int rowsAffected = db.ExecuteNonQuery(cmd);

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDelete);

                db.AddInParameter(cmd, "RoomInfoID", DbType.Int32, id);
                int rowsAffected = db.ExecuteNonQuery(cmd);

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public RoomInformation GetById(int? id)
        {
            RoomInformation _roomInformation = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RoomInformation> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RoomInformation>(sqlGetById, rowMapper);
                _roomInformation = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _roomInformation;
            }

            return _roomInformation;
        }

        public List<RoomInformation> GetAll()
        {
            List<RoomInformation> RoomInformationList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RoomInformation> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RoomInformation>(sqlGetAll, mapper);
                IEnumerable<RoomInformation> collection = accessor.Execute();

                RoomInformationList = collection.ToList();
            }

            catch (Exception ex)
            {
                return RoomInformationList;
            }

            return RoomInformationList;
        }

        public List<RoomInformation> GetAllByBuildingIdCustom(int buildingId, int acaCalId, int examScheduleSetId, int dayId, int timeSlotId)
        {
            List<RoomInformation> roomInformationList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RoomInformation> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RoomInformation>(sqlGetAllByBuildingIdCustom, mapper);
                IEnumerable<RoomInformation> collection = accessor.Execute(buildingId, acaCalId, examScheduleSetId, dayId, timeSlotId);

                roomInformationList = collection.ToList();
            }

            catch (Exception ex)
            {
                return roomInformationList;
            }

            return roomInformationList;
        }

        public List<RoomList> LoadRoom(int examScheduleSetId, int dayId, int timeSlotId)
        {
            List<RoomList> roomList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RoomList> mapper = GetRoomListMaper();

                var accessor = db.CreateSprocAccessor<RoomList>("RoomList", mapper);
                IEnumerable<RoomList> collection = accessor.Execute(examScheduleSetId, dayId, timeSlotId);

                roomList = collection.ToList();
            }

            catch (Exception ex)
            {
                return roomList;
            }

            return roomList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, RoomInformation roominformation, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "RoomInfoID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "RoomInfoID", DbType.Int32, roominformation.RoomInfoID);
            }


            db.AddInParameter(cmd, "RoomNumber", DbType.String, roominformation.RoomNumber);
            db.AddInParameter(cmd, "RoomName", DbType.String, roominformation.RoomName);
            db.AddInParameter(cmd, "RoomFloorNo", DbType.String, roominformation.RoomFloorNo);
            db.AddInParameter(cmd, "RoomTypeID", DbType.Int32, roominformation.RoomTypeID);
            db.AddInParameter(cmd, "Capacity", DbType.Int32, roominformation.Capacity);
            db.AddInParameter(cmd, "ExamCapacity", DbType.Int32, roominformation.ExamCapacity);
            db.AddInParameter(cmd, "Rows", DbType.Int32, roominformation.Rows);
            db.AddInParameter(cmd, "Columns", DbType.Int32, roominformation.Columns);
            db.AddInParameter(cmd, "BuildingId", DbType.Int32, roominformation.BuildingId);
            db.AddInParameter(cmd, "AddressID", DbType.Int32, roominformation.AddressID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, roominformation.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, roominformation.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, roominformation.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, roominformation.ModifiedDate);

            return db;
        }

        private IRowMapper<RoomInformation> GetMaper()
        {
            IRowMapper<RoomInformation> mapper = MapBuilder<RoomInformation>.MapAllProperties()
            .Map(m => m.RoomInfoID).ToColumn("RoomInfoID")
            .Map(m => m.RoomNumber).ToColumn("RoomNumber")
            .Map(m => m.RoomName).ToColumn("RoomName")
            .Map(m => m.RoomFloorNo).ToColumn("RoomFloorNo")
            .Map(m => m.RoomTypeID).ToColumn("RoomTypeID")
            .Map(m => m.Capacity).ToColumn("Capacity")
            .Map(m => m.ExamCapacity).ToColumn("ExamCapacity")
            .Map(m => m.Rows).ToColumn("Rows")
            .Map(m => m.Columns).ToColumn("Columns")
            .Map(m => m.BuildingId).ToColumn("BuildingId")
            .Map(m => m.AddressID).ToColumn("AddressID")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .DoNotMap(m => m.RomeTypeName)
            .DoNotMap(m => m.CampusName)
            .DoNotMap(m => m.BuildingName)


            .Build();

            return mapper;
        }

        private IRowMapper<RoomList> GetRoomListMaper()
        {
            IRowMapper<RoomList> mapper = MapBuilder<RoomList>.MapAllProperties()
            .Map(m => m.id).ToColumn("id")
            .Map(m => m.RoomNo).ToColumn("RoomNo")
            .Map(m => m.RoomName).ToColumn("RoomName")

            .Build();

            return mapper;
        }
        #endregion

    }
}