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
    public partial class SqlEmployeeTypeRepository : IEmployeeTypeRepository
    {

        Database db = null;

        private string sqlInsert = "EmployeeTypeInsert";
        private string sqlUpdate = "EmployeeTypeUpdate";
        private string sqlDelete = "EmployeeTypeDeleteById";
        private string sqlGetById = "EmployeeTypeGetById";
        private string sqlGetAll = "EmployeeTypeGetAll";
               
        public int Insert(EmployeeType employeetype)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, employeetype, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "EmployeeTypeId");

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

        public bool Update(EmployeeType employeetype)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, employeetype, isInsert);

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

                db.AddInParameter(cmd, "EmployeeTypeId", DbType.Int32, id);
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

        public EmployeeType GetById(int? id)
        {
            EmployeeType _employeetype = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EmployeeType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<EmployeeType>(sqlGetById, rowMapper);
                _employeetype = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _employeetype;
            }

            return _employeetype;
        }

        public List<EmployeeType> GetAll()
        {
            List<EmployeeType> employeetypeList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<EmployeeType> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<EmployeeType>(sqlGetAll, mapper);
                IEnumerable<EmployeeType> collection = accessor.Execute();

                employeetypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return employeetypeList;
            }

            return employeetypeList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, EmployeeType employeetype, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "EmployeeTypeId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "EmployeeTypeId", DbType.Int32, employeetype.EmployeeTypeId);
            }

            	
		db.AddInParameter(cmd,"EmployeTypeName",DbType.String,employeetype.EmployeTypeName);
		db.AddInParameter(cmd,"Attribute1",DbType.String,employeetype.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,employeetype.Attribute2);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,employeetype.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,employeetype.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,employeetype.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,employeetype.ModifiedDate);
            
            return db;
        }

        private IRowMapper<EmployeeType> GetMaper()
        {
            IRowMapper<EmployeeType> mapper = MapBuilder<EmployeeType>.MapAllProperties()

       	   .Map(m => m.EmployeeTypeId).ToColumn("EmployeeTypeId")
		.Map(m => m.EmployeTypeName).ToColumn("EmployeTypeName")
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

