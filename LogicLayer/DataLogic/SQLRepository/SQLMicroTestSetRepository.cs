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
    public partial class SQLExamSetRepository : IExamSetRepository
    {

        Database db = null;

        private string sqlInsert = "ExamSetInsert";
        private string sqlUpdate = "ExamSetUpdate";
        private string sqlDelete = "ExamSetDeleteById";
        private string sqlGetById = "ExamSetGetById";
        private string sqlGetAll = "ExamSetGetAll";
        private string sqlGetByName = "ExamSetGetByName";

        public int Insert(ExamSet examSet)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examSet, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "SetId");

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

        public bool Update(ExamSet examSet)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examSet, isInsert);

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

                db.AddInParameter(cmd, "SetId", DbType.Int32, id);
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

        public ExamSet GetById(int? id)
        {
            ExamSet _examSet = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamSet> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamSet>(sqlGetById, rowMapper);
                _examSet = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examSet;
            }

            return _examSet;
        }

        public List<ExamSet> GetAll()
        {
            List<ExamSet> examSetList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamSet> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamSet>(sqlGetAll, mapper);
                IEnumerable<ExamSet> collection = accessor.Execute();

                examSetList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examSetList;
            }

            return examSetList;
        }

        public ExamSet GetExamSetByName(string examSetName)
        {
            ExamSet _examSet = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamSet> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamSet>(sqlGetByName, rowMapper);
                _examSet = accessor.Execute(examSetName).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examSet;
            }

            return _examSet;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamSet examSet, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "SetId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "SetId", DbType.Int32, examSet.SetId);
            }


            db.AddInParameter(cmd, "ExamSetName", DbType.String, examSet.ExamSetName);
            db.AddInParameter(cmd, "Mark", DbType.Int32, examSet.Mark);
            db.AddInParameter(cmd, "CriteriaId", DbType.Int32, examSet.CriteriaId);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, examSet.CreatedBy);
            db.AddInParameter(cmd, "CreatedTime", DbType.DateTime, examSet.CreatedTime);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, examSet.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedTime", DbType.DateTime, examSet.ModifiedTime);

            return db;
        }

        private IRowMapper<ExamSet> GetMaper()
        {
            IRowMapper<ExamSet> mapper = MapBuilder<ExamSet>.MapAllProperties()

            .Map(m => m.SetId).ToColumn("SetId")
            .Map(m => m.ExamSetName).ToColumn("ExamSetName")
            .Map(m => m.Mark).ToColumn("Mark")
            .Map(m => m.CriteriaId).ToColumn("CriteriaId")
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
