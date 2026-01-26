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
    public partial class SQLVNodeSetMasterRepository : IVNodeSetMasterRepository
    {
        Database db = null;

        private string sqlInsert = "VNodeSetMasterInsert";
        private string sqlUpdate = "VNodeSetMasterUpdate";
        private string sqlDelete = "VNodeSetMasterDeleteById";
        private string sqlGetById = "VNodeSetMasterGetById";
        private string sqlGetAll = "VNodeSetMasterGetAll";
        private string sqlGetByNodeId = "VNodeSetMasterGetByNodeId";
        
        
        public int Insert(VNodeSetMaster vNodeSetMaster)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, vNodeSetMaster, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "VNodeSetMasterID");

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

        public bool Update(VNodeSetMaster vNodeSetMaster)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, vNodeSetMaster, isInsert);

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

                db.AddInParameter(cmd, "VNodeSetMasterID", DbType.Int32, id);
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

        public VNodeSetMaster GetById(int? id)
        {
            VNodeSetMaster _vNodeSetMaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<VNodeSetMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<VNodeSetMaster>(sqlGetById, rowMapper);
                _vNodeSetMaster = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _vNodeSetMaster;
            }

            return _vNodeSetMaster;
        }

        public List<VNodeSetMaster> GetAll()
        {
            List<VNodeSetMaster> vNodeSetMasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<VNodeSetMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<VNodeSetMaster>(sqlGetAll, mapper);
                IEnumerable<VNodeSetMaster> collection = accessor.Execute();

                vNodeSetMasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return vNodeSetMasterList;
            }

            return vNodeSetMasterList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, VNodeSetMaster vNodeSetMaster, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "VNodeSetMasterID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "VNodeSetMasterID", DbType.Int32, vNodeSetMaster.VNodeSetMasterID);
            }

            db.AddInParameter(cmd, "SetNo", DbType.Int32, vNodeSetMaster.SetNo);
            db.AddInParameter(cmd, "NodeID", DbType.Int32, vNodeSetMaster.NodeID);
            db.AddInParameter(cmd, "RequiredUnits", DbType.Decimal, vNodeSetMaster.RequiredUnits);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, vNodeSetMaster.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, vNodeSetMaster.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, vNodeSetMaster.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.Boolean, vNodeSetMaster.ModifiedDate);
            
            return db;
        }

        private IRowMapper<VNodeSetMaster> GetMaper()
        {
            IRowMapper<VNodeSetMaster> mapper = MapBuilder<VNodeSetMaster>.MapAllProperties()
            .Map(m => m.VNodeSetMasterID).ToColumn("VNodeSetMasterID")
            .Map(m => m.SetNo).ToColumn("SetNo")
            .Map(m => m.NodeID).ToColumn("NodeID")
            .Map(m => m.RequiredUnits).ToColumn("RequiredUnits")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
           
            .Build();

            return mapper;
        }
        #endregion

        public List<VNodeSetMaster> GetByNodeId(int nodeId)
        {
            List<VNodeSetMaster> vNodeSetMasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<VNodeSetMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<VNodeSetMaster>(sqlGetByNodeId, mapper);
                IEnumerable<VNodeSetMaster> collection = accessor.Execute(nodeId);

                vNodeSetMasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return vNodeSetMasterList;
            }

            return vNodeSetMasterList;
        }
    }
}
