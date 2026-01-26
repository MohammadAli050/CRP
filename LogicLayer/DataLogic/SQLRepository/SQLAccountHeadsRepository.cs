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
    public partial class SQLAccountHeadsRepository : IAccountHeadsRepository
    {
        Database db = null;

        private string sqlInsert = "AccountHeadsInsert";
        private string sqlUpdate = "AccountHeadsUpdate";
        private string sqlDelete = "AccountHeadsDeleteById";
        private string sqlGetById = "AccountHeadsGetById";
        private string sqlGetAll = "AccountHeadsGetAll";
        
        public int Insert(AccountHeads accountHeads)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, accountHeads, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "AccountsID");

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

        public bool Update(AccountHeads accountHeads)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, accountHeads, isInsert);

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

                db.AddInParameter(cmd, "AccountsID", DbType.Int32, id);
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

        public AccountHeads GetById(int? id)
        {
            AccountHeads _accountHeads = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AccountHeads> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AccountHeads>(sqlGetById, rowMapper);
                _accountHeads = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _accountHeads;
            }

            return _accountHeads;
        }

        public List<AccountHeads> GetAll()
        {
            List<AccountHeads> accountHeadsList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<AccountHeads> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<AccountHeads>(sqlGetAll, mapper);
                IEnumerable<AccountHeads> collection = accessor.Execute();

                accountHeadsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return accountHeadsList;
            }

            return accountHeadsList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, AccountHeads accountHeads, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "AccountsID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "AccountsID", DbType.Int32, accountHeads.AccountsID);
            }

            db.AddInParameter(cmd, "Name", DbType.String, accountHeads.Name);
            db.AddInParameter(cmd, "Code", DbType.String, accountHeads.Code);
            db.AddInParameter(cmd, "ParentID", DbType.Int32, accountHeads.ParentID);
            db.AddInParameter(cmd, "Tag", DbType.String, accountHeads.Tag);
            db.AddInParameter(cmd, "Remarks", DbType.String, accountHeads.Remarks);
            db.AddInParameter(cmd, "IsLeaf", DbType.Boolean, accountHeads.IsLeaf);
            db.AddInParameter(cmd, "SysMandatory", DbType.Boolean, accountHeads.SysMandatory);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, accountHeads.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, accountHeads.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, accountHeads.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, accountHeads.ModifiedDate);
           
            return db;
        }

        private IRowMapper<AccountHeads> GetMaper()
        {
            IRowMapper<AccountHeads> mapper = MapBuilder<AccountHeads>.MapAllProperties()
            .Map(m => m.AccountsID).ToColumn("AccountsID")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.Code).ToColumn("Code")
            .Map(m => m.ParentID).ToColumn("ParentID")
            .Map(m => m.Tag).ToColumn("Tag")
            .Map(m => m.Remarks).ToColumn("Remarks")
            .Map(m => m.IsLeaf).ToColumn("IsLeaf")
            .Map(m => m.SysMandatory).ToColumn("SysMandatory")
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
