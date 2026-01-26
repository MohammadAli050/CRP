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
    public partial class SQLFeeSetupRepository : IFeeSetupRepository
    {

        Database db = null;

        private string sqlInsert = "FeeSetupInsert";
        private string sqlUpdate = "FeeSetupUpdate";
        private string sqlDelete = "FeeSetupDeleteById";
        private string sqlGetById = "FeeSetupGetById";
        private string sqlGetAll = "FeeSetupGetAll";
        private string sqlGetByTypeDefinationAndSession = "FeeSetupGetByTypeDefinationAndSession";

        public int Insert(FeeSetup feeSetup)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, feeSetup, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "FeeSetUpID");

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

        public bool Update(FeeSetup feeSetup)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, feeSetup, isInsert);

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

                db.AddInParameter(cmd, "FeeSetUpID", DbType.Int32, id);
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

        public FeeSetup GetById(int id)
        {
            FeeSetup _feeSetup = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FeeSetup> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FeeSetup>(sqlGetById, rowMapper);
                _feeSetup = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _feeSetup;
            }

            return _feeSetup;
        }

        public List<FeeSetup> GetAll()
        {
            List<FeeSetup> feeSetupList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FeeSetup> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FeeSetup>(sqlGetAll, mapper);
                IEnumerable<FeeSetup> collection = accessor.Execute();

                feeSetupList = collection.ToList();
            }

            catch (Exception ex)
            {
                return feeSetupList;
            }

            return feeSetupList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, FeeSetup feesetup, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "FeeSetupID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "FeeSetupID", DbType.Int32, feesetup.FeeSetupID);
            }


            db.AddInParameter(cmd, "AcaCalID", DbType.Int32, feesetup.AcaCalID);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, feesetup.ProgramID);
            db.AddInParameter(cmd, "TypeDefID", DbType.Int32, feesetup.TypeDefID);
            db.AddInParameter(cmd, "Amount", DbType.Int32, feesetup.Amount);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, feesetup.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, feesetup.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, feesetup.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, feesetup.ModifiedDate);
            db.AddInParameter(cmd, "BatchID", DbType.Int32, feesetup.BatchID);

            return db;
        }

        private IRowMapper<FeeSetup> GetMaper()
        {
            IRowMapper<FeeSetup> mapper = MapBuilder<FeeSetup>.MapAllProperties()

            .Map(m => m.FeeSetupID).ToColumn("FeeSetupID")
            .Map(m => m.AcaCalID).ToColumn("AcaCalID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.TypeDefID).ToColumn("TypeDefID")
            .Map(m => m.Amount).ToColumn("Amount")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.BatchID).ToColumn("BatchID")

            .Build();

            return mapper;
        }
        #endregion

        public FeeSetup GetByTypeDefinationAndSession(int typeDefinitionID, int sessionId)
        {
            FeeSetup _feeSetup = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FeeSetup> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FeeSetup>(sqlGetByTypeDefinationAndSession, rowMapper);
                _feeSetup = accessor.Execute(typeDefinitionID, sessionId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _feeSetup;
            }

            return _feeSetup;
        }


        public FeeSetup GetByTypeDefinationSessionProgram(int typeDefinitionID, int sessionId, int? ProgramID)
        {
            FeeSetup _feeSetup = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FeeSetup> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FeeSetup>("FeeSetupGetByTypeDefinationSessionProgram", rowMapper);
                _feeSetup = accessor.Execute(typeDefinitionID, sessionId, ProgramID).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _feeSetup;
            }

            return _feeSetup;
        }


        public List<FeeSetup> GetByProgramSession(int programId, int batchId)
        {
            List<FeeSetup> feeSetupList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FeeSetup> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FeeSetup>("FeeSetupGetByProgramSession", mapper);
                IEnumerable<FeeSetup> collection = accessor.Execute(programId, batchId);

                feeSetupList = collection.ToList();
            }

            catch (Exception ex)
            {
                return feeSetupList;
            }

            return feeSetupList;
        }

        public List<rFeeSetup> GetFeeSetup(int programId, int batchId)
        {
            List<rFeeSetup> feeSetupList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rFeeSetup> mapper = GetFeeSetupMaper();

                var accessor = db.CreateSprocAccessor<rFeeSetup>("RptFeeSetup", mapper);
                IEnumerable<rFeeSetup> collection = accessor.Execute(programId, batchId);

                feeSetupList = collection.ToList();
            }

            catch (Exception ex)
            {
                return feeSetupList;
            }

            return feeSetupList;
        }

        private IRowMapper<rFeeSetup> GetFeeSetupMaper()
        {
            IRowMapper<rFeeSetup> mapper = MapBuilder<rFeeSetup>.MapAllProperties()

            .Map(m => m.Type).ToColumn("Type")
            .Map(m => m.Definition).ToColumn("Definition")
            .Map(m => m.Amount).ToColumn("Amount")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.Amount).ToColumn("Amount")
            .Map(m => m.BatchID).ToColumn("BatchID")
            .Map(m => m.BatchNO).ToColumn("BatchNO")
            
            .Build();

            return mapper;
        }
    }
}
