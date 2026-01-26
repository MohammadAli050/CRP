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
    public partial class SqlExamTypeRepository : IExamTypeRepository
    {

        Database db = null;

        private string sqlInsert = "ExamTypeInsert";
        private string sqlUpdate = "ExamTypeUpdate";
        private string sqlDelete = "ExamTypeDelete";
        private string sqlGetById = "ExamTypeGetById";
        private string sqlGetAll = "ExamTypeGetAll";
               
        public int Insert(ExamType examtype)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, examtype, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ExamTypeId");

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

        public bool Update(ExamType examtype)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, examtype, isInsert);

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

                db.AddInParameter(cmd, "ExamTypeId", DbType.Int32, id);
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

        public ExamType GetById(int? id)
        {
            ExamType _examtype = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamType> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamType>(sqlGetById, rowMapper);
                _examtype = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _examtype;
            }

            return _examtype;
        }

        public List<ExamType> GetAll()
        {
            List<ExamType> examtypeList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<ExamType> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<ExamType>(sqlGetAll, mapper);
                IEnumerable<ExamType> collection = accessor.Execute();

                examtypeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examtypeList;
            }

            return examtypeList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, ExamType examtype, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ExamTypeId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ExamTypeId", DbType.Int32, examtype.ExamTypeId);
            }

            	
		db.AddInParameter(cmd,"ProgramId",DbType.Int32,examtype.ProgramId);
        db.AddInParameter(cmd, "ExamTypeName", DbType.String, examtype.ExamTypeName);
		db.AddInParameter(cmd,"ExamMetaTypeId",DbType.Int32,examtype.ExamMetaTypeId);
		db.AddInParameter(cmd,"Attribute1",DbType.String,examtype.Attribute1);
		db.AddInParameter(cmd,"Attribute2",DbType.String,examtype.Attribute2);
		db.AddInParameter(cmd,"Attribute3",DbType.String,examtype.Attribute3);
		db.AddInParameter(cmd,"CreatedBy",DbType.Int32,examtype.CreatedBy);
		db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,examtype.CreatedDate);
		db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,examtype.ModifiedBy);
		db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,examtype.ModifiedDate);
            
            return db;
        }

        private IRowMapper<ExamType> GetMaper()
        {
            IRowMapper<ExamType> mapper = MapBuilder<ExamType>.MapAllProperties()

       	   .Map(m => m.ExamTypeId).ToColumn("ExamTypeId")
		.Map(m => m.ProgramId).ToColumn("ProgramId")
        .Map(m => m.ExamTypeName).ToColumn("ExamTypeName")
		.Map(m => m.ExamMetaTypeId).ToColumn("ExamMetaTypeId")
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

