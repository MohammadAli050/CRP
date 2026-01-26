using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
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
    partial class SQLNode_CourseRepository : INode_CourseRepository
    {
        Database db = null;

        private string sqlInsert = "NodeCourseInsert";
        private string sqlUpdate = "NodeCourseUpdate";
        private string sqlDelete = "NodeCourseDeleteById";
        private string sqlGetById = "NodeCourseGetById";
        private string sqlGetAll = "NodeCourseGetAll";
        private string sqlGetByNodeId = "sp_AllCourseByNode";

        public int Insert(Node_Course node_Course)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, node_Course, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Node_CourseID");

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

        public bool Update(Node_Course node_Course)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, node_Course, isInsert);

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

                db.AddInParameter(cmd, "Node_CourseID", DbType.Int32, id);
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

        public Node_Course GetById(int? id)
        {
            Node_Course _node_Course = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Node_Course> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Node_Course>(sqlGetById, rowMapper);
                _node_Course = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _node_Course;
            }

            return _node_Course;
        }

        public List<Node_Course> GetAll()
        {
            List<Node_Course> node_CourseList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Node_Course> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Node_Course>(sqlGetAll, mapper);
                IEnumerable<Node_Course> collection = accessor.Execute();

                node_CourseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return node_CourseList;
            }

            return node_CourseList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Node_Course node_Course, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Node_CourseID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Node_CourseID", DbType.Int32, node_Course.Node_CourseID);
            }

            db.AddInParameter(cmd, "Node_CourseID", DbType.Int32, node_Course.Node_CourseID);
            db.AddInParameter(cmd, "NodeID", DbType.Int32, node_Course.NodeID);
            db.AddInParameter(cmd, "CourseID", DbType.Int32, node_Course.CourseID);
            db.AddInParameter(cmd, "VersionID", DbType.Int32, node_Course.VersionID);
            db.AddInParameter(cmd, "Priority", DbType.Int32, node_Course.Priority);
            db.AddInParameter(cmd, "IsActive", DbType.Boolean, node_Course.IsActive);
            db.AddInParameter(cmd, "PassingGPA", DbType.Decimal, node_Course.PassingGPA);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, node_Course.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, node_Course.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, node_Course.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, node_Course.ModifiedDate);

            return db;
        }

        private IRowMapper<Node_Course> GetMaper()
        {
            IRowMapper<Node_Course> mapper = MapBuilder<Node_Course>.MapAllProperties()
            .Map(m => m.Node_CourseID).ToColumn("Node_CourseID")
            .Map(m => m.NodeID).ToColumn("NodeID")
            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.VersionID).ToColumn("VersionID")
            .Map(m => m.Priority).ToColumn("Priority")
            .Map(m => m.IsActive).ToColumn("IsActive")
            .Map(m => m.PassingGPA).ToColumn("PassingGPA")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Build();

            return mapper;
        }
        #endregion

        public List<NodeCoursesDTO> GetByNodeId(int nodeId)
        {
            List<NodeCoursesDTO> node_CourseList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<NodeCoursesDTO> mapper = GetNodeCourseMaper();

                var accessor = db.CreateSprocAccessor<NodeCoursesDTO>(sqlGetByNodeId, mapper);
                IEnumerable<NodeCoursesDTO> collection = accessor.Execute(nodeId);

                node_CourseList = collection.ToList();
            }

            catch (Exception ex)
            {
                return node_CourseList;
            }

            return node_CourseList;
        }

        private IRowMapper<NodeCoursesDTO> GetNodeCourseMaper()
        {
            IRowMapper<NodeCoursesDTO> mapper = MapBuilder<NodeCoursesDTO>.MapAllProperties()
                //.Map(m => m.Node_CourseID).ToColumn("Node_CourseID")
            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.VersionID).ToColumn("VersionID")
            .Map(m => m.FormalCode).ToColumn("FormalCode")
            .Map(m => m.VersionCode).ToColumn("VersionCode")
            .Map(m => m.CourseTitle).ToColumn("CourseTitle")
            .DoNotMap(m => m.Credits)
            .Build();

            return mapper;
        }
    }
}
