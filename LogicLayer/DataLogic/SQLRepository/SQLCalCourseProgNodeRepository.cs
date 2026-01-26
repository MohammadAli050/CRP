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
    public partial class SQLCalCourseProgNodeRepository : ICalCourseProgNodeRepository
    {
        Database db = null;

        private string sqlInsert = "CalCourseProgNodeInsert";
        private string sqlUpdate = "CalCourseProgNodeUpdate";
        private string sqlDelete = "CalCourseProgNodeDeleteById";
        private string sqlGetById = "CalCourseProgNodeGetById";
        private string sqlGetAll = "CalCourseProgNodeGetAll";
        private string sqlGetByProgramIdCourseIdVersionId = "";
        private string sqlGetByTreeCalenderDetailId = "CalCourseProgNodeGetTreeDetailId";
        private string sqlGetByTreeCalDetCourseIdVersionIdNodeCourseIdPriority = "CalCourseProgNodeByTreeCalDetCourseIdVersionId";
        private string sqlGetByTreeCalDetNodeIdPriority = "CalCourseProgNodeIdPriority";


        public int Insert(CalCourseProgNode calCourseProgNode)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, calCourseProgNode, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "CalCorProgNodeID");

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

        public bool Update(CalCourseProgNode calCourseProgNode)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, calCourseProgNode, isInsert);

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

                db.AddInParameter(cmd, "CalCorProgNodeID", DbType.Int32, id);
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

        public CalCourseProgNode GetById(int? id)
        {
            CalCourseProgNode _calCourseProgNode = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CalCourseProgNode> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CalCourseProgNode>(sqlGetById, rowMapper);
                _calCourseProgNode = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _calCourseProgNode;
            }

            return _calCourseProgNode;
        }

        public List<CalCourseProgNode> GetAll()
        {
            List<CalCourseProgNode> calCourseProgNodeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CalCourseProgNode> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CalCourseProgNode>(sqlGetAll, mapper);
                IEnumerable<CalCourseProgNode> collection = accessor.Execute();

                calCourseProgNodeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return calCourseProgNodeList;
            }

            return calCourseProgNodeList;
        }

        #region Mapper

        private Database addParam(Database db, DbCommand cmd, CalCourseProgNode calCourseProgNode, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "CalCorProgNodeID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "CalCorProgNodeID", DbType.Int32, calCourseProgNode.CalCorProgNodeID);
            }

            db.AddInParameter(cmd, "OfferedinTrimesterID", DbType.Int32, calCourseProgNode.OfferedinTrimesterID);
            db.AddInParameter(cmd, "TreeCalendarDetailID", DbType.Int32, calCourseProgNode.TreeCalendarDetailID);
            db.AddInParameter(cmd, "OfferedByProgramID", DbType.Int32, calCourseProgNode.OfferedByProgramID);
            db.AddInParameter(cmd, "CourseID", DbType.Int32, calCourseProgNode.CourseID);
            db.AddInParameter(cmd, "VersionID", DbType.Int32, calCourseProgNode.VersionID);
            db.AddInParameter(cmd, "Node_CourseID", DbType.Int32, calCourseProgNode.Node_CourseID);
            db.AddInParameter(cmd, "NodeID", DbType.Int32, calCourseProgNode.NodeID);
            db.AddInParameter(cmd, "NodeLinkName", DbType.String, calCourseProgNode.NodeLinkName);
            db.AddInParameter(cmd, "Priority", DbType.Int32, calCourseProgNode.Priority);
            db.AddInParameter(cmd, "Credits", DbType.Int32, calCourseProgNode.Credits);
            db.AddInParameter(cmd, "IsMajorRelated", DbType.Boolean, calCourseProgNode.IsMajorRelated);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, calCourseProgNode.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, calCourseProgNode.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, calCourseProgNode.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, calCourseProgNode.ModifiedDate);
            db.AddInParameter(cmd, "PostMajorLevel", DbType.Int32, calCourseProgNode.PostMajorLevel);
            db.AddInParameter(cmd, "TopNodeId", DbType.Int32, calCourseProgNode.TopNodeId);

            return db;
        }

        private IRowMapper<CalCourseProgNode> GetMaper()
        {
            IRowMapper<CalCourseProgNode> mapper = MapBuilder<CalCourseProgNode>.MapAllProperties()
            .Map(m => m.CalCorProgNodeID).ToColumn("CalCorProgNodeID")
            .Map(m => m.OfferedinTrimesterID).ToColumn("OfferedinTrimesterID")
            .Map(m => m.TreeCalendarDetailID).ToColumn("TreeCalendarDetailID")
            .Map(m => m.OfferedByProgramID).ToColumn("OfferedByProgramID")
            .Map(m => m.CourseID).ToColumn("CourseID")
            .Map(m => m.VersionID).ToColumn("VersionID")
            .Map(m => m.Node_CourseID).ToColumn("Node_CourseID")
            .Map(m => m.NodeID).ToColumn("NodeID")
            .Map(m => m.NodeLinkName).ToColumn("NodeLinkName")
            .Map(m => m.Priority).ToColumn("Priority")
            .Map(m => m.Credits).ToColumn("Credits")
            .Map(m => m.IsMajorRelated).ToColumn("IsMajorRelated")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.PostMajorLevel).ToColumn("PostMajorLevel")
            .Map(m => m.TopNodeId).ToColumn("TopNodeId")
            .Build();

            return mapper;
        }

        #endregion

        public CalCourseProgNode GetByProgramIdCourseIdVersionId(int programId, int courseId, int versionId)
        {
            CalCourseProgNode _calCourseProgNode = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CalCourseProgNode> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CalCourseProgNode>(sqlGetById, rowMapper);
                _calCourseProgNode = accessor.Execute(programId, courseId, versionId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _calCourseProgNode;
            }

            return _calCourseProgNode;
        }

        public List<CalCourseProgNode> GetByTreeCalenderDetailId(int treeCalenderDetailId)
        {
            List<CalCourseProgNode> calCourseProgNodeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CalCourseProgNode> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CalCourseProgNode>(sqlGetByTreeCalenderDetailId, mapper);
                IEnumerable<CalCourseProgNode> collection = accessor.Execute(treeCalenderDetailId);

                calCourseProgNodeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return calCourseProgNodeList;
            }

            return calCourseProgNodeList;
        }

        public CalCourseProgNode GetByTreeCalDetCourseIdVersionIdNodeCourseIdPriority(int treeMasterId, int calenderDistributionId, int courseId, int versionId, int nodeCourseId, int priority)
        {
            CalCourseProgNode _calCourseProgNode = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CalCourseProgNode> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CalCourseProgNode>(sqlGetByTreeCalDetCourseIdVersionIdNodeCourseIdPriority, rowMapper);
                _calCourseProgNode = accessor.Execute(treeMasterId, calenderDistributionId, courseId, versionId, nodeCourseId, priority).FirstOrDefault();

            }
            catch (Exception ex)
            {
                return _calCourseProgNode;
            }

            return _calCourseProgNode;
        }

        public CalCourseProgNode GetByTreeCalDetNodeIdPriority(int treeMasterId, int calenderDistributionId, int nodeId, int priority)
        {
            CalCourseProgNode _calCourseProgNode = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CalCourseProgNode> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CalCourseProgNode>(sqlGetByTreeCalDetNodeIdPriority, rowMapper);
                _calCourseProgNode = accessor.Execute(treeMasterId, calenderDistributionId, nodeId, priority).FirstOrDefault();

            }
            catch (Exception ex)
            {
                return _calCourseProgNode;
            }

            return _calCourseProgNode;
        }
    }
}
