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
    public partial class SqlUserAccessProgramRepository : IUserAccessProgramRepository
    {

        Database db = null;

        private string sqlInsert = "UserAccessProgramInsert";
        private string sqlUpdate = "UserAccessProgramUpdate";
        private string sqlDelete = "UserAccessProgramDeleteById";
        private string sqlGetById = "UserAccessProgramGetById";
        private string sqlGetAll = "UserAccessProgramGetAll";
               
        public int Insert(UserAccessProgram useraccessprogram)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, useraccessprogram, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ID");

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

        public bool Update(UserAccessProgram useraccessprogram)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, useraccessprogram, isInsert);

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

                db.AddInParameter(cmd, "ID", DbType.Int32, id);
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

        public UserAccessProgram GetById(int? id)
        {
            UserAccessProgram _useraccessprogram = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserAccessProgram> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserAccessProgram>(sqlGetById, rowMapper);
                _useraccessprogram = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _useraccessprogram;
            }

            return _useraccessprogram;
        }

        public UserAccessProgram GetByUserId(int? id)
        {
            UserAccessProgram _useraccessprogram = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserAccessProgram> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserAccessProgram>("UserAccessProgramGetByUser_ID", rowMapper);
                _useraccessprogram = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _useraccessprogram;
            }

            return _useraccessprogram;
        }

        public List<UserAccessProgram> GetAll()
        {
            List<UserAccessProgram> useraccessprogramList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserAccessProgram> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserAccessProgram>(sqlGetAll, mapper);
                IEnumerable<UserAccessProgram> collection = accessor.Execute();

                useraccessprogramList = collection.ToList();
            }

            catch (Exception ex)
            {
                return useraccessprogramList;
            }

            return useraccessprogramList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, UserAccessProgram useraccessprogram, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ID", DbType.Int32, useraccessprogram.ID);
            }

            	
		db.AddInParameter(cmd,"User_ID",DbType.Int32,useraccessprogram.User_ID);
		db.AddInParameter(cmd,"AccessPattern",DbType.String,useraccessprogram.AccessPattern);
		db.AddInParameter(cmd,"AccessStartDate",DbType.DateTime,useraccessprogram.AccessStartDate);
		db.AddInParameter(cmd,"AccessEndDate",DbType.DateTime,useraccessprogram.AccessEndDate);
        db.AddInParameter(cmd, "IsAllCourse", DbType.Boolean, useraccessprogram.IsAllCourse);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,useraccessprogram.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,useraccessprogram.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,useraccessprogram.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,useraccessprogram.ModifiedDate);
            
            return db;
        }

        private IRowMapper<UserAccessProgram> GetMaper()
        {
            IRowMapper<UserAccessProgram> mapper = MapBuilder<UserAccessProgram>.MapAllProperties()

       	   .Map(m => m.ID).ToColumn("ID")
		.Map(m => m.User_ID).ToColumn("User_ID")
		.Map(m => m.AccessPattern).ToColumn("AccessPattern")
		.Map(m => m.AccessStartDate).ToColumn("AccessStartDate")
		.Map(m => m.AccessEndDate).ToColumn("AccessEndDate")
        .Map(m=>m.IsAllCourse).ToColumn("IsAllCourse")
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

