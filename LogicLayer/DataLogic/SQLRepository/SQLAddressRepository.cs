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
    public partial class SQLAddressRepository : IAddressRepository
    {
        Database db = null;

        private string sqlInsert = "AddressInsert";
        private string sqlUpdate = "AddressUpdate";
        private string sqlDelete = "AddressDeleteById";
        private string sqlGetById = "AddressGetById";
        private string sqlGetAll = "AddressGetAll";
        private string sqlGetByRoll = "AddressGetByRoll";
        private string sqlGetByPersonId = "AddressGetByPersonId";
        
        public int Insert(Address address)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, address, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "AddressID");

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

        public bool Update(Address address)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, address, isInsert);

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

                db.AddInParameter(cmd, "AddressID", DbType.Int32, id);
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

        public Address GetById(int id)
        {
            Address _address = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Address> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Address>(sqlGetById, rowMapper);
                _address = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _address;
            }

            return _address;
        }

        public List<Address> GetAll()
        {
            List<Address> addressList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Address> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Address>(sqlGetAll, mapper);
                IEnumerable<Address> collection = accessor.Execute();

                addressList = collection.ToList();
            }

            catch (Exception ex)
            {
                return addressList;
            }

            return addressList;
        }

        public List<AddressByRoll> GetAddressByRoll(string roll)
        {
            List<AddressByRoll> addressList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AddressByRoll> mapper = GetMaperAdd();

                var accessor = db.CreateSprocAccessor<AddressByRoll>(sqlGetByRoll, mapper);
                IEnumerable<AddressByRoll> collection = accessor.Execute(roll).ToList();

                addressList = collection.ToList();
            }

            catch (Exception ex)
            {
                return addressList;
            }

            return addressList;
        }

        private IRowMapper<AddressByRoll> GetMaperAdd()
        {
            IRowMapper<AddressByRoll> mapper = MapBuilder<AddressByRoll>.MapAllProperties()

            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.AddressTypeId).ToColumn("AddressTypeId")
            .Map(m => m.AddressLine).ToColumn("AddressLine")
           
            .Build();

            return mapper;
        }

        public List<Address> GetAddressByPersonId(int personId)
        {
            List<Address> addressList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Address> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Address>(sqlGetByPersonId, mapper);
                IEnumerable<Address> collection = accessor.Execute(personId).ToList();

                addressList = collection.ToList();
            }

            catch (Exception ex)
            {
                return addressList;
            }

            return addressList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Address address, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "AddressId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "AddressId", DbType.Int32, address.AddressId);
            }

		        db.AddInParameter(cmd,"PersonId",DbType.Int32,address.PersonId);
		        db.AddInParameter(cmd,"AddressTypeId",DbType.Int32,address.AddressTypeId);
		        db.AddInParameter(cmd,"AddressLine",DbType.String,address.AddressLine);
		        db.AddInParameter(cmd,"District",DbType.Int32,address.District);
		        db.AddInParameter(cmd,"Division",DbType.Int32,address.Division);
		        db.AddInParameter(cmd,"PostCode",DbType.String,address.PostCode);
		        db.AddInParameter(cmd,"Phone",DbType.String,address.Phone);
		        db.AddInParameter(cmd,"Mobile",DbType.String,address.Mobile);
		        db.AddInParameter(cmd,"Email",DbType.String,address.Email);
		        db.AddInParameter(cmd,"Attribute1",DbType.String,address.Attribute1);
		        db.AddInParameter(cmd,"Attribute2",DbType.String,address.Attribute2);
		        db.AddInParameter(cmd,"Attribute3",DbType.String,address.Attribute3);
		        db.AddInParameter(cmd,"CreatedBy",DbType.Int32,address.CreatedBy);
		        db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,address.CreatedDate);
		        db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,address.ModifiedBy);
		        db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,address.ModifiedDate);
            
            return db;
        }

        private IRowMapper<Address> GetMaper()
        {
            IRowMapper<Address> mapper = MapBuilder<Address>.MapAllProperties()

            .Map(m => m.AddressId).ToColumn("AddressId")
            .Map(m => m.PersonId).ToColumn("PersonId")
            .Map(m => m.AddressTypeId).ToColumn("AddressTypeId")
            .Map(m => m.AddressLine).ToColumn("AddressLine")
            .Map(m => m.District).ToColumn("District")
            .Map(m => m.Division).ToColumn("Division")
            .Map(m => m.PostCode).ToColumn("PostCode")
            .Map(m => m.Phone).ToColumn("Phone")
            .Map(m => m.Mobile).ToColumn("Mobile")
            .Map(m => m.Email).ToColumn("Email")
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
