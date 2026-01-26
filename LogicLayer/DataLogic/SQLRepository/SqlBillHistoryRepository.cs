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
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.BusinessObjects.RO;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SqlBillHistoryRepository : IBillHistoryRepository
    {

        Database db = null;

        private string sqlInsert = "BillHistoryInsert";
        private string sqlUpdate = "BillHistoryUpdate";
        private string sqlDelete = "BillHistoryDelete";
        private string sqlGetById = "BillHistoryGetById";
        private string sqlGetAll = "BillHistoryGetAll";
        private string sqlGetForBillPrintByStudentIdSessionId = "BillHistoryGetForBillPrintByStudentIdSessionId";
        private string sqlGetForPaymentPosting = "BillHistoryForPaymentPosting2";
        private string sqlGetByBillHistoryMasterId = "BillHistoryGetByBillHistoryMasterId";
        private string sqlGetBillPaymentHistoryBillHistoryMasterId = "BillPaymentHistoryByStudentId";
        private string sqlGetStudentBillPaymentDueByProgramIdSessionId = "RptStudentBillPaymentDueByProgramIdSessionId";
        private string sqlGetBillForDelete = "BillHistoryMasterByProgramBatchSessionDateStudentId";
        private string sqlDeleteByBillHistoryMasterId = "BillHistoryDeleteByBillHistoryMasterId";
        private string sqlGetBillPaymentHistoryMasterByStudentId = "BillHistoryMasterForStudentBillHistory";
               
        public int Insert(BillHistory billhistory)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, billhistory, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "BillHistoryId");

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

        public bool Update(BillHistory billhistory)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, billhistory, isInsert);

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

                db.AddInParameter(cmd, "BillHistoryId", DbType.Int32, id);
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

        public BillHistory GetById(int? id)
        {
            BillHistory _billhistory = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<BillHistory> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<BillHistory>(sqlGetById, rowMapper);
                _billhistory = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _billhistory;
            }

            return _billhistory;
        }

        public List<BillHistory> GetAll()
        {
            List<BillHistory> billhistoryList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<BillHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<BillHistory>(sqlGetAll, mapper);
                IEnumerable<BillHistory> collection = accessor.Execute();

                billhistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return billhistoryList;
            }

            return billhistoryList;
        }

        public List<BillHistory> GetForBillPrintByBillHistoryMasterId(int billHistoryMasterId)
        {
            List<BillHistory> billhistoryList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<BillHistory> mapper = GetBillPrintMaper();

                var accessor = db.CreateSprocAccessor<BillHistory>(sqlGetForBillPrintByStudentIdSessionId, mapper);
                IEnumerable<BillHistory> collection = accessor.Execute(billHistoryMasterId);

                billhistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return billhistoryList;
            }

            return billhistoryList;
        }

        public List<PaymentPostingDTO> GetBillForPaymentPosting(int programId, int sessionId, int batchId, int studentId)
        {
            List<PaymentPostingDTO> paymentPostingList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<PaymentPostingDTO> mapper = GetPaymentPostingMaper();

                var accessor = db.CreateSprocAccessor<PaymentPostingDTO>(sqlGetForPaymentPosting, mapper);
                IEnumerable<PaymentPostingDTO> collection = accessor.Execute(programId, sessionId, batchId, studentId);

                paymentPostingList = collection.ToList();
            }

            catch (Exception ex)
            {
                return paymentPostingList;
            }

            return paymentPostingList;
        }

        private IRowMapper<PaymentPostingDTO> GetPaymentPostingMaper()
        {
            IRowMapper<PaymentPostingDTO> mapper = MapBuilder<PaymentPostingDTO>.MapAllProperties()

            .Map(m => m.BillHistoryMasterId).ToColumn("BillHistoryMasterId")
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.ReferenceNo).ToColumn("ReferenceNo")
            .Map(m => m.Amount).ToColumn("Amount")
            .Map(m => m.BillingDate).ToColumn("BillingDate")
            .Map(m => m.CollectionHistoryId).ToColumn("CollectionHistoryId")

            .Build();

            return mapper;
        }

        public List<BillHistory> GetByBillHistoryMasterId(int billHistorymasterId)
        {
            List<BillHistory> billhistoryList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<BillHistory> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<BillHistory>(sqlGetByBillHistoryMasterId, mapper);
                IEnumerable<BillHistory> collection = accessor.Execute(billHistorymasterId);

                billhistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return billhistoryList;
            }

            return billhistoryList;
        }

        public List<BillPaymentHistoryDTO> GetBillPaymentHistoryByBillHistoryMasterId(int billHistoryMasterId) 
        {
            List<BillPaymentHistoryDTO> billPaymentHistoryList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<BillPaymentHistoryDTO> mapper = GetBillPaymentHistoryMaper();

                var accessor = db.CreateSprocAccessor<BillPaymentHistoryDTO>(sqlGetBillPaymentHistoryBillHistoryMasterId, mapper);
                IEnumerable<BillPaymentHistoryDTO> collection = accessor.Execute(billHistoryMasterId);

                billPaymentHistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return billPaymentHistoryList;
            }

            return billPaymentHistoryList;
        }

        private IRowMapper<BillPaymentHistoryDTO> GetBillPaymentHistoryMaper()
        {
            IRowMapper<BillPaymentHistoryDTO> mapper = MapBuilder<BillPaymentHistoryDTO>.MapAllProperties()
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.BillHistoryMasterId).ToColumn("BillHistoryMasterId")
            .Map(m => m.ReferenceNo).ToColumn("ReferenceNo")
            .Map(m => m.BillHistoryId).ToColumn("BillHistoryId")
            .Map(m => m.Semester).ToColumn("Semester")
            .Map(m => m.FeeTypeId).ToColumn("FeeTypeId")
            .Map(m => m.FeesName).ToColumn("FeesName")
            .Map(m => m.BillAmount).ToColumn("BillAmount")
            .Map(m => m.BillingDate).ToColumn("BillingDate")
            .Map(m => m.PaymentAmount).ToColumn("PaymentAmount")
            .Map(m => m.PaymentDate).ToColumn("PaymentDate")

            .Build();

            return mapper;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, BillHistory billhistory, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "BillHistoryId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "BillHistoryId", DbType.Int32, billhistory.BillHistoryId);
            }

            	
		db.AddInParameter(cmd,"StudentId",DbType.Int32,billhistory.StudentId);
        db.AddInParameter(cmd, "FundTypeId", DbType.Int32, billhistory.FundTypeId);
		db.AddInParameter(cmd,"FeeTypeId",DbType.Int32,billhistory.FeeTypeId);
		db.AddInParameter(cmd,"AcaCalId",DbType.Int32,billhistory.AcaCalId);
        db.AddInParameter(cmd,"Fees", DbType.Decimal, billhistory.Fees);
		db.AddInParameter(cmd,"Remark",DbType.String,billhistory.Remark);
		db.AddInParameter(cmd,"BillingDate",DbType.DateTime,billhistory.BillingDate);
		db.AddInParameter(cmd,"IsDeleted",DbType.Boolean,billhistory.IsDeleted);
		db.AddInParameter(cmd,"BillHistoryMasterId",DbType.Int32,billhistory.BillHistoryMasterId);
		db.AddInParameter(cmd,"Attribute1",DbType.String,billhistory.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,billhistory.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,billhistory.Attribute3);
		db.AddInParameter(cmd,"Attribute4",DbType.String,billhistory.Attribute4);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,billhistory.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,billhistory.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,billhistory.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,billhistory.ModifiedDate);
            
            return db;
        }

        private IRowMapper<BillHistory> GetMaper()
        {
            IRowMapper<BillHistory> mapper = MapBuilder<BillHistory>.MapAllProperties()

       	    .Map(m => m.BillHistoryId).ToColumn("BillHistoryId")
		    .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.FundTypeId).ToColumn("FundTypeId")
		    .Map(m => m.FeeTypeId).ToColumn("FeeTypeId")
		    .Map(m => m.AcaCalId).ToColumn("AcaCalId")
		    .Map(m => m.Fees).ToColumn("Fees")
		    .Map(m => m.Remark).ToColumn("Remark")
		    .Map(m => m.BillingDate).ToColumn("BillingDate")
		    .Map(m => m.IsDeleted).ToColumn("IsDeleted")
		    .Map(m => m.BillHistoryMasterId).ToColumn("BillHistoryMasterId")
		    .Map(m => m.Attribute1).ToColumn("Attribute1")
		    .Map(m => m.Attribute2).ToColumn("Attribute2")
		    .Map(m => m.Attribute3).ToColumn("Attribute3")
		    .Map(m => m.Attribute4).ToColumn("Attribute4")
		    .Map(m => m.CreatedBy).ToColumn("CreatedBy")
		    .Map(m => m.CreatedDate).ToColumn("CreatedDate")
		    .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
		    .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .DoNotMap(m => m.FeeName)
            .DoNotMap(m => m.FundAccountNo)
            .Build();

            return mapper;
        }

        private IRowMapper<BillHistory> GetBillPrintMaper()
        {
            IRowMapper<BillHistory> mapper = MapBuilder<BillHistory>.MapAllProperties()

            .Map(m => m.BillHistoryId).ToColumn("BillHistoryId")
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.FundTypeId).ToColumn("FundTypeId")
            .Map(m => m.FeeTypeId).ToColumn("FeeTypeId")
            .Map(m => m.AcaCalId).ToColumn("AcaCalId")
            .Map(m => m.Fees).ToColumn("Fees")
            .Map(m => m.Remark).ToColumn("Remark")
            .Map(m => m.BillingDate).ToColumn("BillingDate")
            .Map(m => m.IsDeleted).ToColumn("IsDeleted")
            .Map(m => m.BillHistoryMasterId).ToColumn("BillHistoryMasterId")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
            .Map(m => m.Attribute3).ToColumn("Attribute3")
            .Map(m => m.Attribute4).ToColumn("Attribute4")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.FeeName).ToColumn("FeeName")
            .Map(m => m.FundAccountNo).ToColumn("FundAccountNo")
            .Build();

            return mapper;
        }
        #endregion

        public List<rStudentBillPaymentDue> GetBillPaymentDueByProgramIdBatchIdSessionId(int programId, int batchId, int sessionId)
        {
            List<rStudentBillPaymentDue> billPaymentHistoryList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rStudentBillPaymentDue> mapper = GetStudentBillPaymentDueMaper();

                var accessor = db.CreateSprocAccessor<rStudentBillPaymentDue>(sqlGetStudentBillPaymentDueByProgramIdSessionId, mapper);
                IEnumerable<rStudentBillPaymentDue> collection = accessor.Execute(programId,batchId, sessionId);

                billPaymentHistoryList = collection.ToList();
            }

            catch (Exception ex)
            {
                return billPaymentHistoryList;
            }

            return billPaymentHistoryList;
        }

        private IRowMapper<rStudentBillPaymentDue> GetStudentBillPaymentDueMaper()
        {
            IRowMapper<rStudentBillPaymentDue> mapper = MapBuilder<rStudentBillPaymentDue>.MapAllProperties()
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.StudentName).ToColumn("StudentName")
            .Map(m => m.Bill).ToColumn("Bill")
            .Map(m => m.Payment).ToColumn("Payment")
            .Map(m => m.Due).ToColumn("Due")
            .Build();

            return mapper;
        }

        public List<BillDeleteDTO> GetBillForDelete(int programId, int batchId, int sessionId, int studentId, DateTime? date) 
        {
            List<BillDeleteDTO> billDeleteList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<BillDeleteDTO> mapper = GetBillForDeleteMaper();

                var accessor = db.CreateSprocAccessor<BillDeleteDTO>(sqlGetBillForDelete, mapper);
                IEnumerable<BillDeleteDTO> collection = accessor.Execute(programId, batchId, sessionId, studentId, date);

                billDeleteList = collection.ToList();
            }

            catch (Exception ex)
            {
                return billDeleteList;
            }

            return billDeleteList;
        }

        private IRowMapper<BillDeleteDTO> GetBillForDeleteMaper()
        {
            IRowMapper<BillDeleteDTO> mapper = MapBuilder<BillDeleteDTO>.MapAllProperties()
            .Map(m => m.BillHistoryMasterId).ToColumn("BillHistoryMasterId")
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.ReferenceNo).ToColumn("ReferenceNo")
            .Map(m => m.Amount).ToColumn("Amount")
            .Map(m => m.BillingDate).ToColumn("BillingDate")
            .Map(m => m.CollectionHistoryId).ToColumn("CollectionHistoryId")
            .Build();

            return mapper;
        }

        public bool DeleteByBillHistoryMasterId(int billHistoryMasterId) 
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDeleteByBillHistoryMasterId);

                db.AddInParameter(cmd, "BillHistoryMasterId", DbType.Int32, billHistoryMasterId);
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

        public List<BillPaymentHistoryMasterDTO> GetBillPaymentHistoryMasterByStudentId(int studentId) 
        {
            List<BillPaymentHistoryMasterDTO> billPaymentHistoryMasterList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<BillPaymentHistoryMasterDTO> mapper = GetBillPaymentHistoryMasterMaper();

                var accessor = db.CreateSprocAccessor<BillPaymentHistoryMasterDTO>(sqlGetBillPaymentHistoryMasterByStudentId, mapper);
                IEnumerable<BillPaymentHistoryMasterDTO> collection = accessor.Execute(studentId);

                billPaymentHistoryMasterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return billPaymentHistoryMasterList;
            }

            return billPaymentHistoryMasterList;
        }

        private IRowMapper<BillPaymentHistoryMasterDTO> GetBillPaymentHistoryMasterMaper()
        {
            IRowMapper<BillPaymentHistoryMasterDTO> mapper = MapBuilder<BillPaymentHistoryMasterDTO>.MapAllProperties()
            .Map(m => m.BillHistoryMasterId).ToColumn("BillHistoryMasterId")
            .Map(m => m.Roll).ToColumn("Roll")
            .Map(m => m.StudentId).ToColumn("StudentId")
            .Map(m => m.Code).ToColumn("Code")
            .Map(m => m.ReferenceNo).ToColumn("ReferenceNo")
            .Map(m => m.BillingDate).ToColumn("BillingDate")
            .Map(m => m.CollectionDate).ToColumn("CollectionDate")
            .Map(m => m.BillAmount).ToColumn("BillAmount")
            .Map(m => m.PaymentAmount).ToColumn("PaymentAmount")

            .Build();

            return mapper;
        }
    }
}

