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
    public partial class SQLCollectionHistoryRepository : ICollectionHistoryRepository
    {

        Database db = null;

        private string sqlInsert = "CollectionHistoryInsert";
        private string sqlUpdate = "CollectionHistoryUpdate";
        private string sqlDelete = "CollectionHistoryDeleteById";
        private string sqlGetById = "CollectionHistoryGetById";
        private string sqlGetAll = "CollectionHistoryGetAll";
        private string sqlGetGetByMRNoPaymentType = "CollectionHistoryGetByMRNoPaymentType";
               
        public int Insert(CollectionHistory collectionhistory)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, collectionhistory, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "CollectionHistoryId");

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

        public bool Update(CollectionHistory collectionhistory)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, collectionhistory, isInsert);

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

                db.AddInParameter(cmd, "CollectionHistoryId", DbType.Int32, id);
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

        public CollectionHistory GetById(int? id)
        {
            CollectionHistory _collectionhistory = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CollectionHistory> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CollectionHistory>(sqlGetById, rowMapper);
                _collectionhistory = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _collectionhistory;
            }

            return _collectionhistory;
        }

        public List<CollectionHistory> GetAll()
        {
            List<CollectionHistory> collectionhistoryList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CollectionHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CollectionHistory>(sqlGetAll, mapper);
                IEnumerable<CollectionHistory> collection = accessor.Execute();

                collectionhistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return collectionhistoryList;
            }

            return collectionhistoryList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, CollectionHistory collectionhistory, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "CollectionHistoryId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "CollectionHistoryId", DbType.Int32, collectionhistory.CollectionHistoryId);
            }


            db.AddInParameter(cmd, "StudentId", DbType.Int32, collectionhistory.StudentId);
            db.AddInParameter(cmd, "BillHistoryMasterId", DbType.Int32, collectionhistory.BillHistoryMasterId);
            db.AddInParameter(cmd, "BillHistoryId", DbType.Int32, collectionhistory.BillHistoryId);
            db.AddInParameter(cmd, "MoneyReciptSerialNo", DbType.String, collectionhistory.MoneyReciptSerialNo);
            db.AddInParameter(cmd, "AcaCalId", DbType.Int32, collectionhistory.AcaCalId);
            db.AddInParameter(cmd, "TypeDefinitionId", DbType.Int32, collectionhistory.TypeDefinitionId);
            db.AddInParameter(cmd, "Amount", DbType.Decimal, collectionhistory.Amount);
            db.AddInParameter(cmd, "CollectionDate", DbType.DateTime, collectionhistory.CollectionDate);
            db.AddInParameter(cmd, "PaymentType", DbType.String, collectionhistory.PaymentType);
            db.AddInParameter(cmd, "CounterId", DbType.Int32, collectionhistory.CounterId);
            db.AddInParameter(cmd, "BankName", DbType.String, collectionhistory.BankName);
            db.AddInParameter(cmd, "ChequeNo", DbType.String, collectionhistory.ChequeNo);
            db.AddInParameter(cmd, "ReferenceNo", DbType.String, collectionhistory.ReferenceNo);
            db.AddInParameter(cmd, "Comments", DbType.String, collectionhistory.Comments);
            db.AddInParameter(cmd, "IsDeleted", DbType.Boolean, collectionhistory.IsDeleted);
            db.AddInParameter(cmd, "Attribute1", DbType.String, collectionhistory.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, collectionhistory.Attribute2);
            db.AddInParameter(cmd, "Attribute3", DbType.String, collectionhistory.Attribute3);
            db.AddInParameter(cmd, "Attribute4", DbType.String, collectionhistory.Attribute4);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, collectionhistory.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, collectionhistory.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, collectionhistory.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, collectionhistory.ModifiedDate);
            return db;
        }

        private IRowMapper<CollectionHistory> GetMaper()
        {
            IRowMapper<CollectionHistory> mapper = MapBuilder<CollectionHistory>.MapAllProperties()

           .Map(m => m.CollectionHistoryId).ToColumn("CollectionHistoryId")
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.BillHistoryMasterId).ToColumn("BillHistoryMasterId")
            .Map(m => m.BillHistoryId).ToColumn("BillHistoryId")
            .Map(m => m.MoneyReciptSerialNo).ToColumn("MoneyReciptSerialNo")
            .Map(m => m.AcaCalId).ToColumn("AcaCalId")
            .Map(m => m.TypeDefinitionId).ToColumn("TypeDefinitionId")
            .Map(m => m.Amount).ToColumn("Amount")
            .Map(m => m.CollectionDate).ToColumn("CollectionDate")
            .Map(m => m.PaymentType).ToColumn("PaymentType")
            .Map(m => m.CounterId).ToColumn("CounterId")
            .Map(m => m.BankName).ToColumn("BankName")
            .Map(m => m.ChequeNo).ToColumn("ChequeNo")
            .Map(m => m.ReferenceNo).ToColumn("ReferenceNo")
            .Map(m => m.Comments).ToColumn("Comments")
            .Map(m => m.IsDeleted).ToColumn("IsDeleted")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
            .Map(m => m.Attribute3).ToColumn("Attribute3")
            .Map(m => m.Attribute4).ToColumn("Attribute4")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            
            .Build();

            return mapper;
        }
        #endregion

        public CollectionHistory IsDuplicateMoneyReceipt(string moneyReceiptNo, string paymentType) 
        {
            CollectionHistory _collectionhistory = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<CollectionHistory> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<CollectionHistory>(sqlGetGetByMRNoPaymentType, rowMapper);
                _collectionhistory = accessor.Execute(moneyReceiptNo, paymentType).FirstOrDefault();

            }
            catch (Exception ex)
            {
                return _collectionhistory;
            }

            return _collectionhistory;
        }

    }
}

