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
    public partial class SQLValueRepository :  IValueRepository
    {
        Database db = null;

        private string sqlInsert = "ValueInsert";
        private string sqlUpdate = "ValueUpdate";
        private string sqlDelete = "ValueDeleteById";
        private string sqlGetById = "ValueGetById";
        private string sqlGetAll = "ValueGetAll";
        private string sqlGetByValueSetId = "ValueGetByValueSetId";
        
        public int Insert(Value value)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, value, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ValueID");

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

        public bool Update(Value value)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, value, isInsert);

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

                db.AddInParameter(cmd, "ValueID", DbType.Int32, id);
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

        public Value GetById(int id)
        {
            Value _value = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Value> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Value>(sqlGetById, rowMapper);
                _value = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _value;
            }

            return _value;
        }

        public List<Value> GetAll()
        {
            List<Value> valueList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Value> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Value>(sqlGetAll, mapper);
                IEnumerable<Value> collection = accessor.Execute();

                valueList = collection.ToList();
            }

            catch (Exception ex)
            {
                return valueList;
            }

            return valueList;
        }

        public List<Value> GetByValueSetId(int valueSet)
        {
            List<Value> _valueList = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Value> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Value>(sqlGetByValueSetId, rowMapper);
                _valueList = accessor.Execute(valueSet).ToList();

            }
            catch (Exception ex)
            {
                return _valueList;
            }

            return _valueList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Value value, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ValueID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ValueID", DbType.Int32, value.ValueID);
            }

            //db.AddInParameter(cmd, "ValueID", DbType.Int32, value.ValueID);
            db.AddInParameter(cmd, "ValueName", DbType.String, value.ValueName);
            db.AddInParameter(cmd, "ValueCode", DbType.String, value.ValueCode);
            db.AddInParameter(cmd, "ValueSetID", DbType.Int32, value.ValueSetID);
            db.AddInParameter(cmd, "ParentValueID", DbType.Int32, value.ParentValueID);
            db.AddInParameter(cmd, "Remarks", DbType.String, value.Remarks);
            db.AddInParameter(cmd, "Attribute1", DbType.String, value.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, value.Attribute2);
            db.AddInParameter(cmd, "Attribute3", DbType.String, value.Attribute3);
            db.AddInParameter(cmd, "CreatedBy", DbType.String, value.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, value.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, value.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, value.ModifiedDate);

            return db;
        }

        private IRowMapper<Value> GetMaper()
        {
            IRowMapper<Value> mapper = MapBuilder<Value>.MapAllProperties()
            .Map(m => m.ValueID).ToColumn("ValueID")
            .Map(m => m.ValueName).ToColumn("ValueName")
            .Map(m => m.ValueCode).ToColumn("ValueCode")
            .Map(m => m.ValueSetID).ToColumn("ValueSetID")
            .Map(m => m.ParentValueID).ToColumn("ParentValueID")
            .Map(m => m.Remarks).ToColumn("Remarks")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
            .Map(m => m.Attribute3).ToColumn("Attribute3")
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
