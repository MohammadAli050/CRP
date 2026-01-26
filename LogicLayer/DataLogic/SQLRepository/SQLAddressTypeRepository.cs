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
    public partial class SqlAddressTypeRepository : IAddressTypeRepository
    {

        Database db = null;

        private string sqlInsert = "AddressTypeInsert";
        private string sqlUpdate = "AddressTypeUpdate";
        private string sqlDelete = "AddressTypeDelete";
        private string sqlGetById = "AddressTypeGetById";
        private string sqlGetAll = "AddressTypeGetAll";
               
        public int Insert(AddressType addresstype)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, addresstype, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "AddressTypeId");

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

        public bool Update(AddressType addresstype)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, addresstype, isInsert);

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

                db.AddInParameter(cmd, "AddressTypeId", DbType.Int32, id);
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

        public AddressType GetById(int id)
        {
            AddressType _addresstype = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AddressType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AddressType>(sqlGetById, rowMapper);
                _addresstype = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _addresstype;
            }

            return _addresstype;
        }

        public List<AddressType> GetAll()
        {
            List<AddressType> addresstypeList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AddressType> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AddressType>(sqlGetAll, mapper);
                IEnumerable<AddressType> collection = accessor.Execute();

                addresstypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return addresstypeList;
            }

            return addresstypeList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, AddressType addresstype, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "AddressTypeId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "AddressTypeId", DbType.Int32, addresstype.AddressTypeId);
            }

		    db.AddInParameter(cmd,"TypeName",DbType.String,addresstype.TypeName);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,addresstype.CreatedBy);
		    db.AddInParameter(cmd,"CreatedOn",DbType.DateTime,addresstype.CreatedOn);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,addresstype.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedOn",DbType.DateTime,addresstype.ModifiedOn);
            
            return db;
        }

        private IRowMapper<AddressType> GetMaper()
        {
            IRowMapper<AddressType> mapper = MapBuilder<AddressType>.MapAllProperties()

       	   .Map(m => m.AddressTypeId).ToColumn("AddressTypeId")
		.Map(m => m.TypeName).ToColumn("TypeName")
		.Map(m => m.CreatedBy).ToColumn("CreatedBy")
		.Map(m => m.CreatedOn).ToColumn("CreatedOn")
		.Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
		.Map(m => m.ModifiedOn).ToColumn("ModifiedOn")
            
            .Build();

            return mapper;
        }
        #endregion

    }
}

