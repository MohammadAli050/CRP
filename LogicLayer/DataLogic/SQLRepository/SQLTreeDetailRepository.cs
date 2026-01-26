using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLTreeDetailRepository : ITreeDetailRepository
    {
        Database db = null;

        private string sqlInsert = "TreeDetailInsert";
        private string sqlUpdate = "TreeDetailUpdate";
        private string sqlDelete = "TreeDetailDeleteById";
        private string sqlGetById = "TreeDetailGetById";
        private string sqlGetAll = "TreeDetailGetAll";
        private string sqlGetByTreeMasterIdParentNodeId = "TreeDetailGetByTreeMasterIdParentNodeId";


        public int Insert(TreeDetail treeDetail)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, treeDetail, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TreeDetailID");

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

        public bool Update(TreeDetail treeDetail)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, treeDetail, isInsert);

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

                db.AddInParameter(cmd, "TreeDetailID", DbType.Int32, id);
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

        public TreeDetail GetById(int? id)
        {
            TreeDetail _treeDetail = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TreeDetail> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TreeDetail>(sqlGetById, rowMapper);
                _treeDetail = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _treeDetail;
            }

            return _treeDetail;
        }

        public List<TreeDetail> GetAll()
        {
            List<TreeDetail> treeDetailList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TreeDetail> mapper = GetMaper();

                var accessor = db.CreateSqlStringAccessor<TreeDetail>("Select * from TreeDetail", mapper);
                IEnumerable<TreeDetail> collection = accessor.Execute();

                treeDetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return treeDetailList;
            }

            return treeDetailList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, TreeDetail treeDetail, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "TreeDetailID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "TreeDetailID", DbType.Int32, treeDetail.TreeDetailID);
            }

            db.AddInParameter(cmd, "TreeDetailID", DbType.Int32, treeDetail.TreeDetailID);
            db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, treeDetail.TreeMasterID);
            db.AddInParameter(cmd, "ParentNodeID", DbType.Int32, treeDetail.ParentNodeID);
            db.AddInParameter(cmd, "ChildNodeID", DbType.Int32, treeDetail.ChildNodeID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, treeDetail.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, treeDetail.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, treeDetail.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, treeDetail.ModifiedDate);

            return db;
        }

        private IRowMapper<TreeDetail> GetMaper()
        {
            IRowMapper<TreeDetail> mapper = MapBuilder<TreeDetail>.MapAllProperties()
            .Map(m => m.TreeDetailID).ToColumn("TreeDetailID")
            .Map(m => m.TreeMasterID).ToColumn("TreeMasterID")
            .Map(m => m.ParentNodeID).ToColumn("ParentNodeID")
            .Map(m => m.ChildNodeID).ToColumn("ChildNodeID")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Build();

            return mapper;
        }
        #endregion

        public List<TreeDetail> GetByTreeMasterIdParentNodeId(int treeMasterId, int parentNodeId)
        {
            List<TreeDetail> treeDetailList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TreeDetail> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TreeDetail>(sqlGetByTreeMasterIdParentNodeId, mapper);
                IEnumerable<TreeDetail> collection = accessor.Execute(treeMasterId, parentNodeId);

                treeDetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return treeDetailList;
            }

            return treeDetailList;
        }
    }
}
