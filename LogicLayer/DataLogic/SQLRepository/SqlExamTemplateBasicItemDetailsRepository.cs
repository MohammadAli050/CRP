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
    public partial class SqlExamTemplateBasicItemDetailsRepository : IExamTemplateBasicItemDetailsRepository
    {

        Database db = null;

        private string sqlInsert = "ExamTemplateBasicItemDetailsInsert";
        private string sqlUpdate = "ExamTemplateBasicItemDetailsUpdate";
        private string sqlDelete = "ExamTemplateBasicItemDetailsDeleteById";
        private string sqlGetById = "ExamTemplateBasicItemDetailsGetById";
        private string sqlGetAll = "ExamTemplateBasicItemDetailsGetAll";
        private string sqlGetByExamTemplateMasterId = "ExamTemplateBasicItemDetailsGetByExamTemplateMasterId";
               
        public int Insert(ExamTemplateBasicItemDetails examtemplatebasicitemdetails)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examtemplatebasicitemdetails, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ExamTemplateBasicItemId");

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

        public bool Update(ExamTemplateBasicItemDetails examtemplatebasicitemdetails)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examtemplatebasicitemdetails, isInsert);

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

                db.AddInParameter(cmd, "ExamTemplateBasicItemId", DbType.Int32, id);
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

        public ExamTemplateBasicItemDetails GetById(int? id)
        {
            ExamTemplateBasicItemDetails _examtemplatebasicitemdetails = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateBasicItemDetails> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateBasicItemDetails>(sqlGetById, rowMapper);
                _examtemplatebasicitemdetails = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examtemplatebasicitemdetails;
            }

            return _examtemplatebasicitemdetails;
        }

        public List<ExamTemplateBasicItemDetails> GetAll()
        {
            List<ExamTemplateBasicItemDetails> examtemplatebasicitemdetailsList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateBasicItemDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateBasicItemDetails>(sqlGetAll, mapper);
                IEnumerable<ExamTemplateBasicItemDetails> collection = accessor.Execute();

                examtemplatebasicitemdetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examtemplatebasicitemdetailsList;
            }

            return examtemplatebasicitemdetailsList;
        }

        public List<ExamTemplateBasicItemDetails> GetByExamTemplateMasterId(int examTemplateMasterId) 
        {
            List<ExamTemplateBasicItemDetails> examtemplatebasicitemdetailsList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateBasicItemDetails> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateBasicItemDetails>(sqlGetByExamTemplateMasterId, mapper);
                IEnumerable<ExamTemplateBasicItemDetails> collection = accessor.Execute(examTemplateMasterId);

                examtemplatebasicitemdetailsList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examtemplatebasicitemdetailsList;
            }

            return examtemplatebasicitemdetailsList;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamTemplateBasicItemDetails examtemplatebasicitemdetails, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ExamTemplateBasicItemId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ExamTemplateBasicItemId", DbType.Int32, examtemplatebasicitemdetails.ExamTemplateBasicItemId);
            }

            	
		db.AddInParameter(cmd,"ExamTemplateMasterId",DbType.Int32,examtemplatebasicitemdetails.ExamTemplateMasterId);
        db.AddInParameter(cmd, "ExamTemplateBasicItemMark", DbType.Decimal, examtemplatebasicitemdetails.ExamTemplateBasicItemMark);
		db.AddInParameter(cmd,"ExamTemplateBasicItemName",DbType.String,examtemplatebasicitemdetails.ExamTemplateBasicItemName);
		db.AddInParameter(cmd,"ExamTypeId",DbType.Int32,examtemplatebasicitemdetails.ExamTypeId);
		db.AddInParameter(cmd,"ColumnSequence",DbType.Int32,examtemplatebasicitemdetails.ColumnSequence);
		db.AddInParameter(cmd,"Attribute1",DbType.String,examtemplatebasicitemdetails.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,examtemplatebasicitemdetails.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,examtemplatebasicitemdetails.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,examtemplatebasicitemdetails.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,examtemplatebasicitemdetails.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,examtemplatebasicitemdetails.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,examtemplatebasicitemdetails.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamTemplateBasicItemDetails> GetMaper()
        {
            IRowMapper<ExamTemplateBasicItemDetails> mapper = MapBuilder<ExamTemplateBasicItemDetails>.MapAllProperties()

       	   .Map(m => m.ExamTemplateBasicItemId).ToColumn("ExamTemplateBasicItemId")
		.Map(m => m.ExamTemplateMasterId).ToColumn("ExamTemplateMasterId")
		.Map(m => m.ExamTemplateBasicItemMark).ToColumn("ExamTemplateBasicItemMark")
		.Map(m => m.ExamTemplateBasicItemName).ToColumn("ExamTemplateBasicItemName")
		.Map(m => m.ExamTypeId).ToColumn("ExamTypeId")
		.Map(m => m.ColumnSequence).ToColumn("ColumnSequence")
		.Map(m => m.Attribute1).ToColumn("Attribute1")
		.Map(m => m.Attribute2).ToColumn("Attribute2")
		.Map(m => m.Attribute3).ToColumn("Attribute3")
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

