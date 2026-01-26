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
    public partial class SQLRegistrationDateTimeLimitInBatchRepository : IRegistrationDateTimeLimitInBatchRepository
    {
        Database db = null;

        private string sqlInsert = "RegistrationDateTimeLimitInBatchInsert";
        private string sqlUpdate = "RegistrationDateTimeLimitInBatchUpdate";
        private string sqlDelete = "RegistrationDateTimeLimitInBatchDeleteById";
        private string sqlGetById = "RegistrationDateTimeLimitInBatchGetById";
        private string sqlGetAll = "RegistrationDateTimeLimitInBatchGetAll";
        
        
        public int Insert(RegistrationDateTimeLimitInBatch registrationDateTimeLimitInBatch)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, registrationDateTimeLimitInBatch, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "RegistrationDateTimeLimitID");

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

        public bool Update(RegistrationDateTimeLimitInBatch registrationDateTimeLimitInBatch)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, registrationDateTimeLimitInBatch, isInsert);

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

                db.AddInParameter(cmd, "RegistrationDateTimeLimitID", DbType.Int32, id);
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

        public RegistrationDateTimeLimitInBatch GetById(int? id)
        {
            RegistrationDateTimeLimitInBatch _registrationDateTimeLimitInBatch = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RegistrationDateTimeLimitInBatch> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationDateTimeLimitInBatch>(sqlGetById, rowMapper);
                _registrationDateTimeLimitInBatch = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _registrationDateTimeLimitInBatch;
            }

            return _registrationDateTimeLimitInBatch;
        }

        public List<RegistrationDateTimeLimitInBatch> GetAll()
        {
            List<RegistrationDateTimeLimitInBatch> registrationDateTimeLimitInBatchList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RegistrationDateTimeLimitInBatch> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RegistrationDateTimeLimitInBatch>(sqlGetAll, mapper);
                IEnumerable<RegistrationDateTimeLimitInBatch> collection = accessor.Execute();

                registrationDateTimeLimitInBatchList = collection.ToList();
            }

            catch (Exception ex)
            {
                return registrationDateTimeLimitInBatchList;
            }

            return registrationDateTimeLimitInBatchList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, RegistrationDateTimeLimitInBatch registrationDateTimeLimitInBatch, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "RegistrationDateTimeLimitID", DbType.Int32, Int32.MaxValue);
                db.AddOutParameter(cmd, "BatchID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "RegistrationDateTimeLimitID", DbType.Int32, registrationDateTimeLimitInBatch.RegistrationDateTimeLimitID);
                db.AddInParameter(cmd, "BatchID", DbType.Int32, registrationDateTimeLimitInBatch.BatchID);
            }

            db.AddInParameter(cmd, "Pattern", DbType.String, registrationDateTimeLimitInBatch.Pattern);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, registrationDateTimeLimitInBatch.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, registrationDateTimeLimitInBatch.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, registrationDateTimeLimitInBatch.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, registrationDateTimeLimitInBatch.ModifiedDate);
            
            return db;
        }

        private IRowMapper<RegistrationDateTimeLimitInBatch> GetMaper()
        {
            IRowMapper<RegistrationDateTimeLimitInBatch> mapper = MapBuilder<RegistrationDateTimeLimitInBatch>.MapAllProperties()
            .Map(m => m.RegistrationDateTimeLimitID).ToColumn("RegistrationDateTimeLimitID")
            .Map(m => m.BatchID).ToColumn("BatchID")
            .Map(m => m.Pattern).ToColumn("Pattern")
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
