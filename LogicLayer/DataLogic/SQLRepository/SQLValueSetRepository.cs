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
    public partial class SQLValueSetRepository : IValueSetRepository
    {

        Database db = null;

        private string sqlInsert = "ValueSetInsert";
        private string sqlUpdate = "ValueSetUpdate";
        private string sqlDelete = "ValueSetDeleteById";
        private string sqlGetById = "ValueSetGetById";
        private string sqlGetAll = "ValueSetGetAll";
        
        public int Insert(ValueSet valueset)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, valueset, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ValueSetID");

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

        public bool Update(ValueSet valueset)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, valueset, isInsert);

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

                db.AddInParameter(cmd, "ValueSetID", DbType.Int32, id);
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

        public ValueSet GetById(int? id)
        {
            ValueSet _valueset = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ValueSet> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ValueSet>(sqlGetById, rowMapper);
                _valueset = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _valueset;
            }

            return _valueset;
        }

        public List<ValueSet> GetAll()
        {
            List<ValueSet> valuesetList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ValueSet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ValueSet>(sqlGetAll, mapper);
                IEnumerable<ValueSet> collection = accessor.Execute();

                valuesetList = collection.ToList();
            }

            catch (Exception ex)
            {
                return valuesetList;
            }

            return valuesetList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ValueSet valueset, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ValueSetID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ValueSetID", DbType.Int32, valueset.ValueSetID);
            }

            // db.AddInParameter(cmd, "ValueSetID", DbType.Int32, valueset.ValueSetID);
            db.AddInParameter(cmd, "ValueSetName", DbType.String, valueset.ValueSetName);
            db.AddInParameter(cmd, "ValueSetCode", DbType.String, valueset.ValueSetCode);
            db.AddInParameter(cmd, "Remarks", DbType.String, valueset.Remarks);
            db.AddInParameter(cmd, "Attribute1", DbType.String, valueset.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, valueset.Attribute2);
            db.AddInParameter(cmd, "CreatedBy", DbType.String, valueset.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, valueset.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, valueset.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, valueset.ModifiedDate);

            return db;
        }

        private IRowMapper<ValueSet> GetMaper()
        {
            IRowMapper<ValueSet> mapper = MapBuilder<ValueSet>.MapAllProperties()
            .Map(m => m.ValueSetName).ToColumn("ValueSetName")
            .Map(m => m.ValueSetCode).ToColumn("ValueSetCode")
            .Map(m => m.Remarks).ToColumn("Remarks")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
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
