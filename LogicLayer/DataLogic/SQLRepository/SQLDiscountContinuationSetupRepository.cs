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
    public partial class SQLDiscountContinuationSetupRepository : IDiscountContinuationSetupRepository
    {
        Database db = null;

        private string sqlInsert = "DiscountContinuationSetupInsert";
        private string sqlUpdate = "DiscountContinuationSetupUpdate";
        private string sqlDelete = "DiscountContinuationSetupDeleteById";
        private string sqlGetById = "DiscountContinuationSetupGetById";
        private string sqlGetAll = "DiscountContinuationSetupGetAll";
        private string sqlGetAllByBatchProgram = "DiscountContinuationSetupGetAllByBatchProgram";
        
        
        public int Insert(DiscountContinuationSetup discountContinuationSetup)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, discountContinuationSetup, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "DiscountContinuationID");

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

        public bool Update(DiscountContinuationSetup discountContinuationSetup)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, discountContinuationSetup, isInsert);

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

                db.AddInParameter(cmd, "DiscountContinuationID", DbType.Int32, id);
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

        public DiscountContinuationSetup GetById(int id)
        {
            DiscountContinuationSetup _discountContinuationSetup = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DiscountContinuationSetup> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DiscountContinuationSetup>(sqlGetById, rowMapper);
                _discountContinuationSetup = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _discountContinuationSetup;
            }

            return _discountContinuationSetup;
        }

        public List<DiscountContinuationSetup> GetAll()
        {
            List<DiscountContinuationSetup> discountContinuationSetupList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DiscountContinuationSetup> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DiscountContinuationSetup>(sqlGetAll, mapper);
                IEnumerable<DiscountContinuationSetup> collection = accessor.Execute();

                discountContinuationSetupList = collection.ToList();
            }

            catch (Exception ex)
            {
                return discountContinuationSetupList;
            }

            return discountContinuationSetupList;
        }

        public List<DiscountContinuationSetup> GetAll(int batchId, int programId)
        {
            List<DiscountContinuationSetup> discountContinuationSetupList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DiscountContinuationSetup> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DiscountContinuationSetup>(sqlGetAllByBatchProgram, mapper);
                IEnumerable<DiscountContinuationSetup> collection = accessor.Execute(batchId, programId);

                discountContinuationSetupList = collection.ToList();
            }

            catch (Exception ex)
            {
                return discountContinuationSetupList;
            }

            return discountContinuationSetupList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, DiscountContinuationSetup discountContinuationSetup, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "DiscountContinuationID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "DiscountContinuationID", DbType.Int32, discountContinuationSetup.DiscountContinuationID);
            }

            db.AddInParameter(cmd, "BatchAcaCalID", DbType.Int32, discountContinuationSetup.BatchAcaCalID);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, discountContinuationSetup.ProgramID);
            db.AddInParameter(cmd, "TypeDefinitionID", DbType.Int32, discountContinuationSetup.TypeDefinitionID);
            db.AddInParameter(cmd, "MinCredits", DbType.Decimal, discountContinuationSetup.MinCredits);
            db.AddInParameter(cmd, "MaxCredits", DbType.Decimal, discountContinuationSetup.MaxCredits);
            db.AddInParameter(cmd, "MinCGPA", DbType.Decimal, discountContinuationSetup.MinCGPA);
            db.AddInParameter(cmd, "Range", DbType.String, discountContinuationSetup.Range);
            db.AddInParameter(cmd, "PercentMin", DbType.Decimal, discountContinuationSetup.PercentMin);
            db.AddInParameter(cmd, "PercentMax", DbType.Decimal, discountContinuationSetup.PercentMax);
            db.AddInParameter(cmd, "Attribute1", DbType.String, discountContinuationSetup.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, discountContinuationSetup.Attribute2);
            db.AddInParameter(cmd, "Attribute3", DbType.String, discountContinuationSetup.Attribute3);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, discountContinuationSetup.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, discountContinuationSetup.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, discountContinuationSetup.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, discountContinuationSetup.ModifiedDate);
            
            return db;
        }

        private IRowMapper<DiscountContinuationSetup> GetMaper()
        {
            IRowMapper<DiscountContinuationSetup> mapper = MapBuilder<DiscountContinuationSetup>.MapAllProperties()
            .Map(m => m.DiscountContinuationID).ToColumn("DiscountContinuationID")
            .Map(m => m.BatchAcaCalID).ToColumn("BatchAcaCalID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.TypeDefinitionID).ToColumn("TypeDefinitionID")
            .Map(m => m.MinCredits).ToColumn("MinCredits")
            .Map(m => m.MaxCredits).ToColumn("MaxCredits")
            .Map(m => m.MinCGPA).ToColumn("MinCGPA")
            .Map(m => m.Range).ToColumn("Range")
            .Map(m => m.PercentMin).ToColumn("PercentMin")
            .Map(m => m.PercentMax).ToColumn("PercentMax")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
            .Map(m => m.Attribute3).ToColumn("Attribute3")
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
