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
    public partial class SqlUserInstitutionRepository : IUserInstitutionRepository
    {

        Database db = null;

        private string sqlInsert = "UserInstitutionInsert";
        private string sqlUpdate = "UserInstitutionUpdate";
        private string sqlDelete = "UserInstitutionDeleteById";
        private string sqlGetById = "UserInstitutionGetById";
        private string sqlGetAll = "UserInstitutionGetAll";
               
        public int Insert(UserInstitution userinstitution)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, userinstitution, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Id");

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

        public bool Update(UserInstitution userinstitution)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, userinstitution, isInsert);

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

                db.AddInParameter(cmd, "Id", DbType.Int32, id);
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

        public UserInstitution GetById(int? id)
        {
            UserInstitution _userinstitution = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserInstitution> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserInstitution>(sqlGetById, rowMapper);
                _userinstitution = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _userinstitution;
            }

            return _userinstitution;
        }

        public UserInstitution GetByInstituteIdUserId(int instituteId, int userId)
        {
            UserInstitution _userinstitution = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserInstitution> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserInstitution>("UserInstitutionGetById", rowMapper);
                _userinstitution = accessor.Execute(instituteId, userId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _userinstitution;
            }

            return _userinstitution;
        }

        public List<UserInstitution> GetAll()
        {
            List<UserInstitution> userinstitutionList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserInstitution> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserInstitution>(sqlGetAll, mapper);
                IEnumerable<UserInstitution> collection = accessor.Execute();

                userinstitutionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return userinstitutionList;
            }

            return userinstitutionList;
        }
        public List<UserInstitution> GetAllByUserId(int userId)
        {
            List<UserInstitution> userinstitutionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserInstitution> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserInstitution>("UserInstitutionGetAllByUserId", mapper);
                IEnumerable<UserInstitution> collection = accessor.Execute(userId);

                userinstitutionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return userinstitutionList;
            }

            return userinstitutionList;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, UserInstitution userinstitution, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, userinstitution.Id);
            }

            	
		db.AddInParameter(cmd,"User_ID",DbType.Int32,userinstitution.User_ID);
		db.AddInParameter(cmd,"InstitutionID",DbType.Int32,userinstitution.InstitutionID);
		db.AddInParameter(cmd,"ProgramID",DbType.Int32,userinstitution.ProgramID);
		db.AddInParameter(cmd,"Attribute1",DbType.String,userinstitution.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,userinstitution.Attribute2);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,userinstitution.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,userinstitution.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,userinstitution.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,userinstitution.ModifiedDate);
            
            return db;
        }

        private IRowMapper<UserInstitution> GetMaper()
        {
            IRowMapper<UserInstitution> mapper = MapBuilder<UserInstitution>.MapAllProperties()

       	   .Map(m => m.Id).ToColumn("Id")
		.Map(m => m.User_ID).ToColumn("User_ID")
		.Map(m => m.InstitutionID).ToColumn("InstitutionID")
		.Map(m => m.ProgramID).ToColumn("ProgramID")
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

