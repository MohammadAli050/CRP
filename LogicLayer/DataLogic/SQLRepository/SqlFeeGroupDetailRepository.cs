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
    public partial class SqlFeeGroupDetailRepository : IFeeGroupDetailRepository
    {

        Database db = null;

        private string sqlInsert = "FeeGroupDetailInsert";
        private string sqlUpdate = "FeeGroupDetailUpdate";
        private string sqlDelete = "FeeGroupDetailDeleteById";
        private string sqlGetById = "FeeGroupDetailGetById";
        private string sqlGetAll = "FeeGroupDetailGetAll";
        private string sqlGetByFeeGroupMasterId = "FeeGroupDetailGetByFeeGroupMasterId";
               
        public int Insert(FeeGroupDetail feegroupdetail)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, feegroupdetail, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "FeeGroupDetailId");

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

        public bool Update(FeeGroupDetail feegroupdetail)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, feegroupdetail, isInsert);

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

                db.AddInParameter(cmd, "FeeGroupDetailId", DbType.Int32, id);
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

        public FeeGroupDetail GetById(int? id)
        {
            FeeGroupDetail _feegroupdetail = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FeeGroupDetail> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FeeGroupDetail>(sqlGetById, rowMapper);
                _feegroupdetail = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _feegroupdetail;
            }

            return _feegroupdetail;
        }

        public List<FeeGroupDetail> GetAll()
        {
            List<FeeGroupDetail> feegroupdetailList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FeeGroupDetail> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FeeGroupDetail>(sqlGetAll, mapper);
                IEnumerable<FeeGroupDetail> collection = accessor.Execute();

                feegroupdetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return feegroupdetailList;
            }

            return feegroupdetailList;
        }

        public List<FeeGroupDetail> GetByFeeGroupMasterId(int feeGroupMasterId)
        {
            List<FeeGroupDetail> feegroupdetailList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FeeGroupDetail> mapper = GetFeeTypeMaper();

                var accessor = db.CreateSprocAccessor<FeeGroupDetail>(sqlGetByFeeGroupMasterId, mapper);
                IEnumerable<FeeGroupDetail> collection = accessor.Execute(feeGroupMasterId);

                feegroupdetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return feegroupdetailList;
            }

            return feegroupdetailList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, FeeGroupDetail feegroupdetail, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "FeeGroupDetailId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "FeeGroupDetailId", DbType.Int32, feegroupdetail.FeeGroupDetailId);
            }

		    db.AddInParameter(cmd,"FeeGroupMasterId",DbType.Int32,feegroupdetail.FeeGroupMasterId);
		    db.AddInParameter(cmd,"FeeTypeId",DbType.Int32,feegroupdetail.FeeTypeId);
            db.AddInParameter(cmd,"Amount", DbType.String, feegroupdetail.Amount);
		    db.AddInParameter(cmd,"Attribute1",DbType.String,feegroupdetail.Attribute1);
		    db.AddInParameter(cmd,"Attribute2",DbType.String,feegroupdetail.Attribute2);
		    db.AddInParameter(cmd,"Attribute3",DbType.String,feegroupdetail.Attribute3);
		    db.AddInParameter(cmd,"Attribute4",DbType.String,feegroupdetail.Attribute4);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,feegroupdetail.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,feegroupdetail.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,feegroupdetail.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,feegroupdetail.ModifiedDate);
            
            return db;
        }

        private IRowMapper<FeeGroupDetail> GetMaper()
        {
            IRowMapper<FeeGroupDetail> mapper = MapBuilder<FeeGroupDetail>.MapAllProperties()

       	    .Map(m => m.FeeGroupDetailId).ToColumn("FeeGroupDetailId")
		    .Map(m => m.FeeGroupMasterId).ToColumn("FeeGroupMasterId")
		    .Map(m => m.FeeTypeId).ToColumn("FeeTypeId")
		    .Map(m => m.Amount).ToColumn("Amount")
		    .Map(m => m.Attribute1).ToColumn("Attribute1")
		    .Map(m => m.Attribute2).ToColumn("Attribute2")
		    .Map(m => m.Attribute3).ToColumn("Attribute3")
		    .Map(m => m.Attribute4).ToColumn("Attribute4")
		    .Map(m => m.CreatedBy).ToColumn("CreatedBy")
		    .Map(m => m.CreatedDate).ToColumn("CreatedDate")
		    .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
		    .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .DoNotMap(m => m.FeeName)
            .DoNotMap(m => m.FundTypeId)
            .Build();

            return mapper;
        }

        private IRowMapper<FeeGroupDetail> GetFeeTypeMaper()
        {
            IRowMapper<FeeGroupDetail> mapper = MapBuilder<FeeGroupDetail>.MapAllProperties()

            .Map(m => m.FeeGroupDetailId).ToColumn("FeeGroupDetailId")
            .Map(m => m.FeeGroupMasterId).ToColumn("FeeGroupMasterId")
            .Map(m => m.FeeTypeId).ToColumn("FeeTypeId")
            .Map(m => m.Amount).ToColumn("Amount")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
            .Map(m => m.Attribute3).ToColumn("Attribute3")
            .Map(m => m.Attribute4).ToColumn("Attribute4")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.FeeName).ToColumn("FeeName")
            .Map(m => m.FundTypeId).ToColumn("FundTypeId")
            .Build();

            return mapper;
        }
        #endregion

    }
}

