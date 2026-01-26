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
    public partial class SqlUserExamCenterRepository : IUserExamCenterRepository
    {

        Database db = null;

        private string sqlInsert = "UserExamCenterInsert";
        private string sqlUpdate = "UserExamCenterUpdate";
        private string sqlDelete = "UserExamCenterDeleteById";
        private string sqlGetById = "UserExamCenterGetById";
        private string sqlGetAll = "UserExamCenterGetAll";
               
        public int Insert(UserExamCenter userexamcenter)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, userexamcenter, isInsert);
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

        public bool Update(UserExamCenter userexamcenter)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, userexamcenter, isInsert);

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

        public UserExamCenter GetById(int? id)
        {
            UserExamCenter _userexamcenter = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserExamCenter> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserExamCenter>(sqlGetById, rowMapper);
                _userexamcenter = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _userexamcenter;
            }

            return _userexamcenter;
        }

        public UserExamCenter GetByExamCenterIdUserId(int examCenterId, int userId)
        {
            UserExamCenter _userExamCenter = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserExamCenter> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserExamCenter>("UserExamCenterGetById", rowMapper);
                _userExamCenter = accessor.Execute(examCenterId, userId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _userExamCenter;
            }

            return _userExamCenter;
        }

        public List<UserExamCenter> GetAll()
        {
            List<UserExamCenter> userexamcenterList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserExamCenter> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserExamCenter>(sqlGetAll, mapper);
                IEnumerable<UserExamCenter> collection = accessor.Execute();

                userexamcenterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return userexamcenterList;
            }

            return userexamcenterList;
        }

        public List<UserExamCenter> GetAllByUserId(int userId)
        {
            List<UserExamCenter> userexamcenterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserExamCenter> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserExamCenter>("UserExamCenterGetAllByUserId", mapper);
                IEnumerable<UserExamCenter> collection = accessor.Execute(userId);

                userexamcenterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return userexamcenterList;
            }

            return userexamcenterList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, UserExamCenter userexamcenter, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, userexamcenter.Id);
            }

            	
		db.AddInParameter(cmd,"User_Id",DbType.Int32,userexamcenter.User_Id);
		db.AddInParameter(cmd,"ExamCenterId",DbType.Int32,userexamcenter.ExamCenterId);
		db.AddInParameter(cmd,"Attribute1",DbType.String,userexamcenter.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,userexamcenter.Attribute2);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,userexamcenter.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,userexamcenter.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,userexamcenter.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,userexamcenter.ModifiedDate);
            
            return db;
        }

        private IRowMapper<UserExamCenter> GetMaper()
        {
            IRowMapper<UserExamCenter> mapper = MapBuilder<UserExamCenter>.MapAllProperties()

       	   .Map(m => m.Id).ToColumn("Id")
		.Map(m => m.User_Id).ToColumn("User_Id")
		.Map(m => m.ExamCenterId).ToColumn("ExamCenterId")
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

