using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLExamSetItemRepository : IExamSetItemRepository
    {

        Database db = null;

        private string sqlInsert = "ExamSetItemInsert";
        private string sqlUpdate = "ExamSetItemUpdate";
        private string sqlDelete = "ExamSetItemDeleteById";
        private string sqlGetById = "ExamSetItemGetById";
        private string sqlGetAll = "ExamSetItemGetAll";
        private string sqlGetByExamExamSetId = "ExamSetItemByExamExamSetId";
        private string sqlGetAllExamSetItem = "ExamSetItemAllByExamExamSet";

        public int Insert(ExamSetItem examSetItem)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examSetItem, isInsert);
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

        public bool Update(ExamSetItem examSetItem)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examSetItem, isInsert);

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

        public ExamSetItem GetById(int? id)
        {
            ExamSetItem _examSetGroup = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamSetItem> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamSetItem>(sqlGetById, rowMapper);
                _examSetGroup = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examSetGroup;
            }

            return _examSetGroup;
        }

        public List<ExamSetItem> GetAll()
        {
            List<ExamSetItem> examSetGroupList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamSetItem> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamSetItem>(sqlGetAll, mapper);
                IEnumerable<ExamSetItem> collection = accessor.Execute();

                examSetGroupList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examSetGroupList;
            }

            return examSetGroupList;
        }

        public ExamSetItem GetByExamExamSetId(int examSetId, int examId)
        {
            ExamSetItem _examSetGroup = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamSetItem> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamSetItem>(sqlGetById, rowMapper);
                _examSetGroup = accessor.Execute(examSetId, examId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examSetGroup;
            }

            return _examSetGroup;
        }

        public List<ExamSetItemDTO> GetAllExamSetItem()
        {
            List<ExamSetItemDTO> examSetGroupList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamSetItemDTO> mapper = GetExamSetMaper();

                var accessor = db.CreateSprocAccessor<ExamSetItemDTO>(sqlGetAllExamSetItem, mapper);
                IEnumerable<ExamSetItemDTO> collection = accessor.Execute();

                examSetGroupList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examSetGroupList;
            }

            return examSetGroupList;
        }

        private IRowMapper<ExamSetItemDTO> GetExamSetMaper()
        {
            IRowMapper<ExamSetItemDTO> mapper = MapBuilder<ExamSetItemDTO>.MapAllProperties()

            .Map(m => m.ItemId).ToColumn("ItemId")
            .Map(m => m.ExamName).ToColumn("ExamName")
            .Map(m => m.ExamSetName).ToColumn("ExamSetName")

            .Build();
            return mapper;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamSetItem examSetItem, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ItemId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ItemId", DbType.Int32, examSetItem.ItemId);
            }


            db.AddInParameter(cmd, "ExamId", DbType.Int32, examSetItem.ExamId);
            db.AddInParameter(cmd, "ExamSetId", DbType.Int32, examSetItem.ExamSetId);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, examSetItem.CreatedBy);
            db.AddInParameter(cmd, "CreatedTime", DbType.DateTime, examSetItem.CreatedTime);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, examSetItem.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedTime", DbType.DateTime, examSetItem.ModifiedTime);

            return db;
        }

        private IRowMapper<ExamSetItem> GetMaper()
        {
            IRowMapper<ExamSetItem> mapper = MapBuilder<ExamSetItem>.MapAllProperties()

            .Map(m => m.ItemId).ToColumn("ItemId")
            .Map(m => m.ExamId).ToColumn("ExamId")
            .Map(m => m.ExamSetId).ToColumn("ExamSetId")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedTime).ToColumn("CreatedTime")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedTime).ToColumn("ModifiedTime")

            .Build();

            return mapper;
        }
        #endregion

    }
}
