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
    public partial class SQLStdAdmissionDiscountRepository : IStdAdmissionDiscountRepository
    {
        Database db = null;

        private string sqlInsert = "StdAdmissionDiscountInsert";
        private string sqlUpdate = "StdAdmissionDiscountUpdate";
        private string sqlDelete = "StdAdmissionDiscountDeleteById";
        private string sqlGetById = "StdAdmissionDiscountGetById";
        private string sqlGetAll = "StdAdmissionDiscountGetAll";
        
        
        public int Insert(StdAdmissionDiscount stdAdmissionDiscount)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, stdAdmissionDiscount, isInsert);
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

        public bool Update(StdAdmissionDiscount stdAdmissionDiscount)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, stdAdmissionDiscount, isInsert);

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

        public StdAdmissionDiscount GetById(int? id)
        {
            StdAdmissionDiscount _stdAdmissionDiscount = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StdAdmissionDiscount> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StdAdmissionDiscount>(sqlGetById, rowMapper);
                _stdAdmissionDiscount = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _stdAdmissionDiscount;
            }

            return _stdAdmissionDiscount;
        }

        public List<StdAdmissionDiscount> GetAll()
        {
            List<StdAdmissionDiscount> stdAdmissionDiscountList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<StdAdmissionDiscount> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<StdAdmissionDiscount>(sqlGetAll, mapper);
                IEnumerable<StdAdmissionDiscount> collection = accessor.Execute();

                stdAdmissionDiscountList = collection.ToList();
            }

            catch (Exception ex)
            {
                return stdAdmissionDiscountList;
            }

            return stdAdmissionDiscountList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, StdAdmissionDiscount stdAdmissionDiscount, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ID", DbType.Int32, stdAdmissionDiscount.ID);
            }

            db.AddInParameter(cmd, "AdmissionID", DbType.Int32, stdAdmissionDiscount.AdmissionID);
            db.AddInParameter(cmd, "TypeDefID", DbType.Int32, stdAdmissionDiscount.TypeDefID);
            db.AddInParameter(cmd, "TypePercentage", DbType.Decimal, stdAdmissionDiscount.TypePercentage);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, stdAdmissionDiscount.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, stdAdmissionDiscount.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, stdAdmissionDiscount.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, stdAdmissionDiscount.ModifiedDate);
           
            return db;
        }

        private IRowMapper<StdAdmissionDiscount> GetMaper()
        {
            IRowMapper<StdAdmissionDiscount> mapper = MapBuilder<StdAdmissionDiscount>.MapAllProperties()
            .Map(m => m.ID).ToColumn("ID")
            .Map(m => m.AdmissionID).ToColumn("AdmissionID")
            .Map(m => m.TypeDefID).ToColumn("TypeDefID")
            .Map(m => m.TypePercentage).ToColumn("TypePercentage")
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
