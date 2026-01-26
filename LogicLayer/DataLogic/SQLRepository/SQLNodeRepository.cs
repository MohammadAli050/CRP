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
    public partial class SQLNodeRepository : INodeRepository
    {
        Database db = null;

        private string sqlInsert = "NodeInsert";
        private string sqlUpdate = "NodeUpdate";
        private string sqlDelete = "NodeDeleteById";
        private string sqlGetById = "NodeGetById";
        private string sqlGetAll = "NodeGetAll";
        private string sqlGetByTreeMasterId = "NodeByTreeMasterId";


        public int Insert(Node node)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, node, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "NodeID");

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

        public bool Update(Node node)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, node, isInsert);

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

                db.AddInParameter(cmd, "NodeID", DbType.Int32, id);
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

        public Node GetById(int? id)
        {
            Node _node = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Node> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Node>(sqlGetById, rowMapper);
                _node = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _node;
            }

            return _node;
        }

        public List<Node> GetAll()
        {
            List<Node> nodeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Node> mapper = GetMaper();

                var accessor = db.CreateSqlStringAccessor<Node>("Select * from Node", mapper);
                IEnumerable<Node> collection = accessor.Execute();

                nodeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return nodeList;
            }

            return nodeList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Node node, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "NodeID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "NodeID", DbType.Int32, node.NodeID);
            }

            db.AddInParameter(cmd, "NodeID", DbType.Int32, node.NodeID);
            db.AddInParameter(cmd, "Name", DbType.String, node.Name);
            db.AddInParameter(cmd, "IsLastLevel", DbType.Boolean, node.IsLastLevel);
            db.AddInParameter(cmd, "MinCredit", DbType.Decimal, node.MinCredit);
            db.AddInParameter(cmd, "MaxCredit", DbType.Decimal, node.MaxCredit);
            db.AddInParameter(cmd, "MinCourses", DbType.Int32, node.MinCourses);
            db.AddInParameter(cmd, "MaxCourses", DbType.Int32, node.MaxCourses);
            db.AddInParameter(cmd, "IsActive", DbType.Boolean, node.IsActive);
            db.AddInParameter(cmd, "IsVirtual", DbType.Boolean, node.IsVirtual);
            db.AddInParameter(cmd, "IsBundle", DbType.Boolean, node.IsBundle);
            db.AddInParameter(cmd, "IsAssociated", DbType.Boolean, node.IsAssociated);
            db.AddInParameter(cmd, "StartTrimesterID", DbType.Int32, node.StartTrimesterID);
            db.AddInParameter(cmd, "OperatorID", DbType.Int32, node.OperatorID);
            db.AddInParameter(cmd, "OperandNodes", DbType.Int32, node.OperandNodes);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, node.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, node.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, node.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, node.ModifiedDate);
            db.AddInParameter(cmd, "IsMajor", DbType.DateTime, node.IsMajor);

            return db;
        }

        private IRowMapper<Node> GetMaper()
        {
            IRowMapper<Node> mapper = MapBuilder<Node>.MapAllProperties()
            .Map(m => m.NodeID).ToColumn("NodeID")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.IsLastLevel).ToColumn("IsLastLevel")
            .Map(m => m.MinCredit).ToColumn("MinCredit")
            .Map(m => m.MaxCredit).ToColumn("MaxCredit")
            .Map(m => m.MinCourses).ToColumn("MinCourses")
            .Map(m => m.MaxCourses).ToColumn("MaxCourses")
            .Map(m => m.IsActive).ToColumn("IsActive")
            .Map(m => m.IsVirtual).ToColumn("IsVirtual")
            .Map(m => m.IsBundle).ToColumn("IsBundle")
            .Map(m => m.IsAssociated).ToColumn("IsAssociated")
            .Map(m => m.StartTrimesterID).ToColumn("StartTrimesterID")
            .Map(m => m.OperatorID).ToColumn("OperatorID")
            .Map(m => m.OperandNodes).ToColumn("OperandNodes")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.IsMajor).ToColumn("IsMajor")
            .Build();

            return mapper;
        }
        #endregion
        
        public List<Node> GetAllMajorNodeByBatchId(int batchId)
        {
            List<Node> nodeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Node> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Node>("NodeGetAllMajorNodeByBatchId", rowMapper);
                IEnumerable<Node> collection = accessor.Execute(batchId);

                nodeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return nodeList;
            }

            return nodeList;
        }

        public List<Node> GetNodeByTreeMasterId(int treeMasterId)
        {
            List<Node> nodeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Node> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Node>(sqlGetByTreeMasterId, mapper);
                IEnumerable<Node> collection = accessor.Execute(treeMasterId);

                nodeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return nodeList;
            }

            return nodeList;
        }
    }
}
