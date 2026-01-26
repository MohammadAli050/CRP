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
    public partial class SQLExamTemplateRepository : IExamTemplateRepository
    {

        Database db = null;

        private string sqlInsert = "ExamTemplateInsert";
        private string sqlUpdate = "ExamTemplateUpdate";
        private string sqlDelete = "ExamTemplateDeleteById";
        private string sqlGetById = "ExamTemplateGetById";
        private string sqlGetAll = "ExamTemplateGetAll";
        private string sqlGetByName = "ExamTemplateGetByName";
               
        public int Insert(ExamTemplate examTemplate)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examTemplate, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "TemplateId");

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

        public bool Update(ExamTemplate examTemplate)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examTemplate, isInsert);

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

                db.AddInParameter(cmd, "TemplateId", DbType.Int32, id);
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

        public ExamTemplate GetById(int? id)
        {
            ExamTemplate _examTemplate = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplate> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplate>(sqlGetById, rowMapper);
                _examTemplate = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examTemplate;
            }

            return _examTemplate;
        }

        public List<ExamTemplate> GetAll()
        {
            List<ExamTemplate> examTemplateList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplate> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplate>(sqlGetAll, mapper);
                IEnumerable<ExamTemplate> collection = accessor.Execute();

                examTemplateList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examTemplateList;
            }

            return examTemplateList;
        }


        public ExamTemplate GetExamTemplateByName(string examTemplateName)
        {
            ExamTemplate _examTemplate = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplate> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplate>(sqlGetByName, rowMapper);
                _examTemplate = accessor.Execute(examTemplateName).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examTemplate;
            }

            return _examTemplate;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamTemplate testtemplate, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "TemplateId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "TemplateId", DbType.Int32, testtemplate.TemplateId);
            }


            db.AddInParameter(cmd, "TemplateName", DbType.String, testtemplate.TemplateName);
		    db.AddInParameter(cmd,"TemplateMarks",DbType.Int32,testtemplate.TemplateMarks);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,testtemplate.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,testtemplate.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,testtemplate.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,testtemplate.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamTemplate> GetMaper()
        {
            IRowMapper<ExamTemplate> mapper = MapBuilder<ExamTemplate>.MapAllProperties()

            .Map(m => m.TemplateId).ToColumn("TemplateId")
            .Map(m => m.TemplateName).ToColumn("TemplateName")
		    .Map(m => m.TemplateMarks).ToColumn("TemplateMarks")
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

