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
    public partial class SQLStdDiscountCurrentRepository : IStdDiscountCurrentRepository
    {
        Database db = null;

        private string sqlInsert = "StdDiscountCurrentInsert";
        private string sqlUpdate = "StdDiscountCurrentUpdate";
        private string sqlDelete = "StdDiscountCurrentDeleteById";
        private string sqlGetById = "StdDiscountCurrentGetById";
        private string sqlGetAll = "StdDiscountCurrentGetAll";
        
        
        public int Insert(StdDiscountCurrent stdDiscountCurrent)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, stdDiscountCurrent, isInsert);
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

        public bool Update(StdDiscountCurrent stdDiscountCurrent)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, stdDiscountCurrent, isInsert);

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

        public StdDiscountCurrent GetById(int? id)
        {
            StdDiscountCurrent _stdDiscountCurrent = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StdDiscountCurrent> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StdDiscountCurrent>(sqlGetById, rowMapper);
                _stdDiscountCurrent = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _stdDiscountCurrent;
            }

            return _stdDiscountCurrent;
        }

        public List<StdDiscountCurrent> GetAll()
        {
            List<StdDiscountCurrent> stdDiscountCurrentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StdDiscountCurrent> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StdDiscountCurrent>(sqlGetAll, mapper);
                IEnumerable<StdDiscountCurrent> collection = accessor.Execute();

                stdDiscountCurrentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return stdDiscountCurrentList;
            }

            return stdDiscountCurrentList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StdDiscountCurrent stdDiscountCurrent, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ID", DbType.Int32, stdDiscountCurrent.ID);
            }

            db.AddInParameter(cmd, "AdmissionID", DbType.Int32, stdDiscountCurrent.AdmissionID);
            db.AddInParameter(cmd, "TypeDefID", DbType.Int32, stdDiscountCurrent.TypeDefID);
            db.AddInParameter(cmd, "TypePercentage", DbType.Decimal, stdDiscountCurrent.TypePercentage);
            db.AddInParameter(cmd, "EffectiveAcaCalID", DbType.Int32, stdDiscountCurrent.EffectiveAcaCalID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, stdDiscountCurrent.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, stdDiscountCurrent.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, stdDiscountCurrent.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, stdDiscountCurrent.ModifiedDate);
            
            return db;
        }

        private IRowMapper<StdDiscountCurrent> GetMaper()
        {
            IRowMapper<StdDiscountCurrent> mapper = MapBuilder<StdDiscountCurrent>.MapAllProperties()
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
