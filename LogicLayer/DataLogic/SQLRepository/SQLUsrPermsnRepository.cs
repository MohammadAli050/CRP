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
    public partial class SQLUsrPermsnRepository : IUsrPermsnRepository
    {
        Database db = null;

        private string sqlInsert = "UsrPermsnInsert";
        private string sqlUpdate = "UsrPermsnUpdate";
        private string sqlDelete = "UsrPermsnDeleteById";
        private string sqlGetById = "UsrPermsnGetById";
        private string sqlGetAll = "UsrPermsnGetAll";
        
        
        public int Insert(UsrPermsn usrPermsn)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, usrPermsn, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "UsrPermsn_ID");

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

        public bool Update(UsrPermsn usrPermsn)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, usrPermsn, isInsert);

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

                db.AddInParameter(cmd, "UsrPermsn_ID", DbType.Int32, id);
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

        public UsrPermsn GetById(int? id)
        {
            UsrPermsn _usrPermsn = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UsrPermsn> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UsrPermsn>(sqlGetById, rowMapper);
                _usrPermsn = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _usrPermsn;
            }

            return _usrPermsn;
        }

        public List<UsrPermsn> GetAll()
        {
            List<UsrPermsn> usrPermsnList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<UsrPermsn> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<UsrPermsn>(sqlGetAll, mapper);
                IEnumerable<UsrPermsn> collection = accessor.Execute();

                usrPermsnList = collection.ToList();
            }

            catch (Exception ex)
            {
                return usrPermsnList;
            }

            return usrPermsnList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, UsrPermsn usrPermsn, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "UsrPermsn_ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "UsrPermsn_ID", DbType.Int32, usrPermsn.UsrPermsn_ID);
            }

            db.AddInParameter(cmd, "User_ID", DbType.Int32, usrPermsn.User_ID);
            db.AddInParameter(cmd, "AccessIDPattern", DbType.String, usrPermsn.AccessIDPattern);
            db.AddInParameter(cmd, "AccessStartDate", DbType.DateTime, usrPermsn.AccessStartDate);
            db.AddInParameter(cmd, "AccessEndDate", DbType.DateTime, usrPermsn.AccessEndDate);
            db.AddInParameter(cmd, "CreatedBy", DbType.String, usrPermsn.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, usrPermsn.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, usrPermsn.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, usrPermsn.ModifiedDate);
           
            return db;
        }

        private IRowMapper<UsrPermsn> GetMaper()
        {
            IRowMapper<UsrPermsn> mapper = MapBuilder<UsrPermsn>.MapAllProperties()
            .Map(m => m.UsrPermsn_ID).ToColumn("UsrPermsn_ID")
            .Map(m => m.User_ID).ToColumn("User_ID")
            .Map(m => m.AccessIDPattern).ToColumn("AccessIDPattern")
            .Map(m => m.AccessStartDate).ToColumn("AccessStartDate")
            .Map(m => m.AccessEndDate).ToColumn("AccessEndDate")
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
