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
    public partial class SQLFrmDsnrDetailRepository : IFrmDsnrDetailRepository
    {
        Database db = null;

        private string sqlInsert = "FrmDsnrDetailInsert";
        private string sqlUpdate = "FrmDsnrDetailUpdate";
        private string sqlDelete = "FrmDsnrDetailDeleteById";
        private string sqlGetById = "FrmDsnrDetailGetById";
        private string sqlGetAll = "FrmDsnrDetailGetAll";
        
        
        public int Insert(FrmDsnrDetail frmDsnrDetail)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, frmDsnrDetail, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "FrmDsnrDetail_ID");

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

        public bool Update(FrmDsnrDetail frmDsnrDetail)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, frmDsnrDetail, isInsert);

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

                db.AddInParameter(cmd, "FrmDsnrDetail_ID", DbType.Int32, id);
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

        public FrmDsnrDetail GetById(int? id)
        {
            FrmDsnrDetail _frmDsnrDetail = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FrmDsnrDetail> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FrmDsnrDetail>(sqlGetById, rowMapper);
                _frmDsnrDetail = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _frmDsnrDetail;
            }

            return _frmDsnrDetail;
        }

        public List<FrmDsnrDetail> GetAll()
        {
            List<FrmDsnrDetail> frmDsnrDetailList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<FrmDsnrDetail> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<FrmDsnrDetail>(sqlGetAll, mapper);
                IEnumerable<FrmDsnrDetail> collection = accessor.Execute();

                frmDsnrDetailList = collection.ToList();
            }

            catch (Exception ex)
            {
                return frmDsnrDetailList;
            }

            return frmDsnrDetailList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, FrmDsnrDetail frmDsnrDetail, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "FrmDsnrDetail_ID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "FrmDsnrDetail_ID", DbType.Int32, frmDsnrDetail.FrmDsnrDetail_ID);
            }

            db.AddInParameter(cmd, "FrmDsnrMaster_ID", DbType.Int32, frmDsnrDetail.FrmDsnrMaster_ID);
            db.AddInParameter(cmd, "FieldName", DbType.String, frmDsnrDetail.FieldName);
            db.AddInParameter(cmd, "FieldType", DbType.String, frmDsnrDetail.FieldType);
            db.AddInParameter(cmd, "FieldPosition", DbType.Int32, frmDsnrDetail.FieldPosition);
            db.AddInParameter(cmd, "IsAdmitField", DbType.Boolean, frmDsnrDetail.IsAdmitField);
            db.AddInParameter(cmd, "AdmitPosition", DbType.Int32, frmDsnrDetail.AdmitPosition);
            db.AddInParameter(cmd, "TableColName", DbType.String, frmDsnrDetail.TableColName);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, frmDsnrDetail.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, frmDsnrDetail.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, frmDsnrDetail.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, frmDsnrDetail.ModifiedDate);
            
            return db;
        }

        private IRowMapper<FrmDsnrDetail> GetMaper()
        {
            IRowMapper<FrmDsnrDetail> mapper = MapBuilder<FrmDsnrDetail>.MapAllProperties()
            .Map(m => m.FrmDsnrDetail_ID).ToColumn("FrmDsnrDetail_ID")
            .Map(m => m.FrmDsnrMaster_ID).ToColumn("FrmDsnrMaster_ID")
            .Map(m => m.FieldName).ToColumn("FieldName")
            .Map(m => m.FieldType).ToColumn("FieldType")
            .Map(m => m.FieldPosition).ToColumn("FieldPosition")
            .Map(m => m.IsAdmitField).ToColumn("IsAdmitField")
            .Map(m => m.AdmitPosition).ToColumn("AdmitPosition")
            .Map(m => m.TableColName).ToColumn("TableColName")
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
