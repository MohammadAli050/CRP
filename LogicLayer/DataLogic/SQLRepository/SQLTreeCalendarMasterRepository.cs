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
    public partial class SQLTreeCalendarMasterRepository : ITreeCalendarMasterRepository
    {
        Database db = null;

        private string sqlInsert                = "TreeCalendarMasterInsert";
        private string sqlUpdate                = "TreeCalendarMasterUpdate";
        private string sqlDelete                = "TreeCalendarMasterDeleteById";
        private string sqlGetById               = "TreeCalendarMasterGetById";
        private string sqlGetAll                = "TreeCalendarMasterGetAll";
        private string sqlGetAllByTreeMasterID  = "TreeCalendarMasterGetAllByTreeMasterID";
        private string sqlGetByNameTreeMasterId = "TreeCalenderMasterGetByNameTreeMasterId";
        public int Insert(TreeCalendarMaster treeCalendarMaster)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, treeCalendarMaster, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TreeCalendarMasterID");

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

        public bool Update(TreeCalendarMaster treeCalendarMaster)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, treeCalendarMaster, isInsert);

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

                db.AddInParameter(cmd, "TreeCalendarMasterID", DbType.Int32, id);
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

        public TreeCalendarMaster GetById(int? id)
        {
            TreeCalendarMaster _treeCalendarMaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TreeCalendarMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TreeCalendarMaster>(sqlGetById, rowMapper);
                _treeCalendarMaster = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _treeCalendarMaster;
            }

            return _treeCalendarMaster;
        }

        public List<TreeCalendarMaster> GetAll()
        {
            List<TreeCalendarMaster> treeCalendarMasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TreeCalendarMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TreeCalendarMaster>(sqlGetAll, mapper);
                IEnumerable<TreeCalendarMaster> collection = accessor.Execute();

                treeCalendarMasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return treeCalendarMasterList;
            }

            return treeCalendarMasterList;
        }

        public List<TreeCalendarMaster> GetAllByTreeMasterID(int treeMasterID)
        {
            List<TreeCalendarMaster> treeCalendarMasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TreeCalendarMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TreeCalendarMaster>(sqlGetAllByTreeMasterID, mapper);
                IEnumerable<TreeCalendarMaster> collection = accessor.Execute(treeMasterID);

                treeCalendarMasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return treeCalendarMasterList;
            }

            return treeCalendarMasterList;
        }


        public TreeCalendarMaster GetByTreeCalenderNameTreeMasterId(string treeCalenderName, int treeMasterId)
        {
            TreeCalendarMaster _treeCalendarMaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TreeCalendarMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TreeCalendarMaster>(sqlGetByNameTreeMasterId, rowMapper);
                _treeCalendarMaster = accessor.Execute(treeCalenderName, treeMasterId).FirstOrDefault();

            }
            catch (Exception ex)
            {
                return _treeCalendarMaster;
            }

            return _treeCalendarMaster;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, TreeCalendarMaster treeCalendarMaster, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "TreeCalendarMasterID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "TreeCalendarMasterID", DbType.Int32, treeCalendarMaster.TreeCalendarMasterID);
            }

            db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, treeCalendarMaster.TreeMasterID);
            db.AddInParameter(cmd, "CalendarMasterID", DbType.Int32, treeCalendarMaster.CalendarMasterID);
            db.AddInParameter(cmd, "Name", DbType.String, treeCalendarMaster.Name);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, treeCalendarMaster.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, treeCalendarMaster.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, treeCalendarMaster.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, treeCalendarMaster.ModifiedDate);

            return db;
        }

        private IRowMapper<TreeCalendarMaster> GetMaper()
        {
            IRowMapper<TreeCalendarMaster> mapper = MapBuilder<TreeCalendarMaster>.MapAllProperties()
            .Map(m => m.TreeCalendarMasterID).ToColumn("TreeCalendarMasterID")
            .Map(m => m.TreeMasterID).ToColumn("TreeMasterID")
            .Map(m => m.CalendarMasterID).ToColumn("CalendarMasterID")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Build();

            return mapper;
        }
        #endregion
    }
}
