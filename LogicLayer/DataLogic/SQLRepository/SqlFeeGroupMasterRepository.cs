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
    public partial class SqlFeeGroupMasterRepository : IFeeGroupMasterRepository
    {

        Database db = null;

        private string sqlInsert = "FeeGroupMasterInsert";
        private string sqlUpdate = "FeeGroupMasterUpdate";
        private string sqlDelete = "FeeGroupMasterDelete";
        private string sqlGetById = "FeeGroupMasterGetById";
        private string sqlGetAll = "FeeGroupMasterGetAll";
               
        public int Insert(FeeGroupMaster feegroupmaster)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, feegroupmaster, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "FeeGroupMasterId");

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

        public bool Update(FeeGroupMaster feegroupmaster)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, feegroupmaster, isInsert);

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

                db.AddInParameter(cmd, "{5}", DbType.Int32, id);
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

        public FeeGroupMaster GetById(int? id)
        {
            FeeGroupMaster _feegroupmaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FeeGroupMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FeeGroupMaster>(sqlGetById, rowMapper);
                _feegroupmaster = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _feegroupmaster;
            }

            return _feegroupmaster;
        }

        public List<FeeGroupMaster> GetAll()
        {
            List<FeeGroupMaster> feegroupmasterList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FeeGroupMaster> mapper = GetFeeGroupSetupMaper();

                var accessor = db.CreateSprocAccessor<FeeGroupMaster>(sqlGetAll, mapper);
                IEnumerable<FeeGroupMaster> collection = accessor.Execute();

                feegroupmasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return feegroupmasterList;
            }

            return feegroupmasterList;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, FeeGroupMaster feegroupmaster, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "FeeGroupMasterId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "FeeGroupMasterId", DbType.Int32, feegroupmaster.FeeGroupMasterId);
            }

		    db.AddInParameter(cmd,"FeeGroupName",DbType.String,feegroupmaster.FeeGroupName);
		    db.AddInParameter(cmd,"ProgramId",DbType.Int32,feegroupmaster.ProgramId);
		    db.AddInParameter(cmd,"BatchId",DbType.Int32,feegroupmaster.BatchId);
		    db.AddInParameter(cmd,"FundTypeId",DbType.Int32,feegroupmaster.FundTypeId);
		    db.AddInParameter(cmd,"Remarks",DbType.String,feegroupmaster.Remarks);
		    db.AddInParameter(cmd,"Attribute1",DbType.String,feegroupmaster.Attribute1);
		    db.AddInParameter(cmd,"Attribute2",DbType.String,feegroupmaster.Attribute2);
		    db.AddInParameter(cmd,"Attribute3",DbType.String,feegroupmaster.Attribute3);
		    db.AddInParameter(cmd,"Attribute4",DbType.String,feegroupmaster.Attribute4);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,feegroupmaster.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,feegroupmaster.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,feegroupmaster.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,feegroupmaster.ModifiedDate);
            
            return db;
        }

        private IRowMapper<FeeGroupMaster> GetMaper()
        {
            IRowMapper<FeeGroupMaster> mapper = MapBuilder<FeeGroupMaster>.MapAllProperties()

       	    .Map(m => m.FeeGroupMasterId).ToColumn("FeeGroupMasterId")
		    .Map(m => m.FeeGroupName).ToColumn("FeeGroupName")
		    .Map(m => m.ProgramId).ToColumn("ProgramId")
		    .Map(m => m.BatchId).ToColumn("BatchId")
		    .Map(m => m.FundTypeId).ToColumn("FundTypeId")
		    .Map(m => m.Remarks).ToColumn("Remarks")
		    .Map(m => m.Attribute1).ToColumn("Attribute1")
		    .Map(m => m.Attribute2).ToColumn("Attribute2")
		    .Map(m => m.Attribute3).ToColumn("Attribute3")
		    .Map(m => m.Attribute4).ToColumn("Attribute4")
		    .Map(m => m.CreatedBy).ToColumn("CreatedBy")
		    .Map(m => m.CreatedDate).ToColumn("CreatedDate")
		    .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
		    .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")

            .DoNotMap(m => m.ProgramName)
            .DoNotMap(m => m.ProgramDetailName)
            .DoNotMap(m => m.BatchNO)
            .DoNotMap(m => m.FundName)

            .Build();

            return mapper;
        }

        private IRowMapper<FeeGroupMaster> GetFeeGroupSetupMaper()
        {
            IRowMapper<FeeGroupMaster> mapper = MapBuilder<FeeGroupMaster>.MapAllProperties()

            .Map(m => m.FeeGroupMasterId).ToColumn("FeeGroupMasterId")
            .Map(m => m.FeeGroupName).ToColumn("FeeGroupName")
            .Map(m => m.ProgramId).ToColumn("ProgramId")
            .Map(m => m.BatchId).ToColumn("BatchId")
            .Map(m => m.FundTypeId).ToColumn("FundTypeId")
            .Map(m => m.Remarks).ToColumn("Remarks")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
            .Map(m => m.Attribute3).ToColumn("Attribute3")
            .Map(m => m.Attribute4).ToColumn("Attribute4")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")

            .Map(m => m.ProgramName).ToColumn("ProgramName")
            .Map(m => m.ProgramDetailName).ToColumn("ProgramDetailName")
            .Map(m => m.BatchNO).ToColumn("BatchNO")
            .Map(m => m.FundName).ToColumn("FundName")

            .Build();

            return mapper;
        }
        #endregion

    }
}

