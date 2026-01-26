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

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLTemplateGroupRepository : ITemplateGroupRepository
    {

        Database db = null;

        private string sqlInsert = "ExamTemplateItemInsert";
        private string sqlUpdate = "ExamTemplateItemUpdate";
        private string sqlDelete = "ExamTemplateItemDeleteById";
        private string sqlGetById = "ExamTemplateItemGetById";
        private string sqlGetAll = "ExamTemplateItemGetAll";
        private string sqlGetAllExamTemplate = "ExamTemplateExamSetExamGetAll";
        private string sqlGetByTemplateExamSetId = "ExamTemplateItemByTemplateExamsetId";
        private string sqlGetByTemplateExamSetExamId = "ExamTemplateItemByTemplateExamsetExamId";
        private string sqlGetByTemplateItemTotalMark = "ExamTemplateItemTotalMark";
        private string sqlGetByTemplateId = "ExamTemplateItemByTemplateId";
        private string sqlGetByTemplateIdNew = "ExamTemplateItemByTemplateIdNew";

        public int Insert(ExamTemplateItem templategroup)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, templategroup, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ItemId");

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

        public bool Update(ExamTemplateItem templategroup)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, templategroup, isInsert);

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

                db.AddInParameter(cmd, "ItemId", DbType.Int32, id);
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

        public ExamTemplateItem GetById(int id)
        {
            ExamTemplateItem _templategroup = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateItem> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateItem>(sqlGetById, rowMapper);
                _templategroup = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _templategroup;
            }

            return _templategroup;
        }

        public List<ExamTemplateItem> GetAll()
        {
            List<ExamTemplateItem> templategroupList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateItem> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateItem>(sqlGetAll, mapper);
                IEnumerable<ExamTemplateItem> collection = accessor.Execute();

                templategroupList = collection.ToList();
            }

            catch (Exception ex)
            {
                return templategroupList;
            }

            return templategroupList;
        }

        public ExamTemplateItem GetByTemplateExamSetId(int templateId, int examSetId)
        {
            ExamTemplateItem _templategroup = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateItem> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateItem>(sqlGetById, rowMapper);
                _templategroup = accessor.Execute(templateId, examSetId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _templategroup;
            }

            return _templategroup;
        }

        public ExamTemplateItem GetByTemplateExamSetExamId(int templateId, int examSetId, int examId)
        {
            ExamTemplateItem _templategroup = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateItem> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateItem>(sqlGetByTemplateExamSetExamId, rowMapper);
                _templategroup = accessor.Execute(templateId, examSetId, examId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _templategroup;
            }

            return _templategroup;
        }

        public decimal GetByTemplateExamSetExamId(int templateId)
        {
            decimal _templateItemMark = 0;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                var accessor = db.CreateSprocAccessor<decimal>(sqlGetByTemplateItemTotalMark);
                _templateItemMark = accessor.Execute(templateId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _templateItemMark;
            }

            return _templateItemMark;
        }

        public List<ExamTemplateItemDTO> GetAllItemByTemplateId(int templateId)
        {
            List<ExamTemplateItemDTO> templategroupList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateItemDTO> mapper = GetTemplateGroupDTOMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateItemDTO>(sqlGetByTemplateId, mapper);
                IEnumerable<ExamTemplateItemDTO> collection = accessor.Execute(templateId).ToList();

                templategroupList = collection.ToList();
            }

            catch (Exception ex)
            {
                return templategroupList;
            }

            return templategroupList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamTemplateItem templategroup, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ItemId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ItemId", DbType.Int32, templategroup.ItemId);
            }


            db.AddInParameter(cmd, "TemplateId", DbType.Int32, templategroup.TemplateId);
            db.AddInParameter(cmd, "ExamSetId", DbType.Int32, templategroup.ExamSetId);
            db.AddInParameter(cmd, "ExamId", DbType.Int32, templategroup.ExamId);
            db.AddInParameter(cmd, "CalculativeColumnName", DbType.String, templategroup.CalculativeColumnName);
            db.AddInParameter(cmd, "ColumnSequence", DbType.Int32, templategroup.ColumnSequence);
            db.AddInParameter(cmd, "DivideBy", DbType.Int32, templategroup.DivideBy);
            db.AddInParameter(cmd, "MultiplyBy", DbType.Int32, templategroup.MultiplyBy);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, templategroup.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, templategroup.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, templategroup.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, templategroup.ModifiedDate);

            return db;
        }

        private IRowMapper<ExamTemplateItem> GetMaper()
        {
            IRowMapper<ExamTemplateItem> mapper = MapBuilder<ExamTemplateItem>.MapAllProperties()

            .Map(m => m.ItemId).ToColumn("ItemId")
            .Map(m => m.TemplateId).ToColumn("TemplateId")
            .Map(m => m.ExamSetId).ToColumn("ExamSetId")
            .Map(m => m.ExamId).ToColumn("ExamId")
            .Map(m => m.CalculativeColumnName).ToColumn("CalculativeColumnName")
            .Map(m => m.ColumnSequence).ToColumn("ColumnSequence")
            .Map(m => m.DivideBy).ToColumn("DivideBy")
            .Map(m => m.MultiplyBy).ToColumn("MultiplyBy")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")

            .Build();

            return mapper;
        }
        #endregion

        #region Get Template Group Usin Join With Micro Test, Test Set, Template Table

        public List<ExamTemplateItemDTO> GetAllTemplateItem()
        {
            List<ExamTemplateItemDTO> templategroupList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateItemDTO> mapper = GetTemplateGroupDTOMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateItemDTO>(sqlGetAllExamTemplate, mapper);
                IEnumerable<ExamTemplateItemDTO> collection = accessor.Execute();

                templategroupList = collection.ToList();
            }

            catch (Exception ex)
            {
                return templategroupList;
            }

            return templategroupList;
        }

        private IRowMapper<ExamTemplateItemDTO> GetTemplateGroupDTOMaper()
        {
            IRowMapper<ExamTemplateItemDTO> mapper = MapBuilder<ExamTemplateItemDTO>.MapAllProperties()

            .Map(m => m.ItemId).ToColumn("ItemId")
            .Map(m => m.TemplateId).ToColumn("TemplateId")
            .Map(m => m.TemplateName).ToColumn("TemplateName")
            .Map(m => m.TemplateMarks).ToColumn("TemplateMarks")
            .Map(m => m.ColumnSequence).ToColumn("ColumnSequence")
            .Map(m => m.CalculativeColumnName).ToColumn("CalculativeColumnName")
            .Map(m => m.SetId).ToColumn("SetId")
            .Map(m => m.ExamSetName).ToColumn("ExamSetName")
            .Map(m => m.ExamId).ToColumn("ExamId")
            .Map(m => m.ExamName).ToColumn("ExamName")

            .Build();

            return mapper;
        }

        #endregion Get Template Group Usin Join With Micro Test, Test Set, Template Table

        public List<ExamTemplateItem> GetAllByTemplateId(int templateId)
        {
            List<ExamTemplateItem> templategroupList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamTemplateItem> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamTemplateItem>(sqlGetByTemplateId, mapper);
                IEnumerable<ExamTemplateItem> collection = accessor.Execute(templateId).ToList();

                templategroupList = collection.ToList();
            }

            catch (Exception ex)
            {
                return templategroupList;
            }

            return templategroupList;
        }

    }
}

