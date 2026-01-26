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
    public partial class SQLTreeCalendarDetailRepository : ITreeCalendarDetailRepository
    {
        Database db = null;

        private string sqlInsert = "TreeCalendarDetailInsert";
        private string sqlUpdate = "TreeCalendarDetailUpdate";
        private string sqlDelete = "TreeCalendarDetailDeleteById";
        private string sqlGetById = "TreeCalendarDetailGetById";
        private string sqlGetAll = "TreeCalendarDetailGetAll";
        private string sqlGetByTreeCalenderMasterId = "TreeCalendarDetailGetByTreeCalenderMasterId";

        public int Insert(TreeCalendarDetail treeCalendarDetail)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, treeCalendarDetail, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TreeCalendarDetailID");

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

        public bool Update(TreeCalendarDetail treeCalendarDetail)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, treeCalendarDetail, isInsert);

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

                db.AddInParameter(cmd, "TreeCalendarDetailID", DbType.Int32, id);
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

        public TreeCalendarDetail GetById(int? id)
        {
            TreeCalendarDetail _treeCalendarDetail = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TreeCalendarDetail> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TreeCalendarDetail>(sqlGetById, rowMapper);
                _treeCalendarDetail = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _treeCalendarDetail;
            }

            return _treeCalendarDetail;
        }

        public List<TreeCalendarDetail> GetAll()
        {
            List<TreeCalendarDetail> treeCalendarDetailList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TreeCalendarDetail> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TreeCalendarDetail>(sqlGetAll, mapper);
                IEnumerable<TreeCalendarDetail> collection = accessor.Execute();

                treeCalendarDetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return treeCalendarDetailList;
            }

            return treeCalendarDetailList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, TreeCalendarDetail treeCalendarDetail, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "TreeCalendarDetailID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "TreeCalendarDetailID", DbType.Int32, treeCalendarDetail.TreeCalendarDetailID);
            }

            db.AddInParameter(cmd, "TreeCalendarMasterID", DbType.Int32, treeCalendarDetail.TreeCalendarMasterID);
            db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, treeCalendarDetail.TreeMasterID);
            db.AddInParameter(cmd, "CalendarMasterID", DbType.Int32, treeCalendarDetail.CalendarMasterID);
            db.AddInParameter(cmd, "CalendarUnitDistributionID", DbType.Int32, treeCalendarDetail.CalendarDetailID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, treeCalendarDetail.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, treeCalendarDetail.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, treeCalendarDetail.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, treeCalendarDetail.ModifiedDate);

            return db;
        }

        private IRowMapper<TreeCalendarDetail> GetMaper()
        {
            IRowMapper<TreeCalendarDetail> mapper = MapBuilder<TreeCalendarDetail>.MapAllProperties()
            .Map(m => m.TreeCalendarDetailID).ToColumn("TreeCalendarDetailID")
            .Map(m => m.TreeCalendarMasterID).ToColumn("TreeCalendarMasterID")
            .Map(m => m.TreeMasterID).ToColumn("TreeMasterID")
            .Map(m => m.CalendarMasterID).ToColumn("CalendarMasterID")
            .Map(m => m.CalendarDetailID).ToColumn("CalendarUnitDistributionID")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .DoNotMap(m => m.CalenderUnitDistributionName)
            .Build();

            return mapper;
        }
        #endregion

        public List<TreeCalendarDetail> GetByTreeCalenderMasterId(int treeCalenderMasterId)
        {
            List<TreeCalendarDetail> treeCalendarDetailList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TreeCalendarDetail> mapper = GetCalenderUnitNameMaper();

                var accessor = db.CreateSprocAccessor<TreeCalendarDetail>(sqlGetByTreeCalenderMasterId, mapper);
                IEnumerable<TreeCalendarDetail> collection = accessor.Execute(treeCalenderMasterId);

                treeCalendarDetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return treeCalendarDetailList;
            }

            return treeCalendarDetailList;
        }

        private IRowMapper<TreeCalendarDetail> GetCalenderUnitNameMaper()
        {
            IRowMapper<TreeCalendarDetail> mapper = MapBuilder<TreeCalendarDetail>.MapAllProperties()
            .Map(m => m.TreeCalendarDetailID).ToColumn("TreeCalendarDetailID")
            .Map(m => m.TreeCalendarMasterID).ToColumn("TreeCalendarMasterID")
            .Map(m => m.TreeMasterID).ToColumn("TreeMasterID")
            .Map(m => m.CalendarMasterID).ToColumn("CalendarMasterID")
            .Map(m => m.CalendarDetailID).ToColumn("CalendarUnitDistributionID")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.CalenderUnitDistributionName).ToColumn("CalenderUnitDistributionName")
            .Build();

            return mapper;
        }
    }
}
