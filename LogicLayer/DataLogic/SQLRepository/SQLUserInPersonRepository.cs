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
   partial class SQLUserInPersonRepository:IUserInPersonRepository
    {
        Database db = null;

        private string sqlInsert = "UserInPersonInsert";
        private string sqlUpdate = "UserInPersonUpdate";
        private string sqlDelete = "UserInPersonDeleteById";
        private string sqlGetById = "UserInPersonGetById";
        private string sqlGetAll = "UserInPersonGetAll";
        private string sqlGetByPersonId = "UserInPersonGetAllByPersonId";


        public int Insert(UserInPerson userInPerson)
        {
            int rowsAffected = 0;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, userInPerson, isInsert);


                rowsAffected = db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }

            return rowsAffected;
        }

        public bool Update(UserInPerson userInPerson)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, userInPerson, isInsert);

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

                db.AddInParameter(cmd, "UserInPersonID", DbType.Int32, id);
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

        public UserInPerson GetById(int id)
        {
            UserInPerson _userInPerson = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserInPerson> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserInPerson>(sqlGetById, rowMapper);
                _userInPerson = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _userInPerson;
            }

            return _userInPerson;
        }

        public List<UserInPerson> GetAll()
        {
            List<UserInPerson> userInPersonList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UserInPerson> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserInPerson>(sqlGetAll, mapper);
                IEnumerable<UserInPerson> collection = accessor.Execute();

                userInPersonList = collection.ToList();
            }

            catch (Exception ex)
            {
                return userInPersonList;
            }

            return userInPersonList;
        }

        //public UserInPerson GetByPersonId(int? id)
        //{
        //    UserInPerson _userInPerson = null;
        //    try
        //    {

        //        db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

        //        IRowMapper<UserInPerson> rowMapper = GetMaper();

        //        var accessor = db.CreateSprocAccessor<UserInPerson>(sqlGetByPersonId, rowMapper);
        //        _userInPerson = accessor.Execute(id).SingleOrDefault();

        //    }
        //    catch (Exception ex)
        //    {
        //        return _userInPerson;
        //    }

        //    return _userInPerson;
        //}

        public List<UserInPerson> GetByPersonId(int id)
        {
            List<UserInPerson> list = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                IRowMapper<UserInPerson> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UserInPerson>(sqlGetByPersonId, mapper);
                IEnumerable<UserInPerson> collection = accessor.Execute(id);

                list = collection.ToList();
            }

            catch (Exception ex)
            {
                return list;
            }

            return list;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, UserInPerson userInPerson, bool isInsert)
        {
            db.AddInParameter(cmd, "User_ID", DbType.Int32, userInPerson.User_ID);
            db.AddInParameter(cmd, "PersonID", DbType.Int32, userInPerson.PersonID);
            
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, userInPerson.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, userInPerson.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, userInPerson.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, userInPerson.ModifiedDate);

            return db;
        }

        private IRowMapper<UserInPerson> GetMaper()
        {
            IRowMapper<UserInPerson> mapper = MapBuilder<UserInPerson>.MapAllProperties()
            .Map(m => m.User_ID).ToColumn("User_ID")
            .Map(m => m.PersonID).ToColumn("PersonID")
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
