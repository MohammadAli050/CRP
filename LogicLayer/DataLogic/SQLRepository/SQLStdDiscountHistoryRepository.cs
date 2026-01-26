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
    public partial class SQLStdDiscountHistoryRepository : IStdDiscountHistoryRepository
    {
        Database db = null;

        private string sqlInsert = "StdDiscountHistoryInsert";
        private string sqlUpdate = "StdDiscountHistoryUpdate";
        private string sqlDelete = "StdDiscountHistoryDeleteById";
        private string sqlGetById = "StdDiscountHistoryGetById";
        private string sqlGetAll = "StdDiscountHistoryGetAll";
        
        
        public int Insert(StdDiscountHistory stdDiscountHistory)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, stdDiscountHistory, isInsert);
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

        public bool Update(StdDiscountHistory stdDiscountHistory)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, stdDiscountHistory, isInsert);

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

        public StdDiscountHistory GetById(int? id)
        {
            StdDiscountHistory _stdDiscountHistory = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StdDiscountHistory> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StdDiscountHistory>(sqlGetById, rowMapper);
                _stdDiscountHistory = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _stdDiscountHistory;
            }

            return _stdDiscountHistory;
        }

        public List<StdDiscountHistory> GetAll()
        {
            List<StdDiscountHistory> stdDiscountHistoryList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StdDiscountHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StdDiscountHistory>(sqlGetAll, mapper);
                IEnumerable<StdDiscountHistory> collection = accessor.Execute();

                stdDiscountHistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return stdDiscountHistoryList;
            }

            return stdDiscountHistoryList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StdDiscountHistory stdDiscountHistory, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ID", DbType.Int32, stdDiscountHistory.ID);
            }

            db.AddInParameter(cmd, "AdmissionID", DbType.Int32, stdDiscountHistory.AdmissionID);
            db.AddInParameter(cmd, "TypeDefID", DbType.Int32, stdDiscountHistory.TypeDefID);
            db.AddInParameter(cmd, "TypePercentage", DbType.Decimal, stdDiscountHistory.TypePercentage);
            db.AddInParameter(cmd, "EffectiveAcaCalID", DbType.Int32, stdDiscountHistory.EffectiveAcaCalID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, stdDiscountHistory.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, stdDiscountHistory.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, stdDiscountHistory.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, stdDiscountHistory.ModifiedDate);
           
            return db;
        }

        private IRowMapper<StdDiscountHistory> GetMaper()
        {
            IRowMapper<StdDiscountHistory> mapper = MapBuilder<StdDiscountHistory>.MapAllProperties()
            .Map(m => m.ID).ToColumn("ID")
            .Map(m => m.AdmissionID).ToColumn("AdmissionID")
            .Map(m => m.TypeDefID).ToColumn("TypeDefID")
            .Map(m => m.TypePercentage).ToColumn("TypePercentage")
            .Map(m => m.EffectiveAcaCalID).ToColumn("EffectiveAcaCalID")
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
