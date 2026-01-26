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
    partial class SQLStatusTypeRepository : IStatusTypeRepository
    {
        Database db = null;

        private string sqlInsert = "StatusTypeInsert";
        private string sqlUpdate = "StatusTypeUpdate";
        private string sqlDelete = "StatusTypeDeleteById";
        private string sqlGetById = "StatusTypeGetById";
        private string sqlGetAll = "StatusTypeGetAll";


        public int Insert(StatusType statusType)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, statusType, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "StatusTypeID");

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

        public bool Update(StatusType statusType)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, statusType, isInsert);

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

                db.AddInParameter(cmd, "StatusTypeID", DbType.Int32, id);
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

        public StatusType GetById(int? id)
        {
            StatusType _statusType = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StatusType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StatusType>(sqlGetById, rowMapper);
                _statusType = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _statusType;
            }

            return _statusType;
        }

        public List<StatusType> GetAll()
        {
            List<StatusType> statusTypeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StatusType> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StatusType>(sqlGetAll, mapper);
                IEnumerable<StatusType> collection = accessor.Execute();

                statusTypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return statusTypeList;
            }

            return statusTypeList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StatusType statusType, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "StatusTypeID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "StatusTypeID", DbType.Int32, statusType.StatusTypeID);
            }

            db.AddInParameter(cmd, "StatusTypeID", DbType.Int32, statusType.StatusTypeID);
            db.AddInParameter(cmd, "TypeDescription", DbType.String, statusType.TypeDescription);
            return db;
        }

        private IRowMapper<StatusType> GetMaper()
        {
            IRowMapper<StatusType> mapper = MapBuilder<StatusType>.MapAllProperties()
            .Map(m => m.StatusTypeID).ToColumn("StatusTypeID")
            .Map(m => m.TypeDescription).ToColumn("TypeDescription")
            
            .Build();

            return mapper;
        }
        #endregion
    }
}
