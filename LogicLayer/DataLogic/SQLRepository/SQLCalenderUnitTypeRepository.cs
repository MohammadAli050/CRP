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
    public partial class SQLCalenderUnitTypeRepository : ICalenderUnitTypeRepository
    {
        Database db = null;

        private string sqlInsert = "CalenderUnitTypeInsert";
        private string sqlUpdate = "CalenderUnitTypeUpdate";
        private string sqlDelete = "CalenderUnitTypeDeleteById";
        private string sqlGetById = "CalenderUnitTypeGetById";
        private string sqlGetAll = "CalenderUnitTypeGetAll";
        private string sqlGetByCalenderUnitTypeID = "CalenderUnitTypeGetByCalenderUnitTypeID";


        public int Insert(CalenderUnitType calenderUnitType)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, calenderUnitType, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "CalenderUnitTypeID");

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

        public bool Update(CalenderUnitType calenderUnitType)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, calenderUnitType, isInsert);

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

                db.AddInParameter(cmd, "CalenderUnitTypeID", DbType.Int32, id);
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

        public CalenderUnitType GetById(int? id)
        {
            CalenderUnitType _calenderUnitType = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CalenderUnitType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CalenderUnitType>(sqlGetById, rowMapper);
                _calenderUnitType = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _calenderUnitType;
            }

            return _calenderUnitType;
        }

        public CalenderUnitType GetByCalenderUnitTypeID(int calenderUnitTypeID)
        {
            CalenderUnitType _calenderUnitType = new CalenderUnitType();
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CalenderUnitType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CalenderUnitType>(sqlGetByCalenderUnitTypeID, rowMapper);
                _calenderUnitType = accessor.Execute(calenderUnitTypeID).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _calenderUnitType;
            }

            return _calenderUnitType;
        }

        public List<CalenderUnitType> GetAll()
        {
            List<CalenderUnitType> calenderUnitTypeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CalenderUnitType> mapper = GetMaper();

                var accessor = db.CreateSqlStringAccessor<CalenderUnitType>("Select * from CalenderUnitType", mapper);
                IEnumerable<CalenderUnitType> collection = accessor.Execute();

                calenderUnitTypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return calenderUnitTypeList;
            }

            return calenderUnitTypeList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CalenderUnitType calenderUnitType, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "CalenderUnitTypeID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "CalenderUnitTypeID", DbType.Int32, calenderUnitType.CalenderUnitTypeID);
            }

            db.AddInParameter(cmd, "CalenderUnitMasterID", DbType.Int32, calenderUnitType.CalenderUnitMasterID);
            db.AddInParameter(cmd, "TypeName", DbType.String, calenderUnitType.TypeName);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, calenderUnitType.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, calenderUnitType.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, calenderUnitType.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, calenderUnitType.ModifiedDate);

            return db;
        }

        private IRowMapper<CalenderUnitType> GetMaper()
        {
            IRowMapper<CalenderUnitType> mapper = MapBuilder<CalenderUnitType>.MapAllProperties()
            .Map(m => m.CalenderUnitTypeID).ToColumn("CalenderUnitTypeID")
            .Map(m => m.CalenderUnitMasterID).ToColumn("CalenderUnitMasterID")
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
