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
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLPrerequisiteDetailRepository : IPrerequisiteDetailRepository
    {
        Database db = null;

        private string sqlInsert = "PrerequisiteDetailInsert";//
        private string sqlUpdate = "PrerequisiteDetailUpdate";//
        private string sqlDelete = "PrerequisiteDetailDeleteById";//
        private string sqlGetById = "PrerequisiteDetailGetById";//
        private string sqlGetAll = "PrerequisiteDetailGetAll";//


        public int Insert(PrerequisiteDetail prerequisiteDetail)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, prerequisiteDetail, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "PrerequisiteID");

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

        public bool Update(PrerequisiteDetail prerequisiteDetail)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, prerequisiteDetail, isInsert);

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

                db.AddInParameter(cmd, "PrerequisiteID", DbType.Int32, id);
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

        public PrerequisiteDetail GetById(int? id)
        {
            PrerequisiteDetail _prerequisiteDetail = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PrerequisiteDetail> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<PrerequisiteDetail>(sqlGetById, rowMapper);
                _prerequisiteDetail = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _prerequisiteDetail;
            }

            return _prerequisiteDetail;
        }

        public List<PrerequisiteDetail> GetAll()
        {
            List<PrerequisiteDetail> prerequisiteDetailList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PrerequisiteDetail> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<PrerequisiteDetail>(sqlGetAll, mapper);
                IEnumerable<PrerequisiteDetail> collection = accessor.Execute();

                prerequisiteDetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return prerequisiteDetailList;
            }

            return prerequisiteDetailList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, PrerequisiteDetail prerequisiteDetail, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "PrerequisiteID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "PrerequisiteID", DbType.Int32, prerequisiteDetail.PrerequisiteID);
            }

            db.AddInParameter(cmd, "PrerequisiteMasterID", DbType.Int32, prerequisiteDetail.PrerequisiteMasterID);
            db.AddInParameter(cmd, "NodeCourseID", DbType.Int32, prerequisiteDetail.NodeCourseID);
            db.AddInParameter(cmd, "PreReqNodeCourseID", DbType.Int32, prerequisiteDetail.PreReqNodeCourseID);
            db.AddInParameter(cmd, "OperatorID", DbType.Int32, prerequisiteDetail.OperatorID);
            db.AddInParameter(cmd, "OperatorIDMinOccurance", DbType.Int32, prerequisiteDetail.OperatorIDMinOccurance);
            db.AddInParameter(cmd, "ReqCredits", DbType.Decimal, prerequisiteDetail.ReqCredits);
            db.AddInParameter(cmd, "NodeID", DbType.Int32, prerequisiteDetail.NodeID);
            db.AddInParameter(cmd, "PreReqNodeID", DbType.Int32, prerequisiteDetail.PreReqNodeID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, prerequisiteDetail.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, prerequisiteDetail.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, prerequisiteDetail.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, prerequisiteDetail.ModifiedDate);

            return db;
        }

        private IRowMapper<PrerequisiteDetail> GetMaper()
        {
            IRowMapper<PrerequisiteDetail> mapper = MapBuilder<PrerequisiteDetail>.MapAllProperties()
            .Map(m => m.PrerequisiteID).ToColumn("PrerequisiteID")
            .Map(m => m.PrerequisiteMasterID).ToColumn("PrerequisiteMasterID")
            .Map(m => m.NodeCourseID).ToColumn("NodeCourseID")
            .Map(m => m.PreReqNodeCourseID).ToColumn("PreReqNodeCourseID")
            .Map(m => m.OperatorID).ToColumn("OperatorID")
            .Map(m => m.OperatorIDMinOccurance).ToColumn("OperatorIDMinOccurance")
            .Map(m => m.ReqCredits).ToColumn("ReqCredits")
            .Map(m => m.NodeID).ToColumn("NodeID")
            .Map(m => m.PreReqNodeID).ToColumn("PreReqNodeID")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")

            .Build();

            return mapper;
        }
        #endregion


        public List<PrerequisiteDetail> GetAllByProgramId(int programId)
        {
            List<PrerequisiteDetail> prerequisiteDetailList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PrerequisiteDetail> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<PrerequisiteDetail>("UIUEMS_CC_PrerequisiteDetailGetByProgramId", mapper);
                IEnumerable<PrerequisiteDetail> collection = accessor.Execute(programId);

                prerequisiteDetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return prerequisiteDetailList;
            }

            return prerequisiteDetailList;
        }
        
    }
}
