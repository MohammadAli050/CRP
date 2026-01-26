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
    public partial class SQLPrerequisiteMasterRepository : IPrerequisiteMasterRepository
    {
        Database db = null;

        private string sqlInsert = "PrerequisiteMasterInsert";
        private string sqlUpdate = "PrerequisiteMasterUpdate";
        private string sqlDelete = "PrerequisiteMasterDeleteById";
        private string sqlGetById = "PrerequisiteMasterGetById";
        private string sqlGetAll = "PrerequisiteMasterGetAll";
        
        
        public int Insert(PrerequisiteMaster prerequisiteMaster)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, prerequisiteMaster, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "PrerequisiteMasterID");

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

        public bool Update(PrerequisiteMaster prerequisiteMaster)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, prerequisiteMaster, isInsert);

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

                db.AddInParameter(cmd, "PrerequisiteMasterID", DbType.Int32, id);
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

        public PrerequisiteMaster GetById(int? id)
        {
            PrerequisiteMaster _prerequisiteMaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PrerequisiteMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<PrerequisiteMaster>(sqlGetById, rowMapper);
                _prerequisiteMaster = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _prerequisiteMaster;
            }

            return _prerequisiteMaster;
        }

        public List<PrerequisiteMaster> GetAll()
        {
            List<PrerequisiteMaster> prerequisiteMasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PrerequisiteMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<PrerequisiteMaster>(sqlGetAll, mapper);
                IEnumerable<PrerequisiteMaster> collection = accessor.Execute();

                prerequisiteMasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return prerequisiteMasterList;
            }

            return prerequisiteMasterList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, PrerequisiteMaster prerequisiteMaster, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "PrerequisiteMasterID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "PrerequisiteMasterID", DbType.Int32, prerequisiteMaster.PrerequisiteMasterID);
            }

            db.AddInParameter(cmd, "Name", DbType.String, prerequisiteMaster.Name);
            db.AddInParameter(cmd, "ProgramID", DbType.Int32, prerequisiteMaster.ProgramID);
            db.AddInParameter(cmd, "NodeID", DbType.Int32, prerequisiteMaster.NodeID);
            db.AddInParameter(cmd, "NodeCourseID", DbType.Int32, prerequisiteMaster.NodeCourseID);
            db.AddInParameter(cmd, "ReqCredits", DbType.Decimal, prerequisiteMaster.ReqCredits);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, prerequisiteMaster.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, prerequisiteMaster.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, prerequisiteMaster.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, prerequisiteMaster.ModifiedDate);
            
            return db;
        }

        private IRowMapper<PrerequisiteMaster> GetMaper()
        {
            IRowMapper<PrerequisiteMaster> mapper = MapBuilder<PrerequisiteMaster>.MapAllProperties()
            .Map(m => m.PrerequisiteMasterID).ToColumn("PrerequisiteMasterID")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.NodeID).ToColumn("NodeID")
            .Map(m => m.NodeCourseID).ToColumn("NodeCourseID")
            .Map(m => m.ReqCredits).ToColumn("ReqCredits")
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
