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
    public partial class SQLTreeMasterRepository : ITreeMasterRepository
    {
        Database db = null;

        private string sqlInsert            = "TreeMasterInsert";
        private string sqlUpdate            = "TreeMasterUpdate";
        private string sqlDelete            = "TreeMasterDeleteById";
        private string sqlGetById           = "TreeMasterGetById";
        private string sqlGetAll            = "TreeMasterGetAll";
        private string sqlGetAllByProgramID = "TreeMasterGetAllByProgramID";

        public int Insert(TreeMaster treeMaster)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, treeMaster, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TreeMasterID");

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

        public bool Update(TreeMaster treeMaster)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, treeMaster, isInsert);

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

                db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, id);
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

        public TreeMaster GetById(int? id)
        {
            TreeMaster _treeMaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TreeMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TreeMaster>(sqlGetById, rowMapper);
                _treeMaster = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _treeMaster;
            }

            return _treeMaster;
        }

        public List<TreeMaster> GetAll()
        {
            List<TreeMaster> treeMasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TreeMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TreeMaster>(sqlGetAll, mapper);
                IEnumerable<TreeMaster> collection = accessor.Execute();

                treeMasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return treeMasterList;
            }

            return treeMasterList;
        }

        public List<TreeMaster> GetAllByProgramID(int programID)
        {
            List<TreeMaster> treeMasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TreeMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TreeMaster>(sqlGetAllByProgramID, mapper);
                IEnumerable<TreeMaster> collection = accessor.Execute(programID);

                treeMasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return treeMasterList;
            }

            return treeMasterList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, TreeMaster treeMaster, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "TreeMasterID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, treeMaster.TreeMasterID);
            }

            db.AddInParameter(cmd, "TreeMasterID", DbType.Int32, treeMaster.TreeMasterID);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, treeMaster.ProgramID);
            db.AddInParameter(cmd, "RootNodeID", DbType.Int32, treeMaster.RootNodeID);
            db.AddInParameter(cmd, "StartTrimesterID", DbType.Int32, treeMaster.StartTrimesterID);
            db.AddInParameter(cmd, "RequiredUnits", DbType.Decimal, treeMaster.RequiredUnits);
            db.AddInParameter(cmd, "PassingGPA", DbType.Decimal, treeMaster.PassingGPA);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, treeMaster.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, treeMaster.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, treeMaster.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, treeMaster.ModifiedDate);

            return db;
        }

        private IRowMapper<TreeMaster> GetMaper()
        {
            IRowMapper<TreeMaster> mapper = MapBuilder<TreeMaster>.MapAllProperties()
            .Map(m => m.TreeMasterID).ToColumn("TreeMasterID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.RootNodeID).ToColumn("RootNodeID")
            .Map(m => m.StartTrimesterID).ToColumn("StartTrimesterID")
            .Map(m => m.RequiredUnits).ToColumn("RequiredUnits")
            .Map(m => m.PassingGPA).ToColumn("PassingGPA")
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
