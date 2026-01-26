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
    public partial class SqlExamCenterRepository : IExamCenterRepository
    {

        Database db = null;

        private string sqlInsert = "ExamCenterInsert";
        private string sqlUpdate = "ExamCenterUpdate";
        private string sqlDelete = "ExamCenterDelete";
        private string sqlGetById = "ExamCenterGetById";
        private string sqlGetAll = "ExamCenterGetAll";
               
        public int Insert(ExamCenter examcenter)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examcenter, isInsert);
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

        public bool Update(ExamCenter examcenter)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examcenter, isInsert);

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

        public ExamCenter GetById(int? id)
        {
            ExamCenter _examcenter = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamCenter> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamCenter>(sqlGetById, rowMapper);
                _examcenter = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examcenter;
            }

            return _examcenter;
        }

        public List<ExamCenter> GetAll()
        {
            List<ExamCenter> examcenterList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamCenter> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamCenter>(sqlGetAll, mapper);
                IEnumerable<ExamCenter> collection = accessor.Execute();

                examcenterList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examcenterList;
            }

            return examcenterList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamCenter examcenter, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, examcenter.Id);
            }

            	
		db.AddInParameter(cmd,"ExamCenterName",DbType.String,examcenter.ExamCenterName);
		db.AddInParameter(cmd,"ExamCenterAddress",DbType.String,examcenter.ExamCenterAddress);
		db.AddInParameter(cmd,"Attribute1",DbType.String,examcenter.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,examcenter.Attribute2);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,examcenter.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,examcenter.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,examcenter.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,examcenter.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamCenter> GetMaper()
        {
            IRowMapper<ExamCenter> mapper = MapBuilder<ExamCenter>.MapAllProperties()

       	   .Map(m => m.Id).ToColumn("Id")
		.Map(m => m.ExamCenterName).ToColumn("ExamCenterName")
		.Map(m => m.ExamCenterAddress).ToColumn("ExamCenterAddress")
		.Map(m => m.Attribute1).ToColumn("Attribute1")
		.Map(m => m.Attribute2).ToColumn("Attribute2")
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

