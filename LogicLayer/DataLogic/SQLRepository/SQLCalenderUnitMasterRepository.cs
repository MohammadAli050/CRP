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
    public partial class SQLCalenderUnitMasterRepository : ICalenderUnitMasterRepository
    {
        Database db = null;

        private string sqlInsert = "CalenderUnitMasterInsert";
        private string sqlUpdate = "CalenderUnitMasterUpdate";
        private string sqlDelete = "CalenderUnitMasterDeleteById";
        private string sqlGetById = "CalenderUnitMasterGetById";
        private string sqlGetAll = "CalenderUnitMasterGetAll";


        public int Insert(CalenderUnitMaster calenderUnitMaster)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, calenderUnitMaster, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "CalenderUnitMasterID");

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

        public bool Update(CalenderUnitMaster calenderUnitMaster)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, calenderUnitMaster, isInsert);

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

                db.AddInParameter(cmd, "CalenderUnitMasterID", DbType.Int32, id);
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

        public CalenderUnitMaster GetById(int? id)
        {
            CalenderUnitMaster _calenderUnitMaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CalenderUnitMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CalenderUnitMaster>(sqlGetById, rowMapper);
                _calenderUnitMaster = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _calenderUnitMaster;
            }

            return _calenderUnitMaster;
        }

        public List<CalenderUnitMaster> GetAll()
        {
            List<CalenderUnitMaster> calenderUnitMasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CalenderUnitMaster> mapper = GetMaper();

                var accessor = db.CreateSqlStringAccessor<CalenderUnitMaster>("Select * from CalenderUnitMaster", mapper);
                IEnumerable<CalenderUnitMaster> collection = accessor.Execute();

                calenderUnitMasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return calenderUnitMasterList;
            }

            return calenderUnitMasterList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CalenderUnitMaster calenderUnitMaster, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "CalenderUnitMasterID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "CalenderUnitMasterID", DbType.Int32, calenderUnitMaster.CalenderUnitMasterID);
            }

            db.AddInParameter(cmd, "CalenderUnitMasterID", DbType.Int32, calenderUnitMaster.CalenderUnitMasterID);
            db.AddInParameter(cmd, "Name", DbType.String, calenderUnitMaster.Name);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, calenderUnitMaster.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, calenderUnitMaster.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, calenderUnitMaster.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, calenderUnitMaster.ModifiedDate);

            return db;
        }

        private IRowMapper<CalenderUnitMaster> GetMaper()
        {
            IRowMapper<CalenderUnitMaster> mapper = MapBuilder<CalenderUnitMaster>.MapAllProperties()
            .Map(m => m.CalenderUnitMasterID).ToColumn("CalenderUnitMasterID")
            .Map(m => m.Name).ToColumn("Name")
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
