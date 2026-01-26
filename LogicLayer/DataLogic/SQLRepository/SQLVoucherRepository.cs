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
    public partial class SQLVoucherRepository : IVoucherRepository
    {

        Database db = null;

        private string sqlInsert = "VoucherInsert";
        private string sqlUpdate = "VoucherUpdate";
        private string sqlDelete = "VoucherDelete";
        private string sqlGetById = "VoucherGetById";
        private string sqlGetAll = "VoucherGetAll";

        public int Insert(Voucher voucher)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, voucher, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "VoucherID");

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

        public bool Update(Voucher voucher)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, voucher, isInsert);

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

                db.AddInParameter(cmd, "VoucherID", DbType.Int32, id);
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

        public Voucher GetById(int id)
        {
            Voucher _voucher = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Voucher> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Voucher>(sqlGetById, rowMapper);
                _voucher = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _voucher;
            }

            return _voucher;
        }

        public List<Voucher> GetAll()
        {
            List<Voucher> voucherList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Voucher> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Voucher>(sqlGetAll, mapper);
                IEnumerable<Voucher> collection = accessor.Execute();

                voucherList = collection.ToList();
            }

            catch (Exception ex)
            {
                return voucherList;
            }

            return voucherList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Voucher voucher, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "VoucherID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "VoucherID", DbType.Int32, voucher.VoucherID);
            }


            db.AddInParameter(cmd, "VoucherCode", DbType.String, voucher.VoucherCode);
            db.AddInParameter(cmd, "Prefix", DbType.String, voucher.Prefix);
            db.AddInParameter(cmd, "SLNO", DbType.Int64, voucher.SLNO);
            db.AddInParameter(cmd, "AccountHeadsID", DbType.Int32, voucher.AccountHeadsID);
            db.AddInParameter(cmd, "AccountTypeID", DbType.Int32, voucher.AccountTypeID);
            db.AddInParameter(cmd, "Amount", DbType.Decimal, voucher.Amount);
            db.AddInParameter(cmd, "PostedBy", DbType.String, voucher.PostedBy);
            db.AddInParameter(cmd, "CourseID", DbType.Int32, voucher.CourseID);
            db.AddInParameter(cmd, "VersionID", DbType.Int32, voucher.VersionID);
            db.AddInParameter(cmd, "Remarks", DbType.String, voucher.Remarks);
            db.AddInParameter(cmd, "AcaCalID", DbType.Int32, voucher.AcaCalID);
            db.AddInParameter(cmd, "ReferenceNo", DbType.String, voucher.ReferenceNo);
            db.AddInParameter(cmd, "ChequeNo", DbType.String, voucher.ChequeNo);
            db.AddInParameter(cmd, "ChequeBankName", DbType.String, voucher.ChequeBankName);
            db.AddInParameter(cmd, "ChequeDate", DbType.DateTime, voucher.ChequeDate);
            db.AddInParameter(cmd, "IsChequeCleare", DbType.Boolean, voucher.IsChequeCleare);
            db.AddInParameter(cmd, "ChequeCleareDate", DbType.DateTime, voucher.ChequeCleareDate);
            db.AddInParameter(cmd, "Adjust", DbType.Int32, voucher.Adjust);

            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, voucher.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, voucher.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, voucher.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, voucher.ModifiedDate);
            db.AddInParameter(cmd, "Attribute1", DbType.Int32, voucher.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.Int32, voucher.Attribute2);
            db.AddInParameter(cmd, "Attribute3", DbType.Int32, voucher.Attribute3);
            db.AddInParameter(cmd, "Attribute4", DbType.String, voucher.Attribute4);
            db.AddInParameter(cmd, "Attribute5", DbType.String, voucher.Attribute5);
            db.AddInParameter(cmd, "Attribute6", DbType.String, voucher.Attribute6);

            return db;
        }

        private IRowMapper<Voucher> GetMaper()
        {
            IRowMapper<Voucher> mapper = MapBuilder<Voucher>.MapAllProperties()

           .Map(m => m.VoucherID).ToColumn("VoucherID")
        .Map(m => m.VoucherCode).ToColumn("VoucherCode")
        .Map(m => m.Prefix).ToColumn("Prefix")
        .Map(m => m.SLNO).ToColumn("SLNO")
        .Map(m => m.AccountHeadsID).ToColumn("AccountHeadsID")
        .Map(m => m.AccountTypeID).ToColumn("AccountTypeID")
        .Map(m => m.Amount).ToColumn("Amount")
        .Map(m => m.PostedBy).ToColumn("PostedBy")
        .Map(m => m.CourseID).ToColumn("CourseID")
        .Map(m => m.VersionID).ToColumn("VersionID")
        .Map(m => m.Remarks).ToColumn("Remarks")
        .Map(m => m.AcaCalID).ToColumn("AcaCalID")
        .Map(m => m.ReferenceNo).ToColumn("ReferenceNo")
        .Map(m => m.ChequeNo).ToColumn("ChequeNo")
        .Map(m => m.ChequeBankName).ToColumn("ChequeBankName")
        .Map(m => m.ChequeDate).ToColumn("ChequeDate")
        .Map(m => m.IsChequeCleare).ToColumn("IsChequeCleare")
        .Map(m => m.ChequeCleareDate).ToColumn("ChequeCleareDate")
        .Map(m => m.Adjust).ToColumn("Adjust")
        .Map(m => m.GUID).ToColumn("GUID")
        .Map(m => m.CreatedBy).ToColumn("CreatedBy")
        .Map(m => m.CreatedDate).ToColumn("CreatedDate")
        .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
        .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
        .Map(m => m.Attribute1).ToColumn("Attribute1")
        .Map(m => m.Attribute2).ToColumn("Attribute2")
        .Map(m => m.Attribute3).ToColumn("Attribute3")
        .Map(m => m.Attribute4).ToColumn("Attribute4")
        .Map(m => m.Attribute5).ToColumn("Attribute5")
        .Map(m => m.Attribute6).ToColumn("Attribute6")

            .Build();

            return mapper;
        }
        #endregion



        public int Insert(List<Voucher> voucherList)
        {
            int id = 0;
            int count = 0;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                foreach (Voucher item in voucherList)
                {
                    DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                    db = addParam(db, cmd, item, true);
                    db.ExecuteNonQuery(cmd);

                    object obj = db.GetParameterValue(cmd, "VoucherID");

                    if (obj != null)
                    {
                        int.TryParse(obj.ToString(), out id);
                        if (id > 0)
                            count++;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return 0;
        }
    }
}

