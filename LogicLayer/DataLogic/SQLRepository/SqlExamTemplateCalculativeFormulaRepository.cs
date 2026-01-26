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
    public partial class SqlExamTemplateCalculativeFormulaRepository : IExamTemplateCalculativeFormulaRepository
    {

        Database db = null;

        private string sqlInsert = "ExamTemplateCalculativeFormulaInsert";
        private string sqlUpdate = "ExamTemplateCalculativeFormulaUpdate";
        private string sqlDelete = "ExamTemplateCalculativeFormulaDeleteById";
        private string sqlGetById = "ExamTemplateCalculativeFormulaGetById";
        private string sqlGetAll = "ExamTemplateCalculativeFormulaGetAll";
        private string sqlGetExamTemplateMasterId = "ExamTemplateCalculativeFormulaGetByExamTemplateMasterId";
               
        public int Insert(ExamTemplateCalculativeFormula examtemplatecalculativeformula)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examtemplatecalculativeformula, isInsert);
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

        public bool Update(ExamTemplateCalculativeFormula examtemplatecalculativeformula)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examtemplatecalculativeformula, isInsert);

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

        public ExamTemplateCalculativeFormula GetById(int? id)
        {
            ExamTemplateCalculativeFormula _examtemplatecalculativeformula = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateCalculativeFormula> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateCalculativeFormula>(sqlGetById, rowMapper);
                _examtemplatecalculativeformula = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examtemplatecalculativeformula;
            }

            return _examtemplatecalculativeformula;
        }

        public List<ExamTemplateCalculativeFormula> GetAll()
        {
            List<ExamTemplateCalculativeFormula> examtemplatecalculativeformulaList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateCalculativeFormula> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateCalculativeFormula>(sqlGetAll, mapper);
                IEnumerable<ExamTemplateCalculativeFormula> collection = accessor.Execute();

                examtemplatecalculativeformulaList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examtemplatecalculativeformulaList;
            }

            return examtemplatecalculativeformulaList;
        }

        public List<ExamTemplateCalculativeFormula> GetByExamTemplateMasterId(int examTemplateMasterId)
        {
            List<ExamTemplateCalculativeFormula> examtemplatecalculativeformulaList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateCalculativeFormula> mapper = GetExamTemplateMasterMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateCalculativeFormula>(sqlGetExamTemplateMasterId, mapper);
                IEnumerable<ExamTemplateCalculativeFormula> collection = accessor.Execute(examTemplateMasterId);

                examtemplatecalculativeformulaList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examtemplatecalculativeformulaList;
            }

            return examtemplatecalculativeformulaList;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamTemplateCalculativeFormula examtemplatecalculativeformula, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, examtemplatecalculativeformula.Id);
            }

            	
		    db.AddInParameter(cmd,"ExamTemplateMasterId",DbType.Int32,examtemplatecalculativeformula.ExamTemplateMasterId);
		    db.AddInParameter(cmd,"CalculationType",DbType.Int32,examtemplatecalculativeformula.CalculationType);
		    db.AddInParameter(cmd,"ExamMetaTypeId",DbType.Int32,examtemplatecalculativeformula.ExamMetaTypeId);
		    db.AddInParameter(cmd,"Attribute1",DbType.String,examtemplatecalculativeformula.Attribute1);
		    db.AddInParameter(cmd,"Attribute2",DbType.String,examtemplatecalculativeformula.Attribute2);
		    db.AddInParameter(cmd,"Attribute3",DbType.String,examtemplatecalculativeformula.Attribute3);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,examtemplatecalculativeformula.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,examtemplatecalculativeformula.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,examtemplatecalculativeformula.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,examtemplatecalculativeformula.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamTemplateCalculativeFormula> GetMaper()
        {
            IRowMapper<ExamTemplateCalculativeFormula> mapper = MapBuilder<ExamTemplateCalculativeFormula>.MapAllProperties()

       	   .Map(m => m.Id).ToColumn("Id")
		.Map(m => m.ExamTemplateMasterId).ToColumn("ExamTemplateMasterId")
		.Map(m => m.CalculationType).ToColumn("CalculationType")
		.Map(m => m.ExamMetaTypeId).ToColumn("ExamMetaTypeId")
		.Map(m => m.Attribute1).ToColumn("Attribute1")
		.Map(m => m.Attribute2).ToColumn("Attribute2")
		.Map(m => m.Attribute3).ToColumn("Attribute3")
		.Map(m => m.CreatedBy).ToColumn("CreatedBy")
		.Map(m => m.CreatedDate).ToColumn("CreatedDate")
		.Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
		.Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
        .DoNotMap(m => m.ExamMetaTypeName)
        .DoNotMap(m => m.ExamCalculationTypeName)
            .Build();

            return mapper;
        }

        private IRowMapper<ExamTemplateCalculativeFormula> GetExamTemplateMasterMaper()
        {
            IRowMapper<ExamTemplateCalculativeFormula> mapper = MapBuilder<ExamTemplateCalculativeFormula>.MapAllProperties()

           .Map(m => m.Id).ToColumn("Id")
        .Map(m => m.ExamTemplateMasterId).ToColumn("ExamTemplateMasterId")
        .Map(m => m.CalculationType).ToColumn("CalculationType")
        .Map(m => m.ExamMetaTypeId).ToColumn("ExamMetaTypeId")
        .Map(m => m.Attribute1).ToColumn("Attribute1")
        .Map(m => m.Attribute2).ToColumn("Attribute2")
        .Map(m => m.Attribute3).ToColumn("Attribute3")
        .Map(m => m.CreatedBy).ToColumn("CreatedBy")
        .Map(m => m.CreatedDate).ToColumn("CreatedDate")
        .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
        .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
        .Map(m => m.ExamMetaTypeName).ToColumn("ExamMetaTypeName")
        .Map(m => m.ExamCalculationTypeName).ToColumn("ExamCalculationTypeName")
            .Build();

            return mapper;
        }
        #endregion

    }
}

