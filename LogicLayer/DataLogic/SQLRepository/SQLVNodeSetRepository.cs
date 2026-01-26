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
    public partial class SQLVNodeSetRepository : IVNodeSetRepository
    {
        Database db = null;

        private string sqlInsert = "VNodeSetInsert";
        private string sqlUpdate = "VNodeSetUpdate";
        private string sqlDelete = "VNodeSetDeleteById";
        private string sqlGetById = "VNodeSetGetById";
        private string sqlGetAll = "VNodeSetGetAll";
        private string sqlGetByNodeId = "VNodeSetGetByNodeId";
        private string sqlGetbyVNodeSetMasterId = "VNodeSetGetByVNodeSetMasterId";
        
        public int Insert(VNodeSet vNodeSet)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, vNodeSet, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "VNodeSetID");

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

        public bool Update(VNodeSet vNodeSet)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, vNodeSet, isInsert);

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

                db.AddInParameter(cmd, "VNodeSetID", DbType.Int32, id);
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

        public VNodeSet GetById(int? id)
        {
            VNodeSet _vNodeSet = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<VNodeSet> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<VNodeSet>(sqlGetById, rowMapper);
                _vNodeSet = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _vNodeSet;
            }

            return _vNodeSet;
        }

        public List<VNodeSet> GetAll()
        {
            List<VNodeSet> vNodeSetList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<VNodeSet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<VNodeSet>(sqlGetAll, mapper);
                IEnumerable<VNodeSet> collection = accessor.Execute();

                vNodeSetList = collection.ToList();
            }

            catch (Exception ex)
            {
                return vNodeSetList;
            }

            return vNodeSetList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, VNodeSet vNodeSet, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "VNodeSetID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "VNodeSetID", DbType.Int32, vNodeSet.VNodeSetID);
            }

            db.AddInParameter(cmd, "VNodeSetMasterID", DbType.Int32, vNodeSet.VNodeSetMasterID);
            db.AddInParameter(cmd, "NodeID", DbType.Int32, vNodeSet.NodeID);
            db.AddInParameter(cmd, "SetNo", DbType.Int32, vNodeSet.SetNo);
            db.AddInParameter(cmd, "OperandNodeID", DbType.Int32, vNodeSet.OperandNodeID);
            db.AddInParameter(cmd, "OperandCourseID", DbType.Int32, vNodeSet.OperandCourseID);
            db.AddInParameter(cmd, "OperandVersionID", DbType.Int32, vNodeSet.OperandVersionID);
            db.AddInParameter(cmd, "NodeCourseID", DbType.Int32, vNodeSet.NodeCourseID);
            db.AddInParameter(cmd, "OperatorID", DbType.Int32, vNodeSet.OperatorID);
            db.AddInParameter(cmd, "WildCard", DbType.String, vNodeSet.WildCard);
            db.AddInParameter(cmd, "IsStudntSpec", DbType.Boolean, vNodeSet.IsStudntSpec);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, vNodeSet.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, vNodeSet.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, vNodeSet.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, vNodeSet.ModifiedDate);
           
            return db;
        }

        private IRowMapper<VNodeSet> GetMaper()
        {
            IRowMapper<VNodeSet> mapper = MapBuilder<VNodeSet>.MapAllProperties()
            .Map(m => m.VNodeSetID).ToColumn("VNodeSetID")
            .Map(m => m.VNodeSetMasterID).ToColumn("VNodeSetMasterID")
            .Map(m => m.NodeID).ToColumn("NodeID")
            .Map(m => m.SetNo).ToColumn("SetNo")
            .Map(m => m.OperandNodeID).ToColumn("OperandNodeID")
            .Map(m => m.OperandCourseID).ToColumn("OperandCourseID")
            .Map(m => m.OperandVersionID).ToColumn("OperandVersionID")
            .Map(m => m.NodeCourseID).ToColumn("NodeCourseID")
            .Map(m => m.OperatorID).ToColumn("OperatorID")
            .Map(m => m.WildCard).ToColumn("WildCard")
            .Map(m => m.IsStudntSpec).ToColumn("IsStudntSpec")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
           
            .Build();

            return mapper;
        }
        #endregion

        public List<VNodeSet> GetbyNodeId(int nodeId)
        {
            List<VNodeSet> vNodeSetList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<VNodeSet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<VNodeSet>(sqlGetByNodeId, mapper);
                IEnumerable<VNodeSet> collection = accessor.Execute(nodeId);

                vNodeSetList = collection.ToList();
            }

            catch (Exception ex)
            {
                return vNodeSetList;
            }

            return vNodeSetList;
        }

        public List<VNodeSet> GetbyVNodeSetMasterId(int vNodeSetMasterId)
        {
            List<VNodeSet> vNodeSetList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<VNodeSet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<VNodeSet>(sqlGetbyVNodeSetMasterId, mapper);
                IEnumerable<VNodeSet> collection = accessor.Execute(vNodeSetMasterId);

                vNodeSetList = collection.ToList();
            }

            catch (Exception ex)
            {
                return vNodeSetList;
            }

            return vNodeSetList;
        }
    }
}
