using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLRoomTypeRepository : IRoomTypeRepository
    {
        Database db = null;

        private string sqlInsert = "RoomTypeInsert";
        private string sqlUpdate = "RoomTypeUpdate";
        private string sqlDelete = "RoomTypeDeleteById";
        private string sqlGetById = "RoomTypeGetById";
        private string sqlGetAll = "RoomTypeGetAll";
        
        public int Insert(RoomType roomType)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, roomType, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "RoomTypeID");

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

        public bool Update(RoomType roomType)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, roomType, isInsert);

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

                db.AddInParameter(cmd, "RoomTypeID", DbType.Int32, id);
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

        public RoomType GetById(int? id)
        {
            RoomType _roomType = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RoomType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RoomType>(sqlGetById, rowMapper);
                _roomType = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _roomType;
            }

            return _roomType;
        }

        public List<RoomType> GetAll()
        {
            List<RoomType> roomTypeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RoomType> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RoomType>(sqlGetAll, mapper);
                IEnumerable<RoomType> collection = accessor.Execute();

                roomTypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return roomTypeList;
            }

            return roomTypeList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, RoomType roomType, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "RoomTypeID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "RoomTypeID", DbType.Int32, roomType.RoomTypeID);
            }

            db.AddInParameter(cmd, "TypeName", DbType.String, roomType.TypeName);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, roomType.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, roomType.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, roomType.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, roomType.ModifiedDate);
            
            return db;
        }

        private IRowMapper<RoomType> GetMaper()
        {
            IRowMapper<RoomType> mapper = MapBuilder<RoomType>.MapAllProperties()
            .Map(m => m.RoomTypeID).ToColumn("RoomTypeID")
            .Map(m => m.TypeName).ToColumn("TypeName")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            
            .Build();

            return mapper;
        }
        #endregion
    }
}
