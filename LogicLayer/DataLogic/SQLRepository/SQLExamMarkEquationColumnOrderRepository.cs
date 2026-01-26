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
    public partial class SQLExamMarkEquationColumnOrderRepository : IExamMarkEquationColumnOrderRepository
    {

        Database db = null;

        private string sqlInsert = "ExamMarkEquationColumnOrderInsert";
        private string sqlUpdate = "ExamMarkEquationColumnOrderUpdate";
        private string sqlDelete = "ExamMarkEquationColumnOrderDeleteById";
        private string sqlGetById = "ExamMarkEquationColumnOrderGetById";
        private string sqlGetAll = "ExamMarkEquationColumnOrderGetAll";
        private string sqlGetByTemplateItemId = "ExamMarkEquationColumnOrderGetByTemplateItemId";
        private string sqlDeleteByTemplateSequenceId = "ExamMarkEquationColumnOrderDeleteByTemplateId";
               
        public int Insert(ExamMarkEquationColumnOrder exammarkequationcolumnorder)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, exammarkequationcolumnorder, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Id");

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

        public bool Update(ExamMarkEquationColumnOrder exammarkequationcolumnorder)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, exammarkequationcolumnorder, isInsert);

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

                db.AddInParameter(cmd, "Id", DbType.Int32, id);
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

        public ExamMarkEquationColumnOrder GetById(int id)
        {
            ExamMarkEquationColumnOrder _exammarkequationcolumnorder = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkEquationColumnOrder> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkEquationColumnOrder>(sqlGetById, rowMapper);
                _exammarkequationcolumnorder = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _exammarkequationcolumnorder;
            }

            return _exammarkequationcolumnorder;
        }

        public List<ExamMarkEquationColumnOrder> GetAll()
        {
            List<ExamMarkEquationColumnOrder> exammarkequationcolumnorderList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkEquationColumnOrder> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkEquationColumnOrder>(sqlGetAll, mapper);
                IEnumerable<ExamMarkEquationColumnOrder> collection = accessor.Execute();

                exammarkequationcolumnorderList = collection.ToList();
            }

            catch (Exception ex)
            {
                return exammarkequationcolumnorderList;
            }

            return exammarkequationcolumnorderList;
        }

        public List<ExamMarkEquationColumnOrder> GetByTemplateItemId(int templateItemId)
        {
            List<ExamMarkEquationColumnOrder> exammarkequationcolumnorderList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarkEquationColumnOrder> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarkEquationColumnOrder>(sqlGetByTemplateItemId, mapper);
                IEnumerable<ExamMarkEquationColumnOrder> collection = accessor.Execute(templateItemId).ToList();

                exammarkequationcolumnorderList = collection.ToList();
            }

            catch (Exception ex)
            {
                return exammarkequationcolumnorderList;
            }

            return exammarkequationcolumnorderList;
        }

        public bool DeleteByTemplateItemSequenceId(int templateId, int columnSequence)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDeleteByTemplateSequenceId);

                db.AddInParameter(cmd, "TemplateId", DbType.Int32, templateId);
                db.AddInParameter(cmd, "ColumnSequence", DbType.Int32, columnSequence);
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
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamMarkEquationColumnOrder exammarkequationcolumnorder, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, exammarkequationcolumnorder.Id);
            }

            	
		db.AddInParameter(cmd,"TemplateItemId",DbType.Int32,exammarkequationcolumnorder.TemplateItemId);
		db.AddInParameter(cmd,"ColumnSequence",DbType.Int32,exammarkequationcolumnorder.ColumnSequence);
		db.AddInParameter(cmd,"SumColumnNo",DbType.Int32,exammarkequationcolumnorder.SumColumnNo);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,exammarkequationcolumnorder.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,exammarkequationcolumnorder.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,exammarkequationcolumnorder.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,exammarkequationcolumnorder.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamMarkEquationColumnOrder> GetMaper()
        {
            IRowMapper<ExamMarkEquationColumnOrder> mapper = MapBuilder<ExamMarkEquationColumnOrder>.MapAllProperties()

       	   .Map(m => m.Id).ToColumn("Id")
		.Map(m => m.TemplateItemId).ToColumn("TemplateItemId")
		.Map(m => m.ColumnSequence).ToColumn("ColumnSequence")
		.Map(m => m.SumColumnNo).ToColumn("SumColumnNo")
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

