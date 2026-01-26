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
    public partial class SqlExamScheduleSectionRepository : IExamScheduleSectionRepository
    {

        Database db = null;

        private string sqlInsert = "ExamScheduleSectionInsert";
        private string sqlUpdate = "ExamScheduleSectionUpdate";
        private string sqlDelete = "ExamScheduleSectionDelete";
        private string sqlDeleteByExamSchedule = "ExamScheduleSectionDeleteByExamSchedule";
        private string sqlGetById = "ExamScheduleSectionGetById";
        private string sqlGetAll = "ExamScheduleSectionGetAll";
        private string sqlGetAllByExamSchedule = "ExamScheduleDayGetAllByExamSchedule";
               
        public int Insert(ExamScheduleSection examschedulesection)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examschedulesection, isInsert);
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

        public bool Update(ExamScheduleSection examschedulesection)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examschedulesection, isInsert);

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

        public bool DeleteByExamSchedule(int examScheduleId)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDeleteByExamSchedule);

                db.AddInParameter(cmd, "ExamScheduleId", DbType.Int32, examScheduleId);
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

        public ExamScheduleSection GetById(int id)
        {
            ExamScheduleSection _examschedulesection = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleSection> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleSection>(sqlGetById, rowMapper);
                _examschedulesection = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examschedulesection;
            }

            return _examschedulesection;
        }

        public List<ExamScheduleSection> GetAll()
        {
            List<ExamScheduleSection> examschedulesectionList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleSection>(sqlGetAll, mapper);
                IEnumerable<ExamScheduleSection> collection = accessor.Execute();

                examschedulesectionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examschedulesectionList;
            }

            return examschedulesectionList;
        }

        public List<ExamScheduleSection> GetAllByExamSchedule(int examScheduleId)
        {
            List<ExamScheduleSection> examScheduleSectionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamScheduleSection> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamScheduleSection>(sqlGetAllByExamSchedule, mapper);
                IEnumerable<ExamScheduleSection> collection = accessor.Execute(examScheduleId);

                examScheduleSectionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examScheduleSectionList;
            }

            return examScheduleSectionList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamScheduleSection examschedulesection, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, examschedulesection.Id);
            }

            	
		db.AddInParameter(cmd,"ExamScheduleId",DbType.Int32,examschedulesection.ExamScheduleId);
		db.AddInParameter(cmd,"Section",DbType.String,examschedulesection.Section);
		db.AddInParameter(cmd,"Attribute1",DbType.String,examschedulesection.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,examschedulesection.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,examschedulesection.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,examschedulesection.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,examschedulesection.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,examschedulesection.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,examschedulesection.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamScheduleSection> GetMaper()
        {
            IRowMapper<ExamScheduleSection> mapper = MapBuilder<ExamScheduleSection>.MapAllProperties()

       	   .Map(m => m.Id).ToColumn("Id")
		.Map(m => m.ExamScheduleId).ToColumn("ExamScheduleId")
		.Map(m => m.Section).ToColumn("Section")
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

