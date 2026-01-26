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
    public partial class SQLFrmDsnrMasterRepository : IFrmDsnrMasterRepository
    {
        Database db = null;

        private string sqlInsert = "FrmDsnrMasterInsert";
        private string sqlUpdate = "FrmDsnrMasterUpdate";
        private string sqlDelete = "FrmDsnrMasterDeleteById";
        private string sqlGetById = "FrmDsnrMasterGetById";
        private string sqlGetAll = "FrmDsnrMasterGetAll";
        
        
        public int Insert(FrmDsnrMaster frmDsnrMaster)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, frmDsnrMaster, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "FrmDsnrMaster_ID");

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

        public bool Update(FrmDsnrMaster frmDsnrMaster)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, frmDsnrMaster, isInsert);

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

                db.AddInParameter(cmd, "FrmDsnrMaster_ID", DbType.Int32, id);
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

        public FrmDsnrMaster GetById(int? id)
        {
            FrmDsnrMaster _frmDsnrMaster = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FrmDsnrMaster> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FrmDsnrMaster>(sqlGetById, rowMapper);
                _frmDsnrMaster = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _frmDsnrMaster;
            }

            return _frmDsnrMaster;
        }

        public List<FrmDsnrMaster> GetAll()
        {
            List<FrmDsnrMaster> frmDsnrMasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FrmDsnrMaster> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FrmDsnrMaster>(sqlGetAll, mapper);
                IEnumerable<FrmDsnrMaster> collection = accessor.Execute();

                frmDsnrMasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return frmDsnrMasterList;
            }

            return frmDsnrMasterList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, FrmDsnrMaster frmDsnrMaster, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "FrmDsnrMaster_ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "FrmDsnrMaster_ID", DbType.Int32, frmDsnrMaster.FrmDsnrMaster_ID);
            }

            db.AddInParameter(cmd, "TrimesterID", DbType.Int32, frmDsnrMaster.TrimesterID);
            db.AddInParameter(cmd, "FrmTableName", DbType.String, frmDsnrMaster.FrmTableName);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, frmDsnrMaster.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, frmDsnrMaster.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, frmDsnrMaster.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, frmDsnrMaster.ModifiedDate);
            
            return db;
        }

        private IRowMapper<FrmDsnrMaster> GetMaper()
        {
            IRowMapper<FrmDsnrMaster> mapper = MapBuilder<FrmDsnrMaster>.MapAllProperties()
            .Map(m => m.FrmDsnrMaster_ID).ToColumn("FrmDsnrMaster_ID")
            .Map(m => m.TrimesterID).ToColumn("TrimesterID")
            .Map(m => m.FrmTableName).ToColumn("FrmTableName")
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
