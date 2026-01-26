using LogicLayer.BusinessObjects;
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
    partial class SQLExamMarksAllocationRepository : IExamMarksAllocationRepository
    {
        Database db = null;

        private string sqlInsert = "ExamMarksAllocationInsert";
        private string sqlUpdate = "ExamMarksAllocationUpdate";
        private string sqlDelete = "ExamMarksAllocationDeleteById";
        private string sqlGetById ="ExamMarksAllocationGetById";
        private string sqlGetAll = "ExamMarksAllocationGetAll";


        public int Insert(ExamMarksAllocation examMarksAllocation)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examMarksAllocation, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ExamMarksAllocationID");

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

        public bool Update(ExamMarksAllocation examMarksAllocation)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examMarksAllocation, isInsert);

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

                db.AddInParameter(cmd, "ExamMarksAllocationID", DbType.Int32, id);
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

        public ExamMarksAllocation GetById(int? id)
        {
            ExamMarksAllocation _examMarksAllocation = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarksAllocation> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarksAllocation>(sqlGetById, rowMapper);
                _examMarksAllocation = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examMarksAllocation;
            }

            return _examMarksAllocation;
        }

        public List<ExamMarksAllocation> GetAll()
        {
            List<ExamMarksAllocation> examMarksAllocationList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamMarksAllocation> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamMarksAllocation>(sqlGetAll, mapper);
                IEnumerable<ExamMarksAllocation> collection = accessor.Execute();

                examMarksAllocationList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examMarksAllocationList;
            }

            return examMarksAllocationList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamMarksAllocation examMarksAllocation, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ExamMarksAllocationID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ExamMarksAllocationID", DbType.Int32, examMarksAllocation.ExamMarksAllocationID);
            }

            db.AddInParameter(cmd, "ExamMarksAllocationID", DbType.Int32, examMarksAllocation.ExamMarksAllocationID);
            db.AddInParameter(cmd, "ExamTypeNameID", DbType.Int32, examMarksAllocation.ExamTypeNameID);
            db.AddInParameter(cmd, "AllottedMarks", DbType.Int32, examMarksAllocation.AllottedMarks);
            db.AddInParameter(cmd, "ExamName", DbType.String, examMarksAllocation.ExamName);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, examMarksAllocation.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, examMarksAllocation.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, examMarksAllocation.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, examMarksAllocation.ModifiedDate);

            return db;
        }

        private IRowMapper<ExamMarksAllocation> GetMaper()
        {
            IRowMapper<ExamMarksAllocation> mapper = MapBuilder<ExamMarksAllocation>.MapAllProperties()
            .Map(m => m.ExamMarksAllocationID).ToColumn("ExamMarksAllocationID")
            .Map(m => m.ExamTypeNameID).ToColumn("ExamTypeNameID")
            .Map(m => m.AllottedMarks).ToColumn("AllottedMarks")
            .Map(m => m.ExamName).ToColumn("ExamName")
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
